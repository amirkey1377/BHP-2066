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

namespace Skydat.forms2065
{
    public partial class frmMain12065 : Form
    {
        public frmMain12065()
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

       
        public string Version_No = "Version 1.1.15";
      
       
        //public Magnify Magnify1 = new Magnify();
        public frmOption12065 opForm = new frmOption12065();
        public frmanalyseGraph2065 grf = new frmanalyseGraph2065();
        public frmshowGraph2065 grf1 = new frmshowGraph2065();
        public frmselecttech2065 stf ;
        public frmtempfiles ftf;
        public frmofflinestate osf;

         public Color_params cpar = new Color_params();

        //public int OCP_Timer = 0;
        //public bool OCP_Ok = false;
        //public bool CP_Ok = false;
       
        
        public Color drawColor = new Color();
        
       // double E1_Input = 0, E2_Input = 0;
       
        public double ScanRate = 0;    

       
        //bool detect_Error = false;

        //bool showMessageError = false;
        public short grfNum = 0, dfrmNum = 0;
      
        public int BRate = 1000000, RT = 3000, WT = 1000;
        public string portname = "COM2";
     
        bool Save_is_Cancel = false;
       
        public bool Detect_Peak = false;
      
      

        public void Enable_Button(int Num_Ser)
        {         
           
            this.grf.clearPlotsToolStripMenuItem.Enabled = Num_Ser > 0;
            this.grf.optionsToolStripMenuItem.Enabled = Num_Ser > 0;
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            panel3.Controls.Clear();
            panel4.Controls.Clear();
            panel5.Controls.Clear();

            //درون هر صفحه تب، یک پنل قرار دارد که فرم های مربوط به هر قسمت کاربرنامه به صفحه ها افزوده میشود
            stf = new frmselecttech2065();
            stf.mainf = this;
            stf.opForm = this.opForm;
            stf.Name = "stf" + grfNum.ToString();
            stf.MdiParent = this;
            stf.TopLevel = false;
            panel1.Controls.Add(stf);
            stf.Dock = DockStyle.Fill;
            stf.Show();

            grf1 = new frmshowGraph2065();
            grf1.mainf = this;
            grf1.opForm = this.opForm;
            grf1.Name = "grf" + grfNum.ToString();
            grf1.MdiParent = this;
            grf1.TopLevel = false;
            grf1.Parent = this.tabPage3;
            panel2.Controls.Add(this.grf1);
            grf1.Dock = DockStyle.Fill;
            grf1.Show();


            grf = new frmanalyseGraph2065();
            grf.mainf = this;
            grfNum = 0;
            grf.opForm = this.opForm;
            grf.Name = "grf" + grfNum.ToString();
            grf.MdiParent = this;
            grf.TopLevel = false;
            grf.MdiParent = this;
            grf.Parent = this.tabPage1;
            panel3.Controls.Add(this.grf);
            grf.Dock = DockStyle.Fill;
            grf.Show();

            ftf = new frmtempfiles();
            ftf.Name = "ftf" + grfNum.ToString();
            ftf.MdiParent = this;
            ftf.TopLevel = false;
            ftf.MdiParent = this;
            ftf.Parent = this.tabPage2;
            panel4.Controls.Add(this.ftf);
            ftf.Dock = DockStyle.Fill;
            ftf.Show();

            osf = new frmofflinestate();
            osf.Name = "osf" + grfNum.ToString();
            osf.MdiParent = this;
            osf.TopLevel = false;
            osf.MdiParent = this;
            osf.Parent = this.tabPage5;
            panel5.Controls.Add(this.osf);
            osf.Dock = DockStyle.Fill;
            osf.Show();

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
            opForm.cmbStyleDraw.SelectedIndex = 0;
            drawColor = Color.Fuchsia;
            Enable_Button(0);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (Visible)//ذخیره کردن پارامترها هنگام بستن پنجره
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

       
        public void WriteToFile(string tchName, string fileName)//تابع ذخیره پارامترها درون فایل پروژه
        {
            string s = "";
            if (fileName != "")
                s = fileName;
            else
                s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Prms.prj";

            StreamWriter Fl = new StreamWriter(s, false, Encoding.ASCII);
            if (tchName != "APP" && tchName != "SEQ")
            {
                Fl.WriteLine("Settings for Parameters:\n");
                Fl.WriteLine("Date:" + DateTime.Today.ToString());
                if (tchName == "DCV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DCV");
                    //Fl.WriteLine("TchNo: "+ DCV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.dcvprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.dcvprms.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.dcvprms.HStep.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.dcvprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.dcvprms.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.dcvprms.TStep.ToString());

                }

                if (tchName == "NPV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: NPV");
                    //Fl.WriteLine("TchNo: " + NPV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.npvprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.npvprms.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.npvprms.HStep.ToString());
                    Fl.WriteLine("Pulse Width:" + clasglobal.npvprms.PulseWidth.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.npvprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.npvprms.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.npvprms.TStep.ToString());
                }

                if (tchName == "DPV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DPV");
                    //Fl.WriteLine("TchNo: " + DPV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.dpvprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.dpvprms.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.dpvprms.HStep.ToString());
                    Fl.WriteLine("Pulse Height:" + clasglobal.dpvprms.PulseHeight.ToString());
                    Fl.WriteLine("Pulse Width:" + clasglobal.dpvprms.PulseWidth.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.dpvprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.dpvprms.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.dpvprms.TStep.ToString());
                }

                if (tchName == "SWV" || tchName == "All")
                {

                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: SWV");
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.swvprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.swvprms.E2.ToString());
                    Fl.WriteLine("Frequency:" + clasglobal.swvprms.Frequency.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.swvprms.HStep.ToString());
                    Fl.WriteLine("Pulse Height:" + clasglobal.swvprms.PulseHeight.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.swvprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.swvprms.ScanRate.ToString());

                }

                if (tchName == "CV" || tchName == "All")
                {

                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CV");
                    //Fl.WriteLine("TchNo: " + CV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("Cycles:" + clasglobal.cvprms.Cycles.ToString());
                    Fl.WriteLine("E1:" + clasglobal.cvprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.cvprms.E2.ToString());
                    Fl.WriteLine("E3:" + clasglobal.cvprms.E3.ToString());
                    Fl.WriteLine("Hold Time:0");
                    Fl.WriteLine("HStep:" + clasglobal.cvprms.HStep.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.cvprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.cvprms.ScanRate.ToString());
                    Fl.WriteLine("ScanRate_R:" + clasglobal.cvprms.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.cvprms.TStep.ToString());
                }

                if (tchName == "LSV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: LSV");
                    //Fl.WriteLine("TchNo: " + LSV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.lsvprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.lsvprms.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.lsvprms.HStep.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.lsvprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.lsvprms.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.lsvprms.TStep.ToString());
                }

                if (tchName == "DCS" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DCS");
                    //Fl.WriteLine("TchNo: " + DCs.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.dcsprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.dcsprms.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.dcsprms.HStep.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.dcsprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.dcsprms.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.dcsprms.TStep.ToString());
                }

                if (tchName == "DPS" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DPS");
                    //Fl.WriteLine("TchNo: " + DPs.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.dpsprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.dpsprms.E2.ToString());
                    Fl.WriteLine("HStep:" + clasglobal.dpsprms.HStep.ToString());
                    Fl.WriteLine("Pulse Height:" + clasglobal.dpsprms.PulseHeight.ToString());
                    Fl.WriteLine("Pulse Width:" + clasglobal.dpsprms.PulseWidth.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.dpsprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Scan Rate:" + clasglobal.dpsprms.ScanRate.ToString());
                    Fl.WriteLine("TStep:" + clasglobal.dpsprms.TStep.ToString());
                }
                //if (tchName == "OCP" || tchName == "All")
                //{
                //    Fl.WriteLine("\n");
                //    Fl.WriteLine("Technic: OCP");
                //    //Fl.WriteLine("TchNo: " + OCP.ToString());
                //    Fl.WriteLine("Time:" + clasglobal.ocpprms.Time.ToString());
                //    Fl.WriteLine("Cycles:" + "1"); //clasglobal.ocpprms.Cycles.ToString());
                //}

                if (tchName == "CPC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CPC");
                    //Fl.WriteLine("TchNo: " + CPC.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.cpcprms.E1.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.cpcprms.EquilibriumTime.ToString());
                    Fl.WriteLine("T1:" + clasglobal.cpcprms.T1.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //clasglobal.cpcprms.Cycles.ToString());
                }

                if (tchName == "CCC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CCC");
                    //Fl.WriteLine("TchNo: " + CCC.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + clasglobal.cccprms.I1.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.cccprms.EquilibriumTime.ToString());
                    Fl.WriteLine("T1:" + clasglobal.cccprms.T1.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //clasglobal.cpcprms.Cycles.ToString());
                }
                if (tchName == "CHP" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CHP");
                    //Fl.WriteLine("TchNo: " + CA.ToString());
                    Fl.WriteLine("I1:" + clasglobal.chpprms.I1.ToString());
                    Fl.WriteLine("I2:" + clasglobal.chpprms.I2.ToString());
                    Fl.WriteLine("EUP:" + clasglobal.chpprms.EUP.ToString());
                    Fl.WriteLine("EDO:" + clasglobal.chpprms.EDO.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.chpprms.EquilibriumTime.ToString());
                    Fl.WriteLine("T1:" + clasglobal.chpprms.T1.ToString());
                    Fl.WriteLine("T2:" + clasglobal.chpprms.T2.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                }
                if (tchName == "CHA" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CHA");
                    //Fl.WriteLine("TchNo: " + CA.ToString());
                    Fl.WriteLine("E1:" + clasglobal.chaprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.chaprms.E2.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.chaprms.EquilibriumTime.ToString());
                    Fl.WriteLine("T1:" + clasglobal.chaprms.T1.ToString());
                    Fl.WriteLine("T2:" + clasglobal.chaprms.T2.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                }
                if (tchName == "CHC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CHC");
                    //Fl.WriteLine("TchNo: " + CA.ToString());
                    Fl.WriteLine("E1:" + clasglobal.chcprms.E1.ToString());
                    Fl.WriteLine("E2:" + clasglobal.chcprms.E2.ToString());
                    Fl.WriteLine("Equilibrium Time:" + clasglobal.chcprms.EquilibriumTime.ToString());
                    Fl.WriteLine("T1:" + clasglobal.chcprms.T1.ToString());
                    Fl.WriteLine("T2:" + clasglobal.chcprms.T2.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                }
                /*
                if (tchName == "SC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: SC");
                    //Fl.WriteLine("TchNo: " + SC.ToString());
                    Fl.WriteLine("E1:" + scprms.E1.ToString());
                    Fl.WriteLine("Q1:" + scprms.Q1.ToString());
                    Fl.WriteLine("E2:" + scprms.E2.ToString());
                    Fl.WriteLine("Q2:" + scprms.Q2.ToString());
                    Fl.WriteLine("Equilibrium Time:" + scprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Cycles:" + scprms.Cycles.ToString());
                    Fl.WriteLine("OCPMeas.:" + scprms.OCPmeasurment.ToString());
                    Fl.WriteLine("vs_OCP:" + (Convert.ToByte(scprms.vs_OCP)).ToString());
                }
                 * */
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
                //Fl.WriteLine("Normal:" + rbNormal.Checked.ToString());
                //Fl.WriteLine("MulRun:" + rbMultiRun.Checked.ToString());
                //Fl.WriteLine("SeqRun:" + rbSequence.Checked.ToString());
                Fl.WriteLine("FLine:" + opForm.rbfLine.Checked.ToString());
                Fl.WriteLine("Line:" + opForm.rbLine.Checked.ToString());
                Fl.WriteLine("Point:" + opForm.rbPoint.Checked.ToString());
            }
           //if (tchName == "SEQ")
                //if (grdTech.RowCount > 1)
                //    for (int i = 0; i < grdTech.RowCount - 1; i++)
                //    {
                //        string Gstr = "";
                //        for (int k = 0; k <21 ; k++)/////grdTech.ColumnCount
                //            Gstr = Gstr + grdTech.Rows[i].Cells[k].Value.ToString() + ";";
                //        Fl.WriteLine(Gstr);
                //    }
                Fl.Close();
            
        }

    
        public void ReadFromFile(string tchName, string fileName)//فراخوانی اطلاعات تکنیک ها و پارامترها از فایل پروژه
        {
            string s = "";
            if (fileName != "")
                s = fileName;
            else
                s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Prms.prj";
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
                                    this.grf.tech = clasglobal.DCV;
                                    s = Fl.ReadLine(); //clasglobal.dcvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.TStep = Convert.ToDouble(s.Substring(6));
                                     s = Fl.ReadLine();
                                     if (s.Trim().Length >= 7)
                                     {
                                         if (s.Trim().Substring(0, 7) == "comment")
                                         {
                                             clasglobal.dcvprms.comment1 = s.Substring(8);
                                         }
                                         else
                                             clasglobal.dcvprms.comment1 = "";

                                     }
                                     else
                                     {
                                         clasglobal.dcvprms.comment1 = "";
                                     }
                                    s="            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "NPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.NPV;
                                    s = Fl.ReadLine(); //clasglobal.npvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.npvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.npvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.npvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.npvprms.PulseWidth = Convert.ToDouble(s.Substring(12));
                                    s = Fl.ReadLine(); clasglobal.npvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.npvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.npvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.npvprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.npvprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.npvprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.DPV;
                                    s = Fl.ReadLine(); //clasglobal.dpvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.PulseWidth = Convert.ToDouble(s.Substring(12));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.dpvprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.dpvprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.dpvprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "SWV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.SWV;
                                    s = Fl.ReadLine(); //clasglobal.swvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.swvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.swvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.swvprms.Frequency = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.swvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.swvprms.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.swvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.swvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.swvprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.swvprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.swvprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CV;
                                    s = Fl.ReadLine(); //clasglobal.cvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.cvprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.cvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cvprms.E3 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //clasglobal.cvprms.HoldTime = Convert.ToDouble(s.Substring(9));
                                    s = Fl.ReadLine(); clasglobal.cvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.cvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); //clasglobal.cvprms.ScanRate_R = Convert.ToDouble(s.Substring(11));
                                    s = Fl.ReadLine(); clasglobal.cvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.cvprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.cvprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.cvprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "LSV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.LSV;
                                    s = Fl.ReadLine(); //clasglobal.lsvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.lsvprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.lsvprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.lsvprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DCS") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.DCs;
                                    s = Fl.ReadLine(); //clasglobal.dcsprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.dcsprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.dcsprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.dcsprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DPS") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.DPs;
                                    s = Fl.ReadLine(); //clasglobal.dpsprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.PulseWidth = Convert.ToDouble(s.Substring(12));//11
                                    s = Fl.ReadLine(); clasglobal.dpsprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.dpsprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.dpsprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.dpsprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            //if ((s.Substring(9) == "OCP") && s.LastIndexOf("Technic") >= 0 && s != "")
                            //    try
                            //    {
                            //        this.grf.tech = clasglobal.OCP;
                            //        s = Fl.ReadLine(); clasglobal.ocpprms.Time = Convert.ToDouble(s.Substring(5));
                            //        s = Fl.ReadLine(); //clasglobal.ocpprms.Cycles = Convert.ToDouble(s.Substring(7));
                            //        s = "            ";
                            //    }
                            //    catch { }
                            if ((s.Substring(9) == "CPC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CPC;
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.cpcprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cpcprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cpcprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.cpcprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.cpcprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.cpcprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CCC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CCC;
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.cccprms.I1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cccprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cccprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //clasglobal.cccprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.cccprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.cccprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.cccprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CHP") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CHP;
                                    s = Fl.ReadLine(); clasglobal.chpprms.I1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.I2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.EUP = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.EDO = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.chpprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.T2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.chpprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.chpprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.chpprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CHA") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CA;
                                    s = Fl.ReadLine(); clasglobal.chaprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chaprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chaprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.chaprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chaprms.T2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (s.Trim().Length >= 7)
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.chaprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.chaprms.comment1 = "";

                                    }
                                    else
                                    {
                                        clasglobal.chaprms.comment1 = "";
                                    }
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CHC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.grf.tech = clasglobal.CHC;
                                    s = Fl.ReadLine(); clasglobal.chcprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chcprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chcprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.chcprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chcprms.T2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine();
                                    if (( s is DBNull) || s.Trim().Length < 7)
                                    {
                                        clasglobal.chcprms.comment1 = "";
                                       
                                    }
                                    else
                                    {
                                        if (s.Trim().Substring(0, 7) == "comment")
                                        {
                                            clasglobal.chcprms.comment1 = s.Substring(8);
                                        }
                                        else
                                            clasglobal.chcprms.comment1 = "";
 
                                    }
                                    s = "            ";
                                }
                                catch { }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtCP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.')
                e.KeyChar = (char)0;
        }

       
      
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
                        this.toolStripStatusLabel5.ForeColor = clr;//تغییر رنگ متن وضعیت نسبت به عملیات
                    }
                    break;
            }
        }
        //delegate void SetenabledCallback(bool bl, int code,int state);
        //private void Setbtnenabled(bool bl, int code,int state)// نمایش تغییر وضعیت و نمایش یا عدم نمایش پروسس بار
        //{
        //    switch (code)
        //    {
        //        case 1:

        //            if (state==1)
        //            {
        //                SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
        //                this.Invoke(d, new object[] { bl, code, 2 });
        //            }
        //            else
        //            {
        //                toolStripProgressBar1.Visible = true;
        //            }
        //            break;
        //        case 2:
        //            if (state == 1)
        //            {
        //                SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
        //                this.Invoke(d, new object[] { bl, code, 2 });
        //            }
        //            else
        //            {
        //                toolStripStatusLabel5.Text = "Stop";
                       
        //                toolStripProgressBar1.Visible = false;
                       
        //            }
        //            break;
        //    }
        //}
        //delegate void SetvisibleCallback(bool bl, int code, int state);
        //private void Setvisibleitem(bool bl, int code, int state)
        //{
        //    switch (code)
        //    {
        //        case 1:

        //            if (state == 1)
        //            {
        //                SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
        //                this.Invoke(d, new object[] { bl, code, 2 });
        //            }
        //            else
        //            {
                       
        //                toolStripProgressBar1.Visible = true;
        //            }
        //            break;
        //    }
        //}
        //delegate void SetcheckedCallback(bool bl, int code, int state);
        //private void Setcheckeditem(bool bl, int code, int state)
        //{
        //    switch (code)
        //    {
        //        case 1:

        //            if (state == 1)
        //            {
        //                SetenabledCallback d = new SetenabledCallback(Setbtnenabled);
        //                this.Invoke(d, new object[] { bl, code, 2 });
        //            }
        //            else
        //            {
        //               // cbOverlay.Checked = bl;
        //            }
        //            break;
        //    }
        //}
        //delegate void SetgrfnodeCallback(string txt, int code, int state);
        //private void Setgrfnodetext(string txt, int code, int state)
        //{
        //    switch (code)
        //    {
        //        case 1:

        //            if (state == 1)
        //            {
        //                SetgrfnodeCallback d = new SetgrfnodeCallback(Setgrfnodetext);
        //                this.Invoke(d, new object[] { txt, code, 2 });
        //            }
        //            else
        //            {
        //                this.grf.treeView3.Nodes[0].Nodes.Add(txt);
        //            }
        //            break;
        //    }
        //}

        //delegate void SetgrfselectnodeCallback(int row, int code, int state);
        //private void Setgrfnodeselect(int row, int code, int state)
        //{
        //    switch (code)
        //    {
        //        case 1:

        //            if (state == 1)
        //            {
        //                SetgrfselectnodeCallback d = new SetgrfselectnodeCallback(Setgrfnodeselect);
        //                this.Invoke(d, new object[] { row, code, 2 });
        //            }
        //            else
        //            {
        //                 this.grf.treeView3.SelectedNode=this.grf.treeView3.Nodes[0].LastNode;
        //            }
        //            break;
        //    }
        //}
        //public void WriteToFile(string tchName, string fileName, int row)
        //{
        //    string s = "";
        //    if (fileName != "")
        //        s = fileName;
        //    else
        //        s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Prms.prj";

        //    StreamWriter Fl = new StreamWriter(s, false, Encoding.ASCII);
        //    if (tchName == "APP")
        //    {
        //        Fl.WriteLine("Port:" + portname);
        //        Fl.WriteLine("BRate:" + BRate.ToString());
        //        Fl.WriteLine("RT:" + RT.ToString());
        //        Fl.WriteLine("WT:" + WT.ToString());
        //        Fl.WriteLine("Technic:" + Convert.ToString(this.grf.tech)); //.ToString());
        //        //Fl.WriteLine("TchNo: " + Convert.ToString(this.grf.tech));
        //        Fl.WriteLine("DFormL:100");// + dfrm.Left.ToString());
        //        Fl.WriteLine("DFormT:100");// + dfrm.Top.ToString());
        //        Fl.WriteLine("DFormH:100");// + dfrm.Height.ToString());
        //        Fl.WriteLine("DFormW:100");// + dfrm.Width.ToString());
        //        Fl.WriteLine("GFormL:" + this.grf.Left.ToString());
        //        Fl.WriteLine("GFormT:" + this.grf.Top.ToString());
        //        Fl.WriteLine("GFormH:" + this.grf.Height.ToString());
        //        Fl.WriteLine("GFormW:" + this.grf.Width.ToString());
        //        //Fl.WriteLine("Normal:" + rbNormal.Checked.ToString());
        //        //Fl.WriteLine("MulRun:" + rbMultiRun.Checked.ToString());
        //        //Fl.WriteLine("SeqRun:" + rbSequence.Checked.ToString());
        //        Fl.WriteLine("FLine:" + opForm.rbfLine.Checked.ToString());
        //        Fl.WriteLine("Line:" + opForm.rbLine.Checked.ToString());
        //        Fl.WriteLine("Point:" + opForm.rbPoint.Checked.ToString());
        //    }
        //    Fl.Close();
        //}
     }
}