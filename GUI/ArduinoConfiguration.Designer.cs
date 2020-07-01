namespace OpenHardwareMonitor.GUI {
  partial class ArduinoConfiguration {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            this.portCOM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.baudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // portCOM
            // 
            this.portCOM.FormattingEnabled = true;
            this.portCOM.Location = new System.Drawing.Point(137, 6);
            this.portCOM.Name = "portCOM";
            this.portCOM.Size = new System.Drawing.Size(84, 21);
            this.portCOM.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Puerto COM de Arduino";
            // 
            // baudRate
            // 
            this.baudRate.FormattingEnabled = true;
            this.baudRate.Location = new System.Drawing.Point(137, 33);
            this.baudRate.Name = "baudRate";
            this.baudRate.Size = new System.Drawing.Size(84, 21);
            this.baudRate.TabIndex = 2;
            this.baudRate.SelectedIndexChanged += new System.EventHandler(this.baudRate_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Baud Rate";
            // 
            // ArduinoConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 327);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.baudRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portCOM);
            this.Name = "ArduinoConfiguration";
            this.Text = "ArduinoConfiguration";
            this.Load += new System.EventHandler(this.ArduinoConfiguration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox portCOM;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox baudRate;
    private System.Windows.Forms.Label label2;
  }
}
