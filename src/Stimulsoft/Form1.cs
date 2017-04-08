using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Core;
using Dapper;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.Drawing.Text;
namespace Stimulsoft
{
    public partial class Form1 : Form
    {
        private string lastChange;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var xx=rtbDoc.Rtf;
            var rpt = new StiReport();
            rpt.Load($"{System.IO.Path.GetDirectoryName(Application.ExecutablePath)}\\Reports\\Report.mrt");
            string CnnStr = Connections.data.ConnectionString;
            rpt.Dictionary.Databases.Clear();
            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection", CnnStr));
            rpt.Compile();
            rpt["@InDate"] = "1395/01/01";
            rpt["@Data"] = xx;
            rpt["Dataaa"] = xx;
            rpt.Show();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            InstalledFontCollection fontsCollection = new InstalledFontCollection();
            FontFamily[] fontFamilies = fontsCollection.Families;
            List<string> fonts = new List<string>();
            foreach (FontFamily font in fontFamilies)
            {
                fonts.Add(font.Name);
            }
            fontBox.ComboBox.DataSource = fonts;
        }


        private void ItalicToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tbrItalic_Click(object sender, System.EventArgs e)
        {
            ItalicToolStripMenuItem_Click(this, e);
        }

        private void UnderlineToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


        private void NormalToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;
                    newFontStyle = FontStyle.Regular;
                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void SelectFontToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //try
            //{
            //    if (!(rtbDoc.SelectionFont == null))
            //    {
            //        FontDialog1.Font = rtbDoc.SelectionFont;
            //    }
            //    else
            //    {
            //        FontDialog1.Font = null;
            //    }
            //    FontDialog1.ShowApply = true;
            //    if (FontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        rtbDoc.SelectionFont = FontDialog1.Font;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), "Error");
            //}
        }
        private void tbrUnderline_Click(object sender, System.EventArgs e)
        {
            UnderlineToolStripMenuItem_Click(this, e);
        }


        private void tbrFont_Click(object sender, System.EventArgs e)
        {
            SelectFontToolStripMenuItem_Click(this, e);
        }


        private void tbrLeft_Click(object sender, System.EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }


        private void tbrCenter_Click(object sender, System.EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }


        private void tbrRight_Click(object sender, System.EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void fontBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeFont();
        }

        private void changeFont()
        {
            var size = int.Parse(fontSize.Text);
            rtbDoc.SelectedRtf = lastChange;
            rtbDoc.SelectionFont = new Font(fontBox.Text, size, FontStyle.Regular);

        }

        private void rtbDoc_Leave(object sender, EventArgs e)
        {
               lastChange = rtbDoc.SelectedRtf;
        }
    }

    internal class Post
    {
        public Guid PostID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Code { get; set; }
    }
}
