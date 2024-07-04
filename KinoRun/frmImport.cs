using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Windows.Forms;
using xNet;

namespace KinoRun
{
    public partial class frmImport : Form
    {
        bool first = false;
        string[] link = new string[3];
        string txt = String.Empty;
        private string[] parsText = new string[15];
        char[] charsToTrim = { ',', '.', ';', '-', '/' };
        string[] _PicPicture = new string[2];
        string[] _KinoRun = new string[2];
        int cnt = 0;
        int _iLine = 0;
        ComboBox[] _cmbFields = new ComboBox[11];
        public static string[] _Fields = new string[11];
        string[] Fields = new string[11];
        string[] descr = new string[0];
        int[] iFields = new int[11];
        Design design = new Design(); //экземпляр класса с настройками

        public frmImport()
        {
            InitializeComponent();
            _cmbFields = new ComboBox[] { cmbField1, cmbField2, cmbField3, cmbField4, cmbField5, cmbField6, cmbField7, cmbField8, cmbField9, cmbField10, cmbField11 };
            _Fields = new string[] { "Русское название",
                "Оригинальное название",
                "Год выпуска",
                "Жанр",
                "Режиссёр",
                "Студия",
                "В ролях",
                "Продолжительность",
                "Страна",
                "Язык",
                "Описание" };
        }

        //Чтение настроек
        public void readSetting()
        {
            try
            {
                design.ReadXml();
            }
            catch (FileNotFoundException)
            {

            }

            for (int i = 0; i < 2; i++)
            {
                _PicPicture[i] = design.Fields.PicPicture[i];
            }
        }

        private void Rules_I()
        {
            string[] str = new string[2];
            string str1 = "";
            string sstr = "";

            str1 = txtInfo_Images.Text.Trim('\n', '\r');
            txtInfo_Images.Text = str1.Trim('\n', '\r');

            try
            {
                str[0] = txtInfo_Images.Lines[0].Trim();
            }
            catch (IndexOutOfRangeException)
            {
                //MessageBox.Show("Заполните хотя бы обязательные поля", "Ошибка", MessageBoxButtons.OK);
            }
            str1 = "";
            for (int i = 1; i < txtInfo_Images.Lines.Length; i++)
            {
                if (str.Length > 0)
                {
                    str1 = txtInfo_Images.Lines[i].Replace("|", "");
                    str1 = str1.Replace("[/url][url", "[/url] [url");
                    //str1 = str1.Replace("http://", "\nhttp://");
                    sstr = sstr + str1.Trim() + "|";
                }
            }

            if ((str[0] != null) || (str[1] != null))
            {
                if (str[1] != null)
                    str[1] = sstr.Trim('|');
                else
                {
                    str1 = str[0];
                    string[] line = str[0].Split('|');
                    str[0] = line[0];
                    str[1] = str1.Replace(str[0], "");
                }
                //sstr = sstr.ToLower();
                txtInfo_Images.Text = str[0] + "\n\r" + str[1] + "\n\r" + sstr;
                //txtInfo_Images.Text = ;
            }
        }

        private void Rules_V()
        {
            string[] str = new string[rtxtInfo_Video.Lines.Length];
            string[] istr = new string[11];
            string sstr = "";
            string chr;
            int j = 0;
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    str[i] = rtxtInfo_Video.Lines[i].Trim();

                    if (i != 7)
                    {
                        string[] line = str[i].Split(':');
                        if (line.Length > 1)
                        {
                            str[i] = line[1].Trim();
                        }
                    }

                    if (i == 2)
                    {
                        int number;
                        //if (Int32.TryParse(rtxtInfo_Video.Lines[i], out number))
                        //    str[i] = rtxtInfo_Video.Lines[i].Trim();
                        //else
                        //{
                        //    foreach (char ch in rtxtInfo_Video.Lines[i].Trim())
                        if (Int32.TryParse(str[i], out number))
                            istr[i] = str[i].Trim();
                        else
                        {
                            foreach (char ch in str[i].Trim())
                            {
                                chr = Convert.ToString(ch);
                                if (Int32.TryParse(chr, out number))
                                    sstr = sstr + chr;
                            }
                            istr[i] = sstr;
                        }
                    }
                    else if (i == 7)
                    {
                        string[] line = str[i].Split(':');
                        int number;
                        if (!(Int32.TryParse(line[0], out number)))
                            istr[i] = str[i].Replace(line[0] + ":", "");

                        DateTime dat;
                        //if (DateTime.TryParse(rtxtInfo_Video.Lines[i], out dat))
                        //    str[i] = rtxtInfo_Video.Lines[i].Trim();
                        if (DateTime.TryParse(str[i], out dat))
                            istr[i] = str[i].Trim();
                        else
                        {
                            sstr = rtxtInfo_Video.Lines[i].Trim();
                            sstr = sstr.Replace("-", " ");
                            sstr = sstr.Replace(".", " ");
                            sstr = sstr.Replace(",", " ");
                            sstr = sstr.Replace(";", " ");
                            sstr = sstr.Replace("/", " ");
                            sstr = sstr.Replace("|", " ");
                            sstr = sstr.Replace("  ", " ");
                            string[] wtime = sstr.Split(' ');

                            j = 0;
                            sstr = "";
                            foreach (string s in wtime)
                            {
                                wtime[j] = wtime[j].TrimStart(charsToTrim);
                                wtime[j] = wtime[j].TrimEnd(charsToTrim);
                                if (DateTime.TryParse(wtime[j], out dat))
                                    sstr = sstr + " " + wtime[j].Trim();

                                j++;
                            }
                            istr[i] = sstr.Trim();
                        }
                    }
                    else
                    {
                        try
                        {
                            istr[i] = str[i].Trim();
                        }
                        catch (IndexOutOfRangeException)
                        {
                            //MessageBox.Show("Заполните хотя бы обязательные поля", "Ошибка", MessageBoxButtons.OK);
                            break;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            sstr = "";
            for (int i = 10; i < rtxtInfo_Video.Lines.Length; i++)
            {
                if (rtxtInfo_Video.Lines[i] != "")
                {
                    if (i == 10)
                    {
                        str[i] = rtxtInfo_Video.Lines[i];
                        string[] strline = str[i].Split('|');

                        if (2 > strline.Length)
                        {
                            string[] line = str[i].Split(':');
                            if (line.Length > 1)
                            {
                                str[i] = line[1].Trim();
                            }
                            else if (line.Length > 2)
                            {
                                str[i] = "";

                                for (int l = 1; l < line.Length; l++)
                                {
                                    sstr = sstr + ":" + line[l].Trim();
                                }

                                str[i] = sstr;
                                sstr = "";
                            }
                        }
                        sstr = str[i] + "|";
                    }
                    else
                    {
                        str[i] = rtxtInfo_Video.Lines[i];
                        //if (i == 11)
                        //    sstr = sstr + "|";
                        if (str[i] != null)
                        {
                            if (str[i].Length > 1)
                                sstr = sstr + str[i].Trim() + "|";
                        }
                    }
                    //try
                    //{
                    //    if (str[i] != "")
                    //        sstr = sstr + str[i].Trim() + "|";
                    //    //m++;
                    //}
                    //catch (NullReferenceException)
                    //{
                    //    sstr = sstr + rtxtInfo_Video.Lines[i].Trim() + "|";
                    //}
                }
                //else
                //{
                //    for (int m = i; m < rtxtInfo_Video.Lines.Length; m++)
                //    {

                //    }
            }

            //sstr = sstr.Replace("\r", "");
            //sstr = sstr.Replace("\n", "");
            istr[10] = sstr.TrimEnd('|');
            rtxtInfo_Video.Clear();

            for (int i = 0; i < istr.Length; i++)
            {
                //str[i] = str[i].Replace("\r", "");
                //str[i] = str[i].Replace("\n", "");
                rtxtInfo_Video.AppendText(istr[i] + "\n");
            }
        }

        private string ClearField(string Line)
        {
            string str = Line;
            string[] sar = Line.Split(':');

            if (sar.Length > 1)
            {
                str = "";
                for (int i = 1; i < sar.Length; i++)
                {
                    str = str + ":" + sar[i];
                }
                str = str.Trim(':', ' ');
            }

            return str;
        }

        private void FieldsControl() //Проверка определения полей на выборность
        {
            for (int i = 0; i < rtxtInfo_Video.Lines.Length; i++)
            {
                int ind = -1;
                int[] _temp = new int[0];

                for (int j = 0; j < _cmbFields.Length; j++)
                {
                    if (iFields[j] < 0)
                    {
                        Array.Resize(ref _temp, _temp.Length + 1);
                        _temp[j] = j;
                    }
                }

                if (_cmbFields.Length > i)
                {
                    if (frmImpControl.Input("Внимание!", "Неудалось определить поле строки: " + (i + 1).ToString(), rtxtInfo_Video.Lines[i], _Fields, _temp, out ind))
                    {
                        _cmbFields[i].SelectedIndex = ind;
                    }
                    else
                    {
                        //Array.Resize(ref descr, descr.Length + 1);
                        //descr[descr.Length - 1] = rtxtInfo_Video.Lines[i];
                        Fields[i] = "";
                        rtxtInfo_Video.Lines = Fields;
                        i--;
                    }
                }
                else
                {
                    Array.Resize(ref descr, descr.Length + 1);
                    descr[descr.Length - 1] = rtxtInfo_Video.Lines[i];
                }
            }
        }

        private int ChekField(int Index)
        {
            int k = -1;

            return k;
        }

        private void FieldControl(int kk)
        {
            string[] oldFields = Fields;
            cnt = 0;
            if (!chkAuto.Checked)
            {
                for (int i = kk; i < rtxtInfo_Video.Lines.Length; i++)
                {
                    try
                    {
                        if ((Fields[i] != "") || (Fields[i] != null))
                            cnt++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        cnt = rtxtInfo_Video.Lines.Length;
                        break;
                    }
                }
            }
            int dsk = -1;
            int count = (rtxtInfo_Video.Lines.Length < _cmbFields.Length) ? rtxtInfo_Video.Lines.Length : _cmbFields.Length;
            //Fields = rtxtInfo_Video.Lines;

            for (int i = kk; i < _cmbFields.Length; i++)
            {
                try
                {
                    if (count > i)
                    {
                        int ind = _cmbFields[i].SelectedIndex;
                        int oldind = _cmbFields[i].SelectedIndex;
                        oldind = iFields[i];
                        iFields[i] = ind;
                        //Fields[ind] = rtxtInfo_Video.Lines[i];

                        if (btnClear.Enabled)
                            _cmbFields[i].SelectedIndex = -1;

                        int indd = ChangeField(i);

                        //_cmbFields[i].SelectedIndex = indd;

                        if (rtxtInfo_Video.Lines[i] != Fields[i])
                        {
                            //if ((dsk > -1) && (indd == 10))
                            //    indd = -1;
                            _cmbFields[i].SelectedIndex = indd;
                            iFields[i] = indd;
                            Fields[i] = rtxtInfo_Video.Lines[i];
                            if (indd == 10)
                            {
                                dsk = i;
                            }
                        }
                        else
                            _cmbFields[i].SelectedIndex = oldind;
                    }
                    else
                    {
                        _cmbFields[i].SelectedIndex = -1;
                    }
                    //else
                    //    _cmbFields[i].SelectedIndex = Array.IndexOf(oldFields, rtxtInfo_Video.Lines[1]);


                    //if ((ind < 0) && ((ind != 0) || (ind != 1) || (ind != 9) || (ind != 10) || (ind != 13)))
                    //{

                    //    _cmbFields[i].SelectedIndex = ChangeField(rtxtInfo_Video.Lines[i]);
                    //}
                    //if(ind < 0)
                    //_cmbFields[i].SelectedIndex = ChangeField(rtxtInfo_Video.Lines[i], _cmbFields[i].SelectedIndex);

                }
                catch (IndexOutOfRangeException ex)
                {
                    break;
                    //continue;
                }
            }
        }

        private int ChangeField(int Index)
        {
            int ind = -1;
            string Line = rtxtInfo_Video.Lines[Index];
            int indd = _cmbFields[Index].SelectedIndex;
            string[] line = Line.Split(':');
            if (Line.Length > 0)
            {
                string[] sline = Line.Split(':');
                string[] s = sline[0].Split(' ');

                //if (s[0] == "Год")
                //    sline[0] = "Год выпуска";
                //if (s[0] == "Имя")
                //    sline[0] = "В ролях";
                //if (s[0] == "Жанры")
                //    sline[0] = "Жанр";
                //if (s[0] == "Подсайт")
                //    sline[0] = "Студия";
                //if (sline[0] == "Оригинальный аудиопоток")
                //    sline[0] = "Язык";
                //if ((sline[0] == "Режиссер") || (sline[0] == "Режисер") || (sline[0] == "Режисёр"))
                //    sline[0] = "Режисcёр";
                //if(Line.Length < )
                if (sline.Length > 1)
                {
                    string sline_ = sline[0].ToLower();

                    if (sline_.IndexOf("год") >= 0)
                        sline[0] = "Год выпуска";
                    if (sline_.IndexOf("имя") >= 0)
                        sline[0] = "В ролях";
                    if (sline_.IndexOf("жанр") >= 0)
                        sline[0] = "Жанр";
                    if (sline_.IndexOf("подсайт") >= 0)
                        sline[0] = "Студия";
                    if (sline_.IndexOf("аудиопоток") >= 0)
                        sline[0] = "Язык";
                }
                s[0] = s[0].ToLower();
                if ((s[0] == "режиссер") || (s[0] == "режисер") || (s[0] == "режисёр"))
                    sline[0] = "Режиссёр";
                //try
                //{
                //    if (s[1] == "описание")
                //        sline[0] = "Описание";
                //}
                //catch(IndexOutOfRangeException)
                //{
                //    if (s[0] == "Описание")
                //        sline[0] = "Описание";
                //}
                //bool f = (sline[0] == "Режисcёр");
                ind = _cmbFields[0].Items.IndexOf(sline[0]);

                if (((ind < 0) && ((indd != 0) && (indd != 1) && (indd != 10))) || (ind == 7)) //Проверка на Время
                {
                    string istr = "";
                    int number;
                    if (!(Int32.TryParse(line[0], out number)))
                        istr = Line.Replace(line[0] + ":", "");

                    DateTime dat;
                    if (DateTime.TryParse(Line, out dat))
                        ind = 7;
                    else
                    {
                        string sstr = Line;
                        sstr = sstr.Replace("-", " ");
                        sstr = sstr.Replace("+", " ");
                        sstr = sstr.Replace(".", " ");
                        sstr = sstr.Replace(",", " ");
                        sstr = sstr.Replace(";", " ");
                        sstr = sstr.Replace("/", " ");
                        sstr = sstr.Replace("|", " ");
                        sstr = sstr.Replace("  ", " ");

                        string sstr1 = sstr.Replace(_Fields[7] + ":", "");
                        sstr1 = sstr1.Trim();

                        string[] wtime = sstr.Split(' ');
                        //string[] wtime1 = new string[3];
                        //string[] wtime = sstr1.Split(':');
                        //if (wtime.Length < 3)
                        //    wtime = sstr1.Split(' ');
                        //else
                        //    wtime = sstr.Split(' ');

                        //int j = 0;
                        //if(wtime.Length > 0)
                        //{
                        //    string chr;
                        //    for(j = 0; j < wtime.Length; j++)
                        //    {
                        //        foreach (char ch in wtime[j].Trim())
                        //        {
                        //            chr = Convert.ToString(ch);
                        //            if (Int32.TryParse(chr, out number))
                        //                wtime1[j] = wtime1[j] + chr;
                        //        }
                        //    }
                        //    if (wtime1[2] == null)
                        //        wtime1[2] = "00";

                        //    wtime[0] = wtime1[0] + ":" + wtime1[1] + ":" + wtime1[2];
                        //}

                        int j = 0;
                        sstr = "";
                        foreach (string st in wtime)
                        {
                            wtime[j] = wtime[j].TrimStart(charsToTrim);
                            wtime[j] = wtime[j].TrimEnd(charsToTrim);
                            if (DateTime.TryParse(wtime[j], out dat))
                                sstr = sstr + " " + wtime[j].Trim();

                            j++;
                        }
                        sstr = sstr.Trim();
                        if (sstr != "")
                        {
                            ind = 7;

                        }
                    }

                }

                if (((ind < 0) && (Line.Length < 11) && (line.Length == 1)) || (ind == 2)) //Проверка на Год
                {
                    string str = "";
                    string[] istr = new string[11];
                    string sstr = "";
                    string chr;
                    int number;
                    if (Int32.TryParse(Line, out number))
                        str = Line.Trim();
                    else
                    {
                        foreach (char ch in Line.Trim())
                        {
                            chr = Convert.ToString(ch);
                            if (Int32.TryParse(chr, out number))
                                sstr = sstr + chr;
                        }
                        try
                        {
                            if (Int32.TryParse(sstr, out number))
                                if (Convert.ToInt32(sstr) > 1900)
                                    str = sstr;
                        }
                        catch (FormatException)
                        {

                        }
                    }

                    if (str != "")
                    {
                        if (Convert.ToInt32(str) > 1900)
                        {
                            ind = 2;
                            Fields[Index] = str;
                        }

                        //if (Line.IndexOf(":") > -1)
                        //    ind = -1;
                    }
                }

                if ((ind < 0) && (Index < 2) && (Line.Any(wordByte => wordByte > 127))) //Проверка на кириллицу
                {
                    ind = 0;
                }
                else if ((ind < 0) && (Index < 2) && (Line.Any(wordByte => wordByte < 128))) //Проверка на латиницу
                    ind = 1;

                if ((ind < 0) && (Line.Length < 15)) //Проверка на язык
                {
                    if (Line.Any(wordByte => wordByte > 127))
                    {
                        string suffix = Line.Remove(0, Line.Length - 3);
                        if (suffix == "кий")
                            ind = 9;
                    }
                    else
                    {
                        if (Array.IndexOf(iFields, 9) == -1)
                        {
                            string[] lang = { "english",
                                            "deutsch",
                                            "franch",
                                            "Italian",
                                            "czech",
                                            "français"};

                            if (Array.IndexOf(lang, Line.ToLower()) > -1)
                                ind = 9;
                        }
                    }
                }

                if ((ind < 0) && (Line.Length < 20)) //Проверка на страну
                {
                    string[] country = { "россия",
                                        "сша",
                                        "европа",
                                        "ес",
                                        "чехия",
                                        "германия",
                                        "италия",
                                        "франция",
                                        "czech republic",
                                        "italy",
                                        "france",
                                        "eu",
                                        "europe",
                                        "usa"};

                    if (Array.IndexOf(iFields, 8) == -1)
                    {
                        if (Array.IndexOf(country, Line.ToLower()) > -1)
                            ind = 8;
                    }
                }

                if ((ind < 0)) //Проверка на Жанры, Актёров и Описание
                {
                    string[] ganre = Line.Split(',');
                    int res = 0;
                    int max = 0;

                    for (int i = 0; i < ganre.Length; i++)
                    {
                        if (Array.IndexOf(frmGenre.strGenre, ganre[i].Trim()) > -1)
                            res++;
                        if (ganre[i].Length > max)
                            max = ganre[i].Length;
                    }
                    if (res > 0)
                        ind = 3;
                    else
                    {
                        if (ganre.Length > 1)
                        {
                            if (max < 20)
                            {
                                if (Array.IndexOf(iFields, 3) > -1)
                                    ind = 6;

                                if (Array.IndexOf(iFields, 6) > -1)
                                    ind = 3;
                            }
                            else
                            {
                                //if (Array.IndexOf(iFields, 10) < 0)
                                ind = 10;
                            }
                        }
                    }
                }

                if ((ind < 0) && (ind < 10))
                {
                }

                int cnt = 0;
                int[] _temp = new int[0];
                for (int j = 0; j < _cmbFields.Length; j++)
                {
                    if (Array.IndexOf(iFields, j) < 0)
                    {
                        try
                        {
                            cnt = _temp.Length;
                        }
                        catch (IndexOutOfRangeException)
                        { }
                        Array.Resize(ref _temp, cnt + 1);
                        _temp[_temp.Length - 1] = j;
                    }
                }

                if ((ind < 0) && (_temp.Length == 1))
                {
                    ind = _temp[0];
                }

                if (ind != iFields[Index])
                {
                    //int indd = Array.IndexOf(Fields, Line);

                    //if (ind < 0)
                    //    ind = iFields[indd];

                    //if (((ind!= 2) || (ind!= 7)) && ((Index == 2) || (Index == 7)))
                    //    ind = Index;
                    if ((ind < 0) && ((ind != 2) || (ind != 7))) // && ((Index == 2) || (Index == 7)))
                        ind = iFields[Index];
                }
            }
            else
                ind = -1;

            return ind;
        }

        private String[] Parser(string strLine)
        {
            string http = strLine.Replace("\nhttp", "http");
            strLine = http.Replace("http", "\nhttp");
            strLine = strLine.Replace("c:\\", "\nc:\\");
            strLine = strLine.Replace("d:\\", "\nd:\\");
            strLine = strLine.Replace("e:\\", "\ne:\\");
            strLine = strLine.Replace("f:\\", "\nf:\\");
            strLine = strLine.Replace("g:\\", "\ng:\\");
            strLine = strLine.Replace("h:\\", "\nh:\\");
            strLine = strLine.Replace("j:\\", "\nj:\\");
            strLine = strLine.Replace("k:\\", "\nk:\\");
            strLine = strLine.Replace("l:\\", "\nl:\\");
            string[] str = strLine.Split('\n');

            Array.Resize(ref parsText, str.Length);

            return parsText = strLine.Split('\n');
        }

        private void Description(int Index)
        {
            string str = rtxtInfo_Video.Lines[Index];
            string[] line = str.Split(':');
            string sstr = "";
            int idescr = descr.Length;
            int chek = 10;

            int tmp = Array.IndexOf(iFields, chek);
            if (Array.IndexOf(iFields, chek) > -1)
                chek = -1;
            else
            {
                if (line[0] == "Описание")
                    chek = 10;
                else
                    chek = -1;
            }

            if (chek == 10)
            {
                Array.Resize(ref descr, idescr + 1);
                if ((line.Length > 1) && (line[0] == "Описание"))
                {
                    //string s = "";
                    //for (int i = 0; i < line.Length; i++)
                    //{
                    //    if ()
                    //    {

                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        if (s == "")
                    //            s = line[i];
                    //        else
                    //            s = s + ":" + line[i];
                    //    }
                    //}
                    //sstr = s.Trim();

                    descr[0] = ClearField(str);
                }
                else
                    descr[0] = ((Fields[Index].Length > 0) || (Fields[Index] != null)) ? Fields[Index] : str;
                //sstr = line[0].Trim();

            }
            else
            {
                int ind = Array.IndexOf(descr, str);
                string s = str;
                try
                {
                    s = Fields[Index];
                }
                catch (IndexOutOfRangeException)
                {
                    s = str;
                }

                if ((ind < 0) && (Array.IndexOf(descr, s) < 0))
                {
                    Array.Resize(ref descr, idescr + 1);
                }
                try
                {
                    descr[descr.Length - 1] = ((Fields[Index].Length > 0) || (Fields[Index] != null)) ? Fields[Index] : str;
                }
                catch (Exception)
                {
                    descr[descr.Length - 1] = str;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FormComplecte()
        {
            char separator = chkScreenshots.Checked ? '|' : ' ';
            string[] sFields = new string[_cmbFields.Length];
            string[] oldFields = rtxtInfo_Video.Lines;

            //rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("Перевод: Отсутствует", "");
            rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("Оригинальное описание:", "Описание:");
            rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("\npicpic", "\n");
            rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("\npic", "\n");
            rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("pic\n", "\n");
            rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("Описание на русском:", "");
            rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("’", "`");

            for (int i = 0; i < _Fields.Length; i++)
            {
                rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace(_Fields[i] + ":\n", _Fields[i] + ": ");
                rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("pic" + _Fields[i], "\n" + _Fields[i]);

                int kos = -1;
                if (i < rtxtInfo_Video.Lines.Length)
                    kos = rtxtInfo_Video.Lines[i].ToLower().IndexOf(": отсутству");

                if (kos > -1)
                    rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace(rtxtInfo_Video.Lines[i], "");

                //if (i == 2)
                //{
                //    rtxtInfo_Video.Text = rtxtInfo_Video.Text.Replace("г.", "");
                //}

                //if( i == 7)
                //{
                //    string old = rtxtInfo_Video.Text.Substring(_Fields[i] + ":", "\n");
                //    string news = "";
                //    string[] arr = old.Split(' ');

                //    if(arr.Length > 1)
                //    {

                //    }
                //}
            }

            for (int i = 0; i < _Fields.Length; i++)
            {
                Text = rtxtInfo_Video.Text.Replace((_Fields[i] + ":\n"), ("\n" + _Fields[i] + ":"));
            }
            string[] _lines = rtxtInfo_Video.Lines;

            chkAuto.Checked = false;
            rtxtInfo_Video.Clear();

            for (int i = 0; i < _lines.Length; i++)
            {
                if ((i == 0) && (_lines[i].Any(wordByte => wordByte > 127)))
                {
                    string st = "";
                    string s = _lines[i];
                    int j = 0;
                    do
                    {
                        if (j < s.Length)
                        {
                            st = st + s[j];
                            j++;
                        }
                        else
                        {
                            break;
                        }

                        if (j == s.Length)
                        {
                            break;
                        }

                    } while ((int)s[j] < 128);

                    if (st.Length == 1)
                        st = s;
                    st = st.Trim('.', '/', '\\', '-', ' ', '=', '+', '[', ']', '(', ')');

                    s = _lines[i].Replace(st, "");
                    string[] sLine = s.Split(' ');
                    s = "";
                    for (j = 0; j < sLine.Length; j++)
                    {
                        if (sLine[j].Any(wordByte => wordByte > 127))
                        {
                            s = s + " " + sLine[j];
                        }
                    }

                    s = s.Replace("г.", "");
                    s = s.Trim('.', '/', '\\', '-', ' ', ',', '=', '+', '[', ']', '(', ')');
                    if (s.Length > 0)
                    {
                        rtxtInfo_Video.AppendText(st + "\r");
                        rtxtInfo_Video.AppendText(s);
                        //i++;
                        continue;
                    }
                    else
                    {
                        rtxtInfo_Video.AppendText(st);
                    }
                }
                else if ((_lines[i].Trim().Length > 1) && (i == 0) && (rtxtInfo_Video.Text.Length <= 0))
                {
                    if (_lines[i].Trim().Length > 0)
                        rtxtInfo_Video.AppendText(_lines[i]);
                    continue;
                }
                //else if (i == 0)
                //{ 
                //}
                if (_lines[i].Trim().Length > 1)
                {
                    if (i > 0)
                        rtxtInfo_Video.AppendText("\r" + _lines[i]);
                }
            }

            for (int i = 0; i < _cmbFields.Length; i++)
            {
                try
                {
                    try
                    {
                        if ((_cmbFields[i].SelectedIndex == 10) || (_cmbFields[i - 1].SelectedIndex == 10))
                        {
                            //Array.Clear(descr, 0, descr.Length);
                            for (int j = i; j < rtxtInfo_Video.Lines.Length; j++)
                            {
                                if (rtxtInfo_Video.Lines[j] != "")
                                    Description(j);
                            }
                            Fields[i] = descr[0];

                            //frm.txtDescriptor.Lines = descr;
                            break;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }

                    if (i <= 9)
                    {
                        if (i < 2)
                            Fields[i] = rtxtInfo_Video.Lines[i];
                        else
                            Fields[i] = ClearField(rtxtInfo_Video.Lines[i]);
                    }
                    else
                    {
                        int cnt = 0;
                        int[] _temp = new int[0];
                        for (int j = 0; j < _cmbFields.Length; j++)
                        {
                            if (Array.IndexOf(iFields, j) < 0)
                            {
                                try
                                {
                                    cnt = _temp.Length;
                                }
                                catch (IndexOutOfRangeException)
                                { }
                                Array.Resize(ref _temp, cnt + 1);
                                _temp[_temp.Length - 1] = j;
                            }
                        }
                        if (_temp.Length == 1)
                        {
                            if (rtxtInfo_Video.Lines[i].Length < 50)
                            {
                                _cmbFields[i].SelectedIndex = _temp[0];
                                if (Array.IndexOf(iFields, _temp[0]) < 0)
                                    iFields[i] = _temp[0];
                                Fields[i] = ClearField(rtxtInfo_Video.Lines[i]);
                            }
                            else
                            {
                                if (Array.IndexOf(iFields, 6) < 0)
                                {
                                    string[] ganre = rtxtInfo_Video.Lines[i].Split(',');
                                    int res = 0;
                                    int max = 0;

                                    for (int k = 0; k < ganre.Length; k++)
                                    {
                                        if (Array.IndexOf(frmGenre.strGenre, ganre[k].Trim()) > -1)
                                            res++;
                                        if (ganre[k].Length > max)
                                            max = ganre[k].Length;
                                    }
                                    if (ganre.Length > 1)
                                    {
                                        if (max < 20)
                                            _cmbFields[i].SelectedIndex = 6;
                                        else
                                            _cmbFields[i].SelectedIndex = 10;
                                    }
                                }
                                else
                                {
                                    Fields[i] = rtxtInfo_Video.Lines[i];

                                }
                            }
                        }
                    }

                    if ((Fields[i] == null) || (Fields[i] == ""))
                    {
                        iFields[i] = -1;
                    }
                    else
                    {
                        if (iFields[i] < 0)
                            _cmbFields[i].SelectedIndex = ChangeField(i);
                    }

                    if (_cmbFields[i].SelectedIndex == 10)
                    {
                        //Array.Clear(descr, 0, descr.Length);
                        for (int j = i; j < rtxtInfo_Video.Lines.Length; j++)
                        {
                            if (rtxtInfo_Video.Lines[j] != "")
                                Description(j);
                        }
                        Fields[i] = descr[0];

                        //frm.txtDescriptor.Lines = descr;
                        break;
                    }

                    if (_cmbFields[i].SelectedIndex < 0)
                    {
                        int ind = -1;
                        int cnt = 0;
                        int[] _temp = new int[0];

                        for (int j = 0; j < _cmbFields.Length; j++)
                        {
                            if (Array.IndexOf(iFields, j) < 0)
                            {
                                try
                                {
                                    cnt = _temp.Length;
                                }
                                catch (IndexOutOfRangeException)
                                { }
                                Array.Resize(ref _temp, cnt + 1);
                                _temp[_temp.Length - 1] = j;
                            }
                        }

                        if ((_cmbFields[i].SelectedIndex < 0) && (Array.IndexOf(descr, rtxtInfo_Video.Lines[i]) < 0))
                        {
                            if (frmImpControl.Input("Внимание!", "Неудалось определить поле строки: " + (i + 1).ToString(), rtxtInfo_Video.Lines[i], _Fields, _temp, out ind))
                            {
                                _cmbFields[i].SelectedIndex = ind;
                                //Fields[i] = rtxtInfo_Video.Lines[i];
                            }
                            else
                            {
                                //Array.Resize(ref descr, descr.Length + 1);
                                //descr[descr.Length - 1] = rtxtInfo_Video.Lines[i];
                                //Fields[i] = "";
                                //Fields = rtxtInfo_Video.Lines;
                                string[] newFields = rtxtInfo_Video.Lines;
                                for (int j = i; j < rtxtInfo_Video.Lines.Length; j++)
                                {
                                    try
                                    {
                                        newFields[j] = rtxtInfo_Video.Lines[j + 1];
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        newFields[j] = rtxtInfo_Video.Lines[i];
                                    }
                                }
                                rtxtInfo_Video.Lines = newFields;
                                //Fields[i] = "";
                                //for (int j = 0; j < rtxtInfo_Video.Lines.Length - 1; j++)
                                //{
                                //    if ((Fields[j] != "")||())
                                //        rtxtInfo_Video.Lines[j] = Fields[j];
                                //}
                                i--;
                                continue;
                            }
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }
            }

            string message = "";

            for (int i = 0; i < _cmbFields.Length; i++)
            {
                try
                {
                    if (iFields[i] != 10)
                        message = message + _Fields[iFields[i]] + ": " + Fields[i] + "\n";
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            int chk = Array.IndexOf(iFields, 10);
            if (chk > -1)
            {
                string des = "";
                for (int i = 0; i < descr.Length; i++)
                {
                    des = des + descr[i] + "\n";
                }
                message = message + _Fields[iFields[chk]] + ":\n" + des;
            }

            message = "Внимательно проверьте правильность распознавания полей:\n\n"
                + message
                + "\nЕсли поля распознаны верно, нажмите Да, иначе - Нет и исправьте ошибки. Для перехода в режим Формы без сохранения данных выберите Отмена.";

            var msg = MessageBox.Show(message,
                "Результат импорта",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Asterisk);

            if (msg == DialogResult.No)
            {
                for (int i = 0; i < _cmbFields.Length; i++)
                {
                    _cmbFields[i].SelectedIndex = -1;
                }

                Array.Clear(descr, 0, descr.Length);
                Array.Resize(ref descr, 0);
                Array.Clear(Fields, 0, _Fields.Length);
                rtxtInfo_Video.Clear();
                rtxtInfo_Video.Lines = oldFields;
                this.Cursor = Cursors.Default;
            }
            else
            {
                frmMain frm = (frmMain)this.Owner;
                if (msg == DialogResult.Yes)
                {
                    for (int i = 0; i < _cmbFields.Length; i++)
                    {
                        switch (iFields[i])
                        {
                            case 0:
                                frm.txtName_R.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 1:
                                frm.txtName_O.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 2:
                                frm.txtYear.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 3:
                                frm.txtGenre.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 4:
                                frm.txtDirector.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 5:
                                frm.txtStudio.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 6:
                                frm.txtActors.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 7:
                                Fields[i] = Fields[i].Replace("+", ";");
                                Fields[i] = Fields[i].Replace(" ", "");
                                Fields[i] = Fields[i].Trim(';', '.', ',', '\r');
                                frm.iTimes = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                frm.txtTimes.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                frm.mtxtTime.Text = (Fields[i] != null) ? AdditionFunc.Calculate(frm.txtTimes.Text) : "00:00:00";
                                break;
                            case 8:
                                frm.txtCountry.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 9:
                                frm.txtLanguage.Text = (Fields[i] != null) ? Fields[i].Trim('\r') : "";
                                break;
                            case 10:
                                frm.txtDescriptor.Lines = descr;
                                break;
                        }
                    }
                    try
                    {
                        txtInfo_Tech.Text = txtInfo_Tech.Text.Trim('\n', '\r');
                        txtInfo_Tech.Text = txtInfo_Tech.Text.Replace("\r", "");
                        if (2 < txtInfo_Tech.Text.Length)
                        {
                            frm.txtVideo.Text = (txtInfo_Tech.Lines[0] != null) ? txtInfo_Tech.Lines[0].Trim('\r') : "";
                            frm.txtAudio.Text = (txtInfo_Tech.Lines[1] != null) ? txtInfo_Tech.Lines[1].Trim('\r') : "";
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }

                    try
                    {
                        //txtInfo_Images.Text = txtInfo_Images.Text.Replace("||", "|");
                        //txtInfo_Images.Text = txtInfo_Images.Text.Replace("\r", "");
                        //txtInfo_Images.Text = txtInfo_Images.Text.Replace("\n\n", "\n");
                        //txtInfo_Images.Text = txtInfo_Images.Text.Trim(separator);
                        //txtInfo_Images.Lines[0] = txtInfo_Images.Lines[0].Trim(separator);
                        //txtInfo_Images.Lines[1] = txtInfo_Images.Lines[1].Trim(separator);

                        //if (2 < txtInfo_Images.Text.Length)
                        //{
                        //    frm.imageFile = (txtInfo_Images.Lines[0] != null) ? txtInfo_Images.Lines[0] : "";

                        //    if (txtInfo_Images.Lines[0] != null)
                        //    {
                        //        frm.PicUploade();
                        //        this.Cursor = Cursors.WaitCursor;
                        //    }


                        //    if (txtInfo_Images.Lines[1] != null)
                        //    {
                        //        frm.imageFiles = txtInfo_Images.Lines[1].Split(separator);
                        //        frm.PicUploade();
                        //        this.Cursor = Cursors.WaitCursor;
                        //    }
                        //}

                        if(txtInfo_Images.Text.Length > 2)
                        {
                            string[] fName = txtInfo_Images.Lines;
                            fName[0] = "";

                            for (int i = 0; i < txtInfo_Images.Lines.Length; i++)
                            {
                                if(txtInfo_Images.Lines[i].Length > 2)
                                {
                                    if(i > 0)
                                    {
                                        frm.imageFiles = fName;
                                        frm.PicUploade();
                                        this.Cursor = Cursors.WaitCursor;
                                    }
                                    else
                                    {
                                        frm.imageFile = ((txtInfo_Images.Lines[0] != null) || (txtInfo_Images.Lines[0] != "")) ? txtInfo_Images.Lines[0] : "";
                                        frm.PicUploade();
                                        this.Cursor = Cursors.WaitCursor;
                                    }
                                }
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { }
                    frmMain.modif = true;
                }
                this.Cursor = Cursors.Default;
                this.Close();
            }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            //btnClear.PerformClick();
            //btnClear.Enabled = true;
            first = true;
            for (int j = 0; j < iFields.Length; j++)
            {
                _cmbFields[j].Items.Clear();
                _cmbFields[j].Items.AddRange(_Fields);
                iFields[j] = _cmbFields[j].SelectedIndex;
            }
        }

        private void frmImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain.Imp = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //int ind = 0;
            //string str = "";
            //string[] link = new string[3];

            //this.Cursor = Cursors.WaitCursor;
            //bool auth = false;
            //Rules_I();
            //Rules_V();
            //string separator = chkScreenshots.Checked ? " " : "\n";

            //txtInfo_Video.Text = rtxtInfo_Video.Text;
            //txtInfo_Video.Text = txtInfo_Video.Text.Trim('\n', '\r');
            //txtInfo_Tech.Text = txtInfo_Tech.Text.Trim('\n', '\r');
            //txtInfo_Images.Text = txtInfo_Images.Text.Trim('\n', '\r');

            //str = txtInfo_Video.Text.Trim('\n','\r') + "\n" + txtInfo_Tech.Text.Trim('\n','\r') + "\n" + txtInfo_Images.Text.Trim('\n','\r');
            //parsText = Parser(str);

            //frmMain frm = (frmMain)this.Owner;
            ////if ((frm._PicPicture[0] != "") || (frm._PicPicture[1] != ""))
            ////{
            ////    auth = Picpicture.AuthPic(frm._PicPicture[0], frm._PicPicture[1]);
            ////}

            //try
            //{
            //    if (12 < txtInfo_Video.Text.Length)
            //    {
            //        frm.txtName_R.Text = parsText[ind+0].Trim('\r');
            //        frm.txtName_O.Text = parsText[ind + 1].Trim('\r');
            //        frm.txtYear.Text = parsText[ind + 2].Trim('\r');
            //        frm.txtGenre.Text = parsText[3].Trim('\r');
            //        frm.iGenre = AdditionFunc.TagBuild(frm.txtGenre.Text);
            //        //str = parsText[3].ToLower();
            //        //frm.iGenre = str.Replace(", ", ",");
            //        frm.txtDirector.Text = parsText[ind + 4].Trim('\r');
            //        frm.txtStudio.Text = parsText[ind + 5].Trim('\r');
            //        frm.txtActors.Text = parsText[ind + 6].Trim('\r');
            //        frm.txtTimes.Text = parsText[ind + 7].Trim('\r');
            //        frm.mtxtTime.Text = AdditionFunc.Calculate(parsText[7]);
            //        frm.txtCountry.Text = parsText[ind + 8].Trim('\r');
            //        frm.txtLanguage.Text = parsText[ind + 9].Trim('\r');
            //        frm.txtDescriptor.Text = parsText[ind + 10].Replace("|", "\n").Trim('\r');
            //        ind = ind + 10;
            //    }
            //    if (2 < txtInfo_Tech.Text.Length)
            //    {
            //        frm.txtVideo.Text = parsText[ind + 1].Trim('\r');
            //        frm.txtAudio.Text = parsText[ind + 2].Trim('\r');
            //        ind = ind + 2;
            //    }
            //    if (2 < txtInfo_Images.Text.Length)
            //    {

            //        string str1 = parsText[ind + 1].Trim('\r');
            //        if(str1.Length < 1)
            //        {
            //            for(int i = ind+1; i < parsText.Length; i++)
            //            {
            //                parsText[i] = parsText[i].Trim('\r');
            //                if (parsText[i] != "-")
            //                {

            //                    if (parsText[i].Length > 5)
            //                    {
            //                        str1 = parsText[i].Trim();
            //                        ind = i;
            //                        break;
            //                    }
            //                }
            //                else
            //                {
            //                    str1 = "";
            //                    ind = i;
            //                    break;
            //                }
            //            }
            //        }
            //        if (str1 != "")
            //        {
            //            str = parsText[ind].Substring("http://", "/");

            //            if (str.Length < 1)
            //                str = parsText[ind].Substring("\\", "\\");

            //            if (str == "picpicture.com")
            //                str = parsText[ind + 1].Trim('\r');
            //            else
            //            {
            //                auth = Picpicture.AuthPic(frmMain._PicPicture[0], frmMain._PicPicture[1]);
            //                str = AdditionFunc.ImageLink(parsText[ind], "p");
            //            }
            //            frm.txtPoster.Text = str;
            //        }
            //        ind = ind + 1;

            //        str1 = parsText[ind + 1].Trim('\r');
            //        if (str1.Length < 5)
            //        {
            //            for (int i = ind + 1; i < parsText.Length; i++)
            //            {
            //                parsText[i] = parsText[i].Trim('\r');
            //                if (parsText[i].Length > 5)
            //                {
            //                    str1 = parsText[i];
            //                    ind = i;
            //                    break;
            //                }
            //            }
            //        }

            //        str = "";
            //        string lnk = "";
            //        for (int i = ind; i < parsText.Length; i++)
            //        {
            //            str1 = parsText[i].Trim('\r', '|');
            //            parsText[i] = parsText[i].Trim('\r', '|');
            //            str = str1.Substring("http://", "/");

            //            if (str1 != "")
            //            {
            //                if ("[url=" != str1)
            //                {
            //                    if ("picpicture.com" != str)
            //                    {
            //                        auth = Picpicture.AuthPic(frmMain._PicPicture[0], frmMain._PicPicture[1]);
            //                        str1 = AdditionFunc.ImageLink(parsText[i], "s");
            //                    }
            //                }
            //                else
            //                {
            //                    str1 = parsText[i].Trim('\r') + parsText[i + 1].Trim('\r') + parsText[i + 2].Trim('\r');
            //                    i = i + 2;
            //                }
            //                lnk = lnk + separator + str1.Replace("[url=][img][/img][/url]", "");
            //            }
            //        }
            //        char ch = Convert.ToChar(separator);
            //        lnk = lnk.TrimStart(ch);
            //        frm.txtScrinshot.Text = "";
            //        frm.txtScrinshot.Text = lnk.Trim('\r');
            //        //frm.txtScrinshot.Text = frm.txtScrinshot.Text.Replace("[url=][img][/img][/url]", "");

            //    }
            //}
            //catch (IndexOutOfRangeException)
            //{
            //    MessageBox.Show("Импорт текста не может быть произведён\nпо причине его отсутствия!", "Ошибка", MessageBoxButtons.OK);
            //}

            this.Cursor = Cursors.WaitCursor;
            //if (chkAuto.Checked)
            //btnClear.PerformClick();
            FormComplecte();
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            frmTimes frmt = new frmTimes();
            frmMain frm = (frmMain)this.Owner;

            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string[] str = new string[rtxtInfo_Video.Lines.Length];
            string[] istr = new string[11];
            string sstr = "";
            string chr;
            int j = 0;
            btnClear.Enabled = false;

            //if (rtxtInfo_Video.Lines[rtxtInfo_Video.Lines.Length - 1].Length == 0)
            //    rtxtInfo_Video.Lines.[rtxtInfo_Video.Lines.Length - 1].;
            for (int i = 0; i < rtxtInfo_Video.Lines.Length; i++)
            {
                if (rtxtInfo_Video.Lines[i] == "")
                    continue;

                int number;
                str[i] = rtxtInfo_Video.Lines[i].Trim();

                string[] line = str[i].Split(':');

                try
                {
                    switch (_cmbFields[i].SelectedIndex)
                    {
                        case 2:
                            if (Int32.TryParse(str[i], out number))
                                istr[i] = str[i].Trim();
                            else
                            {
                                foreach (char ch in str[i].Trim())
                                {
                                    chr = Convert.ToString(ch);
                                    if (Int32.TryParse(chr, out number))
                                        sstr = sstr + chr;
                                }
                                str[i] = sstr;
                            }
                            break;
                        case 7:
                            if (!(Int32.TryParse(line[0], out number)))
                                istr[i] = str[i].Replace(line[0] + ":", "");

                            DateTime dat;
                            if (DateTime.TryParse(str[i], out dat))
                                istr[i] = str[i].Trim();
                            else
                            {
                                sstr = rtxtInfo_Video.Lines[i].Trim();
                                sstr = sstr.Replace("-", " ");
                                sstr = sstr.Replace(".", " ");
                                sstr = sstr.Replace(",", " ");
                                sstr = sstr.Replace(";", " ");
                                sstr = sstr.Replace("/", " ");
                                sstr = sstr.Replace("|", " ");
                                sstr = sstr.Replace("  ", " ");
                                string[] wtime = sstr.Split(' ');

                                j = 0;
                                sstr = "";
                                foreach (string s in wtime)
                                {
                                    wtime[j] = wtime[j].TrimStart(charsToTrim);
                                    wtime[j] = wtime[j].TrimEnd(charsToTrim);
                                    if (DateTime.TryParse(wtime[j], out dat))
                                        sstr = sstr + " " + wtime[j].Trim();

                                    j++;
                                }
                                str[i] = sstr.Trim();
                            }
                            break;
                        case 10:
                            Description(i);
                            sstr = "";
                            for (j = 0; j < descr.Length; j++)
                            {
                                sstr = sstr.Replace("Описание:", "") + "|" + descr[j];
                            }
                            str[i] = sstr.Trim('|');
                            break;
                        default:
                            str[i] = rtxtInfo_Video.Lines[i].Trim();
                            int oldInd = _cmbFields[i].SelectedIndex;
                            if (((i == 0) || (i == 1)) && (_cmbFields[i].SelectedIndex < 0))
                                _cmbFields[i].SelectedIndex = i;

                            if (_cmbFields[i].SelectedIndex < 0)
                            {
                                if (descr.Length > 0)
                                {
                                    Description(i);
                                }
                                else
                                {
                                    string[] newLine = rtxtInfo_Video.Lines;
                                    Array.Resize(ref newLine, newLine.Length + 1);
                                    newLine[newLine.Length - 1] = str[i];
                                    newLine[i] = "";

                                    int ind = -1;

                                    for (int k = 0; k < rtxtInfo_Video.Lines.Length; k++)
                                    {
                                        string s = newLine[k];
                                        ind++;
                                        if (s == "")
                                        {
                                            try
                                            {
                                                first = true;
                                                newLine[k] = rtxtInfo_Video.Lines[ind + 1];

                                            }
                                            catch (IndexOutOfRangeException)
                                            {
                                                newLine[k] = newLine[k + 1];
                                            }

                                            newLine[k + 1] = "";
                                            continue;
                                        }

                                        newLine[k] = rtxtInfo_Video.Lines[ind];
                                    }

                                    Array.Resize(ref newLine, rtxtInfo_Video.Lines.Length);
                                    rtxtInfo_Video.Lines = newLine;
                                    i = -1;
                                    continue;
                                }
                            }

                            if (line.Length > 1)
                            {
                                str[i] = line[1].Trim();
                            }

                            //first = true;
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    str[i] = rtxtInfo_Video.Lines[i].Trim();

                    if (descr.Length > 0)
                    {
                        Description(i);
                    }

                    if (line.Length > 1)
                    {
                        str[i] = line[1].Trim();
                    }
                }
                //rtxtInfo_Video.Lines[i] = str[i];
                //try
                //{
                //    if (_cmbFields[i].SelectedIndex > -1)
                //        Fields[_cmbFields[i].SelectedIndex] = str[i].Replace("Описание:", "");
                //}
                //catch(IndexOutOfRangeException)
                //{
                //    if (descr.Length > 0)
                //    {
                //        Description(i);
                //        Fields[10] = Fields[10] + "\n" + str[i];
                //    }
                //}

                //_cmbFields[i].SelectedIndex = ChangeField(rtxtInfo_Video.Lines[i]);
            }
            rtxtInfo_Video.Lines = str;

            //for(int i = 0; i < str.Length; i++)
            //{

            //}
        }

        private void rtxtInfo_Video_Enter(object sender, EventArgs e)
        {
            mnuContext_Translate.Visible = true;
        }

        private void rtxtInfo_Video_Leave(object sender, EventArgs e)
        {
            mnuContext_Translate.Visible = false;
        }

        private void rtxtInfo_Video_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var rtxt = (RichTextBox)sender;
            int posit = rtxt.SelectionStart;
            int sel = rtxt.SelectionLength;

            //if (sel < 1)
            //    sel = 1;

            if (e.KeyCode == Keys.Delete)
            {
                if (sel > 0)
                    sel--;

                if ((posit > 0) && (posit < rtxt.Text.Length))
                {
                    first = false;
                    rtxt.Text = rtxt.Text.Remove(posit, sel);
                    rtxt.SelectionStart = posit;
                    first = true;

                }
                else if (posit == rtxt.Text.Length)
                {
                    string str = rtxt.Text.Substring(posit - 1, 1);
                    if ((str == " ") || (str == "\n"))
                        rtxt.Text = rtxt.Text.Remove(rtxt.Text.Length - 1, sel);
                    rtxt.SelectionStart = rtxt.Text.Length;
                }
            }
        }

        private void rtxtInfo_Video_TextChanged(object sender, EventArgs e)
        {
            int kk = -1;
            if (chkAuto.Checked)
            {
                bool bLine = false;
                if (rtxtInfo_Video.SelectionStart > 0)
                {
                    int iLine = rtxtInfo_Video.GetLineFromCharIndex(rtxtInfo_Video.SelectionStart);
                    bLine = (iLine == _iLine);
                    _iLine = iLine;
                }
                if (!bLine)//first
                {
                    for (int i = 0; i < rtxtInfo_Video.Lines.Length; i++)
                    {
                        kk = i;
                        if (rtxtInfo_Video.Lines[1] != Fields[i])
                            break;
                    }

                    if (kk > -1)
                    {
                        FieldControl(kk);
                        //int k = 11 - rtxtInfo_Video.Lines.Length;
                        //if (k > 0)
                        //{
                        //    for (int i = k - 1; i < 11; i++)
                        //    {
                        //        _cmbFields[i].SelectedIndex = -1;
                        //    }
                        //}
                    }
                }
            }
            //else
            //{
            //    try
            //    {
            //        Fields[rtxtInfo_Video.GetLineFromCharIndex(rtxtInfo_Video.SelectionStart)] = rtxtInfo_Video.Lines[1];
            //    }
            //    catch(IndexOutOfRangeException)
            //    { }
            //}
        }

        private void txtInfo_Images_Enter(object sender, EventArgs e)
        {
            mnuContext_View.Visible = true;
        }

        private void txtInfo_Images_Leave(object sender, EventArgs e)
        {
            mnuContext_View.Visible = false;
            first = false;
            //Rules_I();
        }

        private void txtInfo_Tech_Enter(object sender, EventArgs e)
        {
            mnuContext_Sep2.Visible = false;
        }

        private void txtInfo_Tech_Leave(object sender, EventArgs e)
        {
            mnuContext_Sep2.Visible = true;
        }

        private void chkScreenshots_CheckedChanged(object sender, EventArgs e)
        {
            //Rules_I();
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuto.Checked)
            {
                for (int i = 0; i < _cmbFields.Length; i++)
                {
                    FieldControl(i);
                }
            }
        }

        private void cmbField_TextChanged(object sender, EventArgs e)
        {
            ComboBox cmb = ((ComboBox)sender);
            int ind = Convert.ToInt32(cmb.Tag);
            if (cmb.SelectedIndex == -1)
            {
                iFields[ind] = -1;
            }
            else
            {
                iFields[ind] = cmb.SelectedIndex;
            }
            //    try
            //    {

            //        int[] _temp = new int[0];

            //        for (int j = 0; j < _cmbFields.Length; j++)
            //        {
            //            if (iFields[j] < 0)
            //            {
            //                Array.Resize(ref _temp, _temp.Length + 1);
            //                _temp[_temp.Length - 1] = j;
            //            }

            //        }

            //        if (cmb.SelectedIndex > -1)
            //        {
            //            if (rtxtInfo_Video.Lines[ind].Length < 1)
            //                cmb.SelectedIndex = -1;

            //            if (Array.IndexOf(_temp, cmb.SelectedIndex) > -1)
            //                cmb.SelectedIndex = -1;
            //            else
            //                iFields[ind] = cmb.SelectedIndex;
            //        }
            //        //Fields[cmb.SelectedIndex] = rtxtInfo_Video.Lines[ind];
            //        //Fields[cmb.SelectedIndex] = rtxtInfo_Video.Lines[ind];
            //    }
            //    catch (IndexOutOfRangeException)
            //    {
            //        cmb.SelectedIndex = -1;
            //    }
            //}
            //else
            //    iFields[ind] = -1;
        }

        private void mnuContext_Copy_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtxtInfo_Video.Focused)
                {
                    RichTextBox txt = new RichTextBox();
                    txt = rtxtInfo_Video;
                    if (txt.SelectedText.Length > 0)
                        Clipboard.SetText(txt.SelectedText);
                }
                else
                {
                    TextBox txt = new TextBox();
                    if (txtInfo_Tech.Focused)
                        txt = txtInfo_Tech;
                    if (txtInfo_Images.Focused)
                        txt = txtInfo_Images;

                    if (txt.SelectedText.Length > 0)
                        Clipboard.SetText(txt.SelectedText);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuContext_Past_Click(object sender, EventArgs e)
        {
            if (rtxtInfo_Video.Focused)
            {
                RichTextBox txt = new RichTextBox();
                txt = rtxtInfo_Video;

                txt.SelectedText = Clipboard.GetText();
            }
            else
            {
                TextBox txt = new TextBox();
                if (txtInfo_Tech.Focused)
                    txt = txtInfo_Tech;
                if (txtInfo_Images.Focused)
                    txt = txtInfo_Images;

                txt.SelectedText = Clipboard.GetText();
            }
        }

        private void mnuContext_Select_Click(object sender, EventArgs e)
        {
            if (rtxtInfo_Video.Focused)
            {
                RichTextBox txt = new RichTextBox();
                txt = rtxtInfo_Video;

                txt.SelectAll();
            }
            else
            {
                TextBox txt = new TextBox();
                if (txtInfo_Tech.Focused)
                    txt = txtInfo_Tech;
                if (txtInfo_Images.Focused)
                    txt = txtInfo_Images;

                txt.SelectAll();
            }

        }

        private void mnuContext_Cut_Click(object sender, EventArgs e)
        {
            if (rtxtInfo_Video.Focused)
            {
                RichTextBox txt = new RichTextBox();
                txt = rtxtInfo_Video;

                if (txt.SelectedText.Length > 0)
                {
                    Clipboard.SetText(txt.SelectedText);
                    txt.SelectedText = "";
                }
            }
            else
            {
                TextBox txt = new TextBox();
                if (txtInfo_Tech.Focused)
                    txt = txtInfo_Tech;
                if (txtInfo_Images.Focused)
                    txt = txtInfo_Images;

                if (txt.SelectedText.Length > 0)
                {
                    Clipboard.SetText(txt.SelectedText);
                    txt.SelectedText = "";
                }
            }
        }

        private void mnuContext_Del_Click(object sender, EventArgs e)
        {
            if (rtxtInfo_Video.Focused)
            {
                RichTextBox txt = new RichTextBox();
                txt = rtxtInfo_Video;

                if (txt.SelectedText.Length > 0)
                    txt.SelectedText = "";
                else
                    txt.Text = "";
            }
            else
            {
                TextBox txt = new TextBox();
                if (txtInfo_Tech.Focused)
                    txt = txtInfo_Tech;
                if (txtInfo_Images.Focused)
                    txt = txtInfo_Images;

                if (txt.SelectedText.Length > 0)
                    txt.SelectedText = "";
                else
                    txt.Text = "";
            }
        }

        private void mnuContext_Translate_Click(object sender, EventArgs e)
        {
            int pos = rtxtInfo_Video.GetLineFromCharIndex(rtxtInfo_Video.SelectionStart);
            string str = "https://translate.google.ru/#auto/ru/" + HttpUtility.HtmlDecode(rtxtInfo_Video.Lines[pos]);

            if (rtxtInfo_Video.SelectedText != "")
                str = "https://translate.google.ru/#auto/ru/" + HttpUtility.HtmlDecode(rtxtInfo_Video.SelectedText);

            System.Diagnostics.Process.Start(str);
        }

        private void mnuContext_View_Click(object sender, EventArgs e)
        {
            dlgOpen.Filter = "Фотографии и изображения|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|Все файлы (*.*)|*.*";
            dlgOpen.FileName = "";
            dlgOpen.Multiselect = true;
            string[] strName = txtInfo_Images.Lines;

            //Array.Resize(ref fName, 0);

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                //if(strName.Length > 0)
                //{
                //    for(int i = 0; i < strName.Length; i++)
                //    {
                //        if(strName[i].Length > 2)
                //        {
                //            Array.Resize(ref fName, fName.Length + 1);
                //            fName[fName.Length - 1] = strName[i];
                //        }
                //    }
                //}

                for (int i = 0; i < dlgOpen.FileNames.Length; i++)
                {
                    if (dlgOpen.FileNames[i].Length > 2)
                    {
                        try
                        {
                            Array.Resize(ref strName, strName.Length + 1);
                        }
                        catch(NullReferenceException)
                        {
                            Array.Resize(ref strName, 1);
                        }
                        strName[strName.Length - 1] = dlgOpen.FileNames[i];
                    }
                }

                txtInfo_Images.Lines = strName;
            }
        }

        private void txtInfo_Images_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
