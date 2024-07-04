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
    public partial class frmDigConverter : Form
    {
        public frmDigConverter()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //strInp = textBox1.Text.Replace(" ", "");
            //strInp = "";
            //foreach (char ch in textBox1.Text)
            //{
            //    int number;
            //    if (Int32.TryParse(Convert.ToString(ch), out number))
            //        strInp = strInp + ch;
            //    i++;
            //}

            Clipboard.SetText(AdditionFunc.DigitalCompl(textBox1.Text.Replace(" ", "")));
            frmMain frm = (frmMain)this.Owner;
            this.Close();
            MessageBox.Show("Конвертация завершена.\nРезультат: " + Clipboard.GetText(), "Завершено", MessageBoxButtons.OK);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить данное действие?", "Действие отменено", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frmDigConverter_Load(object sender, EventArgs e)
        {
            textBox1.Text = Clipboard.GetText();
        }
    }
}
