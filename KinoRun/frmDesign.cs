using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace KinoRun
{
    public partial class frmDesign : Form
    {
        bool picpic = false;
        bool kinr = false;
        string[] _Fields = new string[4];
        string[] _Text = new string[4];
        string[] _Headlines = new string[4];
        string[] _Description = new string[4];

        string s_Fields = "";
        string s_Text = "";
        string s_Headlines = "";
        string s_Description = "";
        string s_fontName = "";

        string f_Fields = "";
        string f_Text = "";
        string f_Headlines = "";
        string f_Description = "";
        string f_fontName = "";

        string[] _PicPicture = new string[2];
        string[] _KinoRun = new string[2];

        TextBox[] _txtPic = new TextBox[2];
        TextBox[] _txtKin = new TextBox[2];
        Label[] _lblField = new Label[4];
        Label[] _lblColor = new Label[4];
        Button[] _btnColor = new Button[4];
        ComboBox[] _cmbColor = new ComboBox[4];
        PictureBox[] _picBold = new PictureBox[4];
        PictureBox[] _picItalic = new PictureBox[4];
        PictureBox[] _picUnderline = new PictureBox[4];
        string fontName = "";
        //Props props = new Props(); //экземпляр класса с настройками
        Design design = new Design(); //экземпляр класса с настройками

        public frmDesign()
        {
            InitializeComponent();
            InitForm();
        }

        //Запись настроек
        private void writeSetting()
        {
            Array.Resize(ref design.Fields.Fields, _Fields.Length);
            Array.Resize(ref design.Fields.Text, _Text.Length);
            Array.Resize(ref design.Fields.Headlines, _Headlines.Length);
            Array.Resize(ref design.Fields.Description, _Description.Length);

            design.Fields.fontName = fontName;
            for (int i=0; i<_lblColor.Length; i++)
            {
                design.Fields.Fields[i] = _Fields[i];
                design.Fields.Text[i] = _Text[i];
                design.Fields.Headlines[i] = _Headlines[i];
                design.Fields.Description[i] = _Description[i];
            }

            for (int i = 0; i < 2; i++)
            {
                if (picTest_Pic.BorderStyle == BorderStyle.Fixed3D)
                   _PicPicture[i] = _txtPic[i].Text; 

                if (picTest_Kin.BorderStyle == BorderStyle.Fixed3D)
                   _KinoRun[i] = _txtKin[i].Text; 
            }

            design.Fields.PicPicture = AdditionFunc.AccCoding(_PicPicture, 0);
            design.Fields.KinoRun = AdditionFunc.AccCoding(_KinoRun, 0);

            //for (int i = 0; i < 2; i++)
            //{
            //    if (picTest_Pic.BorderStyle == BorderStyle.Fixed3D)
            //        design.Fields.PicPicture[i] = _PicPicture[i];

            //    if (picTest_Kin.BorderStyle == BorderStyle.Fixed3D)
            //        design.Fields.KinoRun[i] = _KinoRun[i];
            //}

            design.Fields.FieldLine = chkField.Checked;

            design.Fields.Beta = chkBeta.Checked;
            design.Fields.Theme = frmMain.Theme;
            design.Fields.Tags = txtTags.Text;
            design.Fields.ClearDir = chkClear.Checked;

            design.Fields.SaveDefault = chkSave.Checked;
            design.Fields.PathSave = txtFolder.Text.TrimEnd('\\');

            design.WriteXml();
        }

        //Чтение настроек
        public void readSetting()
        {
            design.ReadXml();

            fontName = design.Fields.fontName;
            for (int i=0; i<_lblColor.Length; i++)
            {
                _Fields[i] = design.Fields.Fields[i];
                _Text[i] = design.Fields.Text[i];
                _Headlines[i] = design.Fields.Headlines[i];
                _Description[i] = design.Fields.Description[i];
            }

            _PicPicture = AdditionFunc.AccCoding(design.Fields.PicPicture, 1);
            _KinoRun = AdditionFunc.AccCoding(design.Fields.KinoRun, 1);

            for (int i = 0; i < 2; i++)
            {
                _txtPic[i].Text = _PicPicture[i];
                _txtKin[i].Text = _KinoRun[i];
            }

            //for (int i = 0; i < 2; i++)
            //{
            //    _PicPicture[i] = design.Fields.PicPicture[i];
            //    _KinoRun[i] = design.Fields.KinoRun[i];
            //}
            chkBeta.Checked = design.Fields.Beta;
            chkField.Checked = design.Fields.FieldLine;

            txtTags.Text = design.Fields.Tags;
            chkClear.Checked = design.Fields.ClearDir;

            chkSave.Checked = design.Fields.SaveDefault;
            txtFolder.Text = design.Fields.PathSave;
        }

        private void InitForm()
        {
            _lblField = new Label[] { lblField_F, lblField_T, lblField_H, lblField_D };
            _lblColor = new Label[] { lblColor_F, lblColor_T, lblColor_H, lblColor_D };
            _cmbColor = new ComboBox[] { cmbColor_F, cmbColor_T, cmbColor_H, cmbColor_D };
            _btnColor = new Button[] { btnColor_F, btnColor_T, btnColor_H, btnColor_D };
            _picBold = new PictureBox[] { picBold_F, picBold_T, picBold_H, picBold_D };
            _picItalic = new PictureBox[] { picItalic_F, picItalic_T, picItalic_H, picItalic_D };
            _picUnderline = new PictureBox[] { picUnderline_F, picUnderline_T, picUnderline_H, picUnderline_D };
            _txtPic = new TextBox[] { txtLogin_Pic, txtPass_Pic };
            _txtKin = new TextBox[] { txtLogin_Kin, txtPass_Kin };
        }

        private void picClick(PictureBox picButton)
        {
            PictureBox pic = picButton;
            Bitmap img;
            BorderStyle style = pic.BorderStyle;

            if (pic == picTest_Pic)
            {
                if (chkEmail.Checked)
                {
                    picpic = Picpicture.RegPic(txtEmail.Text, _txtPic[0].Text, _txtPic[1].Text);
                }
                else
                {
                    picpic = Picpicture.AuthPic(_txtPic[0].Text, _txtPic[1].Text);
                }
            }
            else if (pic == picTest_Kin)
            {
                if (frmMain.authKin)
                    kinr = true;
                else
                    kinr = Kinorun.Auth(_txtKin[0].Text, _txtKin[1].Text);
            } 

            if (((picpic) && (picButton == picTest_Pic) || ((kinr) && (picButton == picTest_Kin))))
            {
                img = Properties.Resources._true;
                style = BorderStyle.Fixed3D;
            }
            else
            {
                img = Properties.Resources._false;
                style = BorderStyle.None;
            }

            pic.BorderStyle = style;
            pic.Image = img;
        }

        private void UserTags()
        {
            string start = "";
            string finish = "";

            //if (_lblColor[0].Text != "")
            //    _Fields[0] = _lblColor[0].Text;
            //else
            //    _Fields[0] = "";
            //if (_lblColor[1].Text != "")
            //    _Text[0] = _lblColor[1].Text;
            //else
            //    _Text[0] = "";
            //if (_lblColor[2].Text != "")
            //    _Headlines[0] = _lblColor[2].Text;
            //else
            //    _Headlines[0] = "";
            //if (_lblColor[3].Text != "")
            //    _Description[0] = _lblColor[3].Text;
            //else
            //    _Description[0] = "";

            if (_picBold[0].BorderStyle == BorderStyle.Fixed3D)
                _Fields[1] = "b";
            else
                _Fields[1] = "";
            if (_picBold[1].BorderStyle == BorderStyle.Fixed3D)
                _Text[1] = "b";
            else
                _Text[1] = "";
            if (_picBold[2].BorderStyle == BorderStyle.Fixed3D)
                _Headlines[1] = "b";
            else
                _Headlines[1] = "";
            if (_picBold[3].BorderStyle == BorderStyle.Fixed3D)
                _Description[1] = "b";
            else
                _Description[1] = "";

            if (_picItalic[0].BorderStyle == BorderStyle.Fixed3D)
                _Fields[2] = "i";
            else
                _Fields[2] = "";
            if (_picItalic[1].BorderStyle == BorderStyle.Fixed3D)
                _Text[2] = "i";
            else
                _Text[2] = "";
            if (_picItalic[2].BorderStyle == BorderStyle.Fixed3D)
                _Headlines[2] = "i";
            else
                _Headlines[2] = "";
            if (_picItalic[3].BorderStyle == BorderStyle.Fixed3D)
                _Description[2] = "i";
            else
                _Description[2] = "";

            if (_picUnderline[0].BorderStyle == BorderStyle.Fixed3D)
                _Fields[3] = "u";
            else
                _Fields[3] = "";
            if (_picUnderline[1].BorderStyle == BorderStyle.Fixed3D)
                _Text[3] = "u";
            else
                _Text[3] = "";
            if (_picUnderline[2].BorderStyle == BorderStyle.Fixed3D)
                _Headlines[3] = "u";
            else
                _Headlines[3] = "";
            if (_picUnderline[3].BorderStyle == BorderStyle.Fixed3D)
                _Description[3] = "u";
            else
                _Description[3] = "";

            if (cmbFont.SelectedIndex != 0)
            {
                s_fontName = start + "<span style=\"font-family:" + cmbFont.Text + "\">";
                f_fontName = "</span>" + finish;
            }

            for (int i = 1; i < _Fields.Length; i++)
            {
                if (_Fields[i] != "")
                {
                    start = start + "<" + _Fields[i] + ">";
                    finish = "</" + _Fields[i] + ">" + finish;
                }
            }

            if (_Fields[0] != "")
            {
                start = start + "<span style=\"color:" + _Fields[0] + "\">";
                finish = "</color>" + finish;
            }
            s_Fields = start;
            f_Fields = finish;
            start = "";
            finish = "";

            for (int i = 1; i < _Text.Length; i++)
            {
                if (_Text[i] != "")
                {
                    start = start + "<" + _Text[i] + ">";
                    finish = "</" + _Text[i] + ">" + finish;
                }
            }

            if (_Text[0] != "")
            {
                start = start + "<span style=\"color:" + _Text[0] + "\">";
                finish = "</color>" + finish;
            }
            s_Text = start;
            f_Text = finish;
            start = "";
            finish = "";

            for (int i = 1; i < _Headlines.Length; i++)
            {
                if (_Headlines[i] != "")
                {
                    start = start + "<" + _Headlines[i] + ">";
                    finish = "</" + _Headlines[i] + ">" + finish;
                }
            }

            if (_Headlines[0] != "")
            {
                start = start + "<span style=\"color:" + _Headlines[0] + "\">";
                finish = "</color>" + finish;
            }
            s_Headlines = start;
            f_Headlines = finish;
            start = "";
            finish = "";

            for (int i = 1; i < _Description.Length; i++)
            {
                if (_Description[i] != "")
                {
                    start = start + "<" + _Description[i] + ">";
                    finish = "</" + _Description[i] + ">" + finish;
                }
            }

            if (_Description[0] != "")
            {
                start = start + "<span style=\"color:" + _Description[0] + "\">";
                finish = "</color>" + finish;
            }
            s_Description = start;
            f_Description = finish;
            start = "";
            finish = "";
        }

        private void HTMLCompleat()
        {
            string fld = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            if (chkField.Checked)
                fld = "<br>";

            rtxtHtml.Clear();
            UserTags();
            rtxtHtml.AppendText("<html><head><style type=\"text/css\"><!--");
            rtxtHtml.AppendText("html { background-color:#eee;padding:0px;margin:0px;min-width:1200px;font-family:Calibri, sans-serif;font-size:14px; }");
            rtxtHtml.AppendText("body { padding:0px;margin:0px;font:normal 14px Calibri;color:#595959;background:#efefef; }");
            rtxtHtml.AppendText("--></style></head><body>");
            rtxtHtml.AppendText(s_fontName);
            rtxtHtml.AppendText(s_Headlines + "Это заголовок раздела<br>" + f_Headlines);
            rtxtHtml.AppendText(s_Fields + "Поле строки: " + f_Fields);
            rtxtHtml.AppendText(s_Text + " Значение<br>" + f_Text);
            rtxtHtml.AppendText(s_Fields + "Поле строки: " + f_Fields);
            rtxtHtml.AppendText(s_Text + " Значение" + fld + f_Text);
            rtxtHtml.AppendText(s_Fields + "Поле строки: " + f_Fields);
            rtxtHtml.AppendText(s_Text + " Значение<br>" + f_Text);
            rtxtHtml.AppendText("<hr>");
            rtxtHtml.AppendText(s_Description + "Этот небольшой блок будет символизировать <br>описание раздачи. Сделаем его побольше, <br>чтоб было заметнее результат.<br>" + f_Description);
            rtxtHtml.AppendText(f_fontName);
            rtxtHtml.AppendText("</body></html>");

            webBrowser1.DocumentText = rtxtHtml.Text;
        }

        private void FormResize()
        {
            readSetting();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

            for (int i = 0; i < _cmbColor.Length; i++)
            {
                foreach (PropertyInfo c in propInfoList)
                {
                    this._cmbColor[i].Items.Add(c.Name);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                _txtPic[i].Text = _PicPicture[i];
                _txtKin[i].Text = _KinoRun[i];
            }

            _picBold[0].BorderStyle = (_Fields[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picBold[1].BorderStyle = (_Text[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picBold[2].BorderStyle = (_Headlines[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picBold[3].BorderStyle = (_Description[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;

            _picItalic[0].BorderStyle = (_Fields[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picItalic[1].BorderStyle = (_Text[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picItalic[2].BorderStyle = (_Headlines[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picItalic[3].BorderStyle = (_Description[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;

            _picUnderline[0].BorderStyle = (_Fields[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picUnderline[1].BorderStyle = (_Text[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picUnderline[2].BorderStyle = (_Headlines[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            _picUnderline[3].BorderStyle = (_Description[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;

            chkAssoc.Checked = FileAssociations.IsAssociated;

            if (fontName == "")
                cmbFont.SelectedIndex = 0;
            else
                cmbFont.Text = fontName;

            _cmbColor[0].Text = _Fields[0];
            _cmbColor[1].Text = _Text[0];
            _cmbColor[2].Text = _Headlines[0];
            _cmbColor[3].Text = _Description[0];

            _lblColor[0].Text = _Fields[0];
            _lblColor[1].Text = _Text[0];
            _lblColor[2].Text = _Headlines[0];
            _lblColor[3].Text = _Description[0];

            _lblColor[0].ForeColor = ColorTranslator.FromHtml(_Fields[0]);
            _lblColor[1].ForeColor = ColorTranslator.FromHtml(_Text[0]);
            _lblColor[2].ForeColor = ColorTranslator.FromHtml(_Headlines[0]);
            _lblColor[3].ForeColor = ColorTranslator.FromHtml(_Description[0]);

            _lblField[0].ForeColor = ColorTranslator.FromHtml(_Fields[0]);
            _lblField[1].ForeColor = ColorTranslator.FromHtml(_Text[0]);
            _lblField[2].ForeColor = ColorTranslator.FromHtml(_Headlines[0]);
            _lblField[3].ForeColor = ColorTranslator.FromHtml(_Description[0]);

            foreach (PictureBox pict in _picBold)
            {
                if (pict.BorderStyle != BorderStyle.Fixed3D)
                    pict.BackColor = SystemColors.Control;
                else
                    pict.BackColor = SystemColors.ActiveCaption;
            }
            foreach (PictureBox pict in _picItalic)
            {
                if (pict.BorderStyle != BorderStyle.Fixed3D)
                    pict.BackColor = SystemColors.Control;
                else
                    pict.BackColor = SystemColors.ActiveCaption;
            }
            foreach (PictureBox pict in _picUnderline)
            {
                if (pict.BorderStyle != BorderStyle.Fixed3D)
                    pict.BackColor = SystemColors.Control;
                else
                    pict.BackColor = SystemColors.ActiveCaption;
            }
        }

        private void Reserving (string Dir, string subDir) //Резервирование настроек
        {
            bool rez = AdditionFunc.DirCopy(frmMain.SetingPath, Dir + subDir);
            if (rez)
            {
                string[] list = AdditionFunc.ListDir(Dir);
                cmbReserv.Items.Clear();
                cmbReserv.Items.AddRange(list);
                cmbReserv.SelectedIndex = 0;
                MessageBox.Show("Актуальные настройки зарезервированы успешно!",
                                "Готово",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void frmDesign_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = this.Tag.ToString() + tbcSeting.SelectedTab.Text;
            //readSetting();
            rtxtHtml.Hide();
            FormResize();
            HTMLCompleat();

            string[] list = AdditionFunc.ListDir(frmMain.SetingPath.TrimEnd('\\') + "_r\\");

            if (list.Length < 2)
                list[0] = "Пусто";

            cmbReserv.Items.AddRange(list);
            cmbReserv.SelectedIndex = 0;

            tbcSeting.SelectedTab = tbcSeting.TabPages["tbpAuthorization"];
            if ((picTest_Pic.BorderStyle != BorderStyle.Fixed3D) || (picTest_Kin.BorderStyle != BorderStyle.Fixed3D))
                tbcSeting.SelectedTab = tbcSeting.TabPages["tbpAuthorization"];
            else
                tbcSeting.SelectedTab = tbcSeting.TabPages["tbpDisign"];

            this.Cursor = Cursors.Default;
        }

        private void frmDesign_Activated(object sender, EventArgs e)
        {
            try
            {
                foreach (PictureBox pict in _picBold)
                {
                    if (pict.BorderStyle != BorderStyle.Fixed3D)
                        pict.BackColor = SystemColors.Control;
                    else
                        pict.BackColor = SystemColors.ActiveCaption;
                }
                foreach (PictureBox pict in _picItalic)
                {
                    if (pict.BorderStyle != BorderStyle.Fixed3D)
                        pict.BackColor = SystemColors.Control;
                    else
                        pict.BackColor = SystemColors.ActiveCaption;
                }
                foreach (PictureBox pict in _picUnderline)
                {
                    if (pict.BorderStyle != BorderStyle.Fixed3D)
                        pict.BackColor = SystemColors.Control;
                    else
                        pict.BackColor = SystemColors.ActiveCaption;
                }
                HTMLCompleat();
            }
            catch (NullReferenceException)
            {
                InitForm();
            }
        }

        private void frmDesign_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((AdditionFunc.ReSetings()) && (!frmMain.modif))
            {
                frmMain.First = true;
                if (frmMain.authKin)
                {
                    var msg = MessageBox.Show("Сейчас произойдёт перезапуск программы для вступления настроек в силу.",
                        "Перезапуск",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    var msg = MessageBox.Show("Внесите данные авторизации на трекере KinoRun.\nБез этого многие функции, в том числе заливка раздачи на трекер, будут невозможны!",
                        "Внимание!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)this.Owner;
            this.Close();
        }

        private void btnColor_K_Click(object sender, EventArgs e)
        {
            string str = "#";
            Button btn = (Button)sender;
            int tag = Convert.ToInt32(btn.Tag);

            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                _lblField[tag].ForeColor = dlgColor.Color;
                _lblColor[tag].ForeColor = dlgColor.Color;
                if (dlgColor.Color.Name[0] == 'f')
                {
                    string hex = dlgColor.Color.R.ToString("X2") + dlgColor.Color.G.ToString("X2") + dlgColor.Color.B.ToString("X2");
                    _lblColor[tag].Text = str + hex;
                }
                else
                    _lblColor[tag].Text = dlgColor.Color.Name;
            }

            if (_lblColor[tag].Text == "Black")
                _lblColor[tag].Text = "";

            switch (tag)
            {
                case 0:
                    _Fields[0] = _lblColor[tag].Text;
                    break;
                case 1:
                    _Text[0] = _lblColor[tag].Text;
                    break;
                case 2:
                    _Headlines[0] = _lblColor[tag].Text;
                    break;
                case 3:
                    _Description[0] = _lblColor[tag].Text;
                    break;
            }

            string s = _lblColor[tag].Text.Substring(0, 1);
            if (s == "#")
                _cmbColor[tag].SelectedIndex = -1;
            else
                _cmbColor[tag].Text = _lblColor[tag].Text;
            HTMLCompleat();

            //string str = "#";
            //string color = "";
            //Button btn = (Button)sender;
            //int tag = Convert.ToInt32(btn.Tag);

            //if (dlgColor.ShowDialog() == DialogResult.OK)
            //{
            //    _lblColor[tag].ForeColor = dlgColor.Color;
            //    if (dlgColor.Color.Name[0] == 'f')
            //    {
            //        string hex = dlgColor.Color.R.ToString("X2") + dlgColor.Color.G.ToString("X2") + dlgColor.Color.B.ToString("X2");
            //        color = str + hex;
            //    }
            //    else
            //        color = dlgColor.Color.Name;
            //}

            //if (color == "Black")
            //    color = "";

            //switch (tag)
            //{
            //    case 0:
            //        _Fields[0] = color;
            //        break;
            //    case 1:
            //        _Text[0] = color;
            //        break;
            //    case 2:
            //        _Headlines[0] = color;
            //        break;
            //    case 3:
            //        _Description[0] = color;
            //        break;
            //}

            //HTMLCompleat();
        }

        private void btnComlete_Click(object sender, EventArgs e)
        {
            string b;
            string it;
            string u;
            string clr;
            frmMain frm = (frmMain)this.Owner;

            //if (picTest_Pic.BorderStyle == BorderStyle.None)
            //    picClick(picTest_Pic);

            //if (picTest_Kin.BorderStyle == BorderStyle.None)
            //    picClick(picTest_Kin);
            
            for (int i = 0; i < 2; i++)
            {
                _PicPicture[i] = _txtPic[i].Text;
                _KinoRun[i] = _txtKin[i].Text;
            }

            for (int i = 0; i < _lblColor.Length; i++)
            {
                if (_picBold[i].BorderStyle == BorderStyle.Fixed3D)
                    b = "b";
                else
                    b = "";

                if(_picItalic[i].BorderStyle == BorderStyle.Fixed3D)
                    it = "i";
                else
                    it = "";

                if(_picUnderline[i].BorderStyle == BorderStyle.Fixed3D)
                    u = "u";
                else
                    u = "";

                if ((_lblColor[i].Text == "Black") || (_lblColor[i].Text == "#FFFFFFF"))
                    clr = "";
                else
                    clr = _lblColor[i].Text;

                for (int j = 0; j < _Fields.Length; j++)
                {
                    switch (i)
                    {
                        case 0:
                            _Fields.SetValue(clr, 0);
                            _Fields.SetValue(b, 1);
                            _Fields.SetValue(it, 2);
                            _Fields.SetValue(u, 3);
                            break;
                        case 1:
                            _Text.SetValue(clr, 0);
                            _Text.SetValue(b, 1);
                            _Text.SetValue(it, 2);
                            _Text.SetValue(u, 3);
                            break;
                        case 2:
                            _Headlines.SetValue(clr, 0);
                            _Headlines.SetValue(b, 1);
                            _Headlines.SetValue(it, 2);
                            _Headlines.SetValue(u, 3);
                            break;
                        case 3:
                            _Description.SetValue(clr, 0);
                            _Description.SetValue(b, 1);
                            _Description.SetValue(it, 2);
                            _Description.SetValue(u, 3);
                            break;
                    }
                }
                frm._Fields[i] = _Fields[i];
                frm._Text[i] = _Text[i];
                frm._Headlines[i] = _Headlines[i];
                frm._Description[i] = _Description[i];
            }

            if (cmbFont.SelectedIndex == 0)
                fontName = "";
            else
                fontName = cmbFont.Text;

            frm.fontName = fontName;
            writeSetting();
            frm.readSetting();
            if (picTest_Pic.BorderStyle == BorderStyle.Fixed3D)
            {
                frmMain._PicPicture[0] = txtLogin_Pic.Text;
                frmMain._PicPicture[1] = txtPass_Pic.Text;
            }
            if (picTest_Kin.BorderStyle == BorderStyle.Fixed3D)
            {
                frmMain._KinoRun[0] = txtLogin_Kin.Text;
                frmMain._KinoRun[1] = txtPass_Kin.Text;
            }

            this.Close();
        }

        private void btnTest_Kin_Click(object sender, EventArgs e)
        {
            var btn = ((Button)sender);
            if (btn.Name == "btnTest_Pic")
                picClick(picTest_Pic);
            else
                picClick(picTest_Kin);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string Directory = frmMain.SetingPath;
            if(AdditionFunc.ClearDirTorrent(Directory))
            {
                try
                {
                    if (AdditionFunc.ReSetings())
                    {
                        readSetting();
                        writeSetting();
                        var msg = MessageBox.Show("Настройки сброшены успешно!\nДля правильной работы программы настройте её заново.", "Настройки сброшены", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
                catch (Exception ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string directory = frmMain.SetingPath.TrimEnd('\\') + "_r\\";
            string mon = DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            string dirName = DateTime.Now.Year.ToString() + "." + mon + "." + day;
            //string str = "";

            int k = 0;
            if (!System.IO.Directory.Exists(directory))
                System.IO.Directory.CreateDirectory(directory);

            string[] list = AdditionFunc.ListDir(directory);

            if(System.IO.Directory.Exists(directory + dirName))
            {
                //int ind = 1;
                //for(int i = 0; i < list.Length + 1; i++)
                //{
                //    str = "_" + ind.ToString();
                //    try
                //    {
                //        if (dirName + str == list[i])
                //        {
                //            ind++;
                //            continue;
                //        }
                //        else
                //            continue;
                //    }
                //    catch(IndexOutOfRangeException)
                //    { break; }
                //}

                //dirName = dirName + str;
                var msg = MessageBox.Show("Сегодня Вы уже резервировали настройки. Хотите заменить сохранённые сегодня настройки?",
                "Внимание!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
                if (msg == DialogResult.Yes)
                {
                    System.IO.DirectoryInfo dir_inf = new System.IO.DirectoryInfo(directory);


                    if (list.Length > 10)
                    {
                        k = list.Length - 10;
                        string[] slist = list;
                        Array.Sort(slist);
                        //for (int i = k; i > 0; i--)
                        for (int i = 0; i < k; i++)
                        {
                            try
                            {
                                //var dat = dir_inf.CreationTime;
                                AdditionFunc.ClearDirTorrent(directory + "\\" + slist[i]);
                                System.IO.Directory.Delete(directory + "\\" + slist[i]);
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("Устаревшие резервные копии удалить не удалось по причине:\n\r" + ex.Message,
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                                continue;
                            }
                        }
                    }
                    Reserving(directory, dirName);
                }
            }
            else
            {
                Directory.CreateDirectory(directory + dirName);
                if(Directory.Exists(directory + dirName))
                {
                    Reserving(directory, dirName);
                }
            }
            Cursor = Cursors.Default;
        }

        private void btnRestor_Click(object sender, EventArgs e)
        {
            if(cmbReserv.SelectedIndex > 0)
            {
                string rdirectory = frmMain.SetingPath.TrimEnd('\\') + "_r\\Temp";
                string directory = frmMain.SetingPath.TrimEnd('\\') + "_r\\" + cmbReserv.Text;

                if (AdditionFunc.DirCopy(frmMain.SetingPath, rdirectory))
                {
                    if (AdditionFunc.ClearDirTorrent(frmMain.SetingPath))
                    {
                        bool rez = AdditionFunc.DirCopy(directory, frmMain.SetingPath);
                        if (rez)
                        {
                            FormResize();
                            MessageBox.Show(
                            "Выбранная вами резервная копия восстановлена успешно!",
                            "Готово",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                            AdditionFunc.ClearDirTorrent(rdirectory);
                            System.IO.Directory.Delete(rdirectory);
                        }
                        else
                        {
                            AdditionFunc.DirCopy(rdirectory, frmMain.SetingPath);
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                        "Восстановить данную резервную копию не удалось!",
                        "Неудача",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                "Выберите в выпадающем списке резервную копию, которую хотите восстановить!",
                "Не выбрана копия",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }

        private void btnViewDir_Click(object sender, EventArgs e)
        {
            if (dlgFolder.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = dlgFolder.SelectedPath;
            }

        }

        private void chkPass_Pic_CheckedChanged(object sender, EventArgs e)
        {
            txtPass_Pic.UseSystemPasswordChar = !chkPass_Pic.Checked;
        }

        private void chkPass_Kin_CheckedChanged(object sender, EventArgs e)
        {
            txtPass_Kin.UseSystemPasswordChar = !chkPass_Kin.Checked;
        }

        private void chkEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtEmail.Enabled = chkEmail.Checked;
            lblEmail.Enabled = chkEmail.Checked;
        }

        private void chkAssoc_Click(object sender, EventArgs e)
        {
            bool chk = FileAssociations.IsAssociated;
            string s = "";

            if (!chkAssoc.Checked)
            {
                s = "Ассоциация будет отменена ";
                FileAssociations.Remove();
            }
            else
            {
                s = "Файлы .knr будут ассоциированы с программой ";
                FileAssociations.Associate();
            }

            chkAssoc.Checked = FileAssociations.IsAssociated;

            if(chk == FileAssociations.IsAssociated)
            {
                frmMain.First = true;
                frmMain.access = "a";

                var msg = MessageBox.Show( s + "после перезапуска.\n" +
                    "Программа запросит у вас расширенные права, это требуется для того, чтобы совершить нужные действия по ассоциации файлов с программой в реестре.\n" +
                    "\nПерезапустить программу сейчас?\n\n" + 
                    "Да - сохранить настройки и перезапустить\n"+
                    "Нет - продолжить изменять настройки. Программа перезапустится после закрытия окна настроек\n" +
                    "Отмена - отказаться от внесённых изменений по ассоциации\n",
                    "Внимание!",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (msg == DialogResult.Yes)
                {
                    btnComlete.PerformClick();
                }
                else if (msg == DialogResult.Cancel)
                {
                    frmMain.First = false;
                    frmMain.access = "";
                }
            }
        }

        private void chkSave_CheckStateChanged(object sender, EventArgs e)
        {
            if ((chkSave.Checked) && (txtFolder.Text == ""))
                btnViewDir.PerformClick();
        }

        private void cmbFont_TextChanged(object sender, EventArgs e)
        {
            if (cmbFont.SelectedIndex == 0)
                fontName = "";
            else
                fontName = cmbFont.Text;
            HTMLCompleat();
        }

        private void cmbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmb = ((ComboBox)sender);
            int ind = Convert.ToInt32(cmb.Tag);
            string color = "";
            try
            {
                color = cmb.SelectedItem.ToString();

                _lblColor[ind].ForeColor = Color.FromName(color);
                _lblField[ind].ForeColor = Color.FromName(color);
                _lblColor[ind].Text = cmb.Text;

                if (color == "Black")
                    color = "";

                switch (ind)
                {
                    case 0:
                        _Fields[0] = color;
                        break;
                    case 1:
                        _Text[0] = color;
                        break;
                    case 2:
                        _Headlines[0] = color;
                        break;
                    case 3:
                        _Description[0] = color;
                        break;
                }

                HTMLCompleat();
            }
            catch (NullReferenceException)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            Process.Start("http://kinorun.online/signup.php");
        }

        private void picBold_K_Click(object sender, EventArgs e)
        {
            PictureBox pict = (PictureBox)sender;
            int picteg = Convert.ToInt32(pict.Tag);
            if (pict.BorderStyle != BorderStyle.Fixed3D)
            {
                pict.BackColor = SystemColors.ActiveCaption;
                pict.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                pict.BackColor = SystemColors.Control;
                pict.BorderStyle = BorderStyle.FixedSingle;
            }
            HTMLCompleat();
        }

        private void picTest_Kin_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            picClick(picTest_Kin);
            Cursor = Cursors.Default;
        }

        private void picTest_Pic_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            picClick(picTest_Pic);
            Cursor = Cursors.Default;
        }

        private void tbcSeting_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = this.Tag.ToString() + tbcSeting.SelectedTab.Text;
            if(tbcSeting.SelectedIndex == 1)
            {
                if ((_txtPic[0].Text != "") && (_txtPic[1].Text != ""))
                {
                    picClick(picTest_Pic);
                }

                if ((_txtKin[0].Text != "") && (_txtKin[1].Text != ""))
                {
                    picClick(picTest_Kin);
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.DocumentText.Length < 50)
                webBrowser1.DocumentText = rtxtHtml.Text;
        }

        private void chkField_CheckedChanged(object sender, EventArgs e)
        {
            HTMLCompleat();
        }

        private void txtLogin_Pic_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = btnComlete;
        }

        private void txtLogin_Pic_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = btnTest_Pic;
        }

        private void txtLogin_Kin_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = btnTest_Kin;
        }
    }
}
