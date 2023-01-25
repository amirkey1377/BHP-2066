using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Skydat.forms2065
{
    public partial class frmAbout2065 : Form
    {
        public frmAbout2065()
        {
            InitializeComponent();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_KeyUp(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           //"http://www.behpajooh.net" 
            System.Diagnostics.Process.Start("www.behpajooh.net");
        }

      

      
       
    }
}
