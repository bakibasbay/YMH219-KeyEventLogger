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
        grpLog.SuspendLayout();
        SuspendLayout();
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
}
