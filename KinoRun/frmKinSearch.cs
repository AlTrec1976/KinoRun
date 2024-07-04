using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinoRun
{
    public partial class frmKinSearch : Form
    {
        string[] InclInd;
        string[] CatInd;
        string[] Search;
        string line;
        public const string startDoc = "<!DOCTYPE HTML>\n<html>\n <head> \n  <meta charset=\"utf - 8\"><style type=\"text/css\"><!--\nhtml { background-color:#eee;padding:0px;margin:0px;min-width:1200px;font-family:Calibri, sans-serif;font-size:14px; }\nbody { padding:0px;margin:0px;font:normal 14px Calibri;color:#595959;background:#efefef; }\n--></style></head><body><table  style=\"width: 100%\">\n";
        public const string finishDoc = "</body>\n</html>";

        public frmKinSearch()
        {
            InitializeComponent();
            webBrowser1.DocumentText = startDoc + "</table>" + finishDoc;
        }

        private void frmKinSearch_Load(object sender, EventArgs e)
        {
            string[,] arrStr = Kinorun.List("cat");
            string[] name = new string[arrStr.Length / 2];
            CatInd = new string[name.Length];

            for(int i = 0; i < name.Length; i++)
            {
                CatInd[i] = arrStr[0, i];
                name[i] = arrStr[1, i];
            }
            cmbCat.Items.AddRange(name);
            cmbCat.SelectedIndex = 0;
            Array.Clear(name, 0, name.Length);
            Array.Clear(arrStr, 0, arrStr.Length);

            arrStr = Kinorun.List("incldead");
            name = new string[arrStr.Length / 2];
            InclInd = new string[name.Length];
            for (int i = 0; i < name.Length; i++)
            {
                InclInd[i] = arrStr[0, i];
                name[i] = arrStr[1, i];
            }
            cmbIncldead.Items.AddRange(name);
            cmbIncldead.SelectedIndex = 1;
            Array.Clear(name, 0, name.Length);
            Array.Clear(arrStr, 0, arrStr.Length);

            cmbUnit.SelectedIndex = 0;
        }

        private void txtSizeOn_TextChanged(object sender, EventArgs e)
        {
            txtSizeOff.Text = txtSizeOn.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)this.Owner;
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            line = startDoc + Kinorun.Searching(txtSearch.Text, InclInd[cmbIncldead.SelectedIndex], CatInd[cmbCat.SelectedIndex], txtSizeOn.Text, txtSizeOff.Text) + finishDoc;
            string str = AdditionFunc.DigitalCompl(txtSizeOff.Text);
            txtSizeOn.Text = AdditionFunc.DigitalCompl(txtSizeOn.Text);
            txtSizeOff.Text = str;

            //if (line.Length < 365)
            //    line = startDoc + "<tr><td><h2 style=\"vertical - align: middle; color: rgb(255, 0, 51)\">К сожалению, поиск не дал результата</h2></td></tr></table>\n</body> </html>";

            line = line.Replace("Ничего не найдено", "<h2 style=\"vertical - align: middle; color: rgb(255, 0, 51)\"><span style=\"text-align: center; display: block;height: 100%;width:100%; position: absolute;\">К сожалению, поиск не дал результата</span></h2>");
            webBrowser1.DocumentText = line;
        }

        private void frmKinSearch_Resize(object sender, EventArgs e)
        {
            //int width = (this.Width - this.MinimumSize.Width) / 2;

            //pnlButton.Location = new Point(width + 3, 574);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSizeOn.Text = "";
            cmbIncldead.SelectedIndex = 1;
            cmbCat.SelectedIndex = 0;
        }

        private void btnBrows_Click(object sender, EventArgs e)
        {
            //var ofd = new OpenFileDialog();
            string str = "";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Видео (*.vob;*.avi;*.mpg;*.wmv;*.mkv;*.flv;*.mov;*.asf;*.mp4;*.ogm;*.rmvb;*.qt;*.mpeg;*.m1v)|*.vob;*.avi;*.mpg;*.wmv;*.mkv;*.flv;*.mov;*.asf;*.mp4;*.ogm;*.rmvb;*.qt;*.mpeg;*.m1v|Фотографии и изображения (*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff)|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|Аудио (*.mp3;*.wma, *.wav)|*.mp3;*.wma, *.wav|Все файлы (*.*)|*.*";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileNames.Length > 1)
                {
                    double sum = 0;
                    this.Cursor = Cursors.WaitCursor;
                    for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                    {
                        sum = sum + Convert.ToDouble(AdditionFunc.GetFileSize(new System.IO.FileInfo(openFileDialog1.FileNames[i]), cmbUnit.SelectedIndex));
                    }

                    str = Convert.ToString(sum);
                    this.Cursor = Cursors.Default;

                }
                else
                {
                    str = AdditionFunc.GetFileSize(new System.IO.FileInfo(openFileDialog1.FileName), cmbUnit.SelectedIndex);
                }
                txtSizeOn.Text = str;
            }
        }
    }
}
