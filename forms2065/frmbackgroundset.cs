using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skydat.classes2065;
using System.IO.Ports;
using System.IO;
using Skydat;


namespace Skydat.forms2065
{
    public partial class frmbackgroundset : Form
    {
        object frmbase1;
        int typeform;
        public frmbackgroundset(object frm1,int typeform1)
        {
            InitializeComponent();
            frmbase1 = frm1;
            typeform = typeform1;
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "JPEG files |*.jpg;*.jpeg;|GIF files|*.gif|PNG Files|*.png| All files (*.*)|*.*";
            openFileDialog1.Title = "Select a image File";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Trim() != "") {
                pic1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void btnsetimage_Click(object sender, EventArgs e)
        {
            if (pic1.Image != null)
            {
                ((frmbase)frmbase1).pictureBox1.Image = null;

                try
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(Application.StartupPath + "\\back.jpg");

                    pic1.Image.Save(Application.StartupPath + "\\back.jpg");
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ((frmbase)frmbase1).pictureBox1.Image = pic1.Image;

                //if (typeform == 1)
                //    ((frmbase)frmbase1).BackgroundImage = pic1.Image;
                ////else
                ////    ((frmbaseadmin)frmbase1).BackgroundImage = pic1.Image;

                //var base64 = string.Empty;
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    pic1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //    base64 = Convert.ToBase64String(ms.ToArray());
                //}

                //if (typeform == 1)
                //    Properties.Settings.Default.backimg = base64;
                ////else
                ////    Properties.Settings.Default.backimgadmin = base64;

                //Properties.Settings.Default.Save();
                //if (typeform == 1)
                //    ((frmbase)frmbase1).Refresh();
                ////else
                ////    ((frmbaseadmin)frmbase1).Refresh();

                this.Close();
            }

        }

      

        
       
      

        
    }
}
