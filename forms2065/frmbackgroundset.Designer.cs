namespace Skydat.forms2065
{
    partial class frmbackgroundset
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnselimage = new System.Windows.Forms.Button();
            this.btnsetimage = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pic1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 279);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "select image";
            // 
            // pic1
            // 
            this.pic1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pic1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic1.Location = new System.Drawing.Point(7, 16);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(357, 256);
            this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic1.TabIndex = 0;
            this.pic1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnselimage);
            this.panel1.Controls.Add(this.btnsetimage);
            this.panel1.Controls.Add(this.btnexit);
            this.panel1.Location = new System.Drawing.Point(7, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 68);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Select";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Set";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Close";
            // 
            // btnselimage
            // 
            this.btnselimage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnselimage.BackgroundImage = global::Skydat.Properties.Resources.Pictures_icon;
            this.btnselimage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnselimage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnselimage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnselimage.FlatAppearance.BorderSize = 2;
            this.btnselimage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnselimage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnselimage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnselimage.Location = new System.Drawing.Point(87, 3);
            this.btnselimage.Name = "btnselimage";
            this.btnselimage.Size = new System.Drawing.Size(42, 40);
            this.btnselimage.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnselimage, "Select image");
            this.btnselimage.UseVisualStyleBackColor = false;
            this.btnselimage.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnsetimage
            // 
            this.btnsetimage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnsetimage.BackgroundImage = global::Skydat.Properties.Resources.pictures_icon__1_;
            this.btnsetimage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnsetimage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnsetimage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnsetimage.FlatAppearance.BorderSize = 2;
            this.btnsetimage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnsetimage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnsetimage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnsetimage.Location = new System.Drawing.Point(165, 3);
            this.btnsetimage.Name = "btnsetimage";
            this.btnsetimage.Size = new System.Drawing.Size(42, 40);
            this.btnsetimage.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnsetimage, "Set image for background");
            this.btnsetimage.UseVisualStyleBackColor = false;
            this.btnsetimage.Click += new System.EventHandler(this.btnsetimage_Click);
            // 
            // btnexit
            // 
            this.btnexit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnexit.BackgroundImage = global::Skydat.Properties.Resources.Exit;
            this.btnexit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnexit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnexit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnexit.FlatAppearance.BorderSize = 2;
            this.btnexit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnexit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnexit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnexit.Location = new System.Drawing.Point(243, 3);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(42, 40);
            this.btnexit.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnexit, "Close");
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AutoUpgradeEnabled = false;
            this.openFileDialog1.DereferenceLinks = false;
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmbackgroundset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 359);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmbackgroundset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "form background Settings";
            this.Load += new System.EventHandler(this.frmbackgroundset_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.Button btnselimage;
        private System.Windows.Forms.Button btnsetimage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}