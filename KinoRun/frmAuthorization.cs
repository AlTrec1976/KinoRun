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
    public partial class frmAuthorization : Form
    {
        string url;
        string url_site;
        public bool authorization = false;
        public string[] PicPicture = new string[] { "", "" };
        public string[] KinoRun = new string[] { "", "" };

        Design design = new Design(); //экземпляр класса с настройками

        //Запись настроек
        private void writeSetting()
        {
            if (chkSave.Checked)
            {
                if (url_site == "PicPicture.com")
                {
                    design.Fields.PicPicture[0] = txtLogin.Text;
                    design.Fields.PicPicture[1] = txtPass.Text;
                }
                else
                {
                    design.Fields.KinoRun[0] = txtLogin.Text;
                    design.Fields.KinoRun[1] = txtPass.Text;
                }

                design.WriteXml();
            }
        }


        public frmAuthorization()
        {
            InitializeComponent();
        }

        public frmAuthorization(string Site)
        {
            InitializeComponent();
            //forma = form;
            url_site = Site;
            url = (Site == "PicPicture.com") ? "http://picpicture.com/": "http://kinorun.com/";
            this.Text = this.Tag.ToString() + url_site;
        }

        private void frmAuthorization_Load(object sender, EventArgs e)
        {
            grpSite.Text = url_site;
        }

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !chkPass.Checked;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Bitmap img;
            BorderStyle style = picTest.BorderStyle;

            if ((txtLogin.Text.Length > 0) && (txtLogin.Text.Length > 0))
            {
                if(url_site == "PicPicture.com")
                {
                    if (chkReg.Checked)
                    {
                        authorization = Picpicture.RegPic(txtEmail.Text, txtLogin.Text, txtPass.Text);

                        if (authorization)
                        {
                            authorization = false;
                            btnOk.PerformClick();
                        }
                    }
                    else
                        authorization = Picpicture.AuthPic(txtLogin.Text, txtPass.Text);
                }
            }
            else
            {
                var msg = MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if(authorization) //авторизация удачна
            {
                img = Properties.Resources._true;
                style = BorderStyle.Fixed3D;
            }
            else
            {
                img = Properties.Resources._false;
                style = BorderStyle.None;
            }

            btnOk.Enabled = authorization;
            btnHide.Enabled = !authorization;
            picTest.BorderStyle = style;
            picTest.Image = img;
            this.Cursor = Cursors.Default;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            writeSetting();
            //if (this.Owner == frmMain.ActiveForm)
            //{
            frmMain frm = (frmMain)this.Owner;
            frm.authPic = authorization;
            frm.readSetting();
            frmMain._PicPicture[0] = txtLogin.Text;
            frmMain._PicPicture[1] = txtPass.Text;
            frm.PicUploade();
            //}
            this.Close();
        }

        private void picTest_Click(object sender, EventArgs e)
        {
            btnHide.PerformClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)this.Owner;
            frm.authPic = authorization;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pnlEmail.Visible = chkReg.Checked;
            if (chkReg.Checked)
                btnHide.Text = "Отправить";
            else
                btnHide.Text = "Тест";
        }
    }
}
