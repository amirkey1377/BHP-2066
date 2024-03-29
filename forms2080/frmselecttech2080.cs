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

namespace Skydat.forms2080
{
    public partial class frmselecttech2080 : Form
    {
        public frmselecttech2080()
        {
            InitializeComponent();
            treeView1.ExpandAll();

        }
        private int rowsel = -1;
        private byte Range;
        private double E1_Input = 0, E2_Input = 0;
        private classes2065.clasgraph gc = new classes2065.clasgraph();
        private bool showMessageError = false;

        public string portname = "COM2";
        public frmMain12080 mainf;
        public frmOption12080 opForm = new frmOption12080();
        public int BRate = 1000000, RT = 3000, WT = 1000;

        public void clearform()
        {
            this.treeView1.SelectedNode = this.treeView1.Nodes[0].FirstNode;
            this.treeView2.Nodes[0].Nodes.Clear();
            this.pGrid.SelectedObject = null;
            this.checkCPsCTs.Checked = false;
            this.radioButton1.Checked = true;
            this.dschart1.chartlist.Clear();
            this.mainf.grf1.clearform();


        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            clearform();
            // this.mainf.grf1.clearform();

        }


        private double set_complete_Comm(double X, double Min, double Max, byte Comm, int Zarib, string strVal, string Unit_)
        {
            if (X >= Min && X <= Max)
            {
                //  Set_Command(Comm, (int)(X * Zarib));
                if (Comm == 3) E1_Input = X;
                if (Comm == 4) E2_Input = X;
            }
            else
            {
                showError(strVal, Min.ToString() + Unit_, Max.ToString() + Unit_);
                X = Min;
            }
            return X;
        }

        private void showError(string err, string val1, string val2)
        {
            if (showMessageError)
                MessageBox.Show("Invalid Value of '" + err + "'" + (char)13 + "Value Must between '" + val1 + "' And '" + val2 + "'", "Parameter error");
            //detect_Error = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


            pGrid2.SelectedObject = clasglobal.cpctprms_ocp;
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

            if (this.dschart1.chartlist.Rows.Count == 0)
            {
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
                this.mainf.grf1.dschart1.chartlist1_run.Rows.Add(dr);
            }

            mainf.toolStripStatusLabel5.Text = "";
            mainf.tsocpTime.Text = " ";
            mainf.toolStripStatusLabel3.Text = " ";
            mainf.tabControl1.SelectedTab = mainf.tabControl1.TabPages[1];
            if (checkCPsCTs.Checked)
            {
                this.mainf.grf1.runf(Range, clasglobal.cpctprms_ocp.CP1, clasglobal.cpctprms_ocp.CT1, clasglobal.cpctprms_ocp.CP2, clasglobal.cpctprms_ocp.CT2, clasglobal.cpctprms_ocp.CP3, clasglobal.cpctprms_ocp.CT3, clasglobal.cpctprms_ocp.CP4, clasglobal.cpctprms_ocp.CT4, clasglobal.cpctprms_ocp.CP5, clasglobal.cpctprms_ocp.CT5);
            }
            else
            {
                this.mainf.grf1.runf(Range, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            }

        }

        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (Visible)
        //    {
        //        DialogResult dlgResult = MessageBox.Show("Save Parameter Settings?", "Caution", MessageBoxButtons.YesNoCancel);
        //        if (dlgResult == DialogResult.Yes)
        //        {
        //            WriteToFile("All", "");
        //            WriteToFile("APP", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\app.prj");
        //            //if (rbSequence.Checked)
        //            //WriteToFile("SEQ", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\SEQ.prj");
        //            e.Cancel = false;
        //        }
        //        if (dlgResult == DialogResult.No)
        //        {

        //            WriteToFile("APP", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\app.prj");
        //            e.Cancel = false;
        //        }
        //        if (dlgResult == DialogResult.Cancel)
        //            e.Cancel = true;
        //    }
        //}


        //private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        //{
        //    Save_is_Cancel = false;
        //}

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
                    DialogResult dd = MessageBox.Show("There are techniques for running .Do you want to add techniques to them ?" + Environment.NewLine + "Yes : running all techniques" + Environment.NewLine + "No : running techniques that are in selected file " + Environment.NewLine + "Cancel : running existing techniques", "", MessageBoxButtons.YesNoCancel);


                    if (dd == DialogResult.Yes)
                    {
                        num_row = treeView2.Nodes[0].Nodes.Count;
                    }
                    else if (dd == DialogResult.No)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0].FirstNode;
                        this.treeView2.Nodes[0].Nodes.Clear();
                        this.pGrid.SelectedObject = null;
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

                        if ((s.Substring(9) == "NPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                            try
                            {
                                this.mainf.grf.tech = clasglobal.NPV2080;
                                s = Fl.ReadLine(); clasglobal.npvprms_ocp.Cycles = Convert.ToDouble(s.Substring(6));
                                s = Fl.ReadLine(); //clasglobal.npvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
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
                                this.mainf.grf.tech = clasglobal.DPV2080;
                                s = Fl.ReadLine(); clasglobal.dpvprms_ocp.Cycles = Convert.ToDouble(s.Substring(6));
                                s = Fl.ReadLine(); //clasglobal.dpvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
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
                                this.mainf.grf.tech = clasglobal.SWV2080;
                                s = Fl.ReadLine(); clasglobal.swvprms_ocp.Cycles = Convert.ToDouble(s.Substring(6));
                                s = Fl.ReadLine(); //clasglobal.swvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
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
                                this.mainf.grf.tech = clasglobal.CV2080;
                                s = Fl.ReadLine(); clasglobal.cvprms_ocp.Cycles = Convert.ToDouble(s.Substring(6));
                                s = Fl.ReadLine(); //clasglobal.cvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
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
                                this.mainf.grf.tech = clasglobal.LSV2080;
                                s = Fl.ReadLine(); clasglobal.lsvprms_ocp.Cycles = Convert.ToDouble(s.Substring(6));
                                s = Fl.ReadLine(); //clasglobal.lsvprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
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


                        if ((s.Substring(9) == "OCP") && s.LastIndexOf("Technic") >= 0 && s != "")
                            try
                            {
                                this.mainf.grf.tech = clasglobal.OCP2080;
                                s = Fl.ReadLine(); clasglobal.ocpprms_ocp.Time = Convert.ToDouble(s.Substring(5));
                                s = "            ";
                            }
                            catch { }
                        if ((s.Substring(9) == "CPC") && s.LastIndexOf("Technic") >= 0 && s != "")
                            try
                            {
                                this.mainf.grf.tech = clasglobal.CPC2080;
                                s = Fl.ReadLine(); //clasglobal.cpcprms_ocp.CurrentRange = Convert.ToDouble(s.Substring(13));
                                s = Fl.ReadLine(); clasglobal.cpcprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine(); clasglobal.cpcprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                s = Fl.ReadLine(); clasglobal.cpcprms_ocp.T1 = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine();
                                if (s.Substring(7).Trim() == "False")
                                    clasglobal.cpcprms_ocp.vs_OCP = false;
                                else
                                    clasglobal.cpcprms_ocp.vs_OCP = true;
                                s = Fl.ReadLine(); clasglobal.cpcprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));

                                s = "            ";
                            }
                            catch { }


                        if ((s.Substring(9) == "CHA") && s.LastIndexOf("Technic") >= 0 && s != "")
                            try
                            {
                                this.mainf.grf.tech = clasglobal.CHA2080;
                                s = Fl.ReadLine(); clasglobal.chaprms_ocp.E1 = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine(); clasglobal.chaprms_ocp.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                                s = Fl.ReadLine(); clasglobal.chaprms_ocp.T1 = Convert.ToDouble(s.Substring(3));
                                s = Fl.ReadLine();
                                if (s.Substring(7).Trim() == "False")
                                    clasglobal.chaprms_ocp.vs_OCP = false;
                                else
                                    clasglobal.chaprms_ocp.vs_OCP = true;
                                s = Fl.ReadLine(); clasglobal.chaprms_ocp.OCPmeasurment = Convert.ToDouble(s.Substring(8));

                                s = "            ";
                            }
                            catch { }

                        flag1 = true;

                    }
                    if (flag1)
                    {
                        treeView2.Nodes[0].Nodes.Add(clasglobal.Tech_FullName2080[this.mainf.grf.tech]);
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
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.' && e.KeyChar != '-')
                e.KeyChar = (char)0;
        }


        private void treeView1_DoubleClick(object sender, EventArgs e)
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

                    case "NPV":
                        clasglobal.set_gridCells_ocp(clasglobal.NPV2080, this.pGrid);
                        techno = clasglobal.NPV2080;
                        break;
                    case "DPV":
                        clasglobal.set_gridCells_ocp(clasglobal.DPV2080, this.pGrid);
                        techno = clasglobal.DPV2080;
                        break;
                    case "SWV":
                        clasglobal.set_gridCells_ocp(clasglobal.SWV2080, this.pGrid);
                        techno = clasglobal.SWV2080;
                        break;
                    case "CV":
                        clasglobal.set_gridCells_ocp(clasglobal.CV2080, this.pGrid);
                        techno = clasglobal.CV2080;
                        break;
                    case "LSV":
                        clasglobal.set_gridCells_ocp(clasglobal.LSV2080, this.pGrid);
                        techno = clasglobal.LSV2080;
                        break;

                    case "CPC":
                        clasglobal.set_gridCells_ocp(clasglobal.CPC2080, this.pGrid);
                        techno = clasglobal.CPC2080;
                        break;

                    case "CHA":
                        clasglobal.set_gridCells_ocp(clasglobal.CHA2080, this.pGrid);
                        techno = clasglobal.CHA2080;
                        break;
                    case "OCP":
                        clasglobal.set_gridCells_ocp(clasglobal.OCP2080, this.pGrid);
                        techno = clasglobal.OCP2080;
                        break;

                }
                if (techno >= 0)
                {
                    dschart1.Tables["chartlist"].Rows.Add(dschart1.Tables["chartlist"].NewRow());
                    UdateDsFromParams(techno, dschart1.Tables["chartlist"].Rows.Count - 1, 1);
                    //this.grf.tech = byte.Parse(techno.ToString());
                }

            }

        }
        private void UdateDsFromParams(int TeqNo, int selectedRow, int typeact)
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
                dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = false;
                dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = "0";


                switch (TeqNo)
                {


                    case clasglobal.NPV2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.npvprms_ocp.Cycles.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.npvprms_ocp.E1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.npvprms_ocp.E2.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.npvprms_ocp.HStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.npvprms_ocp.PulseWidth.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.npvprms_ocp.EquilibriumTime.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.npvprms_ocp.ScanRate.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.npvprms_ocp.TStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = clasglobal.npvprms_ocp.vs_OCP.ToString(); ;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = clasglobal.npvprms_ocp.OCPmeasurment.ToString(); ;

                        break;
                    case clasglobal.DPV2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.dpvprms_ocp.Cycles.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.dpvprms_ocp.E1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.dpvprms_ocp.E2.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.dpvprms_ocp.HStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.dpvprms_ocp.PulseHeight.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["PW"] = clasglobal.dpvprms_ocp.PulseWidth.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.dpvprms_ocp.EquilibriumTime.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.dpvprms_ocp.ScanRate.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.dpvprms_ocp.TStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = clasglobal.dpvprms_ocp.vs_OCP.ToString(); ;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = clasglobal.dpvprms_ocp.OCPmeasurment.ToString(); ;

                        break;
                    case clasglobal.SWV2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.swvprms_ocp.Cycles.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.swvprms_ocp.E1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.swvprms_ocp.E2.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["FR"] = clasglobal.swvprms_ocp.Frequency.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.swvprms_ocp.HStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["PH"] = clasglobal.swvprms_ocp.PulseHeight.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.swvprms_ocp.EquilibriumTime.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.swvprms_ocp.ScanRate.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = clasglobal.swvprms_ocp.vs_OCP.ToString(); ;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = clasglobal.swvprms_ocp.OCPmeasurment.ToString(); ;

                        break;
                    case clasglobal.CV2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.cvprms_ocp.Cycles.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.cvprms_ocp.E1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.cvprms_ocp.E2.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E3"] = clasglobal.cvprms_ocp.E3.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.cvprms_ocp.HStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cvprms_ocp.EquilibriumTime.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.cvprms_ocp.ScanRate.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.cvprms_ocp.TStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = clasglobal.cvprms_ocp.vs_OCP.ToString(); ;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = clasglobal.cvprms_ocp.OCPmeasurment.ToString(); ;

                        break;
                    case clasglobal.LSV2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["CY"] = clasglobal.lsvprms_ocp.Cycles.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.lsvprms_ocp.E1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E2"] = clasglobal.lsvprms_ocp.E2.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = clasglobal.lsvprms_ocp.HStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.lsvprms_ocp.EquilibriumTime.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["SR"] = clasglobal.lsvprms_ocp.ScanRate.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["TS"] = clasglobal.lsvprms_ocp.TStep.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = clasglobal.lsvprms_ocp.vs_OCP.ToString(); ;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = clasglobal.lsvprms_ocp.OCPmeasurment.ToString(); ;

                        break;

                    case clasglobal.OCP2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.ocpprms_ocp.Time.ToString();

                        break;
                    case clasglobal.CPC2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.cpcprms_ocp.E1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.cpcprms_ocp.EquilibriumTime.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.cpcprms_ocp.T1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["HS"] = 1;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = clasglobal.cpcprms_ocp.vs_OCP.ToString(); ;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = clasglobal.cpcprms_ocp.OCPmeasurment.ToString(); ;

                        break;

                    case clasglobal.CHA2080:
                        dschart1.Tables["chartlist"].Rows[selectedRow]["E1"] = clasglobal.chaprms_ocp.E1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["QT"] = clasglobal.chaprms_ocp.EquilibriumTime.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["T1"] = clasglobal.chaprms_ocp.T1.ToString();
                        dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"] = clasglobal.chaprms_ocp.vs_OCP.ToString(); ;
                        dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"] = clasglobal.chaprms_ocp.OCPmeasurment.ToString(); ;

                        break;

                    default: break;
                }
                dschart1.Tables["chartlist"].AcceptChanges();
            }
            else
            {


            }
        }
        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                SetParamsFromDs(e.Node.Index);
                rowsel = e.Node.Index;
                setcolorfortreev2();
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
        private void pGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            pGrid.Refresh();
            showMessageError = true;
            switch (byte.Parse(this.dschart1.chartlist.Rows[rowsel][1].ToString()))
            {

                case clasglobal.NPV2080:
                    if (e.ChangedItem.Label == "Cycles")
                        if (clasglobal.npvprms_ocp.Cycles < 1 || clasglobal.npvprms_ocp.Cycles > 1000) { showError("Cycle(s)", "1", "1000"); clasglobal.npvprms_ocp.Cycles = 1; }
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.npvprms_ocp.E1 < -8 || clasglobal.npvprms_ocp.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.npvprms_ocp.E1 = 0; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.npvprms_ocp.E2 < -8 || clasglobal.npvprms_ocp.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.npvprms_ocp.E2 = 0; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.npvprms_ocp.ScanRate < 0.0001 || clasglobal.npvprms_ocp.ScanRate > 0.25) { showError("Scan Rate(V/S)", "0.0001V/S", "0.25V/S"); clasglobal.npvprms_ocp.ScanRate = 0.0001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.npvprms_ocp.HStep < 0.001 || clasglobal.npvprms_ocp.HStep > 0.025) { showError("HStep(mV)", "0.001mV", "0.025mV"); clasglobal.npvprms_ocp.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.npvprms_ocp.TStep < 0.1 || clasglobal.npvprms_ocp.TStep > 10) { showError("TStep(S)", "0.1S", "10S"); clasglobal.npvprms_ocp.TStep = 0.1; }
                    if (e.ChangedItem.Label == "Quite Time")
                        if (clasglobal.npvprms_ocp.EquilibriumTime < 0 || clasglobal.npvprms_ocp.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.npvprms_ocp.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "Pulse Width")
                        if (clasglobal.npvprms_ocp.PulseWidth < 0.05 || clasglobal.npvprms_ocp.PulseWidth > (clasglobal.npvprms_ocp.TStep - 0.05))
                        {
                            showError("Pulse Width(S)", "0.05S", clasglobal.npvprms_ocp.TStep - 0.05 + "(TStep-0.05S)");
                            clasglobal.npvprms_ocp.PulseWidth = Math.Round(clasglobal.npvprms_ocp.TStep - 0.05, 5);
                        }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.npvprms_ocp.ScanRate != 0) clasglobal.npvprms_ocp.TStep = Math.Round(clasglobal.npvprms_ocp.HStep / clasglobal.npvprms_ocp.ScanRate, 5);
                        if (clasglobal.npvprms_ocp.TStep < 0.1 || clasglobal.npvprms_ocp.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.npvprms_ocp.TStep.ToString() + "!!! \nTStep < 0.1 OR TStep > 10");
                            clasglobal.npvprms_ocp.HStep = 0.01;
                            clasglobal.npvprms_ocp.ScanRate = 0.1;
                            clasglobal.npvprms_ocp.TStep = 0.1;
                        }
                    }
                    break;

                case clasglobal.DPV2080:
                    if (e.ChangedItem.Label == "Cycles")
                        if (clasglobal.dpvprms_ocp.Cycles < 1 || clasglobal.dpvprms_ocp.Cycles > 1000) { showError("Cycle(s)", "1", "1000"); clasglobal.dpvprms_ocp.Cycles = 1; }
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.dpvprms_ocp.E1 < -8 || clasglobal.dpvprms_ocp.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.dpvprms_ocp.E1 = 0; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.dpvprms_ocp.E2 < -8 || clasglobal.dpvprms_ocp.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.dpvprms_ocp.E2 = 0; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.dpvprms_ocp.ScanRate < 0.0001 || clasglobal.dpvprms_ocp.ScanRate > 0.25) { showError("Scan Rate(V/S)", "0.0001V/S", "0.25V/S"); clasglobal.dpvprms_ocp.ScanRate = 0.001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.dpvprms_ocp.HStep < 0.001 || clasglobal.dpvprms_ocp.HStep > 0.025) { showError("HStep(mV)", "0.001mV", "0.025mV"); clasglobal.dpvprms_ocp.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.dpvprms_ocp.TStep < 0.1 || clasglobal.dpvprms_ocp.TStep > 10) { showError("TStep(S)", "0.1S", "10S"); clasglobal.dpvprms_ocp.TStep = 0.1; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.dpvprms_ocp.EquilibriumTime < 0 || clasglobal.dpvprms_ocp.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.dpvprms_ocp.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "Pulse Width")
                        if (clasglobal.dpvprms_ocp.PulseWidth < 0.05 || clasglobal.dpvprms_ocp.PulseWidth > (clasglobal.dpvprms_ocp.TStep - 0.05))
                        {
                            showError("Pulse Width(S)", "0.05S", clasglobal.dpvprms_ocp.TStep - 0.05 + "(TStep-0.05S)");
                            clasglobal.dpvprms_ocp.PulseWidth = Math.Round(clasglobal.dpvprms_ocp.TStep - 0.05, 5);
                        }
                    if (e.ChangedItem.Label == "Pulse Height")
                        if (clasglobal.dpvprms_ocp.PulseHeight < 0.001 || clasglobal.dpvprms_ocp.PulseHeight > 0.250) { showError("Pulse Height(mV)", "0.001mV", "0.250mV"); clasglobal.dpvprms_ocp.PulseHeight = 0.001; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.dpvprms_ocp.ScanRate != 0) clasglobal.dpvprms_ocp.TStep = Math.Round(clasglobal.dpvprms_ocp.HStep / clasglobal.dpvprms_ocp.ScanRate, 5);
                        if (clasglobal.dpvprms_ocp.TStep < 0.1 || clasglobal.dpvprms_ocp.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.dpvprms_ocp.TStep.ToString() + "!!! \nTStep < 0.1 OR TStep > 10\n");
                            clasglobal.dpvprms_ocp.HStep = 0.01;
                            clasglobal.dpvprms_ocp.ScanRate = 0.1;
                            clasglobal.dpvprms_ocp.TStep = 0.1;
                        }
                    }
                    break;

                case clasglobal.SWV2080:
                    if (e.ChangedItem.Label == "Cycles")
                        if (clasglobal.swvprms_ocp.Cycles < 1 || clasglobal.swvprms_ocp.Cycles > 1000) { showError("Cycle(s)", "1", "1000"); clasglobal.swvprms_ocp.Cycles = 1; }
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.swvprms_ocp.E1 < -8 || clasglobal.swvprms_ocp.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.swvprms_ocp.E1 = 1; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.swvprms_ocp.E2 < -8 || clasglobal.swvprms_ocp.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.swvprms_ocp.E2 = 1; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.swvprms_ocp.ScanRate < 0.001 || clasglobal.swvprms_ocp.ScanRate > 25) { showError("Scan Rate(V/S)", "0.001V/S", "25V/S"); clasglobal.swvprms_ocp.ScanRate = 0.001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.swvprms_ocp.HStep < 0.001 || clasglobal.swvprms_ocp.HStep > 0.025) { showError("HStep(mV)", "0.001mV", "0.025mV"); clasglobal.swvprms_ocp.HStep = 0.001; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.swvprms_ocp.EquilibriumTime < 0 || clasglobal.swvprms_ocp.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.swvprms_ocp.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "Pulse Height")
                        if (clasglobal.swvprms_ocp.PulseHeight < 0.001 || clasglobal.swvprms_ocp.PulseHeight > 0.250) { showError("Pulse Height(mV)", "0.001mV", "0.250mV"); clasglobal.swvprms_ocp.PulseHeight = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate") if (clasglobal.swvprms_ocp.HStep != 0) clasglobal.swvprms_ocp.Frequency = Math.Round(clasglobal.swvprms_ocp.ScanRate / clasglobal.swvprms_ocp.HStep, 5);
                    if (clasglobal.swvprms_ocp.Frequency < 1 || clasglobal.swvprms_ocp.Frequency > 1000)
                    {
                        MessageBox.Show("Frequency= " + clasglobal.swvprms_ocp.Frequency.ToString() + "!!! \nFrequency < 1 OR Frequency > 1000\n");
                        clasglobal.swvprms_ocp.HStep = 0.01;
                        clasglobal.swvprms_ocp.ScanRate = 0.1;
                        clasglobal.swvprms_ocp.Frequency = 10;
                    }
                    break;

                case clasglobal.CV2080:
                    if (e.ChangedItem.Label == "Cycles")
                        if (clasglobal.cvprms_ocp.Cycles < 1 || clasglobal.cvprms_ocp.Cycles > 1000) { showError("Cycle(s)", "1", "1000"); clasglobal.cvprms_ocp.Cycles = 1; }
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.cvprms_ocp.E1 < -8 || clasglobal.cvprms_ocp.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.cvprms_ocp.E1 = 1; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.cvprms_ocp.E2 < -8 || clasglobal.cvprms_ocp.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.cvprms_ocp.E2 = 1; }
                    if (e.ChangedItem.Label == "E3")
                        if (clasglobal.cvprms_ocp.E3 < -8 || clasglobal.cvprms_ocp.E3 > 8) { showError("E3(V)", "-8V", "8V"); clasglobal.cvprms_ocp.E3 = 1; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.cvprms_ocp.ScanRate < 0.0001 || clasglobal.cvprms_ocp.ScanRate > 250) { showError("Scan Rate(V/S)", "0.0001V/S", "250V/S"); clasglobal.cvprms_ocp.ScanRate = 0.0001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.cvprms_ocp.HStep < 0.001 || clasglobal.cvprms_ocp.HStep > 0.025) { showError("HStep(mV)", "0.001mV", "0.025mV"); clasglobal.cvprms_ocp.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.cvprms_ocp.TStep < 0.0001 || clasglobal.cvprms_ocp.TStep > 10) { showError("TStep(S)", "0.0001S", "10S"); clasglobal.cvprms_ocp.TStep = 0.0001; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.cvprms_ocp.EquilibriumTime < 0 || clasglobal.cvprms_ocp.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.cvprms_ocp.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.cvprms_ocp.ScanRate != 0)
                        {
                            clasglobal.cvprms_ocp.TStep = Math.Round(clasglobal.cvprms_ocp.HStep / clasglobal.cvprms_ocp.ScanRate, 5);
                        }
                        if (clasglobal.cvprms_ocp.TStep < 0.0001 || clasglobal.cvprms_ocp.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.cvprms_ocp.TStep.ToString() + "!!! \nTStep < 0.0001 OR TStep > 10\n");
                            clasglobal.cvprms_ocp.HStep = 0.01;
                            clasglobal.cvprms_ocp.ScanRate = 0.1;
                            clasglobal.cvprms_ocp.TStep = 0.1;
                        }
                    }
                    if (e.ChangedItem.Label == "E1") clasglobal.cvprms_ocp.E3 = clasglobal.cvprms_ocp.E1;
                    break;

                case clasglobal.LSV2080:
                    if (e.ChangedItem.Label == "Cycles")
                        if (clasglobal.lsvprms_ocp.Cycles < 1 || clasglobal.lsvprms_ocp.Cycles > 1000) { showError("Cycle(s)", "1", "1000"); clasglobal.lsvprms_ocp.Cycles = 1; }
                    if (e.ChangedItem.Label == "E1")
                        if (clasglobal.lsvprms_ocp.E1 < -8 || clasglobal.lsvprms_ocp.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.lsvprms_ocp.E1 = 1; }
                    if (e.ChangedItem.Label == "E2")
                        if (clasglobal.lsvprms_ocp.E2 < -8 || clasglobal.lsvprms_ocp.E2 > 8) { showError("E2(V)", "-8V", "8V"); clasglobal.lsvprms_ocp.E2 = 1; }
                    if (e.ChangedItem.Label == "Scan Rate")
                        if (clasglobal.lsvprms_ocp.ScanRate < 0.0001 || clasglobal.lsvprms_ocp.ScanRate > 250) { showError("Scan Rate(V/S)", "0.0001V/S", "250V/S"); clasglobal.lsvprms_ocp.ScanRate = 0.0001; }
                    if (e.ChangedItem.Label == "HStep")
                        if (clasglobal.lsvprms_ocp.HStep < 0.001 || clasglobal.lsvprms_ocp.HStep > 0.025) { showError("HStep(mV)", "0.001mV", "0.025mV"); clasglobal.lsvprms_ocp.HStep = 0.001; }
                    if (e.ChangedItem.Label == "TStep")
                        if (clasglobal.lsvprms_ocp.TStep < 0.0001 || clasglobal.lsvprms_ocp.TStep > 10) { showError("TStep(S)", "0.0001S", "10S"); clasglobal.lsvprms_ocp.TStep = 0.0001; }
                    if (e.ChangedItem.Label == "Equilibrium Time")
                        if (clasglobal.lsvprms_ocp.EquilibriumTime < 0 || clasglobal.lsvprms_ocp.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.lsvprms_ocp.EquilibriumTime = 0; }
                    if (e.ChangedItem.Label == "HStep" || e.ChangedItem.Label == "Scan Rate")
                    {
                        if (clasglobal.lsvprms_ocp.ScanRate != 0) clasglobal.lsvprms_ocp.TStep = Math.Round(clasglobal.lsvprms_ocp.HStep / clasglobal.lsvprms_ocp.ScanRate, 5);
                        if (clasglobal.lsvprms_ocp.TStep < 0.0001 || clasglobal.lsvprms_ocp.TStep > 10)
                        {
                            MessageBox.Show("TStep= " + clasglobal.lsvprms_ocp.TStep.ToString() + "!!! \nTStep < 0.0001 OR TStep > 10\n");
                            clasglobal.lsvprms_ocp.HStep = 0.01;
                            clasglobal.lsvprms_ocp.ScanRate = 0.1;
                            clasglobal.lsvprms_ocp.TStep = 0.1;
                        }
                    }
                    break;

                case clasglobal.OCP2080:
                    if (clasglobal.ocpprms_ocp.Time < 1 || clasglobal.ocpprms_ocp.Time > 65000) { showError("Time(S)", "1V", "65000V"); clasglobal.ocpprms_ocp.Time = 1; }
                    break;


                case clasglobal.CPC2080:
                    if (clasglobal.cpcprms_ocp.E1 < -8 || clasglobal.cpcprms_ocp.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.cpcprms_ocp.E1 = 0; }
                    if (clasglobal.cpcprms_ocp.T1 < 0 || clasglobal.cpcprms_ocp.T1 > 65000) { showError("T1(V)", "0V", "65000V"); clasglobal.cpcprms_ocp.T1 = 0; }
                    if (clasglobal.cpcprms_ocp.EquilibriumTime < 0 || clasglobal.cpcprms_ocp.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.cpcprms_ocp.EquilibriumTime = 0; }
                    break;


                case clasglobal.CHA2080:
                    if (clasglobal.chaprms_ocp.E1 < -8 || clasglobal.chaprms_ocp.E1 > 8) { showError("E1(V)", "-8V", "8V"); clasglobal.chaprms_ocp.E1 = 0; }
                    if (clasglobal.chaprms_ocp.T1 < 0 || clasglobal.chaprms_ocp.T1 > 65000) { showError("T1(S)", "0S", "65000S"); clasglobal.chaprms_ocp.T1 = 0; }
                    if (clasglobal.chaprms_ocp.EquilibriumTime < 0 || clasglobal.chaprms_ocp.EquilibriumTime > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); clasglobal.chaprms_ocp.EquilibriumTime = 0; }
                    break;


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
        private void SetParamsFromDs(int selectedRow)
        {
            try
            {
                int TeqNo = int.Parse(dschart1.Tables["chartlist"].Rows[selectedRow][1].ToString());
                switch (TeqNo)
                {
                    case clasglobal.NPV2080:
                        clasglobal.npvprms_ocp.Cycles = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.npvprms_ocp.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.npvprms_ocp.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.npvprms_ocp.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.npvprms_ocp.PulseWidth = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PW"].ToString());
                        clasglobal.npvprms_ocp.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.npvprms_ocp.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.npvprms_ocp.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "0" || dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "False")
                            clasglobal.npvprms_ocp.vs_OCP = false;
                        else
                            clasglobal.npvprms_ocp.vs_OCP = true;

                        clasglobal.npvprms_ocp.OCPmeasurment = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"].ToString());

                        break;
                    case clasglobal.DPV2080:
                        clasglobal.dpvprms_ocp.Cycles = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.dpvprms_ocp.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.dpvprms_ocp.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.dpvprms_ocp.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.dpvprms_ocp.PulseHeight = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PH"].ToString());
                        clasglobal.dpvprms_ocp.PulseWidth = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PW"].ToString());
                        clasglobal.dpvprms_ocp.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.dpvprms_ocp.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.dpvprms_ocp.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "0" || dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "False")
                            clasglobal.dpvprms_ocp.vs_OCP = false;
                        else
                            clasglobal.dpvprms_ocp.vs_OCP = true;

                        clasglobal.dpvprms_ocp.OCPmeasurment = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"].ToString());

                        break;
                    case clasglobal.SWV2080:
                        clasglobal.swvprms_ocp.Cycles = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.swvprms_ocp.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.swvprms_ocp.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.swvprms_ocp.Frequency = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["FR"].ToString());
                        clasglobal.swvprms_ocp.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.swvprms_ocp.PulseHeight = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["PH"].ToString());
                        clasglobal.swvprms_ocp.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.swvprms_ocp.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "0" || dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "False")
                            clasglobal.swvprms_ocp.vs_OCP = false;
                        else
                            clasglobal.swvprms_ocp.vs_OCP = true;

                        clasglobal.swvprms_ocp.OCPmeasurment = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"].ToString());

                        break;
                    case clasglobal.CV2080:
                        clasglobal.cvprms_ocp.Cycles = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.cvprms_ocp.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.cvprms_ocp.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.cvprms_ocp.E3 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E3"].ToString());
                        clasglobal.cvprms_ocp.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.cvprms_ocp.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.cvprms_ocp.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.cvprms_ocp.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "0" || dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "False")
                            clasglobal.cvprms_ocp.vs_OCP = false;
                        else
                            clasglobal.cvprms_ocp.vs_OCP = true;

                        clasglobal.cvprms_ocp.OCPmeasurment = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"].ToString());

                        break;
                    case clasglobal.LSV2080:
                        clasglobal.lsvprms_ocp.Cycles = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["CY"].ToString());
                        clasglobal.lsvprms_ocp.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.lsvprms_ocp.E2 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E2"].ToString());
                        clasglobal.lsvprms_ocp.HStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["HS"].ToString());
                        clasglobal.lsvprms_ocp.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.lsvprms_ocp.ScanRate = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["SR"].ToString());
                        clasglobal.lsvprms_ocp.TStep = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["TS"].ToString());
                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "0" || dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "False")
                            clasglobal.lsvprms_ocp.vs_OCP = false;
                        else
                            clasglobal.lsvprms_ocp.vs_OCP = true;

                        clasglobal.lsvprms_ocp.OCPmeasurment = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"].ToString());

                        break;

                    case clasglobal.CPC2080:
                        clasglobal.cpcprms_ocp.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.cpcprms_ocp.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.cpcprms_ocp.T1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "0" || dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "False")
                            clasglobal.cpcprms_ocp.vs_OCP = false;
                        else
                            clasglobal.cpcprms_ocp.vs_OCP = true;

                        clasglobal.cpcprms_ocp.OCPmeasurment = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"].ToString());

                        break;

                    case clasglobal.CHA2080:
                        clasglobal.chaprms_ocp.E1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["E1"].ToString());
                        clasglobal.chaprms_ocp.EquilibriumTime = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["QT"].ToString());
                        clasglobal.chaprms_ocp.T1 = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        if (dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "0" || dschart1.Tables["chartlist"].Rows[selectedRow]["vs_OCP"].ToString() == "False")
                            clasglobal.chaprms_ocp.vs_OCP = false;
                        else
                            clasglobal.chaprms_ocp.vs_OCP = true;

                        clasglobal.chaprms_ocp.OCPmeasurment = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["OCPmeas"].ToString());
                        break;

                    case clasglobal.OCP2080:
                        clasglobal.ocpprms_ocp.Time = Convert.ToDouble(dschart1.Tables["chartlist"].Rows[selectedRow]["T1"].ToString());
                        break;
                    default: break;
                }
                clasglobal.set_gridCells_ocp(TeqNo, this.pGrid);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
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
                    clasglobal.set_gridCells_ocp(int.Parse(dschart1.Tables["chartlist"].Rows[rowsel][1].ToString()), this.pGrid);
                    setcolorfortreev2();
                }
                else
                    rowsel = -1;



            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                if (treeView1.SelectedNode.Level > 0)
                {

                    switch (treeView1.SelectedNode.Index)
                    {
                        case 0:
                            label1.Text = "Normal Pulse Voltammetry";
                            label2.Text = "in this technique, the current resulting from a series of ever larger potential pulses is compared with the current at a constant 'baseline' voltage.\n";
                            label2.Text += "this technique has following properties:\nE1\nE2\nHstep\nTstep\nScan Rate\nPulse Width\nEquilibrium Time";
                            break;
                        case 1:
                            label1.Text = "Differential Pulse Voltammetry";
                            label2.Text = " this technique can be considered as a derivative of linear sweep voltammetry , with a series of regular voltage pulses superimposed on the potential linear sweep or stairsteps. The current is measured immediately before each potential change, and the current difference is plotted as a function of potential.";
                            label2.Text += "this technique has following properties:\nE1\nE2\nHstep\nTstep\nScan Rate\nPulse Width\nPulse Height\nEquilibrium Time";
                            break;
                        case 2:
                            label1.Text = "Square Wave Voltammetry";
                            label2.Text = "In this technique , the current at a working electrode is measured while the potential between the working electrode and a reference electrode is swept linearly in time. The current is sampled at two times - once at the end of the forward potential pulse and again at the end of the reverse potential pulse";
                            label2.Text += "this technique has following properties:\nE1\nE2\nFrequency\nHstep\nScan Rate\nPulse Width\nEquilibrium Time";
                            break;
                        case 3:
                            label1.Text = "Cyclic Voltammetry";
                            label2.Text = "this technique measures the current that develops in an electrochemical cell under conditions where voltage is in excess of that predicted by the Nernst equation. CV is performed by cycling the potential of a working electrode, and measuring the resulting current.";
                            label2.Text += "this technique has following properties:\nE1\nE2\nE3\nHstep\nTstep\nScan Rate\nHold Time\nEquilibrium Time";
                            break;
                        case 4:
                            label1.Text = "Linear Sweep Voltammetry";
                            label2.Text = "in this technique, the current at a working electrode is measured while the potential between the working electrode and a reference electrode is swept linearly in time. Oxidation or reduction of species is registered as a peak or trough in the current signal at the potential at which the species begins to be oxidized or reduced.";
                            label2.Text += "this technique has following properties:\nE1\nE2\nHstep\nTstep\nScan Rate\nEquilibrium Time";
                            break;
                        case 5:
                            label1.Text = "Open Circuit Potential";
                            label2.Text = "this technique refers to the difference that exists in electrical potential. It normally occurs between two device terminals when detached from a circuit involving no external load.It is the potential in a working electrode comparative to the electrode in reference when there is no current or potential existing in the cell. Once a potential relative to the open circuit is made present, the entire system gauges the potential of the open circuit prior to turning on the cell.\n";
                            label2.Text += "this technique has following properties:\ntime";
                            break;
                        case 6:
                            label1.Text = "Controlled Potential Coloumetry";
                            label2.Text = "Coulometry is based on an exhaustive electrolysis of the analyte. By exhaustive , analyte is completely oxidized or reduced at the working electrode or that it reacts completely with a reagent generated at the working electrode.\ncontrolled-potential coulometry, in which we apply a constant potential to the electrochemical cell.\n";
                            label2.Text += "this technique has following properties:\nE1\nT1\nEquilibrium Time";

                            break;
                        case 7:
                            label1.Text = "Chrono Amperometry";
                            label2.Text = "this technique is an electrochemical technique in which the potential of the working electrode is stepped and the resulting current from faradaic processes occurring at the electrode (caused by the potential step) is monitored as a function of time.\n";
                            label2.Text += "this technique has following properties:\nE1\nT1\nEquilibrium Time";

                            break;

                        default:
                            label1.Text = "";
                            label2.Text = "";
                            break;
                    }
                }
                else
                {
                    label1.Text = "";
                    label2.Text = "";

                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void checkCPsCTs_CheckedChanged(object sender, EventArgs e)
        {
            pGrid2.Enabled = (checkCPsCTs.Checked);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
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

        private void pGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            pGrid2.Refresh();
            showMessageError = true;
            if (e.ChangedItem.Label == "CP1")
                if (clasglobal.cpctprms_ocp.CP1 < -8 || clasglobal.cpctprms_ocp.CP1 > 8) { showError("CP1(V)", "-8V", "8V"); clasglobal.cpctprms_ocp.CP1 = 0; }
            if (e.ChangedItem.Label == "CP2")
                if (clasglobal.cpctprms_ocp.CP2 < -8 || clasglobal.cpctprms_ocp.CP2 > 8) { showError("CP2(V)", "-8V", "8V"); clasglobal.cpctprms_ocp.CP2 = 0; }
            if (e.ChangedItem.Label == "CP3")
                if (clasglobal.cpctprms_ocp.CP3 < -8 || clasglobal.cpctprms_ocp.CP3 > 8) { showError("CP3(V)", "-8V", "8V"); clasglobal.cpctprms_ocp.CP3 = 0; }
            if (e.ChangedItem.Label == "CP4")
                if (clasglobal.cpctprms_ocp.CP4 < -8 || clasglobal.cpctprms_ocp.CP4 > 8) { showError("CP4(V)", "-8V", "8V"); clasglobal.cpctprms_ocp.CP4 = 0; }
            if (e.ChangedItem.Label == "CP5")
                if (clasglobal.cpctprms_ocp.CP5 < -8 || clasglobal.cpctprms_ocp.CP5 > 8) { showError("CP5(V)", "-8V", "8V"); clasglobal.cpctprms_ocp.CP5 = 0; }

            if (e.ChangedItem.Label == "CT1")
                if (clasglobal.cpctprms_ocp.CT1 < 0 || clasglobal.cpctprms_ocp.CT1 > 1000) { showError("CT1(S)", "0S", "1000S"); clasglobal.cpctprms_ocp.CT1 = 0; }
            if (e.ChangedItem.Label == "CT2")
                if (clasglobal.cpctprms_ocp.CT2 < 0 || clasglobal.cpctprms_ocp.CT2 > 1000) { showError("CT2(S)", "0S", "1000S"); clasglobal.cpctprms_ocp.CT2 = 0; }
            if (e.ChangedItem.Label == "CT3")
                if (clasglobal.cpctprms_ocp.CT3 < 0 || clasglobal.cpctprms_ocp.CT3 > 1000) { showError("CT3(S)", "0S", "1000S"); clasglobal.cpctprms_ocp.CT3 = 0; }
            if (e.ChangedItem.Label == "CT4")
                if (clasglobal.cpctprms_ocp.CT4 < 0 || clasglobal.cpctprms_ocp.CT4 > 1000) { showError("CT4(S)", "0S", "1000S"); clasglobal.cpctprms_ocp.CT4 = 0; }
            if (e.ChangedItem.Label == "CT5")
                if (clasglobal.cpctprms_ocp.CT5 < 0 || clasglobal.cpctprms_ocp.CT5 > 1000) { showError("CT5(S)", "0S", "1000S"); clasglobal.cpctprms_ocp.CT5 = 0; }

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
                        mainf.toolStripStatusLabel5.ForeColor = clr;
                    }
                    break;
            }
        }

        private void copyOneToolStripMenuItem_Click(object sender, EventArgs e)
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
                clasglobal.set_gridCells_ocp(int.Parse(dschart1.Tables["chartlist"].Rows[rowsel][1].ToString()), this.pGrid);
                setcolorfortreev2();



            }
        }

        private void copyMultiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowsel > -1)
            {
                frmmulticopy2080 frm = new frmmulticopy2080();
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
                        clasglobal.set_gridCells_ocp(int.Parse(dschart1.Tables["chartlist"].Rows[rowsel][1].ToString()), this.pGrid);
                        setcolorfortreev2();

                    }



                }




            }

        }

        private void saveTechniquesparametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "prj";
            sfd.Filter = "Data Files(*.prj)|*.prj";
            sfd.ShowDialog();
            if (sfd.FileName != "")
                WriteToFile_project(sfd.FileName);
        }

        public void WriteToFile_project(string fileName)
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

                    case clasglobal.NPV2080:
                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: NPV");
                        Fl.WriteLine("Cycle:" + dschart1.chartlist.Rows[iii]["CY"].ToString());
                        //Fl.WriteLine("TchNo: " + NPV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Pulse Width:" + double.Parse(dschart1.chartlist.Rows[iii][10].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("vs_OCP:" + dschart1.chartlist.Rows[iii]["vs_OCP"].ToString());
                        Fl.WriteLine("OCPmeas:" + double.Parse(dschart1.chartlist.Rows[iii]["OCPmeas"].ToString()));

                        break;

                    case clasglobal.DPV2080:
                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: DPV");
                        Fl.WriteLine("Cycle:" + dschart1.chartlist.Rows[iii]["CY"].ToString());
                        //Fl.WriteLine("TchNo: " + DPV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + clasglobal.dpvprms_ocp.HStep.ToString());
                        Fl.WriteLine("Pulse Height:" + clasglobal.dpvprms_ocp.PulseHeight.ToString());
                        Fl.WriteLine("Pulse Width:" + clasglobal.dpvprms_ocp.PulseWidth.ToString());
                        Fl.WriteLine("Equilibrium Time:" + clasglobal.dpvprms_ocp.EquilibriumTime.ToString());
                        Fl.WriteLine("Scan Rate:" + clasglobal.dpvprms_ocp.ScanRate.ToString());
                        Fl.WriteLine("TStep:" + clasglobal.dpvprms_ocp.TStep.ToString());
                        Fl.WriteLine("vs_OCP:" + dschart1.chartlist.Rows[iii]["vs_OCP"].ToString());
                        Fl.WriteLine("OCPmeas:" + double.Parse(dschart1.chartlist.Rows[iii]["OCPmeas"].ToString()));

                        break;


                    case clasglobal.SWV2080:
                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: SWV");
                        Fl.WriteLine("Cycle:" + dschart1.chartlist.Rows[iii]["CY"].ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("Frequency:" + double.Parse(dschart1.chartlist.Rows[iii][12].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Pulse Height:" + double.Parse(dschart1.chartlist.Rows[iii][11].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("vs_OCP:" + dschart1.chartlist.Rows[iii]["vs_OCP"].ToString());
                        Fl.WriteLine("OCPmeas:" + double.Parse(dschart1.chartlist.Rows[iii]["OCPmeas"].ToString()));


                        break;

                    case clasglobal.CV2080:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CV");
                        Fl.WriteLine("Cycle:" + dschart1.chartlist.Rows[iii]["CY"].ToString());
                        //Fl.WriteLine("TchNo: " + CV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("E3:" + double.Parse(dschart1.chartlist.Rows[iii][5].ToString()));
                        Fl.WriteLine("Hold Time:0");
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("ScanRate_R:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("vs_OCP:" + dschart1.chartlist.Rows[iii]["vs_OCP"].ToString());
                        Fl.WriteLine("OCPmeas:" + double.Parse(dschart1.chartlist.Rows[iii]["OCPmeas"].ToString()));

                        break;


                    case clasglobal.LSV2080:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: LSV");
                        Fl.WriteLine("Cycle:" + dschart1.chartlist.Rows[iii]["CY"].ToString());
                        //Fl.WriteLine("TchNo: " + LSV.ToString());
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("E2:" + double.Parse(dschart1.chartlist.Rows[iii][4].ToString()));
                        Fl.WriteLine("HStep:" + double.Parse(dschart1.chartlist.Rows[iii][6].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("Scan Rate:" + double.Parse(dschart1.chartlist.Rows[iii][8].ToString()));
                        Fl.WriteLine("TStep:" + double.Parse(dschart1.chartlist.Rows[iii][9].ToString()));
                        Fl.WriteLine("vs_OCP:" + dschart1.chartlist.Rows[iii]["vs_OCP"].ToString());
                        Fl.WriteLine("OCPmeas:" + double.Parse(dschart1.chartlist.Rows[iii]["OCPmeas"].ToString()));

                        break;

                    case clasglobal.CPC2080:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CPC");
                        Fl.WriteLine("Current Range:0");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("vs_OCP:" + dschart1.chartlist.Rows[iii]["vs_OCP"].ToString());
                        Fl.WriteLine("OCPmeas:" + double.Parse(dschart1.chartlist.Rows[iii]["OCPmeas"].ToString()));

                        break;


                    case 11:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: CHA");
                        Fl.WriteLine("E1:" + double.Parse(dschart1.chartlist.Rows[iii][3].ToString()));
                        Fl.WriteLine("Equilibrium Time:" + double.Parse(dschart1.chartlist.Rows[iii][7].ToString()));
                        Fl.WriteLine("T1:" + double.Parse(dschart1.chartlist.Rows[iii][15].ToString()));
                        Fl.WriteLine("vs_OCP:" + dschart1.chartlist.Rows[iii]["vs_OCP"].ToString());
                        Fl.WriteLine("OCPmeas:" + double.Parse(dschart1.chartlist.Rows[iii]["OCPmeas"].ToString()));

                        break;


                    case 15:

                        Fl.WriteLine("\n");
                        Fl.WriteLine("Technic: OCP");
                        Fl.WriteLine("Time:" + clasglobal.ocpprms_ocp.Time.ToString());
                        break;


                }
            }
            Fl.Close();

        }

        private void restoreTechniquesparametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //deletefile(;
            ofd.DereferenceLinks = false;
            ofd.AutoUpgradeEnabled = false;
            ofd.DefaultExt = "prj";
            ofd.Filter = "parameters of technique Files(*.prj)|*.prj";
            ofd.ShowDialog();
            if (ofd.FileName != "")
                ReadFromFile_project(ofd.FileName);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmPortOption2080 frm = new frmPortOption2080();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.baudrate = frm.txtBRate.Text.Trim();
                Properties.Settings.Default.portname = frm.cmbPrtName.Text.Trim();
                Properties.Settings.Default.rtport = frm.txtRT.Text.Trim();
                Properties.Settings.Default.wtport = frm.txtWT.Text.Trim();
                Properties.Settings.Default.Save();
            }
        }

    }

}