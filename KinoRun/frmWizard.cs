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


namespace KinoRun
{
    public partial class frmWizard : Form
    {
        bool picpic = false;
        bool kinr = false;
        int page = 0;
        string[] _Fields = new string[4];
        string[] _Text = new string[4];
        string[] _Headlines = new string[4];
        string[] _Description = new string[4];
        string[] PicPicture = new string[2];
        string[] KinoRun = new string[2];

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

        string fontName = "";

        TextBox[] _PicPicture = new TextBox[2];
        TextBox[] _KinoRun = new TextBox[2];
        TextBox[] _txtPic = new TextBox[2];
        TextBox[] _txtKin = new TextBox[2];
        Label[] _lblColor = new Label[4];
        Button[] _btnColor = new Button[4];
        PictureBox[] _picBold = new PictureBox[4];
        PictureBox[] _picItalic = new PictureBox[4];
        PictureBox[] _picUnderline = new PictureBox[4];
        ComboBox[] _cmbColor = new ComboBox[4];


        Props props = new Props(); //экземпляр класса с настройками
        Design design = new Design(); //экземпляр класса с настройками

        public frmWizard()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            //_lblColor = new Label[] { lblColor_F, lblColor_T, lblColor_H, lblColor_D };
            //_cmbColor = new ComboBox[] { cmbColor_F, cmbColor_T, cmbColor_H, cmbColor_D };
            //_btnColor = new Button[] { btnColor_F, btnColor_T, btnColor_H, btnColor_D };
            //_picBold = new PictureBox[] { picBold_F, picBold_T, picBold_H, picBold_D };
            //_picItalic = new PictureBox[] { picItalic_F, picItalic_T, picItalic_H, picItalic_D };
            //_picUnderline = new PictureBox[] { picUnderline_F, picUnderline_T, picUnderline_H, picUnderline_D };
            //_txtPic = new TextBox[] { txtLogin_Pic, txtPass_Pic };
            //_txtKin = new TextBox[] { txtLogin_Kin, txtPass_Kin };
        }

        ////Запись настроек страницы Авторизации
        //private void writeSettingAuthorization()
        //{

        //    Array.Resize(ref design.Fields.Fields, _Fields.Length);
        //    Array.Resize(ref design.Fields.Text, _Text.Length);
        //    Array.Resize(ref design.Fields.Headlines, _Headlines.Length);
        //    Array.Resize(ref design.Fields.Description, _Description.Length);

        //    design.Fields.fontName = cmbFont.Text;
        //    for (int i = 0; i < _lblColor.Length; i++)
        //    {
        //        design.Fields.Fields[i] = _Fields[i];
        //        design.Fields.Text[i] = _Text[i];
        //        design.Fields.Headlines[i] = _Headlines[i];
        //        design.Fields.Description[i] = _Description[i];
        //    }

        //    for (int i = 0; i < 2; i++)
        //    {
        //        PicPicture[i] =_txtPic[i].Text;
        //        KinoRun[i] = _txtKin[i].Text;
        //    }

        //    design.Fields.PicPicture = AdditionFunc.AccCoding(PicPicture, 0);
        //    design.Fields.KinoRun = AdditionFunc.AccCoding(KinoRun, 0);

        //    //for (int i = 0; i < 2; i++)
        //    //{
        //    //    if (picTest_Pic.BorderStyle == BorderStyle.Fixed3D)
        //    //        design.Fields.PicPicture[i] = _txtPic[i].Text;

        //    //    if (picTest_Kin.BorderStyle == BorderStyle.Fixed3D)
        //    //        design.Fields.KinoRun[i] = _txtKin[i].Text;
        //    //}

        //    design.Fields.Tags = txtTags.Text;
        //    design.Fields.ClearDir = chkClear.Checked;

        //    design.WriteXml();
        //}

        ////Запись настроек страницы Дизайн
        //private void writeSettingDesign()
        //{
        //    Array.Resize(ref design.Fields.Fields, _Fields.Length);
        //    Array.Resize(ref design.Fields.Text, _Text.Length);
        //    Array.Resize(ref design.Fields.Headlines, _Headlines.Length);
        //    Array.Resize(ref design.Fields.Description, _Description.Length);

        //    design.Fields.fontName = cmbFont.Text;
        //    for (int i = 0; i < _lblColor.Length; i++)
        //    {
        //        design.Fields.Fields[i] = _Fields[i];
        //        design.Fields.Text[i] = _Text[i];
        //        design.Fields.Headlines[i] = _Headlines[i];
        //        design.Fields.Description[i] = _Description[i];
        //    }

        //    for (int i = 0; i < 2; i++)
        //    {
        //        PicPicture[i] = _txtPic[i].Text;
        //        KinoRun[i] = _txtKin[i].Text;
        //    }

        //    design.Fields.PicPicture = AdditionFunc.AccCoding(PicPicture, 0);
        //    design.Fields.KinoRun = AdditionFunc.AccCoding(KinoRun, 0);

        //    //for (int i = 0; i < 2; i++)
        //    //{
        //    //    if (picTest_Pic.BorderStyle == BorderStyle.Fixed3D)
        //    //        design.Fields.PicPicture[i] = _txtPic[i].Text;

        //    //    if (picTest_Kin.BorderStyle == BorderStyle.Fixed3D)
        //    //        design.Fields.KinoRun[i] = _txtKin[i].Text;
        //    //}

        //    design.Fields.Tags = txtTags.Text;
        //    design.Fields.ClearDir = chkClear.Checked;

        //    design.WriteXml();
        //}

        ////Чтение настроек
        //public void readSetting()
        //{
        //    design.ReadXml();

        //    cmbFont.Text = design.Fields.fontName;
        //    for (int i = 0; i < _lblColor.Length; i++)
        //    {
        //        _Fields[i] = design.Fields.Fields[i];
        //        _Text[i] = design.Fields.Text[i];
        //        _Headlines[i] = design.Fields.Headlines[i];
        //        _Description[i] = design.Fields.Description[i];
        //    }

        //    PicPicture = AdditionFunc.AccCoding(design.Fields.PicPicture, 1);
        //    KinoRun = AdditionFunc.AccCoding(design.Fields.KinoRun, 1);

        //    for (int i = 0; i < 2; i++)
        //    {
        //        _txtPic[i].Text = PicPicture[i];
        //        _txtKin[i].Text = KinoRun[i];
        //    }

        //    //for (int i = 0; i < 2; i++)
        //    //{
        //    //    _txtPic[i].Text = design.Fields.PicPicture[i];
        //    //    _txtKin[i].Text = design.Fields.KinoRun[i];
        //    //}
        //    txtTags.Text = design.Fields.Tags;
        //    chkClear.Checked = design.Fields.ClearDir;
        //}

        //private void picClick(PictureBox picButton)
        //{
        //    PictureBox pic = picButton;
        //    Bitmap img;
        //    BorderStyle style = pic.BorderStyle;

        //    if (pic == picTest_Pic)
        //    {
        //        if (chkEmail.Checked)
        //        {
        //            picpic = Picpicture.RegPic(txtEmail.Text, _txtPic[0].Text, _txtPic[1].Text);
        //        }
        //        else
        //        {
        //            picpic = Picpicture.AuthPic(_txtPic[0].Text, _txtPic[1].Text);
        //        }
        //    }
        //    else if (pic == picTest_Kin)
        //    {
        //        kinr = Kinorun.Auth(_txtKin[0].Text, _txtKin[1].Text);
        //    }

        //    if ((picpic) || (kinr))
        //    {
        //        img = Properties.Resources._true;
        //        style = BorderStyle.Fixed3D;
        //    }
        //    else
        //    {
        //        img = Properties.Resources._false;
        //        style = BorderStyle.None;
        //    }

        //    pic.BorderStyle = style;
        //    pic.Image = img;
        //}

        //private void UserTags()
        //{
        //    string start = "";
        //    string finish = "";

        //    //if (_lblColor[0].ForeColor.Name.ToString() != "ControlText")
        //    //    _Fields[0] = _lblColor[0].ForeColor.Name.ToString();
        //    //else
        //    //    _Fields[0] = "";
        //    //if (_lblColor[1].ForeColor.Name.ToString() != "ControlText")
        //    //    _Text[0] = _lblColor[1].ForeColor.Name.ToString();
        //    //else
        //    //    _Text[0] = "";
        //    //if (_lblColor[2].ForeColor.Name.ToString() != "ControlText")
        //    //    _Headlines[0] = _lblColor[2].ForeColor.Name.ToString();
        //    //else
        //    //    _Headlines[0] = "";
        //    //if (_lblColor[3].ForeColor.Name.ToString() != "ControlText")
        //    //    _Description[0] = _lblColor[3].ForeColor.Name.ToString();
        //    //else
        //    //    _Description[0] = "";

        //    if (_picBold[0].BorderStyle == BorderStyle.Fixed3D)
        //        _Fields[1] = "b";
        //    else
        //        _Fields[1] = "";
        //    if (_picBold[1].BorderStyle == BorderStyle.Fixed3D)
        //        _Text[1] = "b";
        //    else
        //        _Text[1] = "";
        //    if (_picBold[2].BorderStyle == BorderStyle.Fixed3D)
        //        _Headlines[1] = "b";
        //    else
        //        _Headlines[1] = "";
        //    if (_picBold[3].BorderStyle == BorderStyle.Fixed3D)
        //        _Description[1] = "b";
        //    else
        //        _Description[1] = "";

        //    if (_picItalic[0].BorderStyle == BorderStyle.Fixed3D)
        //        _Fields[2] = "i";
        //    else
        //        _Fields[2] = "";
        //    if (_picItalic[1].BorderStyle == BorderStyle.Fixed3D)
        //        _Text[2] = "i";
        //    else
        //        _Text[2] = "";
        //    if (_picItalic[2].BorderStyle == BorderStyle.Fixed3D)
        //        _Headlines[2] = "i";
        //    else
        //        _Headlines[2] = "";
        //    if (_picItalic[3].BorderStyle == BorderStyle.Fixed3D)
        //        _Description[2] = "i";
        //    else
        //        _Description[2] = "";

        //    if (_picUnderline[0].BorderStyle == BorderStyle.Fixed3D)
        //        _Fields[3] = "u";
        //    else
        //        _Fields[3] = "";
        //    if (_picUnderline[1].BorderStyle == BorderStyle.Fixed3D)
        //        _Text[3] = "u";
        //    else
        //        _Text[3] = "";
        //    if (_picUnderline[2].BorderStyle == BorderStyle.Fixed3D)
        //        _Headlines[3] = "u";
        //    else
        //        _Headlines[3] = "";
        //    if (_picUnderline[3].BorderStyle == BorderStyle.Fixed3D)
        //        _Description[3] = "u";
        //    else
        //        _Description[3] = "";

        //    if (cmbFont.SelectedIndex != 0)
        //    {
        //        s_fontName = start + "<span style=\"font-family:" + cmbFont.Text + "\">";
        //        f_fontName = "</span>" + finish;
        //    }

        //    for (int i = 1; +i < _Fields.Length; i++)
        //    {
        //        if (_Fields[i] != "")
        //        {
        //            start = start + "<" + _Fields[i] + ">";
        //            finish = "</" + _Fields[i] + ">" + finish;
        //        }
        //    }

        //    if (_Fields[0] != "")
        //    {
        //        start = start + "<span style=\"color:" + _Fields[0] + "\">";
        //        finish = "</span>" + finish;
        //    }
        //    s_Fields = start;
        //    f_Fields = finish;
        //    start = "";
        //    finish = "";

        //    for (int i = 1; i < _Text.Length; i++)
        //    {
        //        if (_Text[i] != "")
        //        {
        //            start = start + "<" + _Text[i] + ">";
        //            finish = "</" + _Text[i] + ">" + finish;
        //        }
        //    }

        //    if (_Text[0] != "")
        //    {
        //        start = start + "<span style=\"color:" + _Text[0] + "\">";
        //        finish = "</span>" + finish;
        //    }
        //    s_Text = start;
        //    f_Text = finish;
        //    start = "";
        //    finish = "";

        //    for (int i = 1; i < _Headlines.Length; i++)
        //    {
        //        if (_Headlines[i] != "")
        //        {
        //            start = start + "<" + _Headlines[i] + ">";
        //            finish = "</" + _Headlines[i] + ">" + finish;
        //        }
        //    }

        //    if (_Headlines[0] != "")
        //    {
        //        start = start + "<span style=\"color:" + _Headlines[0] + "\">";
        //        finish = "</span>" + finish;
        //    }
        //    s_Headlines = start;
        //    f_Headlines = finish;
        //    start = "";
        //    finish = "";

        //    for (int i = 1; i < _Description.Length; i++)
        //    {
        //        if (_Description[i] != "")
        //        {
        //            start = start + "<" + _Description[i] + ">";
        //            finish = "</" + _Description[i] + ">" + finish;
        //        }
        //    }

        //    if (_Description[0] != "")
        //    {
        //        start = start + "<span style=\"color:" + _Description[0] + "\">";
        //        finish = "</span>" + finish;
        //    }
        //    s_Description = start;
        //    f_Description = finish;
        //    start = "";
        //    finish = "";
        //}

        //private void HTMLCompleat()
        //{
        //    rtxtHtml.Clear();
        //    UserTags();
        //    rtxtHtml.AppendText("<html><head><style type=\"text/css\"><!--");
        //    rtxtHtml.AppendText("html { background-color:#eee;padding:0px;margin:0px;min-width:1200px;font-family:Calibri, sans-serif;font-size:14px; }");
        //    rtxtHtml.AppendText("body { padding:0px;margin:0px;font:normal 14px Calibri;color:#595959;background:#efefef; }");
        //    rtxtHtml.AppendText("--></style></head><body>");
        //    rtxtHtml.AppendText(s_fontName);
        //    rtxtHtml.AppendText(s_Headlines + "Это заголовок раздела<br>" + f_Headlines);
        //    rtxtHtml.AppendText(s_Fields + "Поле строки: " + f_Fields);
        //    rtxtHtml.AppendText(s_Text + " Значение<br>" + f_Text);
        //    rtxtHtml.AppendText(s_Fields + "Поле строки: " + f_Fields);
        //    rtxtHtml.AppendText(s_Text + " Значение<br>" + f_Text);
        //    rtxtHtml.AppendText(s_Fields + "Поле строки: " + f_Fields);
        //    rtxtHtml.AppendText(s_Text + " Значение<br>" + f_Text);
        //    rtxtHtml.AppendText("<hr>");
        //    rtxtHtml.AppendText(s_Description + "Этот небольшой блок будет символизировать <br>описание раздачи. Сделаем его побольше, <br>чтоб было заметнее результат.<br>" + f_Description);
        //    rtxtHtml.AppendText(f_fontName);
        //    rtxtHtml.AppendText("</body></html>");

        //    webBrowser1.DocumentText = rtxtHtml.Text;
        //}

        //private void frmPreviev_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    frmMain frm = (frmMain)this.Owner;

        //    frm.readSetting();
        //    this.Close();
        //}

        //private void picBold_F_Click(object sender, EventArgs e)
        //{
        //    PictureBox pict = (PictureBox)sender;
        //    int picteg = Convert.ToInt32(pict.Tag);
        //    if (pict.BorderStyle != BorderStyle.Fixed3D)
        //    {
        //        pict.BackColor = SystemColors.ActiveCaption;
        //        pict.BorderStyle = BorderStyle.Fixed3D;
        //    }
        //    else
        //    {
        //        pict.BackColor = SystemColors.Control;
        //        pict.BorderStyle = BorderStyle.FixedSingle;
        //    }
        //    HTMLCompleat();
        //}

        //private void chkEmail_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtEmail.Enabled = chkEmail.Checked;
        //    lblEmail.Enabled = chkEmail.Checked;
        //    if (chkEmail.Checked)
        //        btnPic.Text = "Регистрация";
        //    else
        //        btnPic.Text = "Проверка";
        //}

        //private void cmbboxClr_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    Rectangle rect = e.Bounds;
        //    if (e.Index >= 0)
        //    {
        //        string n = ((ComboBox)sender).Items[e.Index].ToString();
        //        Font f = new Font("Arial", 9, FontStyle.Regular);
        //        Color c = Color.FromName(n);
        //        Brush b = new SolidBrush(c);
        //        g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
        //        g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
        //    }
        //}

        //private void frmWizard_Load(object sender, EventArgs e)
        //{
        //    readSetting();
        //    Type colorType = typeof(System.Drawing.Color);
        //    PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

        //    for (int i = 0; i < _cmbColor.Length; i++)
        //    {
        //        foreach (PropertyInfo c in propInfoList)
        //        {
        //            this._cmbColor[i].Items.Add(c.Name);
        //        }
        //    }


        //    this.Cursor = Cursors.WaitCursor;
        //    //this.Text = this.Tag.ToString() + tbcSeting.SelectedTab.Text;
        //    //readSetting();
        //    rtxtHtml.Hide();

        //    if (fontName == "")
        //        cmbFont.SelectedIndex = 0;
        //    else
        //        cmbFont.Text = fontName;
        //    readSetting();

        //    //for (int i = 0; i < 2; i++)
        //    //{
        //    //    _txtPic[i].Text = _PicPicture[i];
        //    //    _txtKin[i].Text = _KinoRun[i];
        //    //}

        //    //_lblColor[0].Text = _Fields[0];
        //    //_lblColor[1].Text = _Text[0];
        //    //_lblColor[2].Text = _Headlines[0];
        //    //_lblColor[3].Text = _Description[0];

        //    //_lblColor[0].ForeColor = ColorTranslator.FromHtml(_Fields[0]);
        //    //_lblColor[1].ForeColor = ColorTranslator.FromHtml(_Text[0]);
        //    //_lblColor[2].ForeColor = ColorTranslator.FromHtml(_Headlines[0]);
        //    //_lblColor[3].ForeColor = ColorTranslator.FromHtml(_Description[0]);

        //    _picBold[0].BorderStyle = (_Fields[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picBold[1].BorderStyle = (_Text[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picBold[2].BorderStyle = (_Headlines[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picBold[3].BorderStyle = (_Description[1] == "b") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;

        //    _picItalic[0].BorderStyle = (_Fields[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picItalic[1].BorderStyle = (_Text[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picItalic[2].BorderStyle = (_Headlines[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picItalic[3].BorderStyle = (_Description[2] == "i") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;

        //    _picUnderline[0].BorderStyle = (_Fields[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picUnderline[1].BorderStyle = (_Text[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picUnderline[2].BorderStyle = (_Headlines[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        //    _picUnderline[3].BorderStyle = (_Description[3] == "u") ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;

        //    _cmbColor[0].Text = _Fields[0];
        //    _cmbColor[1].Text = _Text[0];
        //    _cmbColor[2].Text = _Headlines[0];
        //    _cmbColor[3].Text = _Description[0];

        //    foreach (PictureBox pict in _picBold)
        //    {
        //        if (pict.BorderStyle != BorderStyle.Fixed3D)
        //            pict.BackColor = SystemColors.Control;
        //        else
        //            pict.BackColor = SystemColors.ActiveCaption;
        //    }
        //    foreach (PictureBox pict in _picItalic)
        //    {
        //        if (pict.BorderStyle != BorderStyle.Fixed3D)
        //            pict.BackColor = SystemColors.Control;
        //        else
        //            pict.BackColor = SystemColors.ActiveCaption;
        //    }
        //    foreach (PictureBox pict in _picUnderline)
        //    {
        //        if (pict.BorderStyle != BorderStyle.Fixed3D)
        //            pict.BackColor = SystemColors.Control;
        //        else
        //            pict.BackColor = SystemColors.ActiveCaption;
        //    }
        //    HTMLCompleat();
        //    this.Cursor = Cursors.Default;

        //    webBrowser1.DocumentText = rtxtHtml.Text;
        //}

        //private void frmWizard_Activated(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (PictureBox pict in _picBold)
        //        {
        //            if (pict.BorderStyle != BorderStyle.Fixed3D)
        //                pict.BackColor = SystemColors.Control;
        //            else
        //                pict.BackColor = SystemColors.ActiveCaption;
        //        }
        //        foreach (PictureBox pict in _picItalic)
        //        {
        //            if (pict.BorderStyle != BorderStyle.Fixed3D)
        //                pict.BackColor = SystemColors.Control;
        //            else
        //                pict.BackColor = SystemColors.ActiveCaption;
        //        }
        //        foreach (PictureBox pict in _picUnderline)
        //        {
        //            if (pict.BorderStyle != BorderStyle.Fixed3D)
        //                pict.BackColor = SystemColors.Control;
        //            else
        //                pict.BackColor = SystemColors.ActiveCaption;
        //        }
        //        HTMLCompleat();
        //    }
        //    catch (NullReferenceException)
        //    {
        //        InitForm();
        //    }
        //}

        //private void cmbColor_F_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var cmb = ((ComboBox)sender);
        //    int ind = Convert.ToInt32(cmb.Tag);
        //    string color = cmb.SelectedItem.ToString();

        //    _lblColor[ind].ForeColor = Color.FromName(color);
        //    //if(color == )

        //    if (color == "Black")
        //        color = "";

        //    switch (ind)
        //    {
        //        case 0:
        //            _Fields[0] = color;
        //            break;
        //        case 1:
        //            _Text[0] = color;
        //            break;
        //        case 2:
        //            _Headlines[0] = color;
        //            break;
        //        case 3:
        //            _Description[0] = color;
        //            break;
        //    }

        //    HTMLCompleat();
        //}

        //private void btnColor_F_Click(object sender, EventArgs e)
        //{
        //    string str = "#";
        //    string color = "";
        //    Button btn = (Button)sender;
        //    int tag = Convert.ToInt32(btn.Tag);

        //    if (dlgColor.ShowDialog() == DialogResult.OK)
        //    {
        //        _lblColor[tag].ForeColor = dlgColor.Color;
        //        if (dlgColor.Color.Name[0] == 'f')
        //        {
        //            string hex = dlgColor.Color.R.ToString("X2") + dlgColor.Color.G.ToString("X2") + dlgColor.Color.B.ToString("X2");
        //            color = str + hex;
        //        }
        //        else
        //            color = dlgColor.Color.Name;
        //    }

        //    if (color == "Black")
        //        color = "";

        //    switch(tag)
        //    {
        //        case 0:
        //            _Fields[0] = color;
        //            break;
        //        case 1:
        //            _Text[0] = color;
        //            break;
        //        case 2:
        //            _Headlines[0] = color;
        //            break;
        //        case 3:
        //            _Description[0] = color;
        //            break;
        //    }

        //    HTMLCompleat();
        //}

        //private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbFont.SelectedIndex == 0)
        //        fontName = "";
        //    else
        //        fontName = cmbFont.Text;
        //    HTMLCompleat();
        //}

        //private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    this.linkLabel1.LinkVisited = true;
        //    Process.Start("http://kinorun.com/signup.php");
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    var btn = ((Button)sender);

        //    if (btn.Text != "Готово")
        //    {
        //        pnlAccounts.Location = new Point(0, 0);
        //        pnlDesign.Location = new Point(0, 0);
        //        pnlEnd.Location = new Point(0, 0);

        //        pnlAccounts.Visible = false;
        //        pnlDesign.Visible = false;
        //        pnlEnd.Visible = false;

        //        btnNext.Visible = false;
        //        btnBack.Visible = false;

        //        if (btn.Name == "btnNext")
        //            page = page + 1;
        //        else
        //            page = page - 1;

        //        if (page == 0)
        //        {
        //            pnlAccounts.Visible = true;
        //            btnBack.Visible = false;
        //            btnNext.Visible = true;
        //        }
        //        else if (page == 1)
        //        {
        //            writeSettingAuthorization();
        //            pnlDesign.Visible = true;
        //            btnBack.Visible = true;
        //            btnNext.Visible = true;
        //            HTMLCompleat();
        //        }
        //        else
        //        {
        //            writeSettingDesign();
        //            pnlEnd.Visible = true;
        //            btnBack.Visible = true;
        //            btnNext.Visible = true;
        //            btnNext.Text = "Готово";
        //        }
        //        if (webBrowser1.DocumentText.Length < 50)
        //            webBrowser1.DocumentText = rtxtHtml.Text;
        //    }
        //    else
        //    {
        //        btnClose.PerformClick();
        //    }
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    if(page < 2)
        //    {
        //        var msg = MessageBox.Show("Сохранить изменения настроек?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //        if (msg == DialogResult.Yes)
        //        {
        //            writeSettingAuthorization();
        //            writeSettingDesign();
        //            this.Close();
        //        }
        //        else if(msg == DialogResult.No)
        //        {
        //            this.Close();
        //        }
                    
        //    }
        //    else
        //    {
        //        this.Close();
        //    }
        //}

        //private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    //if (webBrowser1.DocumentText.Length < 50)
        //    //    webBrowser1.DocumentText = rtxtHtml.Text;
        //}

        //private void btnPic_Click(object sender, EventArgs e)
        //{
        //    var btn = ((Button)sender);
        //    if (btn.Name == "btnPic")
        //        picClick(picTest_Pic);
        //    else
        //        picClick(picTest_Kin);
        //}

        //private void picTest_Kin_Click(object sender, EventArgs e)
        //{
        //    var btn = ((PictureBox)sender);
        //    if (btn == picTest_Pic)
        //        picClick(picTest_Pic);
        //    else
        //        picClick(picTest_Kin);
        //}
    }
}
