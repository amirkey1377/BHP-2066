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
    public partial class frmRDECtrl2065 : Form
    {
        public frmRDECtrl2065()
        {
            InitializeComponent();
        }

        private bool initialPort()
        {
            RDEPort.Close();
            RDEPort.PortName = cmbComport.Text;
            RDEPort.BaudRate = Convert.ToInt32(txtBRate.Text);
            RDEPort.Parity = System.IO.Ports.Parity.None;
            RDEPort.StopBits = System.IO.Ports.StopBits.One;
            RDEPort.DataBits = 8;
            try
            {
                RDEPort.Open();
                return true;
            }
            catch
            {
                MessageBox.Show("Port Is not Opened");
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initialPort();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            char[] buf = { 's'};
            initialPort();
            //RDEPort.Write(buf, 0, 1);
            if (RDEPort.IsOpen)
            {
                RDEPort.Write(buf, 0, 1);
                for (int i = 0; i < txtDesiredRPM.Text.Length; i++)
                {
                    buf[0] = (char)txtDesiredRPM.Text[i];
                    RDEPort.Write(buf, 0, 1);
                    //while (RDEPort.ReadBufferSize < 1) ;
                    //s = RDEPort.ReadExisting();
                    //if (s[0] != buf[0]) ;
                }
            }
        }

        private void SPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //string s = SPort.ReadExisting();
            //txtRealRPM.Text = s;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRunStop_Click(object sender, EventArgs e)
        {
            if (initialPort())
            {
                {
                    char[] buf = { 'a' };
                    RDEPort.Write(buf, 0, 1);
                }
            }
        }

        private void frmRDECtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            RDEPort.Close();
        }
    }
}
