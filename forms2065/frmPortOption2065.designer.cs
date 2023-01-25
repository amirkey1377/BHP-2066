namespace Skydat.forms2065
{
    partial class frmPortOption2065
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPortOption2065));
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBRate = new System.Windows.Forms.TextBox();
            this.txtWT = new System.Windows.Forms.TextBox();
            this.txtRT = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbPrtName = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "Port Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Baud Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Read Timeout";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Write Timeout";
            // 
            // txtBRate
            // 
            this.txtBRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBRate.Location = new System.Drawing.Point(117, 38);
            this.txtBRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBRate.Name = "txtBRate";
            this.txtBRate.Size = new System.Drawing.Size(99, 22);
            this.txtBRate.TabIndex = 2;
            this.txtBRate.Text = "115200";
            this.txtBRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPrtName_KeyDown);
            // 
            // txtWT
            // 
            this.txtWT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWT.Location = new System.Drawing.Point(117, 100);
            this.txtWT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtWT.Name = "txtWT";
            this.txtWT.Size = new System.Drawing.Size(99, 22);
            this.txtWT.TabIndex = 4;
            this.txtWT.Text = "1";
            this.txtWT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPrtName_KeyDown);
            // 
            // txtRT
            // 
            this.txtRT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRT.Location = new System.Drawing.Point(117, 69);
            this.txtRT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRT.Name = "txtRT";
            this.txtRT.Size = new System.Drawing.Size(99, 22);
            this.txtRT.TabIndex = 3;
            this.txtRT.Text = "1";
            this.txtRT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPrtName_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 133);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(81, 133);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 28);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbPrtName
            // 
            this.cmbPrtName.FormattingEnabled = true;
            this.cmbPrtName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.cmbPrtName.Location = new System.Drawing.Point(117, 6);
            this.cmbPrtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPrtName.Name = "cmbPrtName";
            this.cmbPrtName.Size = new System.Drawing.Size(99, 24);
            this.cmbPrtName.TabIndex = 1;
           
            this.cmbPrtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPrtName_KeyDown);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(151, 133);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 28);
            this.button3.TabIndex = 6;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmPortOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 167);
            this.Controls.Add(this.cmbPrtName);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtRT);
            this.Controls.Add(this.txtWT);
            this.Controls.Add(this.txtBRate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmPortOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PortOptionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtBRate;
        public System.Windows.Forms.TextBox txtWT;
        public System.Windows.Forms.TextBox txtRT;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.ComboBox cmbPrtName;
        public System.Windows.Forms.Button button3;
    }
}