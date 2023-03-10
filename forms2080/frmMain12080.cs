using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.IO; 
using System.Threading;
using System.Collections.Specialized;
using PVOID = System.IntPtr;
using Skydat.classes2065;
using System.Windows.Forms.DataVisualization.Charting;

namespace Skydat.forms2080
{
    public partial class frmMain12080 : Form
    {
        public frmMain12080()
        {
            InitializeComponent();
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);

           
        }

        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
           
                TabPage page = tabControl1.TabPages[e.Index];
                e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);  //
                Brush _textBrush;
                Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

                if (e.State == DrawItemState.Selected)
                {
                    _textBrush = new SolidBrush(Color.Red);
                }
                else
                {
                    _textBrush = new System.Drawing.SolidBrush(Color.Black);
                }

                Font _tabFont = new Font(this.Font.Name, (float)12, FontStyle.Regular, GraphicsUnit.Pixel);
                StringFormat _stringFlags = new StringFormat();
                _stringFlags.Alignment = StringAlignment.Center;
                _stringFlags.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(page.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        
        }

        //public Magnify Magnify1 = new Magnify();
        public frmOption12080 opForm;
        public frmanalyseGraph2080 grf ;
        public frmshowGraph2080 grf1 ;
        public frmselecttech2080 stf ;
        public frmtempfiles2080 ftf;
         public Color_params cpar = new Color_params();

        //public int OCP_Timer = 0;
        //public bool OCP_Ok = false;
        //public bool CP_Ok = false;
       
        
        public Color drawColor = new Color();
        
       // double E1_Input = 0, E2_Input = 0;
       
        public double ScanRate = 0;    

       
       // bool detect_Error = false;

      //  bool showMessageError = false;
        public short grfNum = 0, dfrmNum = 0;
      
        public int BRate = 1000000, RT = 3000, WT = 1000;
        public string portname = "COM2";
     
        bool Save_is_Cancel = false;
       
        public bool Detect_Peak = false;
      
        //public void changeform(int typepage) {
        //    switch (typepage) { 
        //        case 1:
        //            this.panel2.Visible = false;
        //            this.panel4.Visible = false;
        //            this.panel1.Visible = true;
        //            break;
        //        case 2:
        //             this.panel1.Visible = false;
        //             this.panel2.Visible = false;
        //             this.panel4.Visible = true;
        //            break;
        //        case 3:
        //            this.panel1.Visible = false;
        //            this.panel4.Visible = false;
        //            this.panel2.Visible = true;
                   
        //            break;
        //    }
        //}

        //public void Enable_Button(int Num_Ser)
        //{           
        //    this.grf.ShowHideGrid.Enabled = Num_Ser > 0;
        //    this.grf.ShowHideAxes.Enabled = Num_Ser > 0;
        //    this.grf.ZoomIn.Enabled = Num_Ser > 0;
        //    this.grf.ZoomOut.Enabled = Num_Ser > 0;
        //    this.grf.clearPlotsToolStripMenuItem.Enabled = Num_Ser > 0;
        //    this.grf.optionsToolStripMenuItem.Enabled = Num_Ser > 0;
        //    this.grf.dimensionToolStripMenuItem.Enabled = Num_Ser > 0;
        //}

        //private void portOptionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
          //   clasmpusbapi usb_fun = new clasmpusbapi();
       
          //if (usb_fun.OpenPipes() != 1)
          //  {
          //      MessageBox.Show("no usb", "Warning");

          //  }
          //  MessageBox.Show(usb_fun.getfwversion(), "Warning");
          //  frmPortOption2080 prtopt = new frmPortOption2080();

          //  bool showingform = true;
          //  if (sender is ToolStripMenuItem)
          //      if ((sender as ToolStripMenuItem) == portOptionToolStripMenuItem)
          //          showingform = true;
          //  if (showingform)
          //      prtopt.ShowDialog();

        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            panel3.Controls.Clear();
            panel4.Controls.Clear();
           

            opForm = new frmOption12080();

            stf = new frmselecttech2080();
            stf.mainf = this;
            stf.opForm = this.opForm;
            stf.Name = "stf" + grfNum.ToString();
            stf.MdiParent = this;
            stf.TopLevel = false;
            stf.MdiParent = this;
            stf.Parent = this.tabPage2;
            panel1.Controls.Add(stf);
            stf.Dock = DockStyle.Fill;
            stf.Show();

            grf1 = new frmshowGraph2080();
            grf1.mainf = this;
            grf1.opForm = this.opForm;
            grf1.Name = "grf" + grfNum.ToString();
            grf1.MdiParent = this;
            grf1.TopLevel = false;
            grf1.Parent = this.tabPage3;
            panel2.Controls.Add(grf1);
            grf1.Dock = DockStyle.Fill;
            grf1.Show();


            grf = new frmanalyseGraph2080();
            grf.mainf = this;
            grfNum = 0;
            grf.opForm = this.opForm;
            grf.Name = "grf" + grfNum.ToString();
            grf.MdiParent = this;
            grf.TopLevel = false;
            grf.MdiParent = this;
            grf.Parent = this.tabPage1;
            panel3.Controls.Add(grf);
            grf.Dock = DockStyle.Fill;
            grf.Show();

            ftf = new frmtempfiles2080();
            ftf.Name = "ftf" + grfNum.ToString();
            ftf.MdiParent = this;
            ftf.TopLevel = false;
            ftf.MdiParent = this;
            ftf.Parent = this.tabPage1;
            panel4.Controls.Add(ftf);
            ftf.Dock = DockStyle.Fill;
            ftf.Show();

         

            try
            {
                ReadFromFile("All", "");
                ReadFromFile("APP", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\app.prj");
                // toolStripStatusLabel1.Text = clasglobal.TechName[this.grf.tech];
                EventArgs e1 = new EventArgs();
            }

            catch { }
            this.grf.tChart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            this.grf.tChart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            //opForm.cmbStyleDraw.SelectedIndex = 0;
            drawColor = Color.Fuchsia;
           // Enable_Button(0);
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Visible)
            {
                DialogResult dlgResult = MessageBox.Show("Save Parameter Settings?", "Caution", MessageBoxButtons.YesNoCancel);
                if (dlgResult == DialogResult.Yes)
                {
                    classes2065.movedata.open_form = 0;

                    WriteToFile("All", "");
                    WriteToFile("APP", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\app.prj");
                    //if (rbSequence.Checked)
                    //WriteToFile("SEQ", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\SEQ.prj");
                    e.Cancel = false;
                }

                if (dlgResult == DialogResult.No)
                {
                    classes2065.movedata.open_form = 0;

                    WriteToFile("APP", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\app.prj");
                    e.Cancel = false;
                }

                if (dlgResult == DialogResult.Cancel)
                    e.Cancel = true;
            }
            grf1.Close();
        }

        
        //private void save_Data(string Rw, string vlt, string C1, string C2, string CS, bool isSave_All)
        //{
        //    sfd.AddExtension = true;
        //    sfd.DefaultExt = "dat";
        //    string dir_sfd = "";
        //    sfd.Filter = "Data Files(*.dat)|*.dat";
        //    int nn = this.grf.treeView3.Nodes[0].Nodes.Count;
        //    for (int j = 0; j < nn; j++)
        //    {
        //        if (this.grf.treeView3.Nodes[0].Nodes[j].Checked)
        //        {

        //            if (dir_sfd != "") sfd.InitialDirectory = dir_sfd;
        //            sfd.FileName = grf.treeView3.Nodes[0].Nodes[j].Text;
        //            if (isSave_All)
        //            {
        //                Save_is_Cancel = false;
        //                if (j < 1)
        //                    sfd.ShowDialog();
        //            }
        //            else
        //            {
        //                sfd.ShowDialog();
        //                dir_sfd = sfd.FileName;
                       
        //            }
        //             if (Save_is_Cancel) break;
        //            string dirstr = sfd.FileName;
        //            int i = dirstr.Length - 1;
        //            while (i > 0)
        //            {
        //                if (dirstr[i] == '\\')
        //                {
        //                    dirstr = dirstr.Substring(0, i + 1);
        //                    break;
        //                }
        //                else i--;
        //            }

        //            if (dirstr != "")
        //            {
        //                string d = sfd.FileName;
        //                if (d.Length > 4 && d.IndexOf('.') > 0)
        //                    d = d.Substring(0, d.IndexOf('.'));
                        
        //                string fName = d + ".dat";
        //                WriteToFile(clasglobal.TechName[this.grf.tech], d + ".dat");
        //                if (this.grf.tChart1.Series.Count <= 1)
        //                    fName = sfd.FileName;

        //                StreamWriter sa1 = new StreamWriter(fName, true);
        //                sa1.WriteLine("============================================================");
        //                string Ln = "";
        //                int r = this.grf.tChart1.Series[j].Points.Count;  
        //                for (i = 0; i < r; i++)
        //                {
        //                    Ln = this.grf.tChart1.Series[j].Points[i].XValue.ToString(); 
        //                    Ln = Ln + "\t" + this.grf.tChart1.Series[j].Points[i].YValues[0].ToString();
        //                     sa1.WriteLine(Ln);
        //                }
        //                sa1.Close();
                       
        //            }
        //        }
        //    }
        //}

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Save_is_Cancel = false;
        }

        //private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    save_Data("Row", "Volt", "Cur1", "Cur2", "CSum", false);
        //}

        //private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    save_Data("Row", "Volt", "Cur1", "Cur2", "CSum", true);
        //}

        //private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    save_Data("Row", "Volt", "Cur1", "Cur2", "CSum", false);
        //}
        public void WriteToFile(string tchName, string fileName)
        {
            string s = "";
            if (fileName != "")
                s = fileName;
            else
                s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Prms2080.prj";

            StreamWriter Fl = new StreamWriter(s, false, Encoding.ASCII);
            if (tchName != "APP" && tchName != "SEQ")
            {
                Fl.WriteLine("Settings for Parameters:\n");
                Fl.WriteLine("Date:" + DateTime.Today.ToString());


                if (tchName == "NPV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: NPV");
                    //Fl.WriteLine("TchNo: " + NPV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("Cycle:" + clasglobal.npvprms_ocp.Cycles.ToString());
                    Fl.WriteLine("E1:" + clasglobal.npvprms_ocp.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.npvprms_ocp.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.npvprms_ocp.HStep.ToString());
                    Fl.WriteLine("Pulse Width:" + clasglobal.npvprms_ocp.PulseWidth.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.npvprms_ocp.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.npvprms_ocp.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.npvprms_ocp.TStep.ToString());
                    Fl.WriteLine("vs_OCP:" + clasglobal.npvprms_ocp.vs_OCP.ToString());
                    Fl.WriteLine("OCPmeas:" + clasglobal.npvprms_ocp.OCPmeasurment.ToString());

                }

                if (tchName == "DPV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DPV");
                    //Fl.WriteLine("TchNo: " + DPV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("Cycle:" + clasglobal.dpvprms_ocp.Cycles.ToString());
                    Fl.WriteLine("E1:" + clasglobal.dpvprms_ocp.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.dpvprms_ocp.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.dpvprms_ocp.HStep.ToString());
                    Fl.WriteLine("Pulse Height:" + clasglobal.dpvprms_ocp.PulseHeight.ToString());
                    Fl.WriteLine("Pulse Width:" + clasglobal.dpvprms_ocp.PulseWidth.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.dpvprms_ocp.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.dpvprms_ocp.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.dpvprms_ocp.TStep.ToString());
                    Fl.WriteLine("vs_OCP:" + clasglobal.dpvprms_ocp.vs_OCP.ToString());
                    Fl.WriteLine("OCPmeas:" + clasglobal.dpvprms_ocp.OCPmeasurment.ToString());

                }

                if (tchName == "SWV" || tchName == "All")
                {

                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: SWV");
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("Cycle:" + clasglobal.swvprms_ocp.Cycles.ToString());
                    Fl.WriteLine("E1:" + clasglobal.swvprms_ocp.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.swvprms_ocp.E2.ToString());
                    Fl.WriteLine("Frequency:" + clasglobal.swvprms_ocp.Frequency.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.swvprms_ocp.HStep.ToString());
                    Fl.WriteLine("Pulse Height:" + clasglobal.swvprms_ocp.PulseHeight.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.swvprms_ocp.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.swvprms_ocp.ScanRate.ToString());
                    Fl.WriteLine("vs_OCP:" + clasglobal.swvprms_ocp.vs_OCP.ToString());
                    Fl.WriteLine("OCPmeas:" + clasglobal.swvprms_ocp.OCPmeasurment.ToString());

                }

                if (tchName == "CV" || tchName == "All")
                {

                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CV");
                    //Fl.WriteLine("TchNo: " + CV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("Cycle:" + clasglobal.cvprms_ocp.Cycles.ToString());
                    Fl.WriteLine("E1:" + clasglobal.cvprms_ocp.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.cvprms_ocp.E2.ToString());
                    Fl.WriteLine("E3:" + clasglobal.cvprms_ocp.E3.ToString());
                    Fl.WriteLine("Hold Time:0");
                    Fl.WriteLine("HStep:" + clasglobal.cvprms_ocp.HStep.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.cvprms_ocp.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.cvprms_ocp.ScanRate.ToString());
                    Fl.WriteLine("ScanRate_R:" + clasglobal.cvprms_ocp.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.cvprms_ocp.TStep.ToString());
                    Fl.WriteLine("vs_OCP:" + clasglobal.cvprms_ocp.vs_OCP.ToString());
                    Fl.WriteLine("OCPmeas:" + clasglobal.cvprms_ocp.OCPmeasurment.ToString());
                }

                if (tchName == "LSV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: LSV");
                    //Fl.WriteLine("TchNo: " + LSV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("Cycle:" + clasglobal.lsvprms_ocp.Cycles.ToString());
                    Fl.WriteLine("E1:" + clasglobal.lsvprms_ocp.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.lsvprms_ocp.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.lsvprms_ocp.HStep.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.lsvprms_ocp.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.lsvprms_ocp.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.lsvprms_ocp.TStep.ToString());
                    Fl.WriteLine("vs_OCP:" + clasglobal.lsvprms_ocp.vs_OCP.ToString());
                    Fl.WriteLine("OCPmeas:" + clasglobal.lsvprms_ocp.OCPmeasurment.ToString());

                }
                if (tchName == "OCP" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: OCP");
                    Fl.WriteLine("Time:" + clasglobal.ocpprms_ocp.Time.ToString());

                }

                if (tchName == "CPC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CPC");
                    //Fl.WriteLine("TchNo: " + CPC.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.cpcprms_ocp.E1.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.cpcprms_ocp.EquilibriumTime.ToString());
                    Fl.WriteLine("T1:" + clasglobal.cpcprms_ocp.T1.ToString());
                    Fl.WriteLine("vs_OCP:" + clasglobal.cpcprms_ocp.vs_OCP.ToString());
                    Fl.WriteLine("OCPmeas:" + clasglobal.cpcprms_ocp.OCPmeasurment.ToString());

                }



                if (tchName == "CHA" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CHA");
                    //Fl.WriteLine("TchNo: " + CA.ToString());
                    Fl.WriteLine("E1:" + clasglobal.chaprms_ocp.E1.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.chaprms_ocp.EquilibriumTime.ToString());
                    Fl.WriteLine("T1:" + clasglobal.chaprms_ocp.T1.ToString());
                    Fl.WriteLine("vs_OCP:" + clasglobal.chaprms_ocp.vs_OCP.ToString());
                    Fl.WriteLine("OCPmeas:" + clasglobal.chaprms_ocp.OCPmeasurment.ToString());

                }
            }
            if (tchName == "APP")
            {
                Fl.WriteLine("Port:" + portname);
                Fl.WriteLine("BRate:" + BRate.ToString());
                Fl.WriteLine("RT:" + RT.ToString());
                Fl.WriteLine("WT:" + WT.ToString());
                Fl.WriteLine("Technic:" + Convert.ToString(this.grf.tech)); //.ToString());
                //Fl.WriteLine("TchNo: " + Convert.ToString(this.grf.tech));
                Fl.WriteLine("DFormL:100");// + dfrm.Left.ToString());
                Fl.WriteLine("DFormT:100");// + dfrm.Top.ToString());
                Fl.WriteLine("DFormH:100");// + dfrm.Height.ToString());
                Fl.WriteLine("DFormW:100");// + dfrm.Width.ToString());
                Fl.WriteLine("GFormL:" + this.grf.Left.ToString());
                Fl.WriteLine("GFormT:" + this.grf.Top.ToString());
                Fl.WriteLine("GFormH:" + this.grf.Height.ToString());
                Fl.WriteLine("GFormW:" + this.grf.Width.ToString());
                Fl.WriteLine("FLine:" + opForm.rbfLine.Checked.ToString());
                Fl.WriteLine("Line:" + opForm.rbLine.Checked.ToString());
                Fl.WriteLine("Point:" + opForm.rbPoint.Checked.ToString());
            }
            Fl.Close();
        }
      
    
        public void ReadFromFile(string tchName, string fileName)
        {
            string s = "";
            if (fileName != "")
                s = fileName;
            else
                s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Prms2080.prj";
            try
            {
                StreamReader Fl = new StreamReader(s, Encoding.ASCII);
                s = "";
                if (tchName != "APP" && tchName != "SEQ")
                    while (!Fl.EndOfStream)
                    {
                        s = Fl.ReadLine();
                        if (s.LastIndexOf("Technic") >= 0)
                        {
                            if ((s.Substring(9) == "DCV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.DCV2080;
                                    s = Fl.ReadLine(); //clasglobal.dcvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                   
                                        if (s.Substring(7).Trim() == "False")
                                            clasglobal.dcvprms_ocp.vs_OCP = false;
                                        else
                                            clasglobal.dcvprms_ocp.vs_OCP = true;
                                    
                                    s = Fl.ReadLine(); clasglobal.dcvprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "NPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.NPV2080;
                                    s = Fl.ReadLine(); //clasglobal.npvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.PulseWidth = Convert.ToDouble(s.Substring(12));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.npvprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.npvprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.npvprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.DPV2080;
                                    s = Fl.ReadLine(); //clasglobal.dpvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.PulseWidth = Convert.ToDouble(s.Substring(12));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.dpvprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.dpvprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.dpvprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                    
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "SWV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.SWV2080;
                                    s = Fl.ReadLine(); //clasglobal.swvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.Frequency = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.swvprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.swvprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.swvprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                    
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CV;
                                    s = Fl.ReadLine(); //clasglobal.cvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                                       //    s = Fl.ReadLine(); clasglobal.cvprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.E3 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //clasglobal.cvprms_ocp.HoldTime = Convert.ToDouble(s.Substring(9));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); //clasglobal.cvprms_ocp.ScanRate_R = Convert.ToDouble(s.Substring(11));
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.cvprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.cvprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.cvprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                   
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "LSV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.LSV;
                                    s = Fl.ReadLine(); //clasglobal.lsvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.lsvprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.lsvprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.lsvprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                 
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DCS") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.DCs2080;
                                    s = Fl.ReadLine(); //clasglobal.dcsprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.dcsprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.dcsprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.dcsprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                 
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DPS") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.DPs2080;
                                    s = Fl.ReadLine(); //clasglobal.dpsprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.PulseWidth = Convert.ToDouble(s.Substring(12));//11
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.dpsprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.dpsprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.dpsprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "OCP") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.OCP2080;
                                    s = Fl.ReadLine(); clasglobal.ocpprms_ocp.Time = Convert.ToDouble(s.Substring(5));
                                   // s = Fl.ReadLine(); //clasglobal.ocpprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CPC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CPC2080;
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.cpcprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cpcprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cpcprms_ocp.T1 = Convert.ToDouble(s.Substring(3));
                                   // s = Fl.ReadLine(); //clasglobal.cpcprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.cpcprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.cpcprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.cpcprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                   
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CCC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CCC2080;
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.cccprms_ocp.I1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cccprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cccprms_ocp.T1 = Convert.ToDouble(s.Substring(3));
                         //           s = Fl.ReadLine(); //clasglobal.cccprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.cccprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.cccprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.cccprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                   
                                    s = "            ";
                                }
                                catch { }
                            //if ((s.Substring(9) == "CHP") && s.LastIndexOf("Technic") >= 0 && s != "")
                            //    try
                            //    {
                            //        this.grf.tech = clasglobal.CHP2080;
                            //        s = Fl.ReadLine(); clasglobal.chpprms.I1 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); clasglobal.chpprms.I2 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); clasglobal.chpprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                            //        s = Fl.ReadLine(); clasglobal.chpprms.T1 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); clasglobal.chpprms.T2 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                            //        s = "            ";
                            //    }
                            //    catch { }
                            if ((s.Substring(9) == "CHA") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CHA2080;
                                    s = Fl.ReadLine(); clasglobal.chaprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chaprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.chaprms_ocp.T1 = Convert.ToDouble(s.Substring(3));
                                  //  s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (s.Substring(7).Trim() == "False")
                                        clasglobal.chaprms_ocp.vs_OCP = false;
                                    else
                                        clasglobal.chaprms_ocp.vs_OCP = true;
                                    s = Fl.ReadLine(); clasglobal.chaprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));
                                    s = "            ";
                                }
                                catch { }
                           
                            //if ((s.Substring(9) == "CHC") && s.LastIndexOf("Technic") >= 0 && s != "")
                            //    try
                            //    {
                            //        this.grf.tech = clasglobal.CHC2080;
                            //        s = Fl.ReadLine(); clasglobal.chcprms.E1 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); clasglobal.chcprms.E2 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); clasglobal.chcprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                            //        s = Fl.ReadLine(); clasglobal.chcprms.T1 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); clasglobal.chcprms.T2 = Convert.ToDouble(s.Substring(3));
                            //        s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                            //        s = "            ";
                            //    }
                            //    catch { }
                        }
                    }
                if (tchName == "APP")
                {
                    s = Fl.ReadLine(); portname = s.Substring(5);
                    s = Fl.ReadLine(); BRate = Convert.ToInt32(s.Substring(6));
                    s = Fl.ReadLine(); RT = Convert.ToInt32(s.Substring(3));
                    s = Fl.ReadLine(); WT = Convert.ToInt32(s.Substring(3));
                    s = Fl.ReadLine(); this.grf.tech = Convert.ToByte(s.Substring(8));
                    s = Fl.ReadLine(); //dfrm.Left = Convert.ToInt32(s.Substring(7));
                    s = Fl.ReadLine(); //dfrm.Top = Convert.ToInt32(s.Substring(7));
                    s = Fl.ReadLine(); //dfrm.Height = Convert.ToInt32(s.Substring(7));
                    s = Fl.ReadLine(); //dfrm.Width = Convert.ToInt32(s.Substring(7));
                    s = Fl.ReadLine(); this.grf.Left = Convert.ToInt32(s.Substring(7));
                    s = Fl.ReadLine(); this.grf.Top = Convert.ToInt32(s.Substring(7));
                    s = Fl.ReadLine(); this.grf.Height = Convert.ToInt32(s.Substring(7));
                    s = Fl.ReadLine(); this.grf.Width = Convert.ToInt32(s.Substring(7));
                    //s = Fl.ReadLine(); rbNormal.Checked = Convert.ToBoolean(s.Substring(7));
                    //s = Fl.ReadLine(); rbMultiRun.Checked = Convert.ToBoolean(s.Substring(7));
                    //s = Fl.ReadLine(); rbSequence.Checked = Convert.ToBoolean(s.Substring(7));
                    s = Fl.ReadLine(); opForm.rbfLine.Checked = Convert.ToBoolean(s.Substring(6));
                    s = Fl.ReadLine(); opForm.rbLine.Checked = Convert.ToBoolean(s.Substring(5));
                    s = Fl.ReadLine(); opForm.rbPoint.Checked = Convert.ToBoolean(s.Substring(6));
                }
                if (tchName == "SEQ")
                {
                    int row = 0;
                    //grdTech.Rows.Insert(row, 1);
                    while (!Fl.EndOfStream)
                    {
                        s = Fl.ReadLine();
                        if (s.Trim() != "")
                        {
                            string GStr = "";
                            int col = 0;
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i] != ';') GStr = GStr + s[i];
                                else
                                {
                                    //grdTech.Rows[row].Cells[col].Value = GStr;
                                    col++;
                                    GStr = "";
                                }
                            }
                        }
                        if (!Fl.EndOfStream)
                        {
                            row++;
                            //grdTech.Rows.Insert(row, 1);
                        }
                    }
                }

                Fl.Close();
            }
            catch { }
        }
        private void calibrationCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClbCurve12080 clbform = new frmClbCurve12080();
            clbform.mf = this;
            //clbform.MdiParent = this;
            clbform.ShowDialog();
        }

        private void standardAdditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstdAddition2080 stdAdd = new frmstdAddition2080();
            //stdAdd.MdiParent = this;
            stdAdd.ShowDialog();
        }

        private void txtCP1_Leave(object sender, EventArgs e)
        {

        }

        //private void bitmapToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    ofd.AddExtension = true;
        //    ofd.DefaultExt = "bmp";
        //    ofd.Filter = "Bitmap Files(*.bmp)|*.bmp";
        //    ofd.ShowDialog();
        //    if (ofd.FileName != "")
        //    {
               
        //        //this.grf.tChart1.Import.Template.FileExtension = "bmp";
        //        //// grf.Set_Graph(0 , true, cbOverlay.Checked);
        //        //this.grf.tChart1.Import.Template.Load(ofd.FileName);
        //       // this.grf.tChart1.Serializer.Load(
        //    }
        //}

       

    
      
        private void toolStripStatusLabel5_TextChanged(object sender, EventArgs e)
        {
            if (toolStripStatusLabel5.Text == "Pause" || toolStripStatusLabel5.Text == "Stop")
                Setforecolor(Color.Red,1,1);
            if (toolStripStatusLabel5.Text == "Run")
                Setforecolor(Color.Green, 1,1);

        }

      

      

      
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text = e.KeyValue.ToString();
        }

       

        private void txtCP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.' && e.KeyChar != '-')
                e.KeyChar = (char)0;
        }

       
      
       
      
        //private void UdateDsFromParams(int TeqNo, int selectedRow, int typeact)
        //{
        //    if (typeact == 1)
        //    {
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["row"] = selectedRow.ToString();
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["techno"] = TeqNo.ToString();
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["CR"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["E3"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["FR"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["HT"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["I1"] = "0";
        //        dschart1.Tables["chartlist"].Rows[selectedRow]["I2"] = "0";
        //        switch (TeqNo)
        //        {
        //            case clasglobal.DCV2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dcvprms_ocp.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dcvprms_ocp.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dcvprms_ocp.HStep.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dcvprms_ocp.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dcvprms_ocp.ScanRate.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dcvprms_ocp.TStep.ToString();
        //                break;

        //            case clasglobal.NPV2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.npvprms_ocp.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.npvprms_ocp.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.npvprms_ocp.HStep.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.npvprms_ocp.PulseWidth.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.npvprms_ocp.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.npvprms_ocp.ScanRate.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.npvprms_ocp.TStep.ToString();
                     
        //                break;
        //            case clasglobal.DPV2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dpvprms_ocp.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dpvprms_ocp.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dpvprms_ocp.HStep.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.dpvprms_ocp.PulseHeight.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.dpvprms_ocp.PulseWidth.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dpvprms_ocp.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dpvprms_ocp.ScanRate.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dpvprms_ocp.TStep.ToString();
                    
        //                break;
        //            case clasglobal.SWV2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.swvprms_ocp.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.swvprms_ocp.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["FR"] = clasglobal.swvprms_ocp.Frequency.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.swvprms_ocp.HStep.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.swvprms_ocp.PulseHeight.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.swvprms_ocp.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.swvprms_ocp.ScanRate.ToString();
                     
        //                break;
        //            case clasglobal.CV2080:
        //            //    dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.cvprms_ocp.Cycles.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.cvprms_ocp.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.cvprms_ocp.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E3"] = clasglobal.cvprms_ocp.E3.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.cvprms_ocp.HStep.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cvprms_ocp.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.cvprms_ocp.ScanRate.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.cvprms_ocp.TStep.ToString();
                    
        //                break;
        //            case clasglobal.LSV2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.lsvprms_ocp.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.lsvprms_ocp.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.lsvprms_ocp.HStep.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.lsvprms_ocp.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.lsvprms_ocp.ScanRate.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.lsvprms_ocp.TStep.ToString();
                     
        //                break;
        //            case clasglobal.DCs2080:

        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dcsprms.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dcsprms.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dcsprms.HStep.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dcsprms.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dcsprms.ScanRate.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dcsprms.TStep.ToString();
                     
        //                break;
        //            case clasglobal.DPs2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dpsprms.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dpsprms.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.dpsprms.PulseHeight.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.dpsprms.PulseWidth.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dpsprms.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dpsprms.ScanRate.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dpsprms.TStep.ToString();
                      
        //                break;
        //            case clasglobal.OCP2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = "1";  // clasglobal.ocpprms.Cycles.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.ocpprms_ocp.Time.ToString();
        //               // dschart1.Tables["chartlist"].Rows[selectedRow]["Dly"] = clasglobal.ocpprms_ocp.Delay.ToString();

        //                break;
        //            case clasglobal.CPC2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.cpcprms.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cpcprms.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.cpcprms.T1.ToString();
                     
        //                break;
        //            case clasglobal.CCC2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["I1"] = clasglobal.cccprms.I1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cccprms.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.cccprms.T1.ToString();
                     
        //                break;
        //            case clasglobal.CHA2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.chaprms.E1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.chaprms.E2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.chaprms.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.chaprms.T1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = clasglobal.chaprms.T2.ToString();
                       
        //                break;
        //            case clasglobal.CHP2080:
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["I1"] = clasglobal.chpprms.I1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["I2"] = clasglobal.chpprms.I2.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.chpprms.EquilibriumTime.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.chpprms.T1.ToString();
        //                dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = clasglobal.chpprms.T2.ToString();
                      
        //                break;
        //            //case clasglobal.CHC2080:
        //            //    dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.chcprms.E1.ToString();
        //            //    dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.chcprms.E2.ToString();
        //            //    dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.chcprms.EquilibriumTime.ToString();
        //            //    dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.chcprms.T1.ToString();
        //            //    dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = clasglobal.chcprms.T2.ToString();
                    
        //            //    break;
        //            default: break;
        //        }
        //        dschart1.Tables["chartlist"].AcceptChanges();
        //    }
        //    else
        //    {


        //    }
        //}
        delegate void SetforecolorCallback(Color clr, int code, int state);
        private void Setforecolor(Color clr, int code, int state)
        {
            switch (code)
            {
                case 1:

                    if (state == 1)
                    {
                        SetforecolorCallback d = new SetforecolorCallback(Setforecolor);
                        this.Invoke(d, new object[] { clr, code, 2 });
                    }
                    else
                    {
                        this.toolStripStatusLabel5.ForeColor = clr;
                    }
                    break;
            }
        }
        delegate void SetenabledCallback(bool bl, int code,int state);
        private void Setbtnenabled(bool bl, int code,int state)
        {
            switch (code)
            {
                case 1:

                    if (state==1)
                    {
                        SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
                        this.Invoke(d, new object[] { bl, code, 2 });
                    }
                    else
                    {
                        //tsbRun.Enabled = false;
                        //tsbOpen.Enabled = false;
                        //tsbNew.Enabled = false;
                        //tsmOpen.Enabled = false;
                        //tsmNew.Enabled = false;
                        //runToolStripMenuItem.Enabled = false;
                        toolStripProgressBar1.Visible = true;
                    }
                    break;
                case 2:
                    if (state == 1)
                    {
                        SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
                        this.Invoke(d, new object[] { bl, code, 2 });
                    }
                    else
                    {
                        toolStripStatusLabel5.Text = "Stop";
                        //tsbOpen.Enabled = true;
                        //tsbNew.Enabled = true;
                        //tsmOpen.Enabled = true;
                        //tsmNew.Enabled = true;
                        toolStripProgressBar1.Visible = false;
                        //toolStrip3.Enabled = true;
                        //grdTech.Enabled = true;
                        //btnDeleteAll.Enabled = true;
                        //pausePlayToolStripMenuItem.Enabled = false;
                        //stopToolStripMenuItem.Enabled = false;
                        //runToolStripMenuItem.Enabled = true;
                        //setupToolStripMenuItem1.Enabled = true;
                        //runToolStripMenuItem.Enabled = true;
                       // tsbRun.Enabled = true;
                      
                       
                    }
                    break;
            }
        }
        delegate void SetvisibleCallback(bool bl, int code, int state);
        private void Setvisibleitem(bool bl, int code, int state)
        {
            switch (code)
            {
                case 1:

                    if (state == 1)
                    {
                        SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
                        this.Invoke(d, new object[] { bl, code, 2 });
                    }
                    else
                    {
                        //tsbRun.Enabled = false;
                        //tsbOpen.Enabled = false;
                        //tsbNew.Enabled = false;
                        //tsmOpen.Enabled = false;
                        //tsmNew.Enabled = false;
                       // runToolStripMenuItem.Enabled = false;
                        
                        toolStripProgressBar1.Visible = true;
                    }
                    break;
            }
        }
        delegate void SetcheckedCallback(bool bl, int code, int state);
        private void Setcheckeditem(bool bl, int code, int state)
        {
            switch (code)
            {
                case 1:

                    if (state == 1)
                    {
                        SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
                        this.Invoke(d, new object[] { bl, code, 2 });
                    }
                    else
                    {
                       // cbOverlay.Checked = bl;
                    }
                    break;
            }
        }
        delegate void SetgrfnodeCallback(string txt, int code, int state);
        private void Setgrfnodetext(string txt, int code, int state)
        {
            switch (code)
            {
                case 1:

                    if (state == 1)
                    {
                        SetgrfnodeCallback d = new SetgrfnodeCallback(Setgrfnodetext);
                        this.Invoke(d, new object[] { txt, code, 2 });
                    }
                    else
                    {
                        this.grf.treeView3.Nodes[0].Nodes.Add(txt);
                    }
                    break;
            }
        }
        delegate void SetgrfselectnodeCallback(int row, int code, int state);
        private void Setgrfnodeselect(int row, int code, int state)
        {
            switch (code)
            {
                case 1:

                    if (state == 1)
                    {
                        SetgrfselectnodeCallback d = new SetgrfselectnodeCallback(Setgrfnodeselect);
                        this.Invoke(d, new object[] { row, code, 2 });
                    }
                    else
                    {
                         this.grf.treeView3.SelectedNode=this.grf.treeView3.Nodes[0].LastNode;
                    }
                    break;
            }
        }
        public void WriteToFile(string tchName, string fileName, int row)
        {
            string s = "";
            if (fileName != "")
                s = fileName;
            else
                s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Prms2080.prj";

            StreamWriter Fl = new StreamWriter(s, false, Encoding.ASCII);
            if (tchName == "APP")
            {
                Fl.WriteLine("Port:" + portname);
                Fl.WriteLine("BRate:" + BRate.ToString());
                Fl.WriteLine("RT:" + RT.ToString());
                Fl.WriteLine("WT:" + WT.ToString());
                Fl.WriteLine("Technic:" + Convert.ToString(this.grf.tech)); //.ToString());
                //Fl.WriteLine("TchNo: " + Convert.ToString(this.grf.tech));
                Fl.WriteLine("DFormL:100");// + dfrm.Left.ToString());
                Fl.WriteLine("DFormT:100");// + dfrm.Top.ToString());
                Fl.WriteLine("DFormH:100");// + dfrm.Height.ToString());
                Fl.WriteLine("DFormW:100");// + dfrm.Width.ToString());
                Fl.WriteLine("GFormL:" + this.grf.Left.ToString());
                Fl.WriteLine("GFormT:" + this.grf.Top.ToString());
                Fl.WriteLine("GFormH:" + this.grf.Height.ToString());
                Fl.WriteLine("GFormW:" + this.grf.Width.ToString());
                //Fl.WriteLine("Normal:" + rbNormal.Checked.ToString());
                //Fl.WriteLine("MulRun:" + rbMultiRun.Checked.ToString());
                //Fl.WriteLine("SeqRun:" + rbSequence.Checked.ToString());
                Fl.WriteLine("FLine:" + opForm.rbfLine.Checked.ToString());
                Fl.WriteLine("Line:" + opForm.rbLine.Checked.ToString());
                Fl.WriteLine("Point:" + opForm.rbPoint.Checked.ToString());
            }
            Fl.Close();
        }

      

      

      
      

      
     }
}