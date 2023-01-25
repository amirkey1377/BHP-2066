namespace Skydat.forms2080
{
    partial class frmOption112080
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOption12080));
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BtnColorDraw = new System.Windows.Forms.Button();
            this.cmbStyleDraw = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cbDrawLine = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbRTStyle = new System.Windows.Forms.ComboBox();
            this.cmbRtButton = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cbRotate = new System.Windows.Forms.CheckBox();
            this.txtRTInertia = new System.Windows.Forms.TextBox();
            this.txtRTSpeed = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numWZoomMagnify = new System.Windows.Forms.NumericUpDown();
            this.numPerMagnify = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cbMagnify = new System.Windows.Forms.CheckBox();
            this.txtHMagnify = new System.Windows.Forms.TextBox();
            this.txtWmagnify = new System.Windows.Forms.TextBox();
            this.cbSelection = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbXValues = new System.Windows.Forms.CheckBox();
            this.txtOmegaStep = new System.Windows.Forms.TextBox();
            this.txtOmegaStart = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cbSmooth = new System.Windows.Forms.CheckBox();
            this.cmbTypeSmooth = new System.Windows.Forms.ComboBox();
            this.rbPoint = new System.Windows.Forms.RadioButton();
            this.rbfLine = new System.Windows.Forms.RadioButton();
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWZoomMagnify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPerMagnify)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Location = new System.Drawing.Point(269, 299);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnColorDraw);
            this.groupBox5.Controls.Add(this.cmbStyleDraw);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.cbDrawLine);
            this.groupBox5.Location = new System.Drawing.Point(6, 205);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(416, 40);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            // 
            // BtnColorDraw
            // 
            this.BtnColorDraw.Location = new System.Drawing.Point(271, 11);
            this.BtnColorDraw.Name = "BtnColorDraw";
            this.BtnColorDraw.Size = new System.Drawing.Size(43, 23);
            this.BtnColorDraw.TabIndex = 23;
            this.BtnColorDraw.Text = "Color";
            this.BtnColorDraw.UseVisualStyleBackColor = true;
            this.BtnColorDraw.Click += new System.EventHandler(this.BtnColorDraw_Click);
            this.BtnColorDraw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // cmbStyleDraw
            // 
            this.cmbStyleDraw.FormattingEnabled = true;
            this.cmbStyleDraw.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot"});
            this.cmbStyleDraw.Location = new System.Drawing.Point(115, 13);
            this.cmbStyleDraw.Name = "cmbStyleDraw";
            this.cmbStyleDraw.Size = new System.Drawing.Size(77, 21);
            this.cmbStyleDraw.TabIndex = 22;
            this.cmbStyleDraw.Text = "Solid";
            this.cmbStyleDraw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(80, 16);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(30, 13);
            this.label32.TabIndex = 3;
            this.label32.Text = "Style";
            // 
            // cbDrawLine
            // 
            this.cbDrawLine.AutoSize = true;
            this.cbDrawLine.ForeColor = System.Drawing.Color.Fuchsia;
            this.cbDrawLine.Location = new System.Drawing.Point(8, -1);
            this.cbDrawLine.Name = "cbDrawLine";
            this.cbDrawLine.Size = new System.Drawing.Size(74, 17);
            this.cbDrawLine.TabIndex = 20;
            this.cbDrawLine.Text = "Draw Line";
            this.cbDrawLine.UseVisualStyleBackColor = true;
            this.cbDrawLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbRTStyle);
            this.groupBox4.Controls.Add(this.cmbRtButton);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.cbRotate);
            this.groupBox4.Controls.Add(this.txtRTInertia);
            this.groupBox4.Controls.Add(this.txtRTSpeed);
            this.groupBox4.Location = new System.Drawing.Point(6, 130);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(416, 70);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            // 
            // cmbRTStyle
            // 
            this.cmbRTStyle.FormattingEnabled = true;
            this.cmbRTStyle.Items.AddRange(new object[] {
            "All",
            "Elevation",
            "Rotation"});
            this.cmbRTStyle.Location = new System.Drawing.Point(115, 43);
            this.cmbRTStyle.Name = "cmbRTStyle";
            this.cmbRTStyle.Size = new System.Drawing.Size(77, 21);
            this.cmbRTStyle.TabIndex = 17;
            this.cmbRTStyle.Text = "All";
            this.cmbRTStyle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // cmbRtButton
            // 
            this.cmbRtButton.AccessibleDescription = "";
            this.cmbRtButton.FormattingEnabled = true;
            this.cmbRtButton.Items.AddRange(new object[] {
            "None",
            "Right",
            "Middle",
            "Left"});
            this.cmbRtButton.Location = new System.Drawing.Point(115, 15);
            this.cmbRtButton.Name = "cmbRtButton";
            this.cmbRtButton.Size = new System.Drawing.Size(77, 21);
            this.cmbRtButton.TabIndex = 16;
            this.cmbRtButton.Text = "Middle";
            this.cmbRtButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(80, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(30, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "Style";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(209, 46);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(38, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "Speed";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(37, 18);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 13);
            this.label27.TabIndex = 3;
            this.label27.Text = "Mouse Button";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(320, 46);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(36, 13);
            this.label28.TabIndex = 3;
            this.label28.Text = "Inertia";
            // 
            // cbRotate
            // 
            this.cbRotate.AutoSize = true;
            this.cbRotate.ForeColor = System.Drawing.Color.Purple;
            this.cbRotate.Location = new System.Drawing.Point(8, -1);
            this.cbRotate.Name = "cbRotate";
            this.cbRotate.Size = new System.Drawing.Size(58, 17);
            this.cbRotate.TabIndex = 14;
            this.cbRotate.Text = "Rotate";
            this.cbRotate.UseVisualStyleBackColor = true;
            this.cbRotate.CheckedChanged += new System.EventHandler(this.cbRotate_CheckedChanged);
            this.cbRotate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // txtRTInertia
            // 
            this.txtRTInertia.Location = new System.Drawing.Point(364, 43);
            this.txtRTInertia.Name = "txtRTInertia";
            this.txtRTInertia.Size = new System.Drawing.Size(43, 20);
            this.txtRTInertia.TabIndex = 19;
            this.txtRTInertia.Text = "30";
            this.txtRTInertia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // txtRTSpeed
            // 
            this.txtRTSpeed.Location = new System.Drawing.Point(259, 43);
            this.txtRTSpeed.Name = "txtRTSpeed";
            this.txtRTSpeed.Size = new System.Drawing.Size(46, 20);
            this.txtRTSpeed.TabIndex = 18;
            this.txtRTSpeed.Text = "30";
            this.txtRTSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numWZoomMagnify);
            this.groupBox3.Controls.Add(this.numPerMagnify);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.cbMagnify);
            this.groupBox3.Controls.Add(this.txtHMagnify);
            this.groupBox3.Controls.Add(this.txtWmagnify);
            this.groupBox3.Location = new System.Drawing.Point(6, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(416, 45);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // numWZoomMagnify
            // 
            this.numWZoomMagnify.Location = new System.Drawing.Point(364, 19);
            this.numWZoomMagnify.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numWZoomMagnify.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWZoomMagnify.Name = "numWZoomMagnify";
            this.numWZoomMagnify.Size = new System.Drawing.Size(44, 20);
            this.numWZoomMagnify.TabIndex = 13;
            this.numWZoomMagnify.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numWZoomMagnify.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // numPerMagnify
            // 
            this.numPerMagnify.Location = new System.Drawing.Point(259, 19);
            this.numPerMagnify.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numPerMagnify.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPerMagnify.Name = "numPerMagnify";
            this.numPerMagnify.Size = new System.Drawing.Size(46, 20);
            this.numPerMagnify.TabIndex = 12;
            this.numPerMagnify.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.numPerMagnify.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(209, 21);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(44, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Percent";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(105, 21);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Height";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(5, 21);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(35, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Width";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(320, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "Zoom";
            // 
            // cbMagnify
            // 
            this.cbMagnify.AutoSize = true;
            this.cbMagnify.ForeColor = System.Drawing.Color.Fuchsia;
            this.cbMagnify.Location = new System.Drawing.Point(8, -1);
            this.cbMagnify.Name = "cbMagnify";
            this.cbMagnify.Size = new System.Drawing.Size(63, 17);
            this.cbMagnify.TabIndex = 9;
            this.cbMagnify.Text = "Magnify";
            this.cbMagnify.UseVisualStyleBackColor = true;
            this.cbMagnify.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // txtHMagnify
            // 
            this.txtHMagnify.Location = new System.Drawing.Point(148, 18);
            this.txtHMagnify.Name = "txtHMagnify";
            this.txtHMagnify.Size = new System.Drawing.Size(44, 20);
            this.txtHMagnify.TabIndex = 11;
            this.txtHMagnify.Text = "100";
            this.txtHMagnify.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // txtWmagnify
            // 
            this.txtWmagnify.Location = new System.Drawing.Point(47, 18);
            this.txtWmagnify.Name = "txtWmagnify";
            this.txtWmagnify.Size = new System.Drawing.Size(45, 20);
            this.txtWmagnify.TabIndex = 10;
            this.txtWmagnify.Text = "100";
            this.txtWmagnify.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // cbSelection
            // 
            this.cbSelection.AutoSize = true;
            this.cbSelection.ForeColor = System.Drawing.Color.Fuchsia;
            this.cbSelection.Location = new System.Drawing.Point(9, 303);
            this.cbSelection.Name = "cbSelection";
            this.cbSelection.Size = new System.Drawing.Size(68, 17);
            this.cbSelection.TabIndex = 28;
            this.cbSelection.Text = "selection";
            this.cbSelection.UseVisualStyleBackColor = true;
            this.cbSelection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbXValues);
            this.groupBox2.Controls.Add(this.txtOmegaStep);
            this.groupBox2.Controls.Add(this.txtOmegaStart);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cbSmooth);
            this.groupBox2.Controls.Add(this.cmbTypeSmooth);
            this.groupBox2.Location = new System.Drawing.Point(6, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 44);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // cbXValues
            // 
            this.cbXValues.AutoSize = true;
            this.cbXValues.Location = new System.Drawing.Point(292, 18);
            this.cbXValues.Name = "cbXValues";
            this.cbXValues.Size = new System.Drawing.Size(68, 17);
            this.cbXValues.TabIndex = 26;
            this.cbXValues.Text = "X Values";
            this.cbXValues.UseVisualStyleBackColor = true;
            this.cbXValues.Visible = false;
            // 
            // txtOmegaStep
            // 
            this.txtOmegaStep.Location = new System.Drawing.Point(364, 16);
            this.txtOmegaStep.Name = "txtOmegaStep";
            this.txtOmegaStep.Size = new System.Drawing.Size(41, 20);
            this.txtOmegaStep.TabIndex = 20;
            this.txtOmegaStep.Text = "0.1";
            this.txtOmegaStep.Visible = false;
            // 
            // txtOmegaStart
            // 
            this.txtOmegaStart.Location = new System.Drawing.Point(298, 16);
            this.txtOmegaStart.Name = "txtOmegaStart";
            this.txtOmegaStart.Size = new System.Drawing.Size(41, 20);
            this.txtOmegaStart.TabIndex = 19;
            this.txtOmegaStart.Text = "0.001";
            this.txtOmegaStart.Visible = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(229, 16);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(170, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(36, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Grade";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Type";
            // 
            // cbSmooth
            // 
            this.cbSmooth.AutoSize = true;
            this.cbSmooth.ForeColor = System.Drawing.Color.Purple;
            this.cbSmooth.Location = new System.Drawing.Point(8, -1);
            this.cbSmooth.Name = "cbSmooth";
            this.cbSmooth.Size = new System.Drawing.Size(62, 17);
            this.cbSmooth.TabIndex = 4;
            this.cbSmooth.Text = "Smooth";
            this.cbSmooth.UseVisualStyleBackColor = true;
            this.cbSmooth.Click += new System.EventHandler(this.cbSmooth_Click);
            this.cbSmooth.CheckedChanged += new System.EventHandler(this.cbSmooth_CheckedChanged);
            this.cbSmooth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // cmbTypeSmooth
            // 
            this.cmbTypeSmooth.Enabled = false;
            this.cmbTypeSmooth.FormattingEnabled = true;
            this.cmbTypeSmooth.Items.AddRange(new object[] {
            "Whittaker Smoother",
            "Moving Avg(0-4)",
            "Fourier Smoothing"});
            this.cmbTypeSmooth.Location = new System.Drawing.Point(47, 16);
            this.cmbTypeSmooth.Name = "cmbTypeSmooth";
            this.cmbTypeSmooth.Size = new System.Drawing.Size(117, 21);
            this.cmbTypeSmooth.TabIndex = 5;
            this.cmbTypeSmooth.Text = "Whittaker smoother";
            this.cmbTypeSmooth.SelectedValueChanged += new System.EventHandler(this.cmbTypeSmooth_SelectedValueChanged);
            this.cmbTypeSmooth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // rbPoint
            // 
            this.rbPoint.AutoSize = true;
            this.rbPoint.Location = new System.Drawing.Point(128, 6);
            this.rbPoint.Name = "rbPoint";
            this.rbPoint.Size = new System.Drawing.Size(49, 17);
            this.rbPoint.TabIndex = 3;
            this.rbPoint.Text = "Point";
            this.rbPoint.UseVisualStyleBackColor = true;
            this.rbPoint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // rbfLine
            // 
            this.rbfLine.AutoSize = true;
            this.rbfLine.Checked = true;
            this.rbfLine.Location = new System.Drawing.Point(6, 6);
            this.rbfLine.Name = "rbfLine";
            this.rbfLine.Size = new System.Drawing.Size(65, 17);
            this.rbfLine.TabIndex = 1;
            this.rbfLine.TabStop = true;
            this.rbfLine.Text = "FastLine";
            this.rbfLine.UseVisualStyleBackColor = true;
            this.rbfLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // rbLine
            // 
            this.rbLine.AutoSize = true;
            this.rbLine.Location = new System.Drawing.Point(77, 6);
            this.rbLine.Name = "rbLine";
            this.rbLine.Size = new System.Drawing.Size(45, 17);
            this.rbLine.TabIndex = 2;
            this.rbLine.Text = "Line";
            this.rbLine.UseVisualStyleBackColor = true;
            this.rbLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(345, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 327);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cbSelection);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.rbPoint);
            this.Controls.Add(this.rbfLine);
            this.Controls.Add(this.rbLine);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOption";
            this.Text = "OptionForm";
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWZoomMagnify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPerMagnify)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Button BtnColorDraw;
        public System.Windows.Forms.ComboBox cmbStyleDraw;
        public System.Windows.Forms.Label label32;
        public System.Windows.Forms.CheckBox cbDrawLine;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.ComboBox cmbRTStyle;
        public System.Windows.Forms.ComboBox cmbRtButton;
        public System.Windows.Forms.Label label24;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.Label label27;
        public System.Windows.Forms.Label label28;
        public System.Windows.Forms.CheckBox cbRotate;
        public System.Windows.Forms.TextBox txtRTInertia;
        public System.Windows.Forms.TextBox txtRTSpeed;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.NumericUpDown numWZoomMagnify;
        public System.Windows.Forms.NumericUpDown numPerMagnify;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label label20;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.CheckBox cbMagnify;
        public System.Windows.Forms.TextBox txtHMagnify;
        public System.Windows.Forms.TextBox txtWmagnify;
        public System.Windows.Forms.CheckBox cbSelection;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.CheckBox cbSmooth;
        public System.Windows.Forms.ComboBox cmbTypeSmooth;
        public System.Windows.Forms.RadioButton rbPoint;
        public System.Windows.Forms.RadioButton rbfLine;
        public System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtOmegaStep;
        public System.Windows.Forms.TextBox txtOmegaStart;
        public System.Windows.Forms.CheckBox cbXValues;
    }
}