using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class frmselecttech2065 : Form
    {
        public frmselecttech2065()
        {
            InitializeComponent();
            treeView1.ExpandAll();
           
        }
        private int rowsel = -1;
        private byte Range;
   
        private classes2065.clasgraph gc = new classes2065.clasgraph();
        private bool  showMessageError = false;
        //public string portname = "COM2";
        public frmMain12065 mainf;
        public frmOption12065 opForm = new frmOption12065();
        //public int BRate = 1000000, RT = 3000, WT = 1000;   

   
        
        public void clearform() {
            this.treeView1.SelectedNode = this.treeView1.Nodes[0].FirstNode;
            this.treeView2.Nodes[0].Nodes.Clear();
            this.pGrid.SelectedObject = null;
            this.checkCPsCTs.Checked = false;
            this.radioButton1.Checked = true;
            this.dschart1.chartlist.Clear();

        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            clearform();
         
        }
        private void showError(string err, string val1, string val2)
        {
            if (showMessageError)
                MessageBox.Show("Invalid Value of '" + err + "'" + (char)13 + "Value Must between '" + val1 + "' And '" + val2 + "'", "Parameter error");
          
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            pGrid2.SelectedObject = clasglobal.cpctprms;
          
            treeView1.SelectedNode = treeView1.Nodes[0].FirstNode;
            checkCPsCTs.Checked = false;
            pGrid2.Enabled = false;

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        unsafe void runToolStripMenuItem_Click(object sender, EventArgs e)
        {    
             if (this.dschart1.chartlist.Rows.Count == 0) {
                MessageBox.Show("please select technique(s) for running!!!");
                return;
            }
             DataRow dr;
             this.runToolStripMenuItem1.Enabled = false;
             this.tsbRun.Enabled = false;

             this.mainf.grf1.dschart1.chartlist1_run.Clear();
           
            for (int nan = 0; nan < this.dschart1.chartlist.Rows.Count; nan++)
            {
                dr = this.mainf.grf1.dschart1.chartlist1_run.NewRow();
                for (int ana = 0; ana < this.dschart1.chartlist.Columns.Count; ana++)
                    dr[ana] = this.dschart1.chartlist.Rows[nan][ana];

                this.mainf.grf1.dschart1.chartlist1_run.Rows.Add(dr);//انتقال اطلاعات تکنیک انتخاب شده به دیتا ست فرم اجرای گراف
            }
            
            mainf.toolStripStatusLabel5.Text = "";
            mainf.tsocpTime.Text = " ";
            mainf.toolStripStatusLabel3.Text = " ";
            mainf.tabControl1.SelectedTab = mainf.tabControl1.TabPages[1];// نمایان کردن فرم اجرا

            // فراخوانی اجرای تکنیک در فرم اجرا
            if (checkCPsCTs.Checked)
            {
                this.mainf.grf1.runf(Range, clasglobal.cpctprms.CP1, clasglobal.cpctprms.CT1, clasglobal.cpctprms.CP2, clasglobal.cpctprms.CT2, clasglobal.cpctprms.CP3, clasglobal.cpctprms.CT3);
            }
            else 
            {
                this.mainf.grf1.runf(Range, 0,0,0,0,0,0);
               
            }
        }
       
      
        private void pGrid_Enter(object sender, EventArgs e)
        {
            gc.Axesx = this.mainf.grf.tChart1.ChartAreas[0].AxisX;
            gc.Axesy = this.mainf.grf.tChart1.ChartAreas[0].AxisY;
            gc.Dock = this.mainf.grf.tChart1.Dock;
            gc.BackGroundImageLayout = this.mainf.grf.tChart1.BackgroundImageLayout;
            gc.BackGroundImageLayout = this.mainf.grf.tChart1.BackgroundImageLayout;
            gc.Series = this.mainf.grf.tChart1.Series;
        }
       
        public void ReadFromFile_project(string fileName)
        {
            
            try
            {
                int num_row = 0;
                if (treeView2.Nodes[0].Nodes.Count > 0)
                {
                DialogResult dd=MessageBox.Show("There are techniques for running .Do you want to add techniques to them ?" + Environment.NewLine + "Yes : running all techniques" + Environment.NewLine + "No : running techniques that are in selected file " + Environment.NewLine + "Cancel : running existing techniques", "", MessageBoxButtons.YesNoCancel);

                
                    if ( dd==DialogResult.Yes) 
                    {
                        num_row = treeView2.Nodes[0].Nodes.Count ;
                    }
                    else if(dd==DialogResult.No)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0].FirstNode;
                        this.treeView2.Nodes[0].Nodes.Clear();
                        this.pGrid.SelectedObject = null;
                     //   this.propertyGrid2.SelectedObject = gc;
                        this.dschart1.chartlist.Clear();
                    }
                
                }

                string s = "";
                bool flag1 = false;
                StreamReader Fl = new StreamReader(fileName, Encoding.ASCII);
                while (!Fl.EndOfStream)
                {
                        s = Fl.ReadLine();
                        if (s.LastIndexOf("Technic") >= 0)
                        {
                            if ((s.Substring(9) == "DCV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.DCV;
                                    s = Fl.ReadLine(); //clasglobal.dcvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dcvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = "            ";
                                   
                                }
                                catch { }
                            if ((s.Substring(9) == "NPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.NPV;
                                    s = Fl.ReadLine(); //clasglobal.npvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.npvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.npvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.npvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.npvprms.PulseWidth = Convert.ToDouble(s.Substring(12));
                                    s = Fl.ReadLine(); clasglobal.npvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.npvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.npvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = "            ";
                                    
                                }
                                catch { }
                            if ((s.Substring(9) == "DPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.DPV;
                                    s = Fl.ReadLine(); //clasglobal.dpvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.PulseWidth = Convert.ToDouble(s.Substring(12));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dpvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "SWV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.SWV;
                                    s = Fl.ReadLine(); //clasglobal.swvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.swvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.swvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.swvprms.Frequency = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.swvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.swvprms.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.swvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.swvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.CV;
                                    s = Fl.ReadLine(); //clasglobal.cvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); //clasglobal.cvprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = Fl.ReadLine(); clasglobal.cvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cvprms.E3 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //clasglobal.cvprms.HoldTime = Convert.ToDouble(s.Substring(9));
                                    s = Fl.ReadLine(); clasglobal.cvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.cvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); //clasglobal.cvprms.ScanRate_R = Convert.ToDouble(s.Substring(11));
                                    s = Fl.ReadLine(); clasglobal.cvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "LSV") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.LSV;
                                    s = Fl.ReadLine(); //clasglobal.lsvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.lsvprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DCS") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.DCs;
                                    s = Fl.ReadLine(); //clasglobal.dcsprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dcsprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "DPS") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.DPs;
                                    s = Fl.ReadLine(); //clasglobal.dpsprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.HStep = Convert.ToDouble(s.Substring(6));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.PulseHeight = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.PulseWidth = Convert.ToDouble(s.Substring(12));//11
                                    s = Fl.ReadLine(); clasglobal.dpsprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.ScanRate = Convert.ToDouble(s.Substring(10));
                                    s = Fl.ReadLine(); clasglobal.dpsprms.TStep = Convert.ToDouble(s.Substring(6));
                                    s = "            ";
                                }
                                catch { }
                          
                            if ((s.Substring(9) == "CPC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.CPC;
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.cpcprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cpcprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cpcprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CCC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.CCC;
                                    s = Fl.ReadLine(); //clasglobal.cpcprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                                    s = Fl.ReadLine(); clasglobal.cccprms.I1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.cccprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.cccprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //clasglobal.cccprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CHP") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.CHP;
                                    s = Fl.ReadLine(); clasglobal.chpprms.I1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.I2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.EUP = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.EDO = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.chpprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chpprms.T2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CHA") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.CHA;
                                    s = Fl.ReadLine(); clasglobal.chaprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chaprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chaprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.chaprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chaprms.T2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = "            ";
                                }
                                catch { }
                            if ((s.Substring(9) == "CHC") && s.LastIndexOf("Technic") >= 0 && s != "")
                                try
                                {
                                    this.mainf.grf.tech = clasglobal.CHC;
                                    s = Fl.ReadLine(); clasglobal.chcprms.E1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chcprms.E2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chcprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                    s = Fl.ReadLine(); clasglobal.chcprms.T1 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); clasglobal.chcprms.T2 = Convert.ToDouble(s.Substring(3));
                                    s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                    s = "            ";
                                }
                                catch { }
                        if ((s.Substring(9) == "CD") && s.LastIndexOf("Technic") >= 0 && s != "")
                            try
                            {
                                this.mainf.grf.tech = clasglobal.CD;
                                s = Fl.ReadLine(); clasglobal.cdprms.I1 = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine(); clasglobal.cdprms.I2 = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine(); clasglobal.cdprms.EUP = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine(); clasglobal.cdprms.EDO = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine(); clasglobal.cdprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                s = Fl.ReadLine(); clasglobal.cdprms.CYCLE = Convert.ToDouble(s.Substring(3));

                                s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                s = "            ";
                            }
                            catch { }
                        flag1 = true;

                        if ((s.Substring(9) == "OCP") && s.LastIndexOf("Technic") >= 0 && s != "")
                            try
                            {
                                this.mainf.grf.tech = clasglobal.OCP;
                                s = Fl.ReadLine(); clasglobal.ocpprms.Time = Convert.ToDouble(s.Substring(3)); 
                                s = Fl.ReadLine(); //caprms.Cycles = Convert.ToDouble(s.Substring(7));
                                s = "            ";
                            }
                            catch { }
                        flag1 = true;

                    }
                        if (flag1)
                        {
                            treeView2.Nodes[0].Nodes.Add(clasglobal.Tech_FullName[this.mainf.grf.tech]);
                            UdateDsFromParams(this.mainf.grf.tech, num_row, 1);
                            num_row++;
                            flag1 = false;
                        }
                    }
               
                

                Fl.Close();
                if (treeView2.Nodes[0].Nodes.Count > 0)
                    treeView2.SelectedNode = treeView2.Nodes[0].FirstNode;
            }
            catch { }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyValue == (int)Keys.M)
            //    Online_Smoothing = true;
            //if (e.Control && e.KeyValue == (int)Keys.N)
            //    Online_Smoothing = false;
        }
        private void txtCP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.')
                e.KeyChar = (char)0;
        }
        private void treeView1_DoubleClick(object sender, EventArgs e)//اضافه کردن تکنیک انتخاب شده به لیست تکنیک ها برای اجرا
        {
            if (treeView1.SelectedNode != null)
            {
                treeView2.Nodes[0].Nodes.Add(treeView1.SelectedNode.Text);
                rowsel = treeView2.Nodes[0].LastNode.Index;
                setcolorfortreev2();
                treeView2.ExpandAll();
                int techno = -1;
                switch (treeView1.SelectedNode.Name)
                {
                    case "DCV":
                        clasglobal.set_gridCells(clasglobal.DCV, this.pGrid);
                        techno = clasglobal.DCV;
                        break;
                    case "NPV":
                        clasglobal.set_gridCells(clasglobal.NPV, this.pGrid);
                        techno = clasglobal.NPV;
                        break;
                    case "DPV":
                        clasglobal.set_gridCells(clasglobal.DPV, this.pGrid);
                        techno = clasglobal.DPV;
                        break;
                    case "SWV":
                        clasglobal.set_gridCells(clasglobal.SWV, this.pGrid);
                        techno = clasglobal.SWV;
                        break;
                    case "CV":
                        clasglobal.set_gridCells(clasglobal.CV, this.pGrid);
                        techno = clasglobal.CV;
                        break;
                    case "LSV":
                        clasglobal.set_gridCells(clasglobal.LSV, this.pGrid);
                        techno = clasglobal.LSV;
                        break;
                    case "DCs":
                        clasglobal.set_gridCells(clasglobal.DCs, this.pGrid);
                        techno = clasglobal.DCs;
                        break;
                    case "DPs":
                        clasglobal.set_gridCells(clasglobal.DPs, this.pGrid);
                        techno = clasglobal.DPs;
                        break;
                    case "CPC":
                        clasglobal.set_gridCells(clasglobal.CPC, this.pGrid);
                        techno = clasglobal.CPC;
                        break;
                    case "CHP":
                        clasglobal.set_gridCells(clasglobal.CHP, this.pGrid);
                        techno = clasglobal.CHP;
                        break;
                    case "CHA":
                        clasglobal.set_gridCells(clasglobal.CHA, this.pGrid);
                        techno = clasglobal.CHA;
                        break;
                    case "CHC":
                        clasglobal.set_gridCells(clasglobal.CHC, this.pGrid);
                        techno = clasglobal.CHC;
                        break;
                    case "CD":
                        clasglobal.set_gridCells(clasglobal.CD, this.pGrid);
                        techno = clasglobal.CD;
                        break;
                    case "OCP":
                        clasglobal.set_gridCells(clasglobal.OCP, this.pGrid);
                        techno = clasglobal.OCP;
                        break;
                }
                if (techno >= 0)
                {
                    dschart1.Tables["chartlist"].Rows.Add(dschart1.Tables["chartlist"].NewRow());
                    UdateDsFromParams(techno, dschart1.Tables["chartlist"].Rows.Count - 1, 1);
                    //this.grf.tech = byte.Parse(techno.ToString());
                }

                if (treeView1.SelectedNode.Name == "CPC")
                {
                    rd_low_cpc.Visible = true;
                    rd_hieght_cpc.Visible = true;
                }
                else
                {
                    rd_low_cpc.Visible = false;
                    rd_hieght_cpc.Visible = false;
                }

                treeView2.SelectedNode = treeView2.Nodes[0].Nodes[treeView2.Nodes[0].Nodes.Count - 1];//وقتی کاربر تکنیک سی پی سی را انتخاب میکند مقدار های و لو جریان برای همان تکنیک آخر ست شود
            }

        }
        private void UdateDsFromParams(int TeqNo, int selectedRow, int typeact)// ست کردن اطلاعات پارامترهای تکنیک ها هنگام اوپن - انتخاب تکنیک و یا تغییر دستی پارامتر
        {
            try
            {
                if (typeact == 1)
                {
                    if (dschart1.chartlist.Rows.Count <= selectedRow)
                    {
                        DataRow dr = dschart1.chartlist.NewRow();
                        dschart1.chartlist.Rows.Add(dr);
                    }
                    dschart1.Tables["chartlist"].Rows[selectedRow]["row"] = selectedRow.ToString();
                    dschart1.Tables["chartlist"].Rows[selectedRow]["techno"] = TeqNo.ToString();
                    dschart1.Tables["chartlist"].Rows[selectedRow]["CR"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["E3"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["FR"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["HT"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["I1"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["I2"] = "0";
                    dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = "";
                   
                    switch (TeqNo)
                    {
                        case clasglobal.DCV:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dcvprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dcvprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dcvprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dcvprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dcvprms.ScanRate.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dcvprms.TStep.ToString();
                            if (clasglobal.dcvprms.comment1 == null)
                                clasglobal.dcvprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.dcvprms.comment1.ToString();
                            break;

                        case clasglobal.NPV:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.npvprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.npvprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.npvprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.npvprms.PulseWidth.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.npvprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.npvprms.ScanRate.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.npvprms.TStep.ToString();
                            if (clasglobal.npvprms.comment1 == null)
                                clasglobal.npvprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.npvprms.comment1.ToString();

                            break;
                        case clasglobal.DPV:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dpvprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dpvprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dpvprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.dpvprms.PulseHeight.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.dpvprms.PulseWidth.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dpvprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dpvprms.ScanRate.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dpvprms.TStep.ToString();
                            if (clasglobal.dpvprms.comment1 == null)
                                clasglobal.dpvprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.dpvprms.comment1.ToString();

                            break;
                        case clasglobal.SWV:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.swvprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.swvprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["FR"] = clasglobal.swvprms.Frequency.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.swvprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.swvprms.PulseHeight.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.swvprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.swvprms.ScanRate.ToString();
                            if (clasglobal.swvprms.comment1 == null)
                                clasglobal.swvprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.swvprms.comment1.ToString();

                            break;
                        case clasglobal.CV:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.cvprms.Cycles.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.cvprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.cvprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E3"] = clasglobal.cvprms.E3.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.cvprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cvprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.cvprms.ScanRate.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.cvprms.TStep.ToString();
                            if (clasglobal.cvprms.comment1 == null)
                                clasglobal.cvprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.cvprms.comment1.ToString();

                            break;
                        case clasglobal.LSV:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.lsvprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.lsvprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.lsvprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.lsvprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.lsvprms.ScanRate.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.lsvprms.TStep.ToString();
                            if (clasglobal.lsvprms.comment1 == null)
                                clasglobal.lsvprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.lsvprms.comment1.ToString();

                            break;
                        case clasglobal.DCs:

                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dcsprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dcsprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dcsprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dcsprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dcsprms.ScanRate.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dcsprms.TStep.ToString();
                            if (clasglobal.dcsprms.comment1 == null)
                                clasglobal.dcsprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.dcsprms.comment1.ToString();

                            break;
                        case clasglobal.DPs:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dpsprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dpsprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dpsprms.HStep.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.dpsprms.PulseHeight.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.dpsprms.PulseWidth.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dpsprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dpsprms.ScanRate.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dpsprms.TStep.ToString();
                            if (clasglobal.dpsprms.comment1 == null)
                                clasglobal.dpsprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.dpsprms.comment1.ToString();

                            break;
                            //case clasglobal.OCP:
                            //    dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = "1";  // clasglobal.ocpprms.Cycles.ToString();
                            //    dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.ocpprms.Time.ToString();
                            //dschart1.Tables["chartlist"].Rows[selectedRow]["Dly"] = clasglobal.ocpprms.Delay.ToString();

                        //    break;
                        case clasglobal.CPC:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.cpcprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cpcprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.cpcprms.T1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = "0";

                            if (clasglobal.cpcprms.comment1 == null)
                                clasglobal.cpcprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.cpcprms.comment1.ToString();

                            break;
                        case clasglobal.CCC:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["I1"] = clasglobal.cccprms.I1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cccprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.cccprms.T1.ToString();
                            if (clasglobal.cccprms.comment1 == null)
                                clasglobal.cccprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.cccprms.comment1.ToString();

                            break;
                        case clasglobal.CHA:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.chaprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.chaprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.chaprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.chaprms.T1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = clasglobal.chaprms.T2.ToString();
                            if (clasglobal.chaprms.comment1 == null)
                                clasglobal.chaprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.chaprms.comment1.ToString();

                            break;
                        case clasglobal.CHP:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["I1"] = clasglobal.chpprms.I1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["I2"] = clasglobal.chpprms.I2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.chpprms.I1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.chpprms.I2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E3"] = clasglobal.chpprms.EUP.ToString();//EUP
                            dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.chpprms.EDO.ToString();//EDO
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.chpprms.T1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = clasglobal.chpprms.T2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.chpprms.EquilibriumTime.ToString();
                            if (clasglobal.chpprms.comment1 == null)
                                clasglobal.chpprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.chpprms.comment1.ToString();

                            break;
                        case clasglobal.CHC:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.chcprms.E1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.chcprms.E2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.chcprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.chcprms.T1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T2"] = clasglobal.chcprms.T2.ToString();
                            if (clasglobal.chcprms.comment1 == null)
                                clasglobal.chcprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.chcprms.comment1.ToString();

                            break;
                        case clasglobal.CD:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["I1"] = clasglobal.cdprms.I1.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["I2"] = clasglobal.cdprms.I2.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["E3"] = clasglobal.cdprms.EUP.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.cdprms.EDO.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cdprms.EquilibriumTime.ToString();
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.cdprms.CYCLE.ToString();
                            
                            if (clasglobal.cdprms.comment1 == null)
                                clasglobal.cdprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.cdprms.comment1.ToString();

                            break;
                        case clasglobal.OCP:
                            dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.ocpprms.Time.ToString();
                            if (clasglobal.ocpprms.comment1 == null)
                                clasglobal.ocpprms.comment1 = "";
                            dschart1.Tables["chartlist"].Rows[selectedRow]["comment"] = clasglobal.ocpprms.comment1.ToString();

                            break;
                        default: break;

                    }
                    dschart1.Tables["chartlist"].AcceptChanges();
                }
                else
                {


                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Settxtfill_func(int v1, string v2, int v3, int v4)
        {
            throw new NotImplementedException();
        }

        private void rd_hieght_cpc_CheckedChanged(object sender, EventArgs e)// جریان بالا و پایین دارد که در فیلد اچ اس ما را تغییر می دهد cpc در تکنیک
        {
            try
            {
                int selectedRow = treeView2.SelectedNode.Index;

                if (rd_hieght_cpc.Checked == true)
                    dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = "1";
                else
                    dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = "0";

                dschart1.Tables["chartlist"].AcceptChanges();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)// نمایش یا عدم نمایش گزینه های یا لو برای سی پی سی
        {
            if (e.Node.Level > 0)
            {
                int TeqNo = int.Parse(dschart1.Tables["chartlist"].Rows[e.Node.Index][1].ToString());

                if (TeqNo == clasglobal.CPC)
                {
                    rd_low_cpc.Visible = true;
                    rd_hieght_cpc.Visible = true;
                }
                else
                {
                    rd_low_cpc.Visible = false;
                    rd_hieght_cpc.Visible = false;
                }

                SetParamsFromDs(e.Node.Index);
                rowsel = e.Node.Index;
                //setcolorfortreev2();
            }
        }
        private void setcolorfortreev2()
        {
            try
            {
                treeView2.Nodes[0].Nodes[rowsel].ForeColor = Color.Red;
                for (int i = 0; i <= treeView2.Nodes[0].LastNode.Index; i++)
                {
                    if (i != rowsel)
                    {
                        if (i % 2 == 0)
                            treeView2.Nodes[0].Nodes[i].ForeColor = Color.White;
                        else
                            treeView2.Nodes[0].Nodes[i].ForeColor = Color.Green;
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void pGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)//ست کردن پارامتر های هر تکنیک در کلاس مربوطه پس از تغییر ستی کاربر
        {
            pGrid.Refresh();
            showMessageError = true;

            switch (byte.Parse(this.dschart1.chartlist.Rows[rowsel][1].ToString()))
            {
                case clasglobal.DCV:
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.dcvprms.E1 < -8 || clasglobal.dcvprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.dcvprms.E1 = 1; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.dcvprms.E2 < -8 || clasglobal.dcvprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.dcvprms.E2 = 1; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.dcvprms.ScanRate < 0.0001 || clasglobal.dcvprms.ScanRate > 0.25) { showError("Scan Rate(V/S)", "0.0001V/S", "0.250V/S"); clasglobal.dcvprms.ScanRate = 0.0001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.dcvprms.HStep < 0.001 || clasglobal.dcvprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.dcvprms.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.dcvprms.TStep < 0.1 || clasglobal.dcvprms.TStep > 10) { showError("TStep(S)", "0.1S", "10S"); clasglobal.dcvprms.TStep = 0.1; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.dcvprms.EquilibriumTime < 0 || clasglobal.dcvprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.dcvprms.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.dcvprms.ScanRate != 0)
                        {
                            clasglobal.dcvprms.TStep = Math.Round(clasglobal.dcvprms.HStep / clasglobal.dcvprms.ScanRate, 5);
                        }
                        if (clasglobal.dcvprms.TStep < 0.1 || clasglobal.dcvprms.TStep > 10)
                        {
                            MessageBox.Show("TStep = " + clasglobal.dcvprms.TStep.ToString() + "!!! \nTStep < 0.1 OR TStep > 10");
                            clasglobal.dcvprms.HStep = 0.01;
                            clasglobal.dcvprms.ScanRate = 0.1;
                            clasglobal.dcvprms.TStep = 0.1;
                        }
                    }
                    break;

                case clasglobal.NPV:
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.npvprms.E1 < -8 || clasglobal.npvprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.npvprms.E1 = 0; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.npvprms.E2 < -8 || clasglobal.npvprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.npvprms.E2 = 0; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.npvprms.ScanRate < 0.0001 || clasglobal.npvprms.ScanRate > 0.25) { showError("Scan Rate(V/S)", "0.0001V/S", "0.25V/S"); clasglobal.npvprms.ScanRate = 0.0001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.npvprms.HStep < 0.001 || clasglobal.npvprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.npvprms.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.npvprms.TStep < 0.1 || clasglobal.npvprms.TStep > 10) { showError("TStep(S)", "0.1S", "10S"); clasglobal.npvprms.TStep = 0.1; }
                    if (e.ChangedItem.Label == "Quite Time")
                        if (clasglobal.npvprms.EquilibriumTime < 0 || clasglobal.npvprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.npvprms.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "Pulse Width")
                        if (clasglobal.npvprms.PulseWidth < 0.05 || clasglobal.npvprms.PulseWidth > (clasglobal.npvprms.TStep - 0.05))
                        {
                            showError("Pulse Width(S)", "0.05S", clasglobal.npvprms.TStep - 0.05 + "(TStep-0.05S)");
                            clasglobal.npvprms.PulseWidth = Math.Round(clasglobal.npvprms.TStep - 0.05, 5);
                        }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.npvprms.ScanRate != 0) clasglobal.npvprms.TStep = Math.Round(clasglobal.npvprms.HStep / clasglobal.npvprms.ScanRate, 5);
                        if (clasglobal.npvprms.TStep < 0.1 || clasglobal.npvprms.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.npvprms.TStep.ToString() + "!!! \nTStep < 0.1 OR TStep > 10");
                            clasglobal.npvprms.HStep = 0.01;
                            clasglobal.npvprms.ScanRate = 0.1;
                            clasglobal.npvprms.TStep = 0.1;
                        }
                    }
                    break;

                case clasglobal.DPV:
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.dpvprms.E1 < -8 || clasglobal.dpvprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.dpvprms.E1 = 0; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.dpvprms.E2 < -8 || clasglobal.dpvprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.dpvprms.E2 = 0; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.dpvprms.ScanRate < 0.0001 || clasglobal.dpvprms.ScanRate > 0.25) { showError("Scan Rate(V/S)", "0.0001V/S", "0.25V/S"); clasglobal.dpvprms.ScanRate = 0.001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.dpvprms.HStep < 0.001 || clasglobal.dpvprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.dpvprms.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.dpvprms.TStep < 0.1 || clasglobal.dpvprms.TStep > 10) { showError("TStep(S)", "0.1S", "10S"); clasglobal.dpvprms.TStep = 0.1; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.dpvprms.EquilibriumTime < 0 || clasglobal.dpvprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.dpvprms.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "Pulse Width")
                        if (clasglobal.dpvprms.PulseWidth < 0.05 || clasglobal.dpvprms.PulseWidth > (clasglobal.dpvprms.TStep - 0.05))
                        {
                            showError("Pulse Width(S)", "0.05S", clasglobal.dpvprms.TStep - 0.05 + "(TStep-0.05S)");
                            clasglobal.dpvprms.PulseWidth = Math.Round(clasglobal.dpvprms.TStep - 0.05, 5);
                        }
                    if (e.ChangedItem.Label == "Pulse Height")
                        if (clasglobal.dpvprms.PulseHeight < 0.001 || clasglobal.dpvprms.PulseHeight > 0.250) { showError("Pulse Height(mV)", "0.001V", "0.250V"); clasglobal.dpvprms.PulseHeight = 0.001; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.dpvprms.ScanRate != 0) clasglobal.dpvprms.TStep = Math.Round(clasglobal.dpvprms.HStep / clasglobal.dpvprms.ScanRate, 5);
                        if (clasglobal.dpvprms.TStep < 0.1 || clasglobal.dpvprms.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.dpvprms.TStep.ToString() + "!!! \nTStep < 0.1 OR TStep > 10\n");
                            clasglobal.dpvprms.HStep = 0.01;
                            clasglobal.dpvprms.ScanRate = 0.1;
                            clasglobal.dpvprms.TStep = 0.1;
                        }
                    }
                    break;

                case clasglobal.SWV:
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.swvprms.E1 < -8 || clasglobal.swvprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.swvprms.E1 = 1; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.swvprms.E2 < -8 || clasglobal.swvprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.swvprms.E2 = 1; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.swvprms.ScanRate < 0.001 || clasglobal.swvprms.ScanRate > 25) { showError("Scan Rate(V/S)", "0.001V/S", "25V/S"); clasglobal.swvprms.ScanRate = 0.001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.swvprms.HStep < 0.001 || clasglobal.swvprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.swvprms.HStep = 0.001; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.swvprms.EquilibriumTime < 0 || clasglobal.swvprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.swvprms.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "Pulse Height")
                        if (clasglobal.swvprms.PulseHeight < 0.001 || clasglobal.swvprms.PulseHeight > 0.250) { showError("Pulse Height(mV)", "0.001mV", "0.250mV"); clasglobal.swvprms.PulseHeight = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.swvprms.HStep != 0)
                            clasglobal.swvprms.Frequency = Math.Round(clasglobal.swvprms.ScanRate / clasglobal.swvprms.HStep, 5);
                    if (clasglobal.swvprms.Frequency < 1 || clasglobal.swvprms.Frequency > 1000)
                    {
                        MessageBox.Show("Frequency= " + clasglobal.swvprms.Frequency.ToString() + "!!! \nFrequency < 1 OR Frequency > 1000\n");
                        clasglobal.swvprms.HStep = 0.01;
                        clasglobal.swvprms.ScanRate = 0.1;
                        clasglobal.swvprms.Frequency = 10;
                    }
                    break;

                case clasglobal.CV:
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.cvprms.E1 < -8 || clasglobal.cvprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.cvprms.E1 = 1; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.cvprms.E2 < -8 || clasglobal.cvprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.cvprms.E2 = 1; }
                    if (e.ChangedItem.Label == "E3")
                        if (clasglobal.cvprms.E3 < -8 || clasglobal.cvprms.E3 > 8) { showError("E3(V)", "-8V", "8V"); clasglobal.cvprms.E3 = 1; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.cvprms.ScanRate < 0.0001 || clasglobal.cvprms.ScanRate > 250) { showError("Scan Rate(V/S)", "0.0001V/S", "250V/S"); clasglobal.cvprms.ScanRate = 0.0001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.cvprms.HStep < 0.001 || clasglobal.cvprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.cvprms.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.cvprms.TStep < 0.0001 || clasglobal.cvprms.TStep > 10) { showError("TStep(S)", "0.0001S", "10S"); clasglobal.cvprms.TStep = 0.0001; }
                    if (e.ChangedItem.Label == "Cycles")
                        if (clasglobal.cvprms.Cycles < 1 || clasglobal.cvprms.Cycles > 1000) { showError("Cycle(s)", "1", "1000"); clasglobal.cvprms.Cycles = 1; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.cvprms.EquilibriumTime < 0 || clasglobal.cvprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.cvprms.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.cvprms.ScanRate != 0)
                        {
                            clasglobal.cvprms.TStep = Math.Round(clasglobal.cvprms.HStep / clasglobal.cvprms.ScanRate, 5);
                        }
                        if (clasglobal.cvprms.TStep < 0.0001 || clasglobal.cvprms.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.cvprms.TStep.ToString() + "!!! \nTStep < 0.0001 OR TStep > 10\n");
                            clasglobal.cvprms.HStep = 0.01;
                            clasglobal.cvprms.ScanRate = 0.1;
                            clasglobal.cvprms.TStep = 0.1;
                        }
                    }
                    if (e.ChangedItem.Label == "E1") clasglobal.cvprms.E3 = clasglobal.cvprms.E1;


                    //تنظیم مقدار یک و سه برای زمانی که کاربر مقدار وارده را مضرب اچ اس قرار نمی دهد
                    //بنا براین میکرو برای شروع و پایان مقدار خودش که از فرمول پایین بدست می آید را معیار قرار می دهد
                    //در نهایت ما در این برنامه به تعداد اچ اس خودمان نقطه روی گراف نمایش می دهیم اما میکرو بخاطر اینکه خودش ست میشود تعدادش کمتر است
                    //و از طرفی برخی از مقدارهای جا
                    //مانده از سیکل قبل را در سیکل بعد نشان میدهد که کلا خطا است. همچنین در روند اجرا سیکل بخاطر اینکه شرط اتمام ندارد ذخیره نمیشود
                    //=========================================== mn               
                    decimal e2 = Convert.ToDecimal(clasglobal.cvprms.E2);
                    decimal e3 = Convert.ToDecimal(clasglobal.cvprms.E3);
                    decimal hss = Convert.ToDecimal(clasglobal.cvprms.HStep);

                    decimal hs_ok = e3 + ((e2 - e3) % hss);//باید حاصل تفریق تقسیم بر اچ اس باقیمانده نداشته باشد چراکه در میکرو پارامتر شروع از مقدار کاربر ست نمیشود

                    if (hs_ok != 0)
                    {
                        clasglobal.cvprms.E1 = (double)hs_ok;
                        clasglobal.cvprms.E3 = (double)hs_ok;
                    }
                    //========================================= mn

                    break;

                case clasglobal.LSV:
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.lsvprms.E1 < -8 || clasglobal.lsvprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.lsvprms.E1 = 1; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.lsvprms.E2 < -8 || clasglobal.lsvprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.lsvprms.E2 = 1; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.lsvprms.ScanRate < 0.0001 || clasglobal.lsvprms.ScanRate > 250) { showError("Scan Rate(V/S)", "0.0001V/S", "250V/S"); clasglobal.lsvprms.ScanRate = 0.0001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.lsvprms.HStep < 0.001 || clasglobal.lsvprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.lsvprms.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.lsvprms.TStep < 0.0001 || clasglobal.lsvprms.TStep > 10) { showError("TStep(S)", "0.0001S", "10S"); clasglobal.lsvprms.TStep = 0.0001; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.lsvprms.EquilibriumTime < 0 || clasglobal.lsvprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.lsvprms.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.lsvprms.ScanRate != 0) clasglobal.lsvprms.TStep = Math.Round(clasglobal.lsvprms.HStep / clasglobal.lsvprms.ScanRate, 5);
                        if (clasglobal.lsvprms.TStep < 0.0001 || clasglobal.lsvprms.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.lsvprms.TStep.ToString() + "!!! \nTStep < 0.0001 OR TStep > 10\n");
                            clasglobal.lsvprms.HStep = 0.01;
                            clasglobal.lsvprms.ScanRate = 0.1;
                            clasglobal.lsvprms.TStep = 0.1;
                        }
                    }
                    break;
                case clasglobal.DCs:
                    if (clasglobal.dcsprms.E1 < -8 || clasglobal.dcsprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.dcsprms.E1 = 0; }
                    if (clasglobal.dcsprms.E2 < -8 || clasglobal.dcsprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.dcsprms.E2 = 0; }
                    if (clasglobal.dcsprms.ScanRate < 0.0001 || clasglobal.dcsprms.ScanRate > 0.25) { showError("Scan Rate(V/S)", "0.0001V/S", "0.25V/S"); clasglobal.dcsprms.ScanRate = 0.0001; }
                    if (clasglobal.dcsprms.HStep < 0.001 || clasglobal.dcsprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.dcsprms.HStep = 0.001; }
                    if (clasglobal.dcsprms.TStep < 0.1 || clasglobal.dcsprms.TStep > 10) { showError("TStep(S)", "0.1S", "10S"); clasglobal.dcsprms.TStep = 0.1; }
                    if (clasglobal.dcsprms.EquilibriumTime < 0 || clasglobal.dcsprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.dcsprms.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.dcsprms.ScanRate != 0) clasglobal.dcsprms.TStep = Math.Round(clasglobal.dcsprms.HStep / clasglobal.dcsprms.ScanRate, 5);
                        if (clasglobal.dcsprms.TStep < 0.1 || clasglobal.dcsprms.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.dcsprms.TStep.ToString() + "!!! \nTStep < 0.1 OR TStep > 10\n");
                            clasglobal.dcsprms.HStep = 0.01;
                            clasglobal.dcsprms.ScanRate = 0.1;
                            clasglobal.dcsprms.TStep = 0.1;
                        }
                    }
                    break;
                case clasglobal.DPs:
                    if (clasglobal.dpsprms.E1 < -8 || clasglobal.dpsprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.dpsprms.E1 = 0; }
                    if (clasglobal.dpsprms.E2 < -8 || clasglobal.dpsprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.dpsprms.E2 = 0; }
                    if (clasglobal.dpsprms.ScanRate < 0.0001 || clasglobal.dpsprms.ScanRate > 0.25) { showError("Scan Rate(V/S)", "0.0001V/S", "0.25V/S"); clasglobal.dpsprms.ScanRate = 0.0001; }
                    if (clasglobal.dpsprms.HStep < 0.001 || clasglobal.dpsprms.HStep > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); clasglobal.dpsprms.HStep = 0.001; }
                    if (clasglobal.dpsprms.TStep < 0.1 || clasglobal.dpsprms.TStep > 10) { showError("TStep(S)", "0.1S", "10S"); clasglobal.dpsprms.TStep = 0.1; }
                    if (clasglobal.dpsprms.EquilibriumTime < 0 || clasglobal.dpsprms.EquilibriumTime > 2000) { showError("EquilibriumT ime(S)", "0S", "2000S"); clasglobal.dpsprms.EquilibriumTime = 0; }
                    if (clasglobal.dpsprms.PulseWidth < 0.05 || clasglobal.dpsprms.PulseWidth > (clasglobal.dpsprms.TStep - 0.05)) { showError("Pulse Width(s)", "0.05V", clasglobal.dpsprms.TStep - 0.05 + "(TStep-0.05S)"); clasglobal.dpsprms.PulseWidth = Math.Round(clasglobal.dpsprms.TStep, 5); }
                    if (clasglobal.dpsprms.PulseHeight < 0.001 || clasglobal.dpsprms.PulseHeight > 0.250) { showError("PulseH eight(V)", "0.001V", "0.250V"); clasglobal.dpsprms.PulseHeight = 0.001; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.dpsprms.ScanRate != 0) clasglobal.dpsprms.TStep = Math.Round(clasglobal.dpsprms.HStep / clasglobal.dpsprms.ScanRate, 5);
                        if (clasglobal.dpsprms.TStep < 0.1 || clasglobal.dpsprms.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.dpsprms.TStep.ToString() + "!!! \nTStep < 0.1 OR TStep > 10\n");
                            clasglobal.dpsprms.HStep = 0.01;
                            clasglobal.dpsprms.ScanRate = 0.1;
                            clasglobal.dpsprms.TStep = 0.1;
                        }
                    }
                    break;
                //case clasglobal.OCP: if (clasglobal.ocpprms.Time < 1 || clasglobal.ocpprms.Time > 65000) { showError("Time(S)", "1V", "65000V"); clasglobal.ocpprms.Time = 1; }
                //    //if (clasglobal.ocpprms.Cycles < 1 || clasglobal.ocpprms.Cycles > 1000) { showError("Cycles", "1", "1000"); clasglobal.ocpprms.Cycles = 1; }
                //    break;
                case clasglobal.CPC:
                    if (clasglobal.cpcprms.E1 < -8 || clasglobal.cpcprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.cpcprms.E1 = 0; }
                    if (clasglobal.cpcprms.T1 < 0 || clasglobal.cpcprms.T1 > 60000) { showError("T1(V)", "0V", "65000V"); clasglobal.cpcprms.T1 = 0; }
                    if (clasglobal.cpcprms.EquilibriumTime < 0 || clasglobal.cpcprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.cpcprms.EquilibriumTime = 0; }
                    //if (clasglobal.cpcprms.Cycles < 1 || clasglobal.cpcprms.Cycles > 1000) { showError("Cycles", "1", "1000"); clasglobal.cpcprms.Cycles = 1; }
                    break;
                case clasglobal.CCC:
                    if (clasglobal.cccprms.I1 < 0 || clasglobal.cccprms.I1 > 2) { showError("I1(A)", "0A", "2A"); clasglobal.cccprms.I1 = 0; }
                    if (clasglobal.cccprms.T1 < 0 || clasglobal.cccprms.T1 > 65000) { showError("T1(V)", "0V", "65000V"); clasglobal.cccprms.T1 = 0; }
                    if (clasglobal.cccprms.EquilibriumTime < 0 || clasglobal.cccprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.cccprms.EquilibriumTime = 0; }
                    //if (clasglobal.cpcprms.Cycles < 1 || clasglobal.cpcprms.Cycles > 1000) { showError("Cycles", "1", "1000"); cpcprms.Cycles = 1; }
                    break;
                case clasglobal.CHP:
                    if (clasglobal.chpprms.I1 < -8 || clasglobal.chpprms.I1 > 8) { showError("I1(mA)", "-8mA", "8mA"); clasglobal.chpprms.I1 = 0; }
                    if (clasglobal.chpprms.I2 < -8 || clasglobal.chpprms.I2 > 8) { showError("I2(mA)", "-8mA", "8mA"); clasglobal.chpprms.I2 = 0; }
                    if (clasglobal.chpprms.T1 < 0 || clasglobal.chpprms.T1 > 32000) { showError("T1(S)", "0S", "32000S"); clasglobal.chpprms.T1 = 0; }
                    if (clasglobal.chpprms.T2 < 0 || clasglobal.chpprms.T2 > (32000 - clasglobal.chpprms.T1)) { showError("T2(S)", "0S", "32000-T1"); clasglobal.chpprms.T2 = 0; }
                    if (clasglobal.chpprms.EUP < -5 || clasglobal.chpprms.EUP > 5) { showError("EUPPER(V)", "-5V", "-5V"); clasglobal.chpprms.EUP = 0; }
                    if (clasglobal.chpprms.EDO < -5 || clasglobal.chpprms.EDO > 5) { showError("ELOWEER(mA)", "-5V", "5V"); clasglobal.chpprms.EDO = 0; }
                    if (clasglobal.chpprms.EquilibriumTime < 0 || clasglobal.chpprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.chpprms.EquilibriumTime = 0; }
                    //if (caprms.Cycles < 1 || caprms.Cycles > 1000) { showError("Cycles", "1", "1000"); caprms.Cycles = 1; }
                    break;
                case clasglobal.CHA:
                    if (clasglobal.chaprms.E1 < -8 || clasglobal.chaprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.chaprms.E1 = 0; }
                    if (clasglobal.chaprms.E2 < -8 || clasglobal.chaprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.chaprms.E2 = 0; }
                    if (clasglobal.chaprms.T1 < 0 || clasglobal.chaprms.T1 > 32000) { showError("T1(S)", "0S", "32000S"); clasglobal.chaprms.T1 = 0; }
                    if (clasglobal.chaprms.T2 < 0 || clasglobal.chaprms.T2 > (32000 - clasglobal.chaprms.T1)) { showError("T2(S)", "0S", "32000-T1S"); clasglobal.chaprms.T2 = 0; }
                    if (clasglobal.chaprms.EquilibriumTime < 0 || clasglobal.chaprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.chaprms.EquilibriumTime = 0; }
                    //if (caprms.Cycles < 1 || caprms.Cycles > 1000) { showError("Cycles", "1", "1000"); caprms.Cycles = 1; }
                    break;
                case clasglobal.CHC:
                    if (clasglobal.chcprms.E1 < -8 || clasglobal.chcprms.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.chcprms.E1 = 0; }
                    if (clasglobal.chcprms.E2 < -8 || clasglobal.chcprms.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.chcprms.E2 = 0; }
                    if (clasglobal.chcprms.T1 < 0 || clasglobal.chcprms.T1 > 32000) { showError("T1(S)", "0S", "32000S"); clasglobal.chcprms.T1 = 0; }
                    if (clasglobal.chcprms.T2 < 0 || clasglobal.chcprms.T2 > 32000) { showError("T2(S)", "0S", "32000-T1S"); clasglobal.chcprms.T2 = 0; }
                    if (clasglobal.chcprms.EquilibriumTime < 0 || clasglobal.chcprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.chcprms.EquilibriumTime = 0; }
                    //if (caprms.Cycles < 1 || caprms.Cycles > 1000) { showError("Cycles", "1", "1000"); caprms.Cycles = 1; }
                    break;
                case clasglobal.CD:
                    if (clasglobal.cdprms.I1 < -150 || clasglobal.cdprms.I1 > 150) { showError("I1(mA)", "-150mA", "150mA"); clasglobal.cdprms.I1 = 0; }
                    if (clasglobal.cdprms.I2 < -150 || clasglobal.cdprms.I2 > 150) { showError("I2(mA)", "-150mA", "150mA"); clasglobal.cdprms.I2 = 0; }
                    if (clasglobal.cdprms.EUP < -8 || clasglobal.cdprms.EUP > 8) { showError("EUPPER(V)", "-5V", "-5V"); clasglobal.cdprms.EUP = 0; }
                    if (clasglobal.cdprms.EDO < -8 || clasglobal.cdprms.EDO > 8) { showError("ELOWEER(mA)", "-5V", "5V"); clasglobal.cdprms.EDO = 0; }
                    if (clasglobal.cdprms.CYCLE < 0 || clasglobal.cdprms.CYCLE > 999) { showError("CYCLE", "0", "999"); clasglobal.cdprms.CYCLE = 0; }
                    
                    if (clasglobal.cdprms.EquilibriumTime < 0 || clasglobal.cdprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.cdprms.EquilibriumTime = 0; }
                    //if (caprms.Cycles < 1 || caprms.Cycles > 1000) { showError("Cycles", "1", "1000"); caprms.Cycles = 1; }
                    break;


                case clasglobal.OCP:
                    if (clasglobal.ocpprms.Time < 0 || clasglobal.ocpprms.Time > 9999) { showError("TIME(s)", "1", "9999"); clasglobal.ocpprms.Time = 0; }
                    //if (caprms.Cycles < 1 || caprms.Cycles > 1000) { showError("Cycles", "1", "1000"); caprms.Cycles = 1; }
                    break;
                    /*
                case SC:
                    if (scprms.E1 < -5 || scprms.E1 > 5) { showError("E1(V)", "-5V", "5V"); scprms.E1 = 0; }
                    if (scprms.Q1 < -65000 || scprms.Q1 > 65000) { showError("Q1(uC)", "-65000uC", "65000uC"); scprms.Q1 = 1; }
                    if (scprms.E2 < -5 || scprms.E2 > 5) { showError("E2(V)", "-5V", "5V"); scprms.E2 = 0; }
                    if (scprms.Q2 < -65000 || scprms.Q2 > 65000) { showError("Q2(uC)", "-65000uC", "65000uC"); scprms.Q2 = 1; }
                    if (scprms.EquilibriumTime < 0 || scprms.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); scprms.EquilibriumTime = 0; }
                    if (scprms.Cycles < 1 || scprms.Cycles > 1000) { showError("Cycles", "1", "1000"); scprms.Cycles = 1; }
                    if (!scprms.vs_OCP) scprms.OCPmeasurment = 0;
                    if (scprms.vs_OCP) if (scprms.OCPmeasurment < 0 || scprms.OCPmeasurment > 65000) scprms.OCPmeasurment = 0;
                    break;
                     * */
            }
            pGrid.Refresh();


            UdateDsFromParams(int.Parse(this.dschart1.chartlist.Rows[rowsel][1].ToString()), rowsel, 1);


            if (e.ChangedItem.Label == "Visible" && e.ChangedItem.Parent.Label == "Values") this.mainf.grf1.tChart1.Visible = gc.Visible;
            if (e.ChangedItem.Label == "BackColor" && e.ChangedItem.Parent.Label == "Values") this.mainf.grf1.tChart1.BackColor = gc.BackColor;
            if (e.ChangedItem.Label == "BackGroundImage" && e.ChangedItem.Parent.Label == "Values") this.mainf.grf1.tChart1.BackgroundImage = gc.BackGroundImage;
            if (e.ChangedItem.Label == "BackGroundImageLayout" && e.ChangedItem.Parent.Label == "Values") this.mainf.grf1.tChart1.BackgroundImageLayout = gc.BackGroundImageLayout;
            if (e.ChangedItem.Label == "Dock" && e.ChangedItem.Parent.Label == "Values") this.mainf.grf1.tChart1.Dock = gc.Dock;
            if (e.ChangedItem.Label == "Enabled" && e.ChangedItem.Parent.Label == "Values") this.mainf.grf1.tChart1.Enabled = gc.Enabled;
            //if (e.ChangedItem.Label == "SmothMode" && e.ChangedItem.Parent.Label == "Values") ;
            showMessageError = false;
        }
        private void SetParamsFromDs(int selectedRow)// فراخوانی اطلاعات تکنیک انتخاب شده از دیتاست 
        {
            try
            {
                int TeqNo = int.Parse(dschart1.Tables["chartlist"].Rows[selectedRow][1].ToString());
                switch (TeqNo)
                {
                    case clasglobal.DCV:
                        clasglobal.dcvprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.dcvprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.dcvprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.dcvprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.dcvprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.dcvprms.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        clasglobal.dcvprms.comment1 =dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                     
                        break;

                    case clasglobal.NPV:
                        clasglobal.npvprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.npvprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.npvprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.npvprms.PulseWidth = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PW"].ToString());
                        clasglobal.npvprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.npvprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.npvprms.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        clasglobal.npvprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                      
                        break;
                    case clasglobal.DPV:
                        clasglobal.dpvprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.dpvprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.dpvprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.dpvprms.PulseHeight = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PH"].ToString());
                        clasglobal.dpvprms.PulseWidth = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PW"].ToString());
                        clasglobal.dpvprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.dpvprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.dpvprms.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        clasglobal.dpvprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                   
                        break;
                    case clasglobal.SWV:
                        clasglobal.swvprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.swvprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.swvprms.Frequency = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["FR"].ToString());
                        clasglobal.swvprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.swvprms.PulseHeight = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PH"].ToString());
                        clasglobal.swvprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.swvprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.swvprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                     
                        break;
                    case clasglobal.CV:
                        clasglobal.cvprms.Cycles = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.cvprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.cvprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.cvprms.E3 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E3"].ToString());
                        clasglobal.cvprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.cvprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.cvprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.cvprms.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        clasglobal.cvprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                         
                        break;
                    case clasglobal.LSV:
                        clasglobal.lsvprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.lsvprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.lsvprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.lsvprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.lsvprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.lsvprms.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        clasglobal.lsvprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                       
                        break;
                    case clasglobal.DCs:
                        clasglobal.dcsprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.dcsprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.dcsprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.dcsprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.dcsprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.dcsprms.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        clasglobal.dcsprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                        
                        break;
                    case clasglobal.DPs:
                        clasglobal.dpsprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.dpsprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.dpsprms.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.dpsprms.PulseHeight = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PH"].ToString());
                        clasglobal.dpsprms.PulseWidth = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PW"].ToString());
                        clasglobal.dpsprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.dpsprms.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.dpsprms.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        clasglobal.dpsprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                       
                        break;
                    case clasglobal.CPC:
                        clasglobal.cpcprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.cpcprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.cpcprms.T1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        clasglobal.cpcprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();

                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString() == "0")
                            rd_low_cpc.Checked = true;
                        else
                            rd_hieght_cpc.Checked = true;

                        break;
                    case clasglobal.CHP:
                        clasglobal.chpprms.I1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["I1"].ToString());
                        clasglobal.chpprms.I2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["I2"].ToString());
                        clasglobal.chpprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.chpprms.T1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        clasglobal.chpprms.EUP = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E3"].ToString());
                        clasglobal.chpprms.EDO = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.chpprms.T2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T2"].ToString());
                        clasglobal.chpprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                       
                        break;
                    case clasglobal.CHA:
                        clasglobal.chaprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.chaprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.chaprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.chaprms.T1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        clasglobal.chaprms.T2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T2"].ToString());
                        clasglobal.chaprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                       
                        break;
                    case clasglobal.CHC:
                        clasglobal.chcprms.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.chcprms.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.chcprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.chcprms.T1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        clasglobal.chcprms.T2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T2"].ToString());
                        clasglobal.chcprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                       
                        break;
                    case clasglobal.CD:
                        clasglobal.cdprms.I1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["I1"].ToString());
                        clasglobal.cdprms.I2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["I2"].ToString());
                        clasglobal.cdprms.EUP = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E3"].ToString());
                        clasglobal.cdprms.EDO = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.cdprms.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.cdprms.CYCLE = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        clasglobal.cdprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                        break;
                    case clasglobal.OCP:
                        clasglobal.ocpprms.Time = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        clasglobal.ocpprms.comment1 = dschart1.Tables["chartlist"].Rows[selectedRow]["comment"].ToString();
                        break;
                    default: break;
                }
                clasglobal.set_gridCells(TeqNo, this.pGrid);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)// هنگام راست کلیک remove گزینه 
        {
            if (rowsel > -1)
            {
               
                dschart1.Tables["chartlist"].Rows.RemoveAt(rowsel);
                dschart1.Tables["chartlist"].AcceptChanges();
                for (int i = 0; i < dschart1.Tables["chartlist"].Rows.Count; i++)
                    dschart1.Tables["chartlist"].Rows[i][0] = i;
                treeView2.Nodes[0].Nodes.RemoveAt(rowsel);
                treeView2.Refresh();
                if (treeView2.Nodes[0].Nodes.Count > 0)
                {
                    rowsel = treeView2.Nodes[0].LastNode.Index;
                    SetParamsFromDs(rowsel);
                    clasglobal.set_gridCells(int.Parse(dschart1.Tables["chartlist"].Rows[rowsel][1].ToString()), this.pGrid);
                    setcolorfortreev2();
                }
                else
                    rowsel = -1;
                
                
            }
        }
      
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)// نمایش اطلاعات پایه ای تکنیک انتخاب شده
        {
            try
            {

                if (treeView1.SelectedNode.Level > 0)
                {

                    switch (treeView1.SelectedNode.Index)
                    {
                        case 0:
                             label1.Text = "Dc Voltammetry";
                             label2.Text = "this technique has following properties:\nE1(v)\nE2(v)\nHstep(v)\nTstep(s)\nScan Rate(v/s)\nEquilibrium Time(s)";
                          
                            break;
                        case 1:
                            label1.Text = "Normal Pulse Voltammetry";
                            label2.Text = "in this technique, the current resulting from a series of ever larger potential pulses is compared with the current at a constant 'baseline' voltage.\n";
                            label2.Text += "this technique has following properties:\nE1(v)\nE2(v)\nHstep(v)\nTstep(s)\nScan Rate(v/s)\nPulse Width(s)\nEquilibrium Time(s)";
                            break;
                        case 2:
                            label1.Text = "Differential Pulse Voltammetry";
                            label2.Text = " this technique can be considered as a derivative of linear sweep voltammetry , with a series of regular voltage pulses superimposed on the potential linear sweep or stairsteps. The current is measured immediately before each potential change, and the current difference is plotted as a function of potential.";
                            label2.Text += "this technique has following properties:\nE1(v)\nE2(v)\nHstep(v)\nTstep(s)\nScan Rate(v/s)\nPulse Width(s)\nPulse Height(v)\nEquilibrium Time(s)";
                            break;
                        case 3:
                            label1.Text = "Square Wave Voltammetry";
                            label2.Text = "In this technique , the current at a working electrode is measured while the potential between the working electrode and a reference electrode is swept linearly in time. The current is sampled at two times - once at the end of the forward potential pulse and again at the end of the reverse potential pulse";
                            label2.Text += "this technique has following properties:\nE1(v)\nE2(v)\nFrequency(Hz)\nHstep(v)\nScan Rate(v/s)\nPulse Width(s)\nEquilibrium Time(s)";
                            break;
                        case 4:
                            label1.Text = "Cyclic Voltammetry";
                            label2.Text = "this technique measures the current that develops in an electrochemical cell under conditions where voltage is in excess of that predicted by the Nernst equation. CV is performed by cycling the potential of a working electrode, and measuring the resulting current.";
                            label2.Text += "this technique has following properties:\nE1(v)\nE2(v)\nE3(v)\nHstep(v)\nTstep(s)\nScan Rate(v/s)\nHold Time(s)\nEquilibrium Time(s)";
                            break;
                        case 5:
                            label1.Text = "Linear Sweep Voltammetry";
                            label2.Text = "in this technique, the current at a working electrode is measured while the potential between the working electrode and a reference electrode is swept linearly in time. Oxidation or reduction of species is registered as a peak or trough in the current signal at the potential at which the species begins to be oxidized or reduced.";
                            label2.Text += "this technique has following properties:\nE1(v)\nE2(v)\nHstep(v)\nTstep(s)\nScan Rate(v/s)\nEquilibrium Time(s)";
                            break;
                        case 6:
                            label1.Text = "stripping Diff. Pulse Voltammetry";
                            label2.Text = "this technique has following properties:\nE1(v)\nE2(v)\nHstep(v)\nTstep(s)\nScan Rate(v/s)\nPulse Width(s)\nPulse Height(v)\nEquilibrium Time(s)";
                            
                            break;
                        case 7:
                            label1.Text = "stripping Dc Voltammetry";
                            label2.Text = "this technique has following properties:\nE1(v)\nE2(v)\nHstep(v)\nTstep(s)\nScan Rate(v/s)\nEquilibrium Time(s)";
                            
                            break;
                        case 8:
                            label1.Text = "Controlled Potential Coloumetry";
                            label2.Text = "Coulometry is based on an exhaustive electrolysis of the analyte. By exhaustive , analyte is completely oxidized or reduced at the working electrode or that it reacts completely with a reagent generated at the working electrode.\ncontrolled-potential coulometry, in which we apply a constant potential to the electrochemical cell.\n";
                            label2.Text += "this technique has following properties:\nE1(v)\nT1(s)\nEquilibrium Time(s)";
                           
                            break;
                        case 9:
                            label1.Text = "Chrono Amperometry";
                            label2.Text = "this technique is an electrochemical technique in which the potential of the working electrode is stepped and the resulting current from faradaic processes occurring at the electrode (caused by the potential step) is monitored as a function of time.\n";
                            label2.Text += "this technique has following properties:\nE1(v)\nT1(s)\nE2(v)\nT2(s)\nEquilibrium Time(s)";
                           
                            break;
                        case 10 :
                            label1.Text = "Chrono Coloumetry";
                            label2.Text = "Chronocoulometry involves measurement of the charge vs. time response to an applied potential step waveform. The shape of the resulting chronocoulogram can be understood by considering the concentration gradients in the solution adjacent to the electrode surface";
                            label2.Text += "this technique has following properties:\nE1(v)\nT1(s)\nE2(v)\nT2(s)\nEquilibrium Time(s)";
                           
                            break;
                        case 11:
                            label1.Text = "Chrono Potential Coloumetry";
                            label2.Text = "this technique has following properties:\nI1(mA)\nT1(s)\nI2(mA)\nT2(s)\nEquilibrium Time(s)";

                            break;
                        case 12:
                            label1.Text = "Charge and Discharge";
                            label2.Text = "this technique has following properties:\nI1(mA)\nT1(s)\nI2(mA)\nT2(s)\nEUP\nEDO\nEquilibrium Time(s)";

                            break;
                        case 13:
                            label1.Text = "OPEN CIRCUIT VOLTAGE";
                            label2.Text = "this technique has following properties:\nTIME(S)";

                            break;
                        default:
                            label1.Text = "";
                            label2.Text = "";
                            break;
                    }
                }
                else {
                    label1.Text = "";
                    label2.Text = "";
                   
                }
            }

            catch (Exception ex) { }
        
        }
        private void checkCPsCTs_CheckedChanged(object sender, EventArgs e)
        {
            pGrid2.Enabled = (checkCPsCTs.Checked);
           //اگر این فعال باشد این امکان هست که تا سه بار قبل از شروع تکنیک به دستگاه به اندازه زمان تعیین شده ولتاژ معین داد که بعد از آن تکنیک شروع به اجرا کند
           //به این ترتیب که ابتدا زمان اول ولتاژ اول داده میشود. بعد که این زمان تمام شد در زمان دوم ولتاژ دوم و بعد هم زمان و ولتاژ سوم
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {//تعین جریان داخلی مدار که از محلول گرفته میشود 0  اتوماتیک جریان می دهد اما اگر ولتاژ داده باشیم رنج از استپ قبلی تا ولت انتخاب شده باشد
            if (radioButton1.Checked)
                Range = 0;
            if (radioButton2.Checked)
                Range = 2;
            if (radioButton3.Checked)
                Range = 3;      
            if (radioButton4.Checked)
                Range = 4;
            if (radioButton5.Checked)
                Range = 5;
            if (radioButton6.Checked)
                Range = 6;
            if (radioButton7.Checked)
                Range = 7; 
        }
        private void pGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)//تعین محدوده رنج زمان و ولتاژ قبل از اجرا تکنیک
        {
            pGrid2.Refresh();
            showMessageError = true;
            if (e.ChangedItem.Label == "CP1")
                if (clasglobal.cpctprms.CP1 < -8 || clasglobal.cpctprms.CP1 > 8) { showError("CP1(V)", "-8V", "8V"); clasglobal.cpctprms.CP1 = 0; }
            if (e.ChangedItem.Label == "CP2")
                if (clasglobal.cpctprms.CP2 < -8 || clasglobal.cpctprms.CP2 > 8) { showError("CP2(V)", "-8V", "8V"); clasglobal.cpctprms.CP2 = 0; }
            if (e.ChangedItem.Label == "CP3")
                if (clasglobal.cpctprms.CP3 < -8 || clasglobal.cpctprms.CP3 > 8) { showError("CP3(V)", "-8V", "8V"); clasglobal.cpctprms.CP3 = 0; }

            if (e.ChangedItem.Label == "CT1")
                if (clasglobal.cpctprms.CT1 < 0 || clasglobal.cpctprms.CT1 >1000) { showError("CT1(S)", "0S", "1000S"); clasglobal.cpctprms.CT1 = 0; }
            if (e.ChangedItem.Label == "CT2")
                if (clasglobal.cpctprms.CT2 < 0 || clasglobal.cpctprms.CT2 > 1000) { showError("CT2(S)", "0S", "1000S"); clasglobal.cpctprms.CT2 = 0; }
            if (e.ChangedItem.Label == "CT3")
                if (clasglobal.cpctprms.CT3 < 0 || clasglobal.cpctprms.CT3 > 1000) { showError("CT3(S)", "0S", "1000S"); clasglobal.cpctprms.CT3 = 0; }
                            
        }

        //delegate void SetforecolorCallback(Color clr, int code, int state);
        //private void Setforecolor(Color clr, int code, int state)
        //{
        //    switch (code)
        //    {
        //        case 1:

        //            if (state == 1)
        //            {
        //                SetforecolorCallback d = new SetforecolorCallback(Setforecolor);
        //                this.Invoke(d, new object[] { clr, code, 2 });
        //            }
        //            else
        //            {
        //                mainf.toolStripStatusLabel5.ForeColor = clr;
        //            }
        //            break;
        //    }
        //}
        private void copyOneToolStripMenuItem_Click(object sender, EventArgs e)// هنگام راست کلیک copy گزینه 
        {
            if (rowsel > -1)
            {
                DataRow dr = dschart1.chartlist.NewRow();
                dr[0] = dschart1.chartlist.Rows.Count;
                for (int ms = 1; ms < dschart1.chartlist.Columns.Count; ms++)
                    dr[ms] = dschart1.chartlist.Rows[rowsel][ms];


                dschart1.Tables["chartlist"].Rows.Add(dr);
                dschart1.Tables["chartlist"].AcceptChanges();
                treeView2.Nodes[0].Nodes.Add(treeView2.Nodes[0].Nodes[rowsel].Text.ToString());
                treeView2.Refresh();
                rowsel = treeView2.Nodes[0].LastNode.Index;
                SetParamsFromDs(rowsel);
                clasglobal.set_gridCells(int.Parse(dschart1.Tables["chartlist"].Rows[rowsel][1].ToString()), this.pGrid);
                setcolorfortreev2();         



            }
        }
        private void copyMultiToolStripMenuItem_Click(object sender, EventArgs e)// هنگام راست کلیک copy multi گزینه 
        {
            if (rowsel > -1)
            {
                frmmulticopy2065 frm = new frmmulticopy2065();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    for (int ms = 1; ms <= int.Parse(frm.txtAdd.Text.Trim()); ms++)
                    {
                        DataRow dr = dschart1.chartlist.NewRow();
                        dr[0] = dschart1.chartlist.Rows.Count;
                        for (int ms1 = 1; ms1 < dschart1.chartlist.Columns.Count; ms1++)
                            dr[ms1] = dschart1.chartlist.Rows[rowsel][ms1];


                        dschart1.Tables["chartlist"].Rows.Add(dr);
                        dschart1.Tables["chartlist"].AcceptChanges();
                        treeView2.Nodes[0].Nodes.Add(treeView2.Nodes[0].Nodes[rowsel].Text.ToString());
                        treeView2.Refresh();
                        rowsel = treeView2.Nodes[0].LastNode.Index;
                        SetParamsFromDs(rowsel);
                        clasglobal.set_gridCells(int.Parse(dschart1.Tables["chartlist"].Rows[rowsel][1].ToString()), this.pGrid);
                        setcolorfortreev2();
                    
                    }
                }
                                
            }
           
        }
        private void saveTechniquesparametersToolStripMenuItem_Click(object sender, EventArgs e)// هنگام راست کلیک savetechnique گزینه 
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "prj";
            sfd.Filter = "Data Files(*.prj)|*.prj";
            sfd.ShowDialog();
            if (sfd.FileName != "")
                WriteToFile_project(sfd.FileName);
                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolstripChart_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public void WriteToFile_project(string fileName)//ذخیره در فایل پروژه هنگام راست کلیک
        {
            if (dschart1.chartlist.Rows.Count < 1)
            {
                MessageBox.Show("There are not techniques for SAVING ." + Environment.NewLine + "Please try again ");
                return;
            }

            StreamWriter Fl = new StreamWriter(fileName, false, Encoding.ASCII);

            for (int iii = 0; iii < dschart1.chartlist.Rows.Count; iii++)
            {
                switch (int.Parse(dschart1.chartlist.Rows[iii][1].ToString()))
                {
                    case 0:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: DCV");
                        //Fl.WriteLine("TchNo: "+ DCV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());
                        break;
                    case 1:
                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: NPV");
                        //Fl.WriteLine("TchNo: " + NPV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Pulse Width:" + double.Parse(dschart1.chartlist.Rows[iii][10].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 2:
                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: DPV");
                        //Fl.WriteLine("TchNo: " + DPV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Pulse Height:" + double.Parse(dschart1.chartlist.Rows[iii][11].ToString()));
                        Fl.WriteLine("Pulse Width:" + double.Parse(dschart1.chartlist.Rows[iii][10].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;


                    case 3:
                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: SWV");
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("Frequency:" + double.Parse(dschart1.chartlist.Rows[iii][12].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Pulse Height:" + double.Parse(dschart1.chartlist.Rows[iii][11].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 4:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CV");
                        //Fl.WriteLine("TchNo: " + CV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("Cycles:" + double.Parse(dschart1.chartlist.Rows[iii][2].ToString()));
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("E3:" + double.Parse(dschart1.chartlist.Rows[iii][5].ToString()));
                        Fl.WriteLine("Hold Time:0");
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("ScanRate_R:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;


                    case 5:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: LSV");
                        //Fl.WriteLine("TchNo: " + LSV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 6:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: DCS");
                        //Fl.WriteLine("TchNo: " + DCs.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;


                    case 7:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: DPS");
                        //Fl.WriteLine("TchNo: " + DPs.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Pulse Height:" + double.Parse(dschart1.chartlist.Rows[iii][11].ToString()));
                        Fl.WriteLine("Pulse Width:" + double.Parse(dschart1.chartlist.Rows[iii][10].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 8:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CCC");
                        //Fl.WriteLine("TchNo: " + CCC.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("Cycles:" + "1"); //clasglobal.cpcprms.Cycles.ToString());
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 9:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CPC");
                        //Fl.WriteLine("TchNo: " + CPC.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("Cycles:" + "1"); //clasglobal.cpcprms.Cycles.ToString());
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 10:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CHP");
                        //Fl.WriteLine("TchNo: " + CA.ToString());
                        Fl.WriteLine("I1:" + double.Parse(dschart1.chartlist.Rows[iii][17].ToString()));
                        Fl.WriteLine("I2:" + double.Parse(dschart1.chartlist.Rows[iii][18].ToString()));
                        Fl.WriteLine("EUP:" + double.Parse(dschart1.chartlist.Rows[iii][5].ToString()));
                        Fl.WriteLine("EDO:" + double.Parse(dschart1.chartlist.Rows[iii][13].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("T2:" + double.Parse(dschart1.chartlist.Rows[iii][16].ToString()));
                        Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 11:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CHA");
                        //Fl.WriteLine("TchNo: " + CA.ToString());
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("T2:" + double.Parse(dschart1.chartlist.Rows[iii][16].ToString()));
                        Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 12:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CHC");
                        //Fl.WriteLine("TchNo: " + CA.ToString());
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("T2:" + double.Parse(dschart1.chartlist.Rows[iii][16].ToString()));
                        Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;
                    case 14:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CD");
                        //Fl.WriteLine("TchNo: " + CA.ToString());
                        Fl.WriteLine("I1:" + double.Parse(dschart1.chartlist.Rows[iii][17].ToString()));
                        Fl.WriteLine("I2:" + double.Parse(dschart1.chartlist.Rows[iii][18].ToString()));

                        Fl.WriteLine("EUP:" + double.Parse(dschart1.chartlist.Rows[iii][5].ToString()));
                        Fl.WriteLine("EDO:" + double.Parse(dschart1.chartlist.Rows[iii][13].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("T2:" + double.Parse(dschart1.chartlist.Rows[iii][16].ToString()));
                        Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                        Fl.WriteLine("comment:" + dschart1.chartlist.Rows[iii]["comment"].ToString());

                        break;

                    case 15:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: OCP");
                        //Fl.WriteLine("TchNo: " + OCP.ToString());
                        Fl.WriteLine("Time:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("Cycles:" + "1"); //clasglobal.ocpprms.Cycles.ToString());
                        break;


                }
            }
            Fl.Close();
            MessageBox.Show(" save was successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void restoreTechniquesparametersToolStripMenuItem_Click(object sender, EventArgs e)//گزینه ریستور تکنیک هنگام راست کلیک
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //deletefile(;
            ofd.DefaultExt = "prj";
            ofd.Filter = "parameters of technique Files(*.prj)|*.prj";
            ofd.ShowDialog();
            if (ofd.FileName != "")
                ReadFromFile_project(ofd.FileName);
        }

        private void pGrid_Click(object sender, EventArgs e)
        {

        }

        private void rd_low_cpc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pGrid2_Click(object sender, EventArgs e)
        {

        }
    }
   

}