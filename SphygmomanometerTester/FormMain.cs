using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SphygmomanometerTester.Sphygmomanometer.MaiBoBo;
using SphygmomanometerTester.Sphygmomanometer.OMRON;

namespace SphygmomanometerTester
{
    public partial class FormMain : Form
    {
        public static FormMain Instance { get; private set; }

        /// <summary>
        /// 血压计实例
        /// </summary>
        private readonly Sphygmomanometer.Sphygmomanometer sphygmomanometer = new Omron();

        #region - 窗口构造与事件 -

        public FormMain()
        {
            Instance = this;
            InitializeComponent();

            if (sphygmomanometer is MaiBoBo maiBoBo)
                maiBoBo.MeasuringPoll += Sphygmomanometer_MeasuringPoll;
            sphygmomanometer.MeasureOver += Sphygmomanometer_MeasureOver;
        }

        /// <summary>
        /// 窗体加载时触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CmbSerialPortName.DataSource = SerialPort.GetPortNames();
            UpdateControlButtons();
        }

        /// <summary>
        /// 窗口关闭时触发
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtLog.Text))
                {
                    // 保存日志文件
                    File.WriteAllText(DateTime.Now.ToString("MMddHHmm") + ".log", TxtLog.Text);
                }
            }
            catch
            {
                // ignored
            }

            sphygmomanometer.CloseCom();

            base.OnFormClosed(e);
        }

        #endregion - 窗口构造与事件 -

        #region - 血压计事件 -

        /// <summary>
        /// 测量过程压力上报事件
        /// </summary>
        private void Sphygmomanometer_MeasuringPoll(object sender, MaiBoBo.MeasurePoll e)
        {
            BeginInvoke(new Action(() =>
            {
                LblPressureValue.Text = e.Pressure.ToString();
            }));
        }

        /// <summary>
        /// 测量结束事件
        /// </summary>
        private void Sphygmomanometer_MeasureOver(object sender, Sphygmomanometer.MeasureResult e)
        {
            BeginInvoke(new Action(() =>
            {
                // 显示结果
                LblPressureValue.Text = "0";
                LblSystolicPressureValue.Text = e.SystolicPressure.ToString();
                LblDiastolicPressureValue.Text = e.DiastolicPressure.ToString();
                LblPulseValue.Text = e.Pulse.ToString();
                if (!string.IsNullOrEmpty(e.Message))
                {
                    StatEndTest(false);
                    Log("测量失败：" + e.Message);
                    if (!ChkLoopTest.Checked)
                    {
                        MessageBox.Show(e.Message, "测量出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    StatEndTest(true);
                    Log($"测量完成，结果：收缩压={e.SystolicPressure} 舒张压={e.DiastolicPressure} 脉搏={e.Pulse}");
                }

                // 检查并循环测量
                CheckAndLoop();
            }));
        }

        #endregion - 血压计事件 -

        #region - 控制按钮 -

        /// <summary>
        /// 更新控制按钮状态
        /// </summary>
        private void UpdateControlButtons()
        {
            var isOpen = sphygmomanometer.IsOpen;
            CmbSerialPortName.Enabled = !isOpen;
            BtnUpdatePortNames.Enabled = !isOpen;
            BtnOpenPort.Enabled = !isOpen;
            BtnClosePort.Enabled = isOpen;
            BtnConnect.Enabled = isOpen;
            BtnStartMeasure.Enabled = isOpen;
            BtnStopMeasure.Enabled = isOpen;
        }

        /// <summary>
        /// 刷新串口列表
        /// </summary>
        private void BtnUpdatePortNames_Click(object sender, EventArgs e)
        {
            CmbSerialPortName.DataSource = SerialPort.GetPortNames();
        }

        /// <summary>
        /// 点击打开串口
        /// </summary>
        private void BtnOpenPort_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CmbSerialPortName.Text))
            {
                Log("打开串口 " + CmbSerialPortName.Text);
                try
                {
                    sphygmomanometer.OpenCom(CmbSerialPortName.Text);
                }
                catch (Exception ex)
                {
                    Log("打开串口异常", ex);
                    MessageBox.Show("串口打开失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请选择端口", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateControlButtons();
        }

        /// <summary>
        /// 点击关闭串口
        /// </summary>
        private void BtnClosePort_Click(object sender, EventArgs e)
        {
            Log("关闭串口");
            sphygmomanometer.CloseCom();
            UpdateControlButtons();
        }

        /// <summary>
        /// 点击测试连接按钮
        /// </summary>
        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!(sphygmomanometer is MaiBoBo maiBoBo))
            {
                MessageBox.Show("当前血压计不支持测试连接状态", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var btn = (Button)sender;
            try
            {
                btn.Enabled = false;
                LblPressureValue.Text = "0";
                Log("尝试连接血压计，发送连接测试");
                await maiBoBo.Connect();
                Log("正常响应");
                await ButtonOk(btn);
            }
            catch (Exception ex)
            {
                Log("连接测试异常", ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn.Enabled = true;
            }
        }

        /// <summary>
        /// 点击开始测量按钮
        /// </summary>
        private async void BtnStartMeasure_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            try
            {
                btn.Enabled = false;
                LblPressureValue.Text = "0";
                Log($"尝试开始第 {TestCount + 1} 次测量...");
                await sphygmomanometer.StartMeasure();
                StatBeginTest();
                await ButtonOk(btn);
            }
            catch (Exception ex)
            {
                Log("开始测量异常", ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn.Enabled = true;
            }
        }

        /// <summary>
        /// 点击停止测量按钮
        /// </summary>
        private async void BtnStopMeasure_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            try
            {
                btn.Enabled = false;
                LblPressureValue.Text = "0";
                Log("尝试取消测量");
                await sphygmomanometer.StopMeasure();
                await ButtonOk(btn);
            }
            catch (Exception ex)
            {
                Log("停止测量异常", ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn.Enabled = true;
            }
        }

        /// <summary>
        /// 检查并启动循环
        /// </summary>
        private void CheckAndLoop()
        {
            if (!ChkLoopTest.Checked) return;
            var interval = (int)NudInterval.Value;
            if (interval > 0)
            {
                PrbIntervalProgress.Maximum = interval;
                PrbIntervalProgress.Value = 0;
                PrbIntervalProgress.Visible = true;
                CancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = CancellationTokenSource.Token;
                Log($"启动任务，等待 {interval} 秒后重新开始测试...");
                Task.Run(async () =>
                {
                    for (var i = 0; i < interval && !cancellationToken.IsCancellationRequested; i++)
                    {
                        await Task.Delay(1000, cancellationToken);
                        Invoke(new Action(() =>
                        {
                            PrbIntervalProgress.Value += 1;
                            if (PrbIntervalProgress.Value == PrbIntervalProgress.Maximum)
                                PrbIntervalProgress.Visible = false;
                        }));
                    }

                    if (!cancellationToken.IsCancellationRequested && sphygmomanometer.IsOpen)
                    {
                        BeginInvoke(new Action(() => BtnStartMeasure_Click(BtnStartMeasure, EventArgs.Empty)));
                    }
                }, cancellationToken);
            }
            else
            {
                Log("无间隔循环测试继续");
                BtnStartMeasure_Click(BtnStartMeasure, EventArgs.Empty);
            }
        }

        private CancellationTokenSource CancellationTokenSource;

        /// <summary>
        /// 循环测试复选框改变事件
        /// </summary>
        private void ChkLoopTest_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkLoopTest.Checked) return;
            if (CancellationTokenSource != null && !CancellationTokenSource.IsCancellationRequested)
            {
                CancellationTokenSource.Cancel();
                PrbIntervalProgress.Visible = false;
                Log("取消循环测试");
            }
        }

        #endregion - 控制按钮 -

        #region - 日志 -

        /// <summary>
        /// 写日志到文本框
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Log), message);
                return;
            }
            var scrollToEnd = TxtLog.SelectionStart == TxtLog.Text.Length;
            TxtLog.AppendText(DateTime.Now.ToLongTimeString());
            TxtLog.AppendText(" ");
            TxtLog.AppendText(message);
            TxtLog.AppendText(Environment.NewLine);
            if (!scrollToEnd) return;
            TxtLog.SelectionStart = TxtLog.Text.Length;
            TxtLog.SelectionLength = 0;
            TxtLog.ScrollToCaret();
        }

        public void Log(string message, Exception ex)
        {
            Log(message + " " + ex);
        }

        #endregion - 日志 -

        #region - 工具 -

        /// <summary>
        /// 按钮OK动画
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        private static async Task ButtonOk(Control btn)
        {
            var text = btn.Text;
            var color = btn.ForeColor;
            btn.Text = "OK";
            btn.ForeColor = Color.LawnGreen;
            await Task.Delay(1000);
            btn.Text = text;
            btn.ForeColor = color;
        }

        #endregion - 工具 -

        #region - 重置统计 -

        /// <summary>
        /// 测试次数
        /// </summary>
        private int TestCount;

        /// <summary>
        /// 测试成功次数
        /// </summary>
        private int TestSuccessCount;

        /// <summary>
        /// 测试失败次数
        /// </summary>
        private int TestFailedCount;

        /// <summary>
        /// 点击重置统计按钮
        /// </summary>
        private void BtnResetStat_Click(object sender, EventArgs e)
        {
            TestCount = 0;
            TestSuccessCount = 0;
            TestFailedCount = 0;
            UpdateStat();
        }

        /// <summary>
        /// 更新统计
        /// </summary>
        private void UpdateStat()
        {
            if (InvokeRequired)
                Invoke(new Action(UpdateStat));
            else
            {
                LblTestCount.Text = TestCount.ToString();
                LblTestSuccessCount.Text = TestSuccessCount.ToString();
                LblTestFailedCount.Text = TestFailedCount.ToString();
            }
        }

        /// <summary>
        /// 统计开始测试
        /// </summary>
        private void StatBeginTest()
        {
            TestCount += 1;
            UpdateStat();
        }

        /// <summary>
        /// 统计测试结束
        /// </summary>
        /// <param name="isSuccess">测试是否成功</param>
        private void StatEndTest(bool isSuccess)
        {
            if (isSuccess) TestSuccessCount++;
            else TestFailedCount++;
            UpdateStat();
        }

        #endregion - 重置统计 -
    }
}