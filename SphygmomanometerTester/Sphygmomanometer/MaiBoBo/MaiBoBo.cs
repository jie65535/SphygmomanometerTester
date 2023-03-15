using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace SphygmomanometerTester.Sphygmomanometer.MaiBoBo
{
    public class MaiBoBo : Sphygmomanometer
    {
        public MaiBoBo()
        {
            // 串口波特率为115200
            // 串口的其它参数保持默认即可
            SerialPort.BaudRate = 115200;

            SerialPort.DataReceived += SerialPort_DataReceived;
        }

        /// <summary>
        /// 通讯尝试超时时间（毫秒）
        /// </summary>
        private const int TryTimeoutMs = 200;

        /// <summary>
        /// 最大重试次数
        /// </summary>
        private const int RetryMax = 5;

        public Task Connect() => RequestAsync(PackageConnect, BloodPressureSubCode.Connect);

        public override Task StartMeasure() => RequestAsync(PackageStart, BloodPressureSubCode.Start);

        public override Task StopMeasure() => RequestAsync(PackageStop, BloodPressureSubCode.Stop);

        public override event EventHandler<MeasureResult> MeasureOver;

        /// <summary>
        /// 测量中报告
        /// </summary>
        public class MeasurePoll : EventArgs
        {
            /// <summary>
            /// 当前压力值
            /// </summary>
            public int Pressure;

            public MeasurePoll(int pressure)
            {
                Pressure = pressure;
            }
        }

        /// <summary>
        /// 测量中压力上报
        /// </summary>
        public event EventHandler<MeasurePoll> MeasuringPoll;

        #region - 通讯协议 -

        /// <summary>
        /// 协议包
        /// </summary>
        /// <param name="type">类型标识</param>
        /// <param name="sub">类型子码</param>
        /// <param name="data">数据内容</param>
        /// <returns>包字节数组</returns>
        private static byte[] GetPackage(byte type, byte sub, byte data = 0)
        {
            var package = new byte[]
            {
                0xCC, 0x80, // 前导码
                0x03, // 设备版本
                0x03, // 数据长度
                type, // 类型标识
                sub,  // 类型子码
                data, // 数据内容
                0x00  // 校验码（包头和校验除外的字节异或）
            };

            byte xorsum = 0;
            for (var i = 2; i < package.Length - 1; i++)
                xorsum ^= package[i];

            package[package.Length - 1] = xorsum;

            return package;
        }

        /// <summary>
        /// 连接指令
        /// </summary>
        private static readonly byte[] PackageConnect = GetPackage((byte)TypeCode.BloodPressure, (byte)BloodPressureSubCode.Connect);

        /// <summary>
        /// 启动指令
        /// </summary>
        private static readonly byte[] PackageStart = GetPackage((byte)TypeCode.BloodPressure, (byte)BloodPressureSubCode.Start);

        /// <summary>
        /// 停止指令
        /// </summary>
        private static readonly byte[] PackageStop = GetPackage((byte)TypeCode.BloodPressure, (byte)BloodPressureSubCode.Stop);

        /// <summary>
        /// 请求信号量，控制请求并发数
        /// </summary>
        private readonly SemaphoreSlim requestSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// 请求信号量集
        /// </summary>
        private readonly SemaphoreSlim[] RequestMap = new SemaphoreSlim[4];

        /// <summary>
        /// 发送并等待响应，错误会以异常的方式抛出，需要捕获并处理异常！
        /// </summary>
        /// <param name="package">通讯包</param>
        /// <param name="code">请求代码</param>
        /// <exception cref="IOException">串口未打开时抛出</exception>
        /// <exception cref="TimeoutException">若等待响应超时时抛出</exception>
        private async Task RequestAsync(byte[] package, BloodPressureSubCode code)
        {
            if (!IsOpen)
                throw new IOException("通讯端口未连接，无法发送数据");

            try
            {
                // 同时只能存在一个请求
                await requestSemaphore.WaitAsync();

                var semaphore = new SemaphoreSlim(0);
                RequestMap[(int)code] = semaphore;

                // 重试RetryMax次
                for (var tryCount = 1; tryCount <= RetryMax; tryCount++)
                {
                    try
                    {
                        Write(package, 0, package.Length);
                        if (await semaphore.WaitAsync(TryTimeoutMs))
                        {
                            // 正常响应，直接返回
                            return;
                        }
                        else
                        {
                            // 超时，重试
                            Console.WriteLine($"等待回复超时，正在重试第{tryCount}次");
                        }
                    }
                    catch (TimeoutException) { } // 忽略超时异常
                    catch (Exception ex)
                    {
                        // 发生异常时忽略，重试RetryMax次
                        Console.WriteLine("等待响应时发生预期外的异常！" + ex);
                    }
                }

                Console.WriteLine("等待响应超时");
                // 未收到回复，抛出超时异常，报告等待响应超时
                throw new TimeoutException("等待响应超时");
            }
            finally
            {
                RequestMap[(int)code] = null;
                requestSemaphore.Release();
            }
        }

        /// <summary>
        /// 包最小字节数，用以判断包是否完整
        /// </summary>
        private const int PackageMinSize = 5;

        /// <summary>
        /// 串口收到数据时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var n = SerialPort.BytesToRead;
            Thread.Sleep(15);

            // 等待接收剩余的数据
            while (n < SerialPort.BytesToRead)
            {
                n = SerialPort.BytesToRead;
                Thread.Sleep(15);
            }

            // 读取到缓冲区
            var buf = new byte[n];
            // buf 为本次收到的报文消息
            Read(buf, 0, n);

            Console.WriteLine("接收到数据：" + BitConverter.ToString(buf).Replace('-', ' ').ToUpper());

            // 包头索引
            var i = 0;

        // 寻找包开始
        findPackage:

            // 找到包头
            while (i < n && (buf[i] != 0xAA || buf[i + 1] != 0x80))
                i++;
            if (n - i >= PackageMinSize) // 是否至少包含一个包
            {
                // 数据长度
                var dataLength = buf[i + 3];
                // 包长 = 数据长度 + 包头包尾长度
                var packageLength = dataLength + PackageMinSize;

                if (i == 0 && packageLength == n)
                {
                    if (CheckPackage(buf))
                    {
                        // 校验包完整后解析包内容
                        OnReceivedPackage(buf);
                    }
                    else
                    {
                        // 包校验失败
                        Console.WriteLine("数据包校验失败！");
                        i++; // 尝试寻找下一包
                        goto findPackage;
                    }
                }
                else
                {
                    if (CheckPackage(buf, i, packageLength))
                    {
                        // 校验包完整后解析包内容
                        var package = new byte[packageLength];
                        Array.Copy(buf, i, package, 0, packageLength);
                        OnReceivedPackage(package);
                    }
                    else
                    {
                        // 包校验失败
                        Console.WriteLine("数据包校验失败！");
                        i++; // 尝试寻找下一包
                        goto findPackage;
                    }
                }
            }
            else
            {
                // 未找到包头或者包不完整
                Console.WriteLine("不是有效的数据包！");
            }
        }

        /// <summary>
        /// 校验包是否正常
        /// </summary>
        /// <param name="package">数据包</param>
        /// <returns>是否正常</returns>
        private static bool CheckPackage(byte[] package)
            => CheckPackage(package, 0, package.Length);

        /// <summary>
        /// 校验包是否正常
        /// </summary>
        /// <param name="package">数据包</param>
        /// <param name="offset">偏移</param>
        /// <param name="count">字节数</param>
        /// <returns>是否正常</returns>
        private static bool CheckPackage(byte[] package, int offset, int count)
        {
            var xorsum = 0;
            for (var i = offset + 2; i < count - 1; i++)
                xorsum ^= package[i];
            return xorsum == package[package.Length - 1];
        }

        /// <summary>
        /// 接收到完整包时触发
        /// </summary>
        /// <param name="package">包内容</param>
        private void OnReceivedPackage(byte[] package)
        {
            var index = 4;
            var type = (TypeCode)package[index++];
            var subType = package[index++];

            if (type != TypeCode.BloodPressure)
                return; // 类型不是血压相关，忽略

            // 根据子码做对应的处理
            switch ((BloodPressureSubCode)subType)
            {
                case BloodPressureSubCode.Connect:  // 应答连接
                case BloodPressureSubCode.Start:    // 应答启动
                case BloodPressureSubCode.Stop:     // 应答停止
                    RequestMap[subType]?.Release(); // 释放信号量
                    break;

                case BloodPressureSubCode.Poll: // 实时压力
                    var pressure = (package[index++] << 8) | package[index];
                    Console.WriteLine($"实时压力值：{pressure}");
                    OnMeasuringPoll(pressure);
                    break;

                case BloodPressureSubCode.Result: // 测量结果
                    var userId = package[index++].ToString();
                    var measureTime = DateTime.Now;
                    try
                    {
                        measureTime =
                            // 测量时间 年-2000，月，日，时，分，秒
                            new DateTime(package[index++] + 2000, package[index++], package[index++],
                                package[index++], package[index++], package[index++]);
                    }
                    catch
                    {
                        // ignored
                    }

                    // 转换测量结果
                    var result = new MeasureResult(
                        // 用户标识
                        userId,
                        // 测量时间
                        measureTime,
                        // 收缩压
                        (package[index++] << 8) | package[index++],
                        // 舒张压
                        (package[index++] << 8) | package[index++],
                        // 脉搏数
                        (package[index++] << 8) | package[index]);
                    Console.WriteLine($"接收到测量结果：{result.MeasureTime}\t收缩压：{result.SystolicPressure}\t舒张压：{result.DiastolicPressure}\t脉搏数：{result.Pulse}");
                    OnMeasureOver(result);
                    break;

                case BloodPressureSubCode.Error: // 发生错误
                    var message = "未知错误";
                    switch (package[index]) // 错误代码
                    {
                        case 1:
                            message = "臂筒内上游气囊压力超过安全压力";
                            break;

                        case 2:
                            message = "测量中手臂放置不正确或臂筒内上游气囊漏气";
                            break;

                        case 5:
                            message = "测量中手臂放置不正确或臂筒内下游气囊漏气";
                            break;

                        case 6:
                            message = "手臂放置方式不正确或脉搏传感器无信号";
                            break;

                        case 9:
                            message = "臂筒内气囊放气时间过长";
                            break;
                    }

                    Console.WriteLine("接收到测量报错信息：" + message);
                    OnMeasureOver(new MeasureResult(message));
                    break;

                default:
                    // 未知的报文
                    Console.WriteLine("未知的上报数据");
                    break;
            }
        }

        /// <summary>
        /// 测量结束时调用
        /// </summary>
        /// <param name="result">测量结果</param>
        private void OnMeasureOver(MeasureResult result)
            => MeasureOver?.Invoke(this, result);

        /// <summary>
        /// 测量过程中上报压力时调用
        /// </summary>
        /// <param name="pressure">压力值</param>
        private void OnMeasuringPoll(int pressure)
            => MeasuringPoll?.Invoke(this, new MeasurePoll(pressure));

        /// <summary>
        /// 类型代码
        /// </summary>
        private enum TypeCode : byte
        {
            /// <summary>
            /// 血压相关
            /// </summary>
            BloodPressure = 0x01,
        }

        /// <summary>
        /// 血压测量子码
        /// </summary>
        private enum BloodPressureSubCode : byte
        {
            /// <summary>
            /// 连接
            /// </summary>
            Connect = 0x01,

            /// <summary>
            /// 开始
            /// </summary>
            Start = 0x02,

            /// <summary>
            /// 停止
            /// </summary>
            Stop = 0x03,

            /// <summary>
            /// 测量过程上报
            /// </summary>
            Poll = 0x05,

            /// <summary>
            /// 测量结果
            /// </summary>
            Result = 0x06,

            /// <summary>
            /// 发生错误
            /// </summary>
            Error = 0x07,
        }

        #endregion - 通讯协议 -
    }
}