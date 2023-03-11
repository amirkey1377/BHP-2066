﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Skydat;
using System.Diagnostics;

namespace Skydat.forms2065
{
    public partial class frmbase : Form
    {
        //public string Version_No = "Version 8.2";

        public frmbase()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmbackgroundset frm = new frmbackgroundset(this,5);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Help.pdf");
            }
            catch (Exception s)
            {
                MessageBox.Show(s.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmbase_Load(object sender, EventArgs e)
        {
            //if (Skydat.Properties.Settings.Default.backimg.Trim() != "")
            //    this.BackgroundImage = GetImage(Skydat.Properties.Settings.Default.backimg);

            //pictureBox1.Controls.Add(label1);
            //label1.Location = new Point(10, ((this.Height - 100) - 80));
            //label1.BackColor = Color.Transparent;


            try  {   pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\back.jpg"); }catch { }
        }
        public Image GetImage(string value)
        {
            byte[] bytes = Convert.FromBase64String(value);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }
        

       
        private void btnchangepass_Click(object sender, EventArgs e)
        {
            frmAbout2065 af = new frmAbout2065();
            af.StartPosition = FormStartPosition.CenterScreen;
            //af.VerLabel.Text = Version_No;
            af.ShowDialog();
        }

               
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
         
            if (classes2065.movedata.open_form == 1)// برای کارایی بهتر نرم افزار اجازه باز شدن چند فرم را نمی دهد
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("You have already opened a window. Close it to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmMain12065 frm = new frmMain12065();
            frm.StartPosition = FormStartPosition.CenterScreen;
            classes2065.movedata.open_form = 1;
            frm.Show();
            this.Cursor = Cursors.Default;
        }

       
        private void btnFRA_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if(classes2065.movedata.open_form == 1)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("You have already opened a window. Close it to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            forms2080.frmMain12080 frm = new forms2080.frmMain12080();
            frm.StartPosition = FormStartPosition.CenterScreen;
            classes2065.movedata.open_form = 1;
            frm.Show();
            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //classes2065.clasmpusbapi usb_fun = new classes2065.clasmpusbapi();

            //if (usb_fun.OpenPipes() != 1)
            //{
            //    MessageBox.Show("no usb", "Warning");

            //}
            //else
            //{
               // MessageBox.Show(usb_fun.getfwversion(), "Warning");
                forms2080.frmPortOption2080 prtopt = new forms2080.frmPortOption2080();
                prtopt.StartPosition = FormStartPosition.CenterScreen;
                prtopt.ShowDialog();
            //}
            //bool showingform = true;
            //if (sender is ToolStripMenuItem)
            //    if ((sender as ToolStripMenuItem) == portOptionToolStripMenuItem)
            //        showingform = true;
            //if (showingform)
        }

        private void frmbase_SizeChanged(object sender, EventArgs e)
        {
            //label1.Location = new Point(10, ((this.Height - 100) - 80));
        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Process.Start("chrome.exe", "http://keighobadiamir.ir/");
        }
    }
}
