using System;
using System.IO.Ports;
using System.Text;

namespace SphygmomanometerTester.Sphygmomanometer
{
    public abstract class SerialControl
    {
        /// <summary>
        /// 串口对象
        /// </summary>
        protected readonly SerialPort SerialPort = new SerialPort();

        public bool IsOpen => SerialPort.IsOpen;

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName">端口号</param>
        /// <returns></returns>
        public void OpenCom(string portName)
        {
            if (SerialPort.IsOpen) return;
            FormMain.Instance.Log("打开串口 " + portName);
            SerialPort.PortName = portName;
            SerialPort.Open();
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        public void CloseCom()
        {
            if (!SerialPort.IsOpen) return;
            FormMain.Instance.Log("关闭串口");
            SerialPort.Close();
        }

        /// <summary>
        /// 字节到Hex字符串
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">字节数</param>
        /// <returns>Hex字符串</returns>
        public static string BytesToHex(byte[] buffer, int offset, int count)
        {
            var result = string.Empty;
            for (var i = 0; i < count; i++)
                result += Convert.ToString(buffer[i + offset], 16)
                    .ToUpper()
                    .PadLeft(2, '0') + " ";
            return result;
        }

        /// <summary>
        /// 写入数据到串口
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">字节数</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            FormMain.Instance.Log("发送出：" + BytesToHex(buffer, offset, count) + " | " + Encoding.UTF8.GetString(buffer, offset, count));
            SerialPort.Write(buffer, offset, count);
        }

        /// <summary>
        /// 从串口读取数据
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">字节数</param>
        /// <returns>读取到的字节数</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            var n = SerialPort.Read(buffer, offset, count);
            FormMain.Instance.Log("接收到：" + BytesToHex(buffer, offset, n) + " | " + Encoding.UTF8.GetString(buffer, offset, n));
            return n;
        }
    }
}