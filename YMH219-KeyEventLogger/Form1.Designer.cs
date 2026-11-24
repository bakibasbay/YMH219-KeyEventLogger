namespace KeyEventLogger;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        grpLog = new GroupBox();
        lstLog = new ListBox();
        lblInfo = new Label();
        btnStart = new Button();
        btnStop = new Button();
        btnClear = new Button();
        lblStatus = new Label();
        grpLog.SuspendLayout();
        SuspendLayout();
        //
        // lblStatus
        //
        lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblStatus.BackColor = Color.Firebrick;
        lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblStatus.ForeColor = Color.White;
        lblStatus.Location = new Point(688, 14);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(200, 40);
        lblStatus.TabIndex = 6;
        lblStatus.Text = "● KAYIT KAPALI";
        lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        //
        // btnStart
        //
        btnStart.Location = new Point(12, 14);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(110, 40);
        btnStart.TabIndex = 2;
        btnStart.TabStop = false;
        btnStart.Text = "▶ Başlat";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        //
        // btnStop
        //
        btnStop.Location = new Point(130, 14);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(110, 40);
        btnStop.TabIndex = 3;
        btnStop.TabStop = false;
        btnStop.Text = "■ Durdur";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += btnStop_Click;
        //
        // btnClear
        //
        btnClear.Location = new Point(248, 14);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(110, 40);
        btnClear.TabIndex = 4;
        btnClear.TabStop = false;
        btnClear.Text = "Temizle";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;
        //
        // grpLog
        //
        grpLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpLog.Controls.Add(lstLog);
        grpLog.Location = new Point(12, 72);
        grpLog.Name = "grpLog";
        grpLog.Padding = new Padding(8);
        grpLog.Size = new Size(580, 440);
        grpLog.TabIndex = 0;
        grpLog.TabStop = false;
        grpLog.Text = "Tuş Kayıtları";
        //
        // lstLog
        //
        lstLog.Dock = DockStyle.Fill;
        lstLog.Font = new Font("Consolas", 10F);
        lstLog.FormattingEnabled = true;
        lstLog.HorizontalScrollbar = true;
        lstLog.IntegralHeight = false;
        lstLog.Location = new Point(8, 24);
        lstLog.Name = "lstLog";
        lstLog.Size = new Size(564, 408);
        lstLog.TabIndex = 0;
        lstLog.PreviewKeyDown += lstLog_PreviewKeyDown;
        //
        // lblInfo
        //
        lblInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblInfo.AutoSize = true;
        lblInfo.ForeColor = Color.DimGray;
        lblInfo.Location = new Point(12, 527);
        lblInfo.Name = "lblInfo";
        lblInfo.Size = new Size(520, 20);
        lblInfo.TabIndex = 1;
        lblInfo.Text = "Bilgi: Tuşlar yalnızca bu pencere aktifken yakalanır. Sistem genelinde kayıt yapılmaz.";
        //
        // Form1
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 560);
        Controls.Add(lblStatus);
        Controls.Add(btnClear);
        Controls.Add(btnStop);
        Controls.Add(btnStart);
        Controls.Add(lblInfo);
        Controls.Add(grpLog);
        KeyPreview = true;
        MinimumSize = new Size(760, 480);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "YMH219 - Key Event Logger";
        KeyDown += Form1_KeyDown;
        KeyUp += Form1_KeyUp;
        grpLog.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox grpLog;
    private ListBox lstLog;
    private Label lblInfo;
    private Button btnStart;
    private Button btnStop;
    private Button btnClear;
    private Label lblStatus;
}
