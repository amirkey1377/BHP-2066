using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Skydat;


namespace Skydat.forms2065
{
    public partial class frmPortOption2065 : Form
    {
        public frmPortOption2065()
        {
            InitializeComponent();
        }
//        MainForm mform = MainForm();// = MainForm();
        public bool isOK = false;

        private void button1_Click(object sender, EventArgs e)
        {
            isOK = true;
            Close();
        }

         private void button2_Click(object sender, EventArgs e)
        {
            isOK = false;
            Close();
        }

        private void txtPName_KeyDown(object sender, KeyEventArgs e)
        {
           // if (e.KeyValue == 13)
           //     button1_Click(button1, e);
           // if (e.KeyValue == 27)
           //     button2_Click(button2, e);
        }

        private void cmbPrtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(button1, e);
            if (e.KeyValue == 27)
                button2_Click(button2, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbPrtName.Text = "COM2";
            txtBRate.Text = "1000000";
            txtRT.Text = "1000";
            txtWT.Text = "1000";
        }

       
    }
}