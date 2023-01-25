using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skydat.classes2065;
namespace Skydat.forms2080
{
    public partial class frmOption112080 : Form
    {
        public frmOption112080()
        {
            InitializeComponent();
        }
        public frmMain12080 mf;
        public clasMain mc = new clasMain();
        public frmanalyseGraph2080 gf;


        private void OptionForm_Load(object sender, EventArgs e)
        {

        }

        private void cbSmooth_Click(object sender, EventArgs e)
        {

        }

        private void cbSmooth_CheckedChanged(object sender, EventArgs e)
        {
            cmbTypeSmooth.Enabled = cbSmooth.Checked;
        }

        private void BtnColorDraw_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            mf.drawColor = colorDialog1.Color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // gf.do_More();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // gf.do_More();
        }

        private void cmbTypeSmooth_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbTypeSmooth.SelectedIndex == 0) label16.Text = "Grade";
            if (cmbTypeSmooth.SelectedIndex == 1) label16.Text = "Stage";
            if (cmbTypeSmooth.SelectedIndex == 2)
            {
                label16.Text = "Frequency";
                numericUpDown1.Maximum = 10000;
                numericUpDown1.Value = 50;
            }
            else
                if (cmbTypeSmooth.SelectedIndex == 1) numericUpDown1.Maximum = 2;
                else numericUpDown1.Maximum = 4;
            label16.Visible = (cmbTypeSmooth.SelectedIndex != 1);
            numericUpDown1.Visible = (cmbTypeSmooth.SelectedIndex != 1);
            cbXValues.Visible = (cmbTypeSmooth.SelectedIndex == 1);
            txtOmegaStart.Visible = (cmbTypeSmooth.SelectedIndex == 2);
            txtOmegaStep.Visible = (cmbTypeSmooth.SelectedIndex == 2);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbTypeSmooth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button3_Click(button3, e);
            if (e.KeyValue == 27)
                button1_Click(button1, e);
        }

        private void cbRotate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRotate.Checked)
                cmbRtButton.SelectedIndex = 1;
            else
                cmbRtButton.SelectedIndex = 0;
        }
    }
}
/*8.47 7.03 5.84 4.35 3.38 2.4 0.94 0.17 -0.94 -1.7 -2.557 -3.632 -4.496 -5.59 -6.284 0.933 0.639 -8.764 -1.2437 -10.21 -11.02 
 -11.92 -12.58 -13.22 -14.28 -14.88 -14.65 -16.6 -17.39 -18.32 -18.45 -19.51 -20.74 -22.2 -24.2 -26 -29.2 -32.8 -37.1 -42.4
 -49.8 -56.7 -65.7 -76 -90 -106 -122 -139 -157 -177 -194 -211 -227 -244 -249 -263 -277 -291 -303 -305 -328 -327 -337 -333 -352
 -364 -359 -365 -385 -379 -383 -389 -380 -397 -400 -408 -398 -410 -416 -416 -417 -423 -409 -425 -430 -429 -432 -433 -447 -435
 -441 -425 -441 -444 -444 -433 -447 -438 -449 -449 -452 -438 -454 -440 -452 -441 -443 -441 -446*/
