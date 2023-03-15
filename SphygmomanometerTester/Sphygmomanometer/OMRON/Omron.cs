using System;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace SphygmomanometerTester.Sphygmomanometer.OMRON
{
    public class Omron : Sphygmomanometer
    {
        public Omron()
        {
            SerialPort.BaudRate = 2400;
            SerialPort.Parity = Parity.Even;
            SerialPort.DataBits = 7;
            SerialPort.StopBits = StopBits.One;

            SerialPort.DataReceived += SerialPort_DataReceived;
        }

        public override Task StartMeasure()
        {
            Write(PackageStart, 0, PackageStart.Length);
            return Task.CompletedTask;
        }

        public Task RestartMeasure()
        {
            Write(PackageRestart, 0, PackageRestart.Length);
            return Task.CompletedTask;
        }

        public override Task StopMeasure()
        {
            Write(PackageStop, 0, PackageStop.Length);
            return Task.CompletedTask;
        }

        public override event EventHandler<MeasureResult> MeasureOver;

        #region 通讯协议

        /// <summary>
        /// 包起始标识
        /// </summary>
        private const byte StartTx = 0x02;

        /// <summary>
        /// 包结束标识
        /// </summary>
        private const byte EndTx = 0x03;

        /// <summary>
        /// 生成通讯报文
        /// </summary>
        /// <param name="payload">数据内容</param>
        /// <returns>通讯包</returns>
        private static byte[] GetPackage(byte payload) => new[] { StartTx, payload, EndTx };

        /// <summary>
        /// 开始报文
        /// </summary>
        private static readonly byte[] PackageStart = GetPackage((byte)'S');

        /// <summary>
        /// 结束报文
        /// </summary>
        private static readonly byte[] PackageStop = GetPackage((byte)'R');

        /// <summary>
        /// 重新开始报文
        /// </summary>
        private static readonly byte[] PackageRestart = GetPackage((byte)'B');

        /// <summary>
        /// 接受通讯缓冲区
        /// </summary>
        private readonly byte[] buffer = new byte[384];

        /// <summary>
        /// 接收索引
        /// </summary>
        private int index;

        /// <summary>
        /// 最后收到数据的时间
        /// </summary>
        private DateTime lastReceivedTime = DateTime.MinValue;

        /// <summary>
        /// 串口收到数据时触发
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var now = DateTime.Now;
            // 如果距离上次收到数据超过了300ms，清空输入缓冲区
            if ((now - lastReceivedTime).TotalMilliseconds > 300)
                index = 0;
            // 更新最后收到数据的时间
            lastReceivedTime = now;

            // 接收数据
            index += Read(buffer, index, buffer.Length - index);

            // 从收到的数据中找到合法的包
            var start = -1;
            for (var i = 0; i < index; i++)
            {
                if (start == -1)
                {
                    // 找到包头
                    if (buffer[i] == StartTx)
                    {
                        start = i;
                    }
                }
                else
                {
                    // 找到包尾
                    if (buffer[i] == EndTx)
                    {
                        // 处理收到的数据
                        var len = i - (start + 1);
                        var data = new byte[len];
                        Array.Copy(buffer, start + 1, data, 0, len);
                        index = 0;
                        OnDataReceived(data);
                    }
                }
            }
        }

        /// <summary>
        /// 当测量完成时触发
        /// </summary>
        /// <param name="data">测量结果报文数据</param>
        private void OnDataReceived(byte[] data)
        {
            // 返回的结果是一个ASCII字符串
            var msg = Encoding.ASCII.GetString(data);
            // 按照通讯协议，收到的是38个字节，开头是固定的ID9999999
            if (msg.Length != 38 || !msg.StartsWith("ID99999999"))
                return;

            var year = msg.Substring(11, 2);
            var month = msg.Substring(14, 2);
            var day = msg.Substring(17, 2);
            var hour = msg.Substring(20, 2);
            var minute = msg.Substring(23, 2);
            if (!DateTime.TryParse($"20{year}/{month}/{day} {hour}:{minute}", out var time))
                time = DateTime.Now;
            var message = "";
            if (!int.TryParse(msg.Substring(26, 3), out var systolicPressure)
                | !int.TryParse(msg.Substring(31, 3), out var diastolicPressure)
                | !int.TryParse(msg.Substring(35, 3), out var pulse))
            {
                message = "没有数据，请调整姿势重新测量!";
            }

            var result = message != "" ? new MeasureResult(message) : new MeasureResult(
                "99999999",
                time,
                systolicPressure,
                diastolicPressure,
                pulse);
            OnMeasureOver(result);
        }

        /// <summary>
        /// 测量结束时调用
        /// </summary>
        /// <param name="result">测量结果</param>
        private void OnMeasureOver(MeasureResult result)
            => MeasureOver?.Invoke(this, result);

        #endregion 通讯协议
    }
}