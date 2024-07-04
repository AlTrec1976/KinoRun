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
    public partial class frmTimes : Form
    {
        string[] sTime = new string[10];
        string iTimes = "";

        public frmTimes()
        {
            InitializeComponent();
        }

        //public String Calculate(string strTimes)
        //{
        //    string[] sStr = new string[3];
        //    string str = "";
        //    string h = "";
        //    string m = "";
        //    string s = "";
        //    int hore = 0;
        //    int minute = 0;
        //    int secund = 0;

        //    iTimes = "";

        //    strTimes = strTimes.Replace("; ", " ");
        //    if (strTimes.Length > 0)
        //    {
        //        str = strTimes.Trim();
        //        sTime = str.Split(' ');

        //        for (int i = 0; i < sTime.Length; i++)
        //        {
        //            try
        //            {
        //                sStr = sTime[i].Split(':');

        //                if (i > sStr.Length)
        //                    Array.Resize(ref sStr, sTime.Length);

        //                secund = secund + Convert.ToInt32(sStr[2]);
        //                if (secund > 60)
        //                {
        //                    secund = secund - 60;
        //                    minute = minute + 1;
        //                }

        //                minute = minute + Convert.ToInt32(sStr[1]);
        //                if (minute > 60)
        //                {
        //                    minute = minute - 60;
        //                    hore = hore + 1;
        //                }

        //                hore = hore + Convert.ToInt32(sStr[0]);

        //                if (iTimes != "")
        //                    iTimes = iTimes + "; " + sTime[i];
        //                else
        //                    iTimes = sTime[i];
        //            }
        //            catch
        //            {
        //                MessageBox.Show("Проверьте, пожалуйста, разделители\nи замените их на пробелы.\nТаже возможно, что вместо цифр во времени\nиспользованы буквы или другие символы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }

        //        }
        //        if (hore < 10)
        //            h = "0" + Convert.ToString(hore);
        //        else
        //            h = Convert.ToString(hore);

        //        if (minute < 10)
        //            m = "0" + Convert.ToString(minute);
        //        else
        //            m = Convert.ToString(minute);

        //        if (secund < 10)
        //            s = "0" + Convert.ToString(secund);
        //        else
        //            s = Convert.ToString(secund);

        //        str = h + ":" + m + ":" + s;
        //    }
        //    return str;
        //}

        private void txtTimes_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //}
            //catch (Exception)
            //{

            //}

            //btnCalculate.PerformClick();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            btnCalculate.PerformClick();

            frmMain frm = (frmMain)this.Owner;
            //frm.Times = mtxtTime.Text;
            frm.iTimes = iTimes;
            frm.txtTimes.Text = txtTimes.Text;
            frm.mtxtTime.Text = mtxtTime.Text;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)this.Owner;
            this.Close();
        }

        private void frmTimes_Load(object sender, EventArgs e)
        {
            txtTimes.Text = " ";
            iTimes = "";
            frmMain frm = (frmMain)this.Owner;

            txtTimes.Text = (frm.iTimes.Replace("; ", " "));
            txtTimes.Text = (frm.iTimes.Replace(";", " "));
            frm.iTimes = "";
            btnCalculate.PerformClick();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string str = "";

            str = AdditionFunc.Calculate(txtTimes.Text);

            mtxtTime.ReadOnly = false;
            mtxtTime.Text = str;
            mtxtTime.ReadOnly = true;
        }
    }
}
