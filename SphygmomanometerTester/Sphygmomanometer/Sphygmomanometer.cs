using System;
using System.Threading.Tasks;

namespace SphygmomanometerTester.Sphygmomanometer
{
    public abstract class Sphygmomanometer : SerialControl
    {
        /// <summary>
        /// 开始测量
        /// </summary>
        public abstract Task StartMeasure();

        /// <summary>
        /// 提前结束测量
        /// </summary>
        public abstract Task StopMeasure();

        /// <summary>
        /// 测量结束事件
        /// </summary>
        public abstract event EventHandler<MeasureResult> MeasureOver;
    }

    /// <summary>
    /// 测量结果
    /// </summary>
    public class MeasureResult : EventArgs
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId;

        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime MeasureTime;

        /// <summary>
        /// 收缩压
        /// </summary>
        public int SystolicPressure;

        /// <summary>
        /// 舒张压
        /// </summary>
        public int DiastolicPressure;

        /// <summary>
        /// 脉搏
        /// </summary>
        public int Pulse;

        /// <summary>
        /// 消息，如果存在异常时设置，正常情况下为空
        /// </summary>
        public string Message;

        public MeasureResult(string message)
        {
            UserId = string.Empty;
            Message = message;
        }

        public MeasureResult(string userId, DateTime measureTime, int systolicPressure, int diastolicPressure, int pulse)
        {
            UserId = userId;
            MeasureTime = measureTime;
            SystolicPressure = systolicPressure;
            DiastolicPressure = diastolicPressure;
            Pulse = pulse;
        }
    }
}