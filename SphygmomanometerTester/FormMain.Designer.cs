namespace SphygmomanometerTester
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GrpSerialPort = new System.Windows.Forms.GroupBox();
            this.CmbSerialPortName = new System.Windows.Forms.ComboBox();
            this.LblPortName = new System.Windows.Forms.Label();
            this.BtnUpdatePortNames = new System.Windows.Forms.Button();
            this.BtnOpenPort = new System.Windows.Forms.Button();
            this.BtnClosePort = new System.Windows.Forms.Button();
            this.GrpSphygmomanometer = new System.Windows.Forms.GroupBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnStartMeasure = new System.Windows.Forms.Button();
            this.BtnStopMeasure = new System.Windows.Forms.Button();
            this.LblSystolicPressure = new System.Windows.Forms.Label();
            this.GrpMeasureResult = new System.Windows.Forms.GroupBox();
            this.LblSystolicPressureValue = new System.Windows.Forms.Label();
            this.LblDiastolicPressure = new System.Windows.Forms.Label();
            this.LblDiastolicPressureValue = new System.Windows.Forms.Label();
            this.LblPulse = new System.Windows.Forms.Label();
            this.LblPulseValue = new System.Windows.Forms.Label();
            this.GrpPressure = new System.Windows.Forms.GroupBox();
            this.LblPressureValue = new System.Windows.Forms.Label();
            this.LblInterval = new System.Windows.Forms.Label();
            this.NudInterval = new System.Windows.Forms.NumericUpDown();
            this.ChkLoopTest = new System.Windows.Forms.CheckBox();
            this.LblLoopTestTip = new System.Windows.Forms.Label();
            this.PrbIntervalProgress = new System.Windows.Forms.ProgressBar();
            this.GrpLog = new System.Windows.Forms.GroupBox();
            this.TxtLog = new System.Windows.Forms.TextBox();
            this.GrpStat = new System.Windows.Forms.GroupBox();
            this.BtnResetStat = new System.Windows.Forms.Button();
            this.FlpStatLabels = new System.Windows.Forms.FlowLayoutPanel();
            this.LblTestStat = new System.Windows.Forms.Label();
            this.LblTestSuccessCount = new System.Windows.Forms.Label();
            this.LblTestStatSeparator1 = new System.Windows.Forms.Label();
            this.LblTestFailedCount = new System.Windows.Forms.Label();
            this.LblTestStatSeparator2 = new System.Windows.Forms.Label();
            this.LblTestCount = new System.Windows.Forms.Label();
            this.GrpSerialPort.SuspendLayout();
            this.GrpSphygmomanometer.SuspendLayout();
            this.GrpMeasureResult.SuspendLayout();
            this.GrpPressure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudInterval)).BeginInit();
            this.GrpLog.SuspendLayout();
            this.GrpStat.SuspendLayout();
            this.FlpStatLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpSerialPort
            // 
            this.GrpSerialPort.Controls.Add(this.BtnClosePort);
            this.GrpSerialPort.Controls.Add(this.BtnOpenPort);
            this.GrpSerialPort.Controls.Add(this.BtnUpdatePortNames);
            this.GrpSerialPort.Controls.Add(this.LblPortName);
            this.GrpSerialPort.Controls.Add(this.CmbSerialPortName);
            this.GrpSerialPort.Location = new System.Drawing.Point(13, 12);
            this.GrpSerialPort.Name = "GrpSerialPort";
            this.GrpSerialPort.Size = new System.Drawing.Size(561, 60);
            this.GrpSerialPort.TabIndex = 0;
            this.GrpSerialPort.TabStop = false;
            this.GrpSerialPort.Text = "串口设置";
            // 
            // CmbSerialPortName
            // 
            this.CmbSerialPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSerialPortName.FormattingEnabled = true;
            this.CmbSerialPortName.Location = new System.Drawing.Point(71, 22);
            this.CmbSerialPortName.Name = "CmbSerialPortName";
            this.CmbSerialPortName.Size = new System.Drawing.Size(120, 25);
            this.CmbSerialPortName.TabIndex = 1;
            // 
            // LblPortName
            // 
            this.LblPortName.AutoSize = true;
            this.LblPortName.Location = new System.Drawing.Point(21, 25);
            this.LblPortName.Name = "LblPortName";
            this.LblPortName.Size = new System.Drawing.Size(44, 17);
            this.LblPortName.TabIndex = 0;
            this.LblPortName.Text = "端口：";
            // 
            // BtnUpdatePortNames
            // 
            this.BtnUpdatePortNames.Location = new System.Drawing.Point(197, 22);
            this.BtnUpdatePortNames.Name = "BtnUpdatePortNames";
            this.BtnUpdatePortNames.Size = new System.Drawing.Size(75, 25);
            this.BtnUpdatePortNames.TabIndex = 2;
            this.BtnUpdatePortNames.Text = "刷新";
            this.BtnUpdatePortNames.UseVisualStyleBackColor = true;
            this.BtnUpdatePortNames.Click += new System.EventHandler(this.BtnUpdatePortNames_Click);
            // 
            // BtnOpenPort
            // 
            this.BtnOpenPort.Location = new System.Drawing.Point(278, 22);
            this.BtnOpenPort.Name = "BtnOpenPort";
            this.BtnOpenPort.Size = new System.Drawing.Size(75, 25);
            this.BtnOpenPort.TabIndex = 3;
            this.BtnOpenPort.Text = "打开";
            this.BtnOpenPort.UseVisualStyleBackColor = true;
            this.BtnOpenPort.Click += new System.EventHandler(this.BtnOpenPort_Click);
            // 
            // BtnClosePort
            // 
            this.BtnClosePort.Location = new System.Drawing.Point(359, 22);
            this.BtnClosePort.Name = "BtnClosePort";
            this.BtnClosePort.Size = new System.Drawing.Size(75, 25);
            this.BtnClosePort.TabIndex = 4;
            this.BtnClosePort.Text = "关闭";
            this.BtnClosePort.UseVisualStyleBackColor = true;
            this.BtnClosePort.Click += new System.EventHandler(this.BtnClosePort_Click);
            // 
            // GrpSphygmomanometer
            // 
            this.GrpSphygmomanometer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GrpSphygmomanometer.Controls.Add(this.PrbIntervalProgress);
            this.GrpSphygmomanometer.Controls.Add(this.LblLoopTestTip);
            this.GrpSphygmomanometer.Controls.Add(this.ChkLoopTest);
            this.GrpSphygmomanometer.Controls.Add(this.NudInterval);
            this.GrpSphygmomanometer.Controls.Add(this.LblInterval);
            this.GrpSphygmomanometer.Controls.Add(this.BtnStopMeasure);
            this.GrpSphygmomanometer.Controls.Add(this.BtnStartMeasure);
            this.GrpSphygmomanometer.Controls.Add(this.BtnConnect);
            this.GrpSphygmomanometer.Location = new System.Drawing.Point(13, 272);
            this.GrpSphygmomanometer.Name = "GrpSphygmomanometer";
            this.GrpSphygmomanometer.Size = new System.Drawing.Size(560, 77);
            this.GrpSphygmomanometer.TabIndex = 3;
            this.GrpSphygmomanometer.TabStop = false;
            this.GrpSphygmomanometer.Text = "血压计控制";
            // 
            // BtnConnect
            // 
            this.BtnConnect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnConnect.Location = new System.Drawing.Point(24, 31);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(100, 30);
            this.BtnConnect.TabIndex = 0;
            this.BtnConnect.Text = "测试连接";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnStartMeasure
            // 
            this.BtnStartMeasure.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnStartMeasure.Location = new System.Drawing.Point(132, 31);
            this.BtnStartMeasure.Name = "BtnStartMeasure";
            this.BtnStartMeasure.Size = new System.Drawing.Size(100, 30);
            this.BtnStartMeasure.TabIndex = 1;
            this.BtnStartMeasure.Text = "开始测量";
            this.BtnStartMeasure.UseVisualStyleBackColor = true;
            this.BtnStartMeasure.Click += new System.EventHandler(this.BtnStartMeasure_Click);
            // 
            // BtnStopMeasure
            // 
            this.BtnStopMeasure.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnStopMeasure.Location = new System.Drawing.Point(238, 31);
            this.BtnStopMeasure.Name = "BtnStopMeasure";
            this.BtnStopMeasure.Size = new System.Drawing.Size(100, 30);
            this.BtnStopMeasure.TabIndex = 2;
            this.BtnStopMeasure.Text = "中断测量";
            this.BtnStopMeasure.UseVisualStyleBackColor = true;
            this.BtnStopMeasure.Click += new System.EventHandler(this.BtnStopMeasure_Click);
            // 
            // LblSystolicPressure
            // 
            this.LblSystolicPressure.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LblSystolicPressure.AutoSize = true;
            this.LblSystolicPressure.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.LblSystolicPressure.Location = new System.Drawing.Point(55, 33);
            this.LblSystolicPressure.Name = "LblSystolicPressure";
            this.LblSystolicPressure.Size = new System.Drawing.Size(74, 25);
            this.LblSystolicPressure.TabIndex = 1;
            this.LblSystolicPressure.Text = "收缩压:";
            // 
            // GrpMeasureResult
            // 
            this.GrpMeasureResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GrpMeasureResult.Controls.Add(this.LblPulseValue);
            this.GrpMeasureResult.Controls.Add(this.LblPulse);
            this.GrpMeasureResult.Controls.Add(this.LblDiastolicPressureValue);
            this.GrpMeasureResult.Controls.Add(this.LblDiastolicPressure);
            this.GrpMeasureResult.Controls.Add(this.LblSystolicPressureValue);
            this.GrpMeasureResult.Controls.Add(this.LblSystolicPressure);
            this.GrpMeasureResult.Location = new System.Drawing.Point(13, 189);
            this.GrpMeasureResult.Name = "GrpMeasureResult";
            this.GrpMeasureResult.Size = new System.Drawing.Size(560, 77);
            this.GrpMeasureResult.TabIndex = 2;
            this.GrpMeasureResult.TabStop = false;
            this.GrpMeasureResult.Text = "测量结果";
            // 
            // LblSystolicPressureValue
            // 
            this.LblSystolicPressureValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LblSystolicPressureValue.AutoSize = true;
            this.LblSystolicPressureValue.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.LblSystolicPressureValue.Location = new System.Drawing.Point(135, 33);
            this.LblSystolicPressureValue.Name = "LblSystolicPressureValue";
            this.LblSystolicPressureValue.Size = new System.Drawing.Size(23, 25);
            this.LblSystolicPressureValue.TabIndex = 2;
            this.LblSystolicPressureValue.Text = "0";
            // 
            // LblDiastolicPressure
            // 
            this.LblDiastolicPressure.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LblDiastolicPressure.AutoSize = true;
            this.LblDiastolicPressure.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.LblDiastolicPressure.Location = new System.Drawing.Point(230, 33);
            this.LblDiastolicPressure.Name = "LblDiastolicPressure";
            this.LblDiastolicPressure.Size = new System.Drawing.Size(69, 25);
            this.LblDiastolicPressure.TabIndex = 3;
            this.LblDiastolicPressure.Text = "舒张压";
            // 
            // LblDiastolicPressureValue
            // 
            this.LblDiastolicPressureValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LblDiastolicPressureValue.AutoSize = true;
            this.LblDiastolicPressureValue.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.LblDiastolicPressureValue.Location = new System.Drawing.Point(305, 33);
            this.LblDiastolicPressureValue.Name = "LblDiastolicPressureValue";
            this.LblDiastolicPressureValue.Size = new System.Drawing.Size(23, 25);
            this.LblDiastolicPressureValue.TabIndex = 4;
            this.LblDiastolicPressureValue.Text = "0";
            // 
            // LblPulse
            // 
            this.LblPulse.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LblPulse.AutoSize = true;
            this.LblPulse.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.LblPulse.Location = new System.Drawing.Point(399, 33);
            this.LblPulse.Name = "LblPulse";
            this.LblPulse.Size = new System.Drawing.Size(55, 25);
            this.LblPulse.TabIndex = 5;
            this.LblPulse.Text = "脉搏:";
            // 
            // LblPulseValue
            // 
            this.LblPulseValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LblPulseValue.AutoSize = true;
            this.LblPulseValue.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.LblPulseValue.Location = new System.Drawing.Point(460, 33);
            this.LblPulseValue.Name = "LblPulseValue";
            this.LblPulseValue.Size = new System.Drawing.Size(23, 25);
            this.LblPulseValue.TabIndex = 6;
            this.LblPulseValue.Text = "0";
            // 
            // GrpPressure
            // 
            this.GrpPressure.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.GrpPressure.Controls.Add(this.LblPressureValue);
            this.GrpPressure.Location = new System.Drawing.Point(192, 78);
            this.GrpPressure.Name = "GrpPressure";
            this.GrpPressure.Size = new System.Drawing.Size(200, 105);
            this.GrpPressure.TabIndex = 1;
            this.GrpPressure.TabStop = false;
            this.GrpPressure.Text = "实时压力值(mmHg)";
            // 
            // LblPressureValue
            // 
            this.LblPressureValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPressureValue.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.LblPressureValue.Location = new System.Drawing.Point(3, 19);
            this.LblPressureValue.Name = "LblPressureValue";
            this.LblPressureValue.Size = new System.Drawing.Size(194, 83);
            this.LblPressureValue.TabIndex = 0;
            this.LblPressureValue.Text = "0";
            this.LblPressureValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblInterval
            // 
            this.LblInterval.AutoSize = true;
            this.LblInterval.Location = new System.Drawing.Point(357, 31);
            this.LblInterval.Name = "LblInterval";
            this.LblInterval.Size = new System.Drawing.Size(50, 17);
            this.LblInterval.TabIndex = 3;
            this.LblInterval.Text = "间隔(S):";
            // 
            // NudInterval
            // 
            this.NudInterval.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NudInterval.Location = new System.Drawing.Point(413, 29);
            this.NudInterval.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NudInterval.Name = "NudInterval";
            this.NudInterval.Size = new System.Drawing.Size(60, 23);
            this.NudInterval.TabIndex = 4;
            this.NudInterval.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // ChkLoopTest
            // 
            this.ChkLoopTest.AutoSize = true;
            this.ChkLoopTest.Location = new System.Drawing.Point(479, 31);
            this.ChkLoopTest.Name = "ChkLoopTest";
            this.ChkLoopTest.Size = new System.Drawing.Size(75, 21);
            this.ChkLoopTest.TabIndex = 5;
            this.ChkLoopTest.Text = "循环测试";
            this.ChkLoopTest.UseVisualStyleBackColor = true;
            this.ChkLoopTest.CheckedChanged += new System.EventHandler(this.ChkLoopTest_CheckedChanged);
            // 
            // LblLoopTestTip
            // 
            this.LblLoopTestTip.AutoSize = true;
            this.LblLoopTestTip.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LblLoopTestTip.Location = new System.Drawing.Point(360, 55);
            this.LblLoopTestTip.Name = "LblLoopTestTip";
            this.LblLoopTestTip.Size = new System.Drawing.Size(188, 17);
            this.LblLoopTestTip.TabIndex = 6;
            this.LblLoopTestTip.Text = "收到测试结果指定时间后自动开始";
            // 
            // PrbIntervalProgress
            // 
            this.PrbIntervalProgress.Location = new System.Drawing.Point(360, 14);
            this.PrbIntervalProgress.Name = "PrbIntervalProgress";
            this.PrbIntervalProgress.Size = new System.Drawing.Size(188, 10);
            this.PrbIntervalProgress.TabIndex = 7;
            this.PrbIntervalProgress.Visible = false;
            // 
            // GrpLog
            // 
            this.GrpLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpLog.Controls.Add(this.TxtLog);
            this.GrpLog.Location = new System.Drawing.Point(579, 12);
            this.GrpLog.Name = "GrpLog";
            this.GrpLog.Size = new System.Drawing.Size(233, 284);
            this.GrpLog.TabIndex = 4;
            this.GrpLog.TabStop = false;
            this.GrpLog.Text = "日志";
            // 
            // TxtLog
            // 
            this.TxtLog.BackColor = System.Drawing.Color.White;
            this.TxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtLog.Location = new System.Drawing.Point(3, 19);
            this.TxtLog.Multiline = true;
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ReadOnly = true;
            this.TxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtLog.Size = new System.Drawing.Size(227, 262);
            this.TxtLog.TabIndex = 0;
            // 
            // GrpStat
            // 
            this.GrpStat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpStat.Controls.Add(this.FlpStatLabels);
            this.GrpStat.Controls.Add(this.BtnResetStat);
            this.GrpStat.Location = new System.Drawing.Point(579, 301);
            this.GrpStat.Name = "GrpStat";
            this.GrpStat.Size = new System.Drawing.Size(230, 48);
            this.GrpStat.TabIndex = 5;
            this.GrpStat.TabStop = false;
            this.GrpStat.Text = "统计";
            // 
            // BtnResetStat
            // 
            this.BtnResetStat.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnResetStat.Location = new System.Drawing.Point(177, 19);
            this.BtnResetStat.Name = "BtnResetStat";
            this.BtnResetStat.Size = new System.Drawing.Size(50, 26);
            this.BtnResetStat.TabIndex = 1;
            this.BtnResetStat.Text = "重置";
            this.BtnResetStat.UseVisualStyleBackColor = true;
            this.BtnResetStat.Click += new System.EventHandler(this.BtnResetStat_Click);
            // 
            // FlpStatLabels
            // 
            this.FlpStatLabels.Controls.Add(this.LblTestStat);
            this.FlpStatLabels.Controls.Add(this.LblTestSuccessCount);
            this.FlpStatLabels.Controls.Add(this.LblTestStatSeparator1);
            this.FlpStatLabels.Controls.Add(this.LblTestFailedCount);
            this.FlpStatLabels.Controls.Add(this.LblTestStatSeparator2);
            this.FlpStatLabels.Controls.Add(this.LblTestCount);
            this.FlpStatLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlpStatLabels.Location = new System.Drawing.Point(3, 19);
            this.FlpStatLabels.Name = "FlpStatLabels";
            this.FlpStatLabels.Size = new System.Drawing.Size(174, 26);
            this.FlpStatLabels.TabIndex = 2;
            // 
            // LblTestStat
            // 
            this.LblTestStat.AutoSize = true;
            this.LblTestStat.Location = new System.Drawing.Point(0, 4);
            this.LblTestStat.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.LblTestStat.Name = "LblTestStat";
            this.LblTestStat.Size = new System.Drawing.Size(59, 17);
            this.LblTestStat.TabIndex = 0;
            this.LblTestStat.Text = "测量次数:";
            // 
            // LblTestSuccessCount
            // 
            this.LblTestSuccessCount.AutoSize = true;
            this.LblTestSuccessCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblTestSuccessCount.ForeColor = System.Drawing.Color.Green;
            this.LblTestSuccessCount.Location = new System.Drawing.Point(59, 4);
            this.LblTestSuccessCount.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.LblTestSuccessCount.Name = "LblTestSuccessCount";
            this.LblTestSuccessCount.Size = new System.Drawing.Size(15, 17);
            this.LblTestSuccessCount.TabIndex = 1;
            this.LblTestSuccessCount.Text = "0";
            // 
            // LblTestStatSeparator1
            // 
            this.LblTestStatSeparator1.AutoSize = true;
            this.LblTestStatSeparator1.Location = new System.Drawing.Point(74, 4);
            this.LblTestStatSeparator1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.LblTestStatSeparator1.Name = "LblTestStatSeparator1";
            this.LblTestStatSeparator1.Size = new System.Drawing.Size(13, 17);
            this.LblTestStatSeparator1.TabIndex = 2;
            this.LblTestStatSeparator1.Text = "/";
            // 
            // LblTestFailedCount
            // 
            this.LblTestFailedCount.AutoSize = true;
            this.LblTestFailedCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblTestFailedCount.ForeColor = System.Drawing.Color.Red;
            this.LblTestFailedCount.Location = new System.Drawing.Point(87, 4);
            this.LblTestFailedCount.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.LblTestFailedCount.Name = "LblTestFailedCount";
            this.LblTestFailedCount.Size = new System.Drawing.Size(15, 17);
            this.LblTestFailedCount.TabIndex = 3;
            this.LblTestFailedCount.Text = "0";
            // 
            // LblTestStatSeparator2
            // 
            this.LblTestStatSeparator2.AutoSize = true;
            this.LblTestStatSeparator2.Location = new System.Drawing.Point(102, 4);
            this.LblTestStatSeparator2.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.LblTestStatSeparator2.Name = "LblTestStatSeparator2";
            this.LblTestStatSeparator2.Size = new System.Drawing.Size(13, 17);
            this.LblTestStatSeparator2.TabIndex = 4;
            this.LblTestStatSeparator2.Text = "/";
            // 
            // LblTestCount
            // 
            this.LblTestCount.AutoSize = true;
            this.LblTestCount.Location = new System.Drawing.Point(115, 4);
            this.LblTestCount.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.LblTestCount.Name = "LblTestCount";
            this.LblTestCount.Size = new System.Drawing.Size(15, 17);
            this.LblTestCount.TabIndex = 5;
            this.LblTestCount.Text = "0";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 361);
            this.Controls.Add(this.GrpStat);
            this.Controls.Add(this.GrpLog);
            this.Controls.Add(this.GrpPressure);
            this.Controls.Add(this.GrpMeasureResult);
            this.Controls.Add(this.GrpSphygmomanometer);
            this.Controls.Add(this.GrpSerialPort);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(840, 400);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "血压计通讯测试工具";
            this.GrpSerialPort.ResumeLayout(false);
            this.GrpSerialPort.PerformLayout();
            this.GrpSphygmomanometer.ResumeLayout(false);
            this.GrpSphygmomanometer.PerformLayout();
            this.GrpMeasureResult.ResumeLayout(false);
            this.GrpMeasureResult.PerformLayout();
            this.GrpPressure.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NudInterval)).EndInit();
            this.GrpLog.ResumeLayout(false);
            this.GrpLog.PerformLayout();
            this.GrpStat.ResumeLayout(false);
            this.FlpStatLabels.ResumeLayout(false);
            this.FlpStatLabels.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpSerialPort;
        private System.Windows.Forms.Button BtnClosePort;
        private System.Windows.Forms.Button BtnOpenPort;
        private System.Windows.Forms.Button BtnUpdatePortNames;
        private System.Windows.Forms.Label LblPortName;
        private System.Windows.Forms.ComboBox CmbSerialPortName;
        private System.Windows.Forms.GroupBox GrpSphygmomanometer;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnStopMeasure;
        private System.Windows.Forms.Button BtnStartMeasure;
        private System.Windows.Forms.Label LblSystolicPressure;
        private System.Windows.Forms.GroupBox GrpMeasureResult;
        private System.Windows.Forms.Label LblSystolicPressureValue;
        private System.Windows.Forms.Label LblPulseValue;
        private System.Windows.Forms.Label LblPulse;
        private System.Windows.Forms.Label LblDiastolicPressureValue;
        private System.Windows.Forms.Label LblDiastolicPressure;
        private System.Windows.Forms.GroupBox GrpPressure;
        private System.Windows.Forms.Label LblPressureValue;
        private System.Windows.Forms.ProgressBar PrbIntervalProgress;
        private System.Windows.Forms.Label LblLoopTestTip;
        private System.Windows.Forms.CheckBox ChkLoopTest;
        private System.Windows.Forms.NumericUpDown NudInterval;
        private System.Windows.Forms.Label LblInterval;
        private System.Windows.Forms.GroupBox GrpLog;
        private System.Windows.Forms.TextBox TxtLog;
        private System.Windows.Forms.GroupBox GrpStat;
        private System.Windows.Forms.FlowLayoutPanel FlpStatLabels;
        private System.Windows.Forms.Label LblTestStat;
        private System.Windows.Forms.Label LblTestSuccessCount;
        private System.Windows.Forms.Label LblTestStatSeparator1;
        private System.Windows.Forms.Label LblTestFailedCount;
        private System.Windows.Forms.Label LblTestStatSeparator2;
        private System.Windows.Forms.Label LblTestCount;
        private System.Windows.Forms.Button BtnResetStat;
    }
}

