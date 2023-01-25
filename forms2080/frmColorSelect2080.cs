using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skydat.forms2080
{
    public partial class frmColorSelect2080 : Form
    {
        public frmColorSelect2080()
        {
            InitializeComponent();
        }
        public frmMain12080 mf;

        private void frmColorSelect_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = mf.cpar;
        }
    }
}
