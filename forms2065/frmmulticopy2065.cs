using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skydat.forms2065
{
    public partial class frmmulticopy2065 : Form
    {

        //public clasMain mc = new clasMain();
        //public frmMain1 mf;
        public frmanalyseGraph2065 gf;
        private int Graph_No_Math = 0;

        public frmmulticopy2065()
        {
            InitializeComponent();
        }
        public frmmulticopy2065(int i1)
        {
            InitializeComponent();
            Graph_No_Math = i1;
        }
       



        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAdd.Text.Trim() == "")
            {
                MessageBox.Show("please enter value for operand");
                return;
            }
            this.DialogResult=DialogResult.OK;

           
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(button1, e);
        }

    }
}
