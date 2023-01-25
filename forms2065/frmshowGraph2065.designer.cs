namespace Skydat.forms2065
{
    partial class frmshowGraph2065

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmshowGraph2065));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.dschart1 = new Skydat.datasets.Dschart1();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnshowvalue = new System.Windows.Forms.Button();
            this.btnselectonly = new System.Windows.Forms.Button();
            this.btnzoomin = new System.Windows.Forms.Button();
            this.btnzoomout = new System.Windows.Forms.Button();
            this.btnshowgrid = new System.Windows.Forms.Button();
            this.btnshowaxes = new System.Windows.Forms.Button();
            this.tChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.RichTextBox();
            this.txtcomment = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbhold = new System.Windows.Forms.ToolStripButton();
            this.tsbcont = new System.Windows.Forms.ToolStripButton();
            this.tsbstop = new System.Windows.Forms.ToolStripButton();
            this.tsbshowd = new System.Windows.Forms.ToolStripButton();
            this.tsbsave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.reSize1 = new LarcomAndYoung.Windows.Forms.ReSize(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.dschart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tChart1)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView3
            // 
            this.treeView3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.treeView3.ForeColor = System.Drawing.Color.Black;
            this.treeView3.LineColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.treeView3, "treeView3");
            this.treeView3.Name = "treeView3";
            this.treeView3.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeView3.Nodes")))});
            this.treeView3.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView3_AfterSelect);
            // 
            // dschart1
            // 
            this.dschart1.DataSetName = "Dschart1";
            this.dschart1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tChart1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnshowvalue);
            this.panel2.Controls.Add(this.btnselectonly);
            this.panel2.Controls.Add(this.btnzoomin);
            this.panel2.Controls.Add(this.btnzoomout);
            this.panel2.Controls.Add(this.btnshowgrid);
            this.panel2.Controls.Add(this.btnshowaxes);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.BackgroundImage = global::Skydat.Properties.Resources.color;
            resources.ApplyResources(this.button1, "button1");
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Name = "button1";
            this.toolTip1.SetToolTip(this.button1, resources.GetString("button1.ToolTip"));
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnshowvalue
            // 
            this.btnshowvalue.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnshowvalue, "btnshowvalue");
            this.btnshowvalue.FlatAppearance.BorderSize = 0;
            this.btnshowvalue.Image = global::Skydat.Properties.Resources.cursor_icon;
            this.btnshowvalue.Name = "btnshowvalue";
            this.toolTip1.SetToolTip(this.btnshowvalue, resources.GetString("btnshowvalue.ToolTip"));
            this.btnshowvalue.UseVisualStyleBackColor = false;
            this.btnshowvalue.Click += new System.EventHandler(this.btnshowvalue_Click);
            // 
            // btnselectonly
            // 
            this.btnselectonly.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnselectonly, "btnselectonly");
            this.btnselectonly.FlatAppearance.BorderSize = 0;
            this.btnselectonly.Image = global::Skydat.Properties.Resources.select_all_text_icon1;
            this.btnselectonly.Name = "btnselectonly";
            this.toolTip1.SetToolTip(this.btnselectonly, resources.GetString("btnselectonly.ToolTip"));
            this.btnselectonly.UseVisualStyleBackColor = false;
            this.btnselectonly.Click += new System.EventHandler(this.btnselectonly_Click);
            // 
            // btnzoomin
            // 
            this.btnzoomin.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnzoomin, "btnzoomin");
            this.btnzoomin.FlatAppearance.BorderSize = 0;
            this.btnzoomin.Image = global::Skydat.Properties.Resources.zoom_in_icon;
            this.btnzoomin.Name = "btnzoomin";
            this.toolTip1.SetToolTip(this.btnzoomin, resources.GetString("btnzoomin.ToolTip"));
            this.btnzoomin.UseVisualStyleBackColor = false;
            this.btnzoomin.Click += new System.EventHandler(this.btnzoomin_Click);
            // 
            // btnzoomout
            // 
            this.btnzoomout.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnzoomout, "btnzoomout");
            this.btnzoomout.FlatAppearance.BorderSize = 0;
            this.btnzoomout.Image = global::Skydat.Properties.Resources.zoom_out_icon__1_;
            this.btnzoomout.Name = "btnzoomout";
            this.toolTip1.SetToolTip(this.btnzoomout, resources.GetString("btnzoomout.ToolTip"));
            this.btnzoomout.UseVisualStyleBackColor = false;
            this.btnzoomout.Click += new System.EventHandler(this.btnzoomout_Click);
            // 
            // btnshowgrid
            // 
            this.btnshowgrid.BackColor = System.Drawing.SystemColors.Control;
            this.btnshowgrid.BackgroundImage = global::Skydat.Properties.Resources.activity_grid21600;
            resources.ApplyResources(this.btnshowgrid, "btnshowgrid");
            this.btnshowgrid.FlatAppearance.BorderSize = 0;
            this.btnshowgrid.Name = "btnshowgrid";
            this.toolTip1.SetToolTip(this.btnshowgrid, resources.GetString("btnshowgrid.ToolTip"));
            this.btnshowgrid.UseVisualStyleBackColor = false;
            this.btnshowgrid.Click += new System.EventHandler(this.btnshowgrid_Click);
            // 
            // btnshowaxes
            // 
            this.btnshowaxes.BackColor = System.Drawing.SystemColors.Control;
            this.btnshowaxes.BackgroundImage = global::Skydat.Properties.Resources.dimensions_icon;
            resources.ApplyResources(this.btnshowaxes, "btnshowaxes");
            this.btnshowaxes.FlatAppearance.BorderSize = 0;
            this.btnshowaxes.Name = "btnshowaxes";
            this.toolTip1.SetToolTip(this.btnshowaxes, resources.GetString("btnshowaxes.ToolTip"));
            this.btnshowaxes.UseVisualStyleBackColor = false;
            this.btnshowaxes.Click += new System.EventHandler(this.btnshowaxes_Click);
            // 
            // tChart1
            // 
            this.tChart1.BackColor = System.Drawing.Color.Black;
            this.tChart1.BorderlineColor = System.Drawing.Color.Black;
            this.tChart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            chartArea3.Name = "ChartArea1";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 100F;
            chartArea3.Position.Width = 100F;
            this.tChart1.ChartAreas.Add(chartArea3);
            resources.ApplyResources(this.tChart1, "tChart1");
            this.tChart1.Name = "tChart1";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Name = "Series1";
            this.tChart1.Series.Add(series3);
            this.tChart1.PostPaint += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs>(this.tChart1_PostPaint);
            this.tChart1.Click += new System.EventHandler(this.tChart1_Click);
            this.tChart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tChart1_MouseMove);
            this.tChart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tChart1_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtcomment);
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Controls.Add(this.treeView3);
            this.panel3.ForeColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.label4.ReadOnly = true;
            // 
            // txtcomment
            // 
            this.txtcomment.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.txtcomment.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.txtcomment, "txtcomment");
            this.txtcomment.Name = "txtcomment";
            this.txtcomment.ReadOnly = true;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbhold,
            this.tsbcont,
            this.tsbstop,
            this.tsbshowd,
            this.tsbsave});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tsbhold
            // 
            resources.ApplyResources(this.tsbhold, "tsbhold");
            this.tsbhold.ForeColor = System.Drawing.Color.Black;
            this.tsbhold.Image = global::Skydat.Properties.Resources.Pause_icon;
            this.tsbhold.Margin = new System.Windows.Forms.Padding(0);
            this.tsbhold.Name = "tsbhold";
            this.tsbhold.Click += new System.EventHandler(this.hToolStripMenuItem_Click);
            // 
            // tsbcont
            // 
            resources.ApplyResources(this.tsbcont, "tsbcont");
            this.tsbcont.ForeColor = System.Drawing.Color.Black;
            this.tsbcont.Image = global::Skydat.Properties.Resources.Play_icon;
            this.tsbcont.Margin = new System.Windows.Forms.Padding(0);
            this.tsbcont.Name = "tsbcont";
            this.tsbcont.Click += new System.EventHandler(this.hToolStripMenuItem_Click);
            // 
            // tsbstop
            // 
            resources.ApplyResources(this.tsbstop, "tsbstop");
            this.tsbstop.ForeColor = System.Drawing.Color.Black;
            this.tsbstop.Image = global::Skydat.Properties.Resources.Stop_icon;
            this.tsbstop.Margin = new System.Windows.Forms.Padding(0);
            this.tsbstop.Name = "tsbstop";
            this.tsbstop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // tsbshowd
            // 
            resources.ApplyResources(this.tsbshowd, "tsbshowd");
            this.tsbshowd.ForeColor = System.Drawing.Color.Black;
            this.tsbshowd.Image = global::Skydat.Properties.Resources.checklist_icon;
            this.tsbshowd.Margin = new System.Windows.Forms.Padding(0);
            this.tsbshowd.Name = "tsbshowd";
            this.tsbshowd.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // tsbsave
            // 
            this.tsbsave.ForeColor = System.Drawing.Color.Black;
            this.tsbsave.Image = global::Skydat.Properties.Resources.Save;
            resources.ApplyResources(this.tsbsave, "tsbsave");
            this.tsbsave.Name = "tsbsave";
            this.tsbsave.Click += new System.EventHandler(this.tsbsave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // shapeContainer1
            // 
            resources.ApplyResources(this.shapeContainer1, "shapeContainer1");
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            resources.ApplyResources(this.lineShape1, "lineShape1");
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Skydat.Properties.Resources.select_by_color_icon;
            resources.ApplyResources(this.toolStripButton4, "toolStripButton4");
            this.toolStripButton4.Name = "toolStripButton4";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::Skydat.Properties.Resources.chart_search_icon;
            resources.ApplyResources(this.toolStripButton6, "toolStripButton6");
            this.toolStripButton6.Name = "toolStripButton6";
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // toolStripButton7
            // 
            resources.ApplyResources(this.toolStripButton7, "toolStripButton7");
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::Skydat.Properties.Resources.zoom_in2;
            this.toolStripButton7.Name = "toolStripButton7";
            // 
            // toolStripButton8
            // 
            resources.ApplyResources(this.toolStripButton8, "toolStripButton8");
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::Skydat.Properties.Resources.zoom_out;
            this.toolStripButton8.Name = "toolStripButton8";
            // 
            // toolStripButton9
            // 
            resources.ApplyResources(this.toolStripButton9, "toolStripButton9");
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton9.ForeColor = System.Drawing.Color.Purple;
            this.toolStripButton9.Name = "toolStripButton9";
            // 
            // toolStripComboBox2
            // 
            resources.ApplyResources(this.toolStripComboBox2, "toolStripComboBox2");
            this.toolStripComboBox2.ForeColor = System.Drawing.Color.Indigo;
            this.toolStripComboBox2.Items.AddRange(new object[] {
            resources.GetString("toolStripComboBox2.Items"),
            resources.GetString("toolStripComboBox2.Items1"),
            resources.GetString("toolStripComboBox2.Items2"),
            resources.GetString("toolStripComboBox2.Items3"),
            resources.GetString("toolStripComboBox2.Items4"),
            resources.GetString("toolStripComboBox2.Items5"),
            resources.GetString("toolStripComboBox2.Items6"),
            resources.GetString("toolStripComboBox2.Items7"),
            resources.GetString("toolStripComboBox2.Items8"),
            resources.GetString("toolStripComboBox2.Items9"),
            resources.GetString("toolStripComboBox2.Items10"),
            resources.GetString("toolStripComboBox2.Items11"),
            resources.GetString("toolStripComboBox2.Items12"),
            resources.GetString("toolStripComboBox2.Items13"),
            resources.GetString("toolStripComboBox2.Items14"),
            resources.GetString("toolStripComboBox2.Items15"),
            resources.GetString("toolStripComboBox2.Items16"),
            resources.GetString("toolStripComboBox2.Items17"),
            resources.GetString("toolStripComboBox2.Items18"),
            resources.GetString("toolStripComboBox2.Items19"),
            resources.GetString("toolStripComboBox2.Items20")});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // reSize1
            // 
            this.reSize1.About = null;
            this.reSize1.AutoCenterFormOnLoad = false;
            this.reSize1.Enabled = true;
            this.reSize1.HostContainer = this;
            this.reSize1.InitialHostContainerHeight = 702D;
            this.reSize1.InitialHostContainerWidth = 884D;
            this.reSize1.Tag = null;
            // 
            // serialPort2
            // 
            this.serialPort2.BaudRate = 115200;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // frmshowGraph2065
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmshowGraph2065";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GraphForm_FormClosing);
            this.Load += new System.EventHandler(this.GraphForm_Load);
            this.Enter += new System.EventHandler(this.GraphForm_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.dschart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tChart1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart Chart2;
        public System.Windows.Forms.TreeView treeView3;
        public datasets.Dschart1 dschart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.DataVisualization.Charting.Chart tChart1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbhold;
        private System.Windows.Forms.ToolStripButton tsbstop;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnshowvalue;
        private System.Windows.Forms.Button btnselectonly;
        private System.Windows.Forms.Button btnzoomin;
        private System.Windows.Forms.Button btnzoomout;
        private System.Windows.Forms.Button btnshowgrid;
        private System.Windows.Forms.Button btnshowaxes;
        private System.Windows.Forms.ToolStripButton tsbcont;
        private System.Windows.Forms.ToolStripButton tsbshowd;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RichTextBox txtcomment;
        private System.Windows.Forms.ToolStripButton tsbsave;
        private System.Windows.Forms.Button button1;
        private LarcomAndYoung.Windows.Forms.ReSize reSize1;
        private System.Windows.Forms.RichTextBox label4;
        private System.IO.Ports.SerialPort serialPort2;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}