using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using xNet;
using System.Diagnostics;
using TorrentFile;

namespace KinoRun
{
    public partial class frmMain : Form
    {
        public static string SetingPath = Environment.GetEnvironmentVariable("AppData") + @"\KinoRun\";
        public static string Html = "";
        public static string ExePath = Application.StartupPath;
        public static DateTime TimeAutoriz;
        public static int Theme = 0;
        public static bool Imp = false;
        public static bool modif = false;
        public static int locX = 0;
        public static int locY = 0;
        public static bool First = false;
        public static string access = "";
        public static CookieDictionary cookie = new CookieDictionary(false);

        string code = "";
        int pos = 0;
        public string Genre;
        public string iGenre;
        public string Tags;
        public bool ClearDir;
        public int Userstat = 0;

        TextBox txtBox;
        ComboBox cmb;
        Color theme = Color.WhiteSmoke;
        //public bool UpdStart = false;

        public bool bGenre = false;
        int Times;
        public string iTimes = "";
        string contr = "";
        //bool precompl = false;
        string duration = "00:00:00";

        public string[] _Fields = new string[4];
        public string[] _Text = new string[4];
        public string[] _Headlines = new string[4];
        public string[] _Description = new string[4];
        public string fontName = "";
        public static string[] _PicPicture = new string[2];
        public static string[] _KinoRun = new string[2];
        public bool authPic = false;
        public static bool authKin = false;
        string oper = "";
        string[] link = new string[3];
        string[] LastName = new string[10];
        string[] LastPath = new string[10];
        string PathSave = "";
        bool SaveDefault = false;
        bool FieldLine = false;
        bool Beta = false;
        bool sett = false;

        //Блок автообновления
        string ExeName = "";
        string ExeExt = "";
        string UpdateServer = "http://altrec.h1n.ru/";
        string Version = "";
        //Блок автообновления

        string[] adrurl;
        string[] field = {"жанр",
                        "продолжительность",
                        "режис",
                        "язык",
                        "страна",
                        "год",
                        "название",
                        "студия",
                        "сайт",
                        "ролях",
                        "актрисы",
                        "описание" };

        string s_Rep = "";
        string s_Fields = "";
        string s_Text = "";
        string s_Headlines = "";
        string s_Description = "";
        string s_fontName = "";

        string rtf = "";
        bool rtxtModif = false;

        string f_Rep = "";
        string f_Fields = "";
        string f_Text = "";
        string f_Headlines = "";
        string f_Description = "";
        string f_fontName = "";

        string[] bbcode = new string[10];
        string[] color = new string[14];
        string[] size = new string[4];
        string[] fontfam = new string[4];
        string selText;

        string[] CatInd;
        string link1 = "";

        ToolStripButton[] _tbtn = new ToolStripButton[12];
        ToolStripMenuItem[] _mnuMainFileLast = new ToolStripMenuItem[10];
        TextBox[] _txt = new TextBox[15];
        frmView fView;

        int ind_bbc = 0;
        string VQuality = "(выбрать)";
        string VFormat = "(выбрать)";
        string VCodac = "(выбрать)";
        string ACodac = "(выбрать)";
        string distribFile;
        string distribFile1;

        public string imageFile;
        public string[] imageFiles;
        string[] imageFiles_text;
        //string imageLink;

        //frmPreviev frm = new frmPreviev();
        AutoUpdater update = new AutoUpdater(); //экземпляр класса с настройками автообновления
        Props props = new Props(); //экземпляр класса с настройками
        Design design = new Design(); //экземпляр класса с настройками
        Distrib distrib; //экземпляр класса с настройками

        //private const int OLECMDID_ZOOM = 63;
        //private const int OLECMDEXECOPT_DONTPROMPTUSER = 2;

        public frmMain(string file)
        {
            InitializeComponent();
            fView = new frmView();

            Genre = "";
            iGenre = "";

            _mnuMainFileLast = new ToolStripMenuItem[] { mnuMainFileLast_1, mnuMainFileLast_2, mnuMainFileLast_3, mnuMainFileLast_4, mnuMainFileLast_5, mnuMainFileLast_6, mnuMainFileLast_7, mnuMainFileLast_8, mnuMainFileLast_9, mnuMainFileLast_10 };
            _txt = new TextBox[] { txtName_R, txtName_O, txtYear, txtGenre, txtDirector, txtStudio, txtActors, txtCountry, txtLanguage, txtDescriptor, txtVideo, txtAudio, txtPoster, txtScrinshot };
            _tbtn = new ToolStripButton[] { tbtnBold, tbtnCenter, tbtnCode, tbtnImage, tbtnItalic, tbtnLink, tbtnLink_query, tbtnQuote, tbtnUnderline, tbtnColor, tbtnSize, tbtnFont };
            color = new String[] { "black", "darkred", "red", "orange", "brown", "yellow", "green", "olive", "cyan", "blue", "darkblue", "indigo", "violet", "white" };
            size = new String[] { "8", "14", "20", "26" };
            //color = new string[tcmbColor.Items.Count];
            //size = new string[tcmbSize.Items.Count];

            distribFile1 = file;
        }

        private void writeSave() //Сохранение раздачи
        {
            distrib.Fields.Name = txtName.Text;
            distrib.Fields.Name_R = txtName_R.Text;
            distrib.Fields.Name_O = txtName_O.Text;
            distrib.Fields.Year = txtYear.Text;
            distrib.Fields.Genre = txtGenre.Text;
            distrib.Fields.Genre_tag = iGenre;
            distrib.Fields.Director = txtDirector.Text;
            distrib.Fields.Studio = txtStudio.Text;
            distrib.Fields.Actors = txtActors.Text;
            distrib.Fields.Country = txtCountry.Text;
            distrib.Fields.Language = txtLanguage.Text;
            distrib.Fields.Video = txtVideo.Text;
            distrib.Fields.Audio = txtAudio.Text;

            distrib.Fields.Poster = txtPoster.Text;

            distrib.Fields.Description = txtDescriptor.Text;

            string str = "";
            for (int i = 0; i < 8; i++)
            {
                try
                {
                    if (mtxtTime.Text[i] != ' ')
                        str = str + mtxtTime.Text[i];
                    else
                        str = str + "0";
                }
                catch (IndexOutOfRangeException)
                {
                    str = str + "0";
                }

            }
            distrib.Fields.Duration = str;
            distrib.Fields.Times = txtTimes.Text;
            distrib.Fields.Translate = cmbTranslation.Text;
            distrib.Fields.Qualiti = cmbQuality.Text;
            distrib.Fields.Format_V = cmbFormat.Text;
            distrib.Fields.Codec_V = cmbVCodac.Text;
            distrib.Fields.Codec_A = cmbACodac.Text;
            distrib.Fields.Annotation = txtAddition.Text;
            distrib.Fields.Vote = cmbVoting.Text;
            distrib.Fields.Category = cmbCategory.Text;
            distrib.Fields.Screanshots = txtScrinshot.Text;
            distrib.Fields.BBCode = rtxtCode.Text;

            if (Userstat > 7)
            {
                distrib.Fields.Gold = chkGold.Checked;
            }
            else
            {
                distrib.Fields.Gold = false;
            }

            distrib.WriteSave();
        }

        public void readSave() //Открытие раздачи
        {
            distrib.ReadSave();

            txtName.Text = distrib.Fields.Name;
            txtPoster.Text = distrib.Fields.Poster;
            txtName_R.Text = distrib.Fields.Name_R;
            txtName_O.Text = distrib.Fields.Name_O;
            txtYear.Text = distrib.Fields.Year;
            txtGenre.Text = distrib.Fields.Genre;
            Genre = distrib.Fields.Genre;
            txtGenre.Tag = distrib.Fields.Genre_tag;
            iGenre = distrib.Fields.Genre_tag;

            if (iGenre.Length > 0)
                bGenre = true;
            else
                bGenre = false;

            txtDirector.Text = distrib.Fields.Director;
            txtStudio.Text = distrib.Fields.Studio;
            txtActors.Text = distrib.Fields.Actors;
            txtCountry.Text = distrib.Fields.Country;
            txtLanguage.Text = distrib.Fields.Language;
            txtVideo.Text = distrib.Fields.Video;
            txtAudio.Text = distrib.Fields.Audio;
            txtDescriptor.Text = distrib.Fields.Description;
            mtxtTime.Text = distrib.Fields.Duration;
            txtTimes.Text = distrib.Fields.Times;
            cmbTranslation.Text = distrib.Fields.Translate;
            cmbQuality.Text = distrib.Fields.Qualiti;
            cmbFormat.Text = distrib.Fields.Format_V;
            cmbVCodac.Text = distrib.Fields.Codec_V;
            cmbACodac.Text = distrib.Fields.Codec_A;
            txtAddition.Text = distrib.Fields.Annotation;
            cmbVoting.Text = distrib.Fields.Vote;
            cmbCategory.Text = distrib.Fields.Category;
            txtScrinshot.Text = distrib.Fields.Screanshots;

            if (Userstat > 7)
            {
                chkGold.Checked = distrib.Fields.Gold;
            }
            else
            {
                chkGold.Checked = false;
            }

                Preview();
            //if (!mnuMain_View__Preview.Checked)
            //    mnuMain_View__Preview.PerformClick();
            //mnuMain_View_Editor.PerformClick();

            tbtnEdit.PerformClick();
            rtxtCode.Clear();
            rtxtCode.Text = distrib.Fields.BBCode;
            //iTimes = txtTimes.Text;
            //frmTimes frm = new frmTimes();
            //Times = AdditionFunc.Calculate(iTimes);
            HTMLCompleat();
        }

        public void readUpdate() //Чтение настроек AutoUpdate
        {
            try
            {
                update.ReadXml();
            }
            catch (FileNotFoundException)
            {
                modif = false;
            }
            //Блок автообновления
            ExeName = update.Fields.ExeName;
            ExeExt = update.Fields.ExeExt;

            UpdateServer = update.Fields.UpdateServer;

            Version = update.Fields.Version;
            //Блок автообновления
        }

        private void writeUpdate() //Запись настроек AutoUpdate
        {
            try
            {
                //Блок автообновления
                update.Fields.ExeName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
                update.Fields.ExeExt = ExeExt;

                if (Beta)
                {
                    if (UpdateServer.IndexOf("test/") < 0)
                        UpdateServer = (UpdateServer[UpdateServer.Length - 1] == '/') ? UpdateServer + "test/" : UpdateServer + "/test/";
                }
                else
                {
                    if (UpdateServer.IndexOf("test/") > -1)
                        UpdateServer = UpdateServer.Replace("test/", "");
                }

                update.Fields.UpdateServer = UpdateServer;
                update.Fields.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //Блок автообновления
                update.WriteXml(); //Автообновление
            }
            catch (IndexOutOfRangeException)
            { }
        }

        public void readSetting() //Чтение настроек
        {
            try
            {
                design.ReadXml();
            }
            catch (FileNotFoundException)
            {
                modif = false;
            }

            Tags = design.Fields.Tags;
            ClearDir = design.Fields.ClearDir;
            fontName = design.Fields.fontName;
            for (int i = 0; i < 4; i++)
            {
                _Fields[i] = design.Fields.Fields[i];
                _Text[i] = design.Fields.Text[i];
                _Headlines[i] = design.Fields.Headlines[i];
                _Description[i] = design.Fields.Description[i];
            }

            Beta = design.Fields.Beta;
            FieldLine = design.Fields.FieldLine;
            if (design.Fields.PicPicture[0] != null)
                _PicPicture = AdditionFunc.AccCoding(design.Fields.PicPicture, 1);
            if (design.Fields.KinoRun[0] != null)
                _KinoRun = AdditionFunc.AccCoding(design.Fields.KinoRun, 1);


            Theme = design.Fields.Theme;
            SaveDefault = design.Fields.SaveDefault;
            PathSave = design.Fields.PathSave;
            switch (Theme)
            {
                case 0:
                    theme = Color.GhostWhite;
                    mnuMain_View_Themes_Light.Checked = true;
                    mnuMain_View_Themes_Neutral.Checked = false;
                    mnuMain_View_Themes_Dark.Checked = false;
                    break;
                case 1:
                    theme = Color.Gainsboro;
                    mnuMain_View_Themes_Light.Checked = false;
                    mnuMain_View_Themes_Neutral.Checked = true;
                    mnuMain_View_Themes_Dark.Checked = false;
                    break;
                case 2:
                    theme = Color.DarkGray;
                    mnuMain_View_Themes_Light.Checked = false;
                    mnuMain_View_Themes_Neutral.Checked = false;
                    mnuMain_View_Themes_Dark.Checked = true;
                    break;
            }
        }

        private void writeSetting() //Запись настроек
        {
            try
            {
                //for (int i = 0; i < 2; i++)
                //{
                //    design.Fields.PicPicture[i] = _PicPicture[i];
                //    design.Fields.KinoRun[i] = _KinoRun[i];
                //}

                design.Fields.PicPicture = AdditionFunc.AccCoding(_PicPicture, 0);
                design.Fields.KinoRun = AdditionFunc.AccCoding(_KinoRun, 0);
                design.Fields.Theme = Theme;
                design.Fields.SaveDefault = SaveDefault;
                design.Fields.PathSave = PathSave;

                design.Fields.fontName = fontName;
                for (int i = 0; i < 4; i++)
                {
                    design.Fields.Fields[i] = _Fields[i];
                    design.Fields.Text[i] = _Text[i];
                    design.Fields.Headlines[i] = _Headlines[i];
                    design.Fields.Description[i] = _Description[i];
                }

                design.Fields.Beta = Beta;
                design.Fields.FieldLine = FieldLine;
                design.Fields.Tags = Tags;
                design.Fields.ClearDir = ClearDir;

                //props.Fields.Name = txtName.Text;
                props.Fields.Addition = txtAddition.Text;

                design.WriteXml();
            }
            catch (IndexOutOfRangeException)
            { }
        }

        public void readField() //Чтение настроек
        {
            try
            {
                props.ReadXml();
            }
            catch (FileNotFoundException)
            {
                modif = false;
            }

            cookie = AdditionFunc.CookiesExport(props.Fields.Cookies);
            //txtName.Text = props.Fields.Name;
            txtAddition.Text = props.Fields.Addition;
            //txtName_R.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Name_R));
            //txtName_O.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Name_O));
            //txtYear.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Year));
            //txtGenre.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Genre));
            //txtDirector.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Director));
            //txtStudio.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Studio));
            //txtActors.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Actors));
            //txtCountry.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Country));
            //txtLanguage.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Language));
            //txtVideo.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Video));
            //txtAudio.AutoCompleteCustomSource.AddRange(RoundProps(props.Fields.Audio));

            txtName_R.AutoCompleteCustomSource.AddRange(props.Fields.Name_R);
            txtName_O.AutoCompleteCustomSource.AddRange(props.Fields.Name_O);
            txtYear.AutoCompleteCustomSource.AddRange(props.Fields.Year);
            txtGenre.AutoCompleteCustomSource.AddRange(props.Fields.Genre);
            txtDirector.AutoCompleteCustomSource.AddRange(props.Fields.Director);
            txtStudio.AutoCompleteCustomSource.AddRange(props.Fields.Studio);
            txtActors.AutoCompleteCustomSource.AddRange(props.Fields.Actors);
            txtCountry.AutoCompleteCustomSource.AddRange(props.Fields.Country);
            txtLanguage.AutoCompleteCustomSource.AddRange(props.Fields.Language);
            txtVideo.AutoCompleteCustomSource.AddRange(props.Fields.Video);
            txtAudio.AutoCompleteCustomSource.AddRange(props.Fields.Audio);

            if (props.Fields.LastName.Length > 0)
            {
                for (int i = 0; i < LastName.Length; i++)
                {
                    try
                    {
                        if (props.Fields.LastPath[i].Length > 2)
                        {
                            LastName[i] = props.Fields.LastName[i];
                            LastPath[i] = props.Fields.LastPath[i];
                        }
                    }
                    catch (NullReferenceException)
                    {
                        LastName[i] = "";
                        LastPath[i] = "";
                    }
                }
            }
        }

        private void writeField() //Запись настроек
        {
            try
            {
                Array.Clear(props.Fields.Name_R, 0, props.Fields.Name_R.Length);
                Array.Clear(props.Fields.Name_O, 0, props.Fields.Name_O.Length);
                Array.Clear(props.Fields.Year, 0, props.Fields.Year.Length);
                Array.Clear(props.Fields.Genre, 0, props.Fields.Genre.Length);
                Array.Clear(props.Fields.Director, 0, props.Fields.Director.Length);
                Array.Clear(props.Fields.Studio, 0, props.Fields.Studio.Length);
                Array.Clear(props.Fields.Actors, 0, props.Fields.Actors.Length);
                Array.Clear(props.Fields.Country, 0, props.Fields.Country.Length);
                Array.Clear(props.Fields.Language, 0, props.Fields.Language.Length);
                Array.Clear(props.Fields.Video, 0, props.Fields.Video.Length);

                Array.Resize(ref props.Fields.Name_R, 0);
                Array.Resize(ref props.Fields.Name_O, 0);
                Array.Resize(ref props.Fields.Year, 0);
                Array.Resize(ref props.Fields.Genre, 0);
                Array.Resize(ref props.Fields.Director, 0);
                Array.Resize(ref props.Fields.Studio, 0);
                Array.Resize(ref props.Fields.Actors, 0);
                Array.Resize(ref props.Fields.Country, 0);
                Array.Resize(ref props.Fields.Language, 0);
                Array.Resize(ref props.Fields.Video, 0);
                Array.Resize(ref props.Fields.Audio, 0);

                //Array.Resize(ref props.Fields.Name_R, txtName_R.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Name_O, txtName_O.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Year, txtYear.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Genre, txtGenre.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Director, txtDirector.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Studio, txtStudio.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Actors, txtActors.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Country, txtCountry.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Language, txtLanguage.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Video, txtVideo.AutoCompleteCustomSource.Count);
                //Array.Resize(ref props.Fields.Audio, txtAudio.AutoCompleteCustomSource.Count);

                Array.Resize(ref props.Fields.LastName, LastName.Length);
                Array.Resize(ref props.Fields.LastPath, LastPath.Length);

                Array.Resize(ref design.Fields.Fields, _Fields.Length);
                Array.Resize(ref design.Fields.Text, _Text.Length);
                Array.Resize(ref design.Fields.Headlines, _Headlines.Length);
                Array.Resize(ref design.Fields.Description, _Description.Length);

                props.Fields.Cookies = AdditionFunc.CookiesImport(cookie);

                int k = -1;
                for (int i = 0; i < txtName_R.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Name_R, txtName_R.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtName_R.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Name_R, props.Fields.Name_R.Length + 1);
                        props.Fields.Name_R[props.Fields.Name_R.Length - 1] = txtName_R.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtName_O.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Name_O, txtName_O.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtName_O.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Name_O, props.Fields.Name_O.Length + 1);
                        props.Fields.Name_O[props.Fields.Name_O.Length - 1] = txtName_O.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtYear.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Year, txtYear.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtYear.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Year, props.Fields.Year.Length + 1);
                        props.Fields.Year[props.Fields.Year.Length - 1] = txtYear.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtGenre.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Genre, txtGenre.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtGenre.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Genre, props.Fields.Genre.Length + 1);
                        props.Fields.Genre[props.Fields.Genre.Length - 1] = txtGenre.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtDirector.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Director, txtDirector.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtDirector.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Director, props.Fields.Director.Length + 1);
                        props.Fields.Director[props.Fields.Director.Length - 1] = txtDirector.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtStudio.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Studio, txtStudio.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtStudio.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Studio, props.Fields.Studio.Length + 1);
                        props.Fields.Studio[props.Fields.Studio.Length - 1] = txtStudio.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtActors.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Actors, txtActors.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtActors.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Actors, props.Fields.Actors.Length + 1);
                        props.Fields.Actors[props.Fields.Actors.Length - 1] = txtActors.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtCountry.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Country, txtCountry.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtCountry.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Country, props.Fields.Country.Length + 1);
                        props.Fields.Country[props.Fields.Country.Length - 1] = txtCountry.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtLanguage.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Language, txtLanguage.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtLanguage.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Language, props.Fields.Language.Length + 1);
                        props.Fields.Language[props.Fields.Language.Length - 1] = txtLanguage.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtVideo.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Video, txtVideo.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtVideo.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Video, props.Fields.Video.Length + 1);
                        props.Fields.Video[props.Fields.Video.Length - 1] = txtVideo.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < txtAudio.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(props.Fields.Audio, txtAudio.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtAudio.AutoCompleteCustomSource[i] != null))
                    {
                        Array.Resize(ref props.Fields.Audio, props.Fields.Audio.Length + 1);
                        props.Fields.Audio[props.Fields.Audio.Length - 1] = txtAudio.AutoCompleteCustomSource[i];
                    }
                }

                for (int i = 0; i < LastName.Length; i++)
                {
                    try
                    {
                        int ia = Array.IndexOf(props.Fields.LastName, LastName[i]);
                        if (LastName[i].Length > 2)
                        {
                            if ((ia < 0) || (i == ia))
                            {
                                props.Fields.LastName[i] = LastName[i];
                                props.Fields.LastPath[i] = LastPath[i];
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                        props.Fields.LastName[i] = "";
                        props.Fields.LastPath[i] = "";
                    }
                }

                //props.Fields.Name = txtName.Text;
                props.Fields.Addition = txtAddition.Text;

                props.WriteXml();
            }
            catch (IndexOutOfRangeException)
            { }
        }

        private string[] RoundProps(string[] PropRound) //Чтение настроек
        {
            string[] coll = new string[0];
            int j = 0;

            for (int i = 0; i < PropRound.Length; i++)
            {
                if (PropRound[i] != "")
                {
                    int index = Array.IndexOf(coll, PropRound[i]);
                    if (index < 0)
                    {
                        Array.Resize(ref coll, coll.Length + 1);
                        coll[j] = PropRound[i];
                        j++;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                    continue;
            }
            return coll;
        }

        //private void SetZoom(int zoom)
        //{
        //    dynamic obj = webBrowser1.ActiveXInstance;

        //    obj.ExecWB(OLECMDID_ZOOM, OLECMDEXECOPT_DONTPROMPTUSER, zoom, IntPtr.Zero);
        //}

        private void SetAutoScrollMargins()
        {
            /* If the text box is outside the panel's bounds, 
               turn on auto-scrolling and set the margin. */
            if (webBrowser1.Location.X > tabPreview.Location.X ||
               webBrowser1.Location.Y > tabPreview.Location.Y)
            {
                tabPreview.AutoScroll = true;
                /* If the AutoScrollMargin is set to less 
                   than (5,5), set it to 5,5. */
                if (tabPreview.AutoScrollMargin.Width < 5 ||
                   tabPreview.AutoScrollMargin.Height < 5)
                {
                    tabPreview.SetAutoScrollMargin(5, 5);
                }
            }
        }

        private void menuLast() //Формирование меню последних раздач
        {
            int ind = 0;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if ((LastName[i] != null) && (LastName[i].Length > 2))
                    {
                        if (ind < LastName.Length)
                        {
                            _mnuMainFileLast[ind].Text = LastName[i];
                            _mnuMainFileLast[ind].Tag = LastPath[i];
                            _mnuMainFileLast[ind].Visible = true;
                            ind++;
                        }
                    }
                    else
                    {
                        _mnuMainFileLast[ind].Visible = false;
                    }

                    if(_mnuMainFileLast[i].Text.Length < 2 )
                    {
                        _mnuMainFileLast[i].Visible = false;
                    }
                }
                catch (NullReferenceException)
                { continue; }
                catch (IndexOutOfRangeException)
                { break; }
            }
            if (ind > 0)
                mnuMain_File_Last.Enabled = true;
            else
                mnuMain_File_Last.Enabled = false;
        }

        private void LastFiles(string FilePath) //Формирование списка последних раздач
        {
            int iLength = 10;
            string[] FNames = FilePath.Split('\\');
            string FName = FNames[FNames.Length - 1].Replace(".knr", "");

            if (FilePath != "")
            {
                string[] lname = new string[10];
                string[] lpath = new string[10];
                int ind = 1;

                for (int i = 0; i < iLength; i++)
                {
                    try
                    {
                        lname[i] = LastName[i];
                        lpath[i] = LastPath[i];
                    }
                    catch
                    { break; }
                }

                LastName[0] = FName;
                LastPath[0] = FilePath;
                for (int i = 0; i < iLength; i++)
                {
                    try
                    {
                        //if (i > LastName.Length)
                        //{
                        //    Array.Resize(ref LastName, LastName.Length + 1);
                        //    Array.Resize(ref LastPath, LastPath.Length + 1);
                        //}

                        //if (2 < LastName[ind].Length)
                        //{
                            int ia = Array.IndexOf(LastName, lname[i]);

                            if (((ia < 0) || (ind == ia)))
                            {
                                LastName[ind] = lname[i];
                                LastPath[ind] = lpath[i];
                                ind++;
                            //}
                        }
                    }
                    catch (NullReferenceException)
                    {
                        LastName[ind] = "";
                        LastPath[ind] = "";
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Array.Resize(ref LastName, LastName.Length + 1);
                        Array.Resize(ref LastPath, LastPath.Length + 1);
                        continue;
                    }
                }
                menuLast();
                writeField();
                distribFile = FilePath;
                modif = false;
            }
        }

        public void PicUploade() //Загрузка изображений
        {
            string lnk = "";
            string lnks = "";

            try
            {
                if ((imageFiles[0].Length > 10) || (imageFiles.Length > 0))
                {
                    authPic = Picpicture.AuthPic(_PicPicture[0], _PicPicture[1]); //проверка авторизации
                    if (authPic)   //если авторизация успешна
                    {
                        this.Cursor = Cursors.WaitCursor;
                        for (int i = 0; i < imageFiles.Length; i++)
                        {

                            if (imageFiles[i].Length > 5)
                            {
                                string str = imageFiles[i].Substring("http://", "/");

                                if (str == "")
                                    str = imageFiles[i].Substring("https://", "/");

                                if (str == "picpicture.com")
                                    lnk = imageFiles[i];
                                else
                                {

                                    lnk = AdditionFunc.ImageLink(imageFiles[i], "");
                                }
                            }

                            lnks = lnks + " " + lnk;
                        }

                        Array.Clear(imageFiles, 0, imageFiles.Length);
                        txtScrinshot.Text = lnks.Trim();
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (NullReferenceException)
            {

            }

            try
            {
                if (imageFile.Length > 10)
                {
                    string str = imageFile.Substring("http://", "/");

                    if (str.Length > 3)
                    {
                        if (str == "picpicture.com")
                            txtPoster.Text = imageFile;
                        else
                        {
                            authPic = Picpicture.AuthPic(_PicPicture[0], _PicPicture[1]); //проверка авторизации
                            if (authPic)   //если авторизация успешна
                            {
                                this.Cursor = Cursors.WaitCursor;
                                txtPoster.Text = AdditionFunc.ImageLink(imageFile, "p");

                                imageFile = "";
                                this.Cursor = Cursors.Default;
                            }
                        }
                    }
                    else
                    {
                        txtPoster.Text = imageFile;
                    }
                }
            }
            catch (NullReferenceException)
            {

            }
        }

        public string PicUploade(string PathFile) //Загрузка изображений с параметрами
        {
            string lnk = "";
            try
            {
                if (PathFile.Length > 10)
                {
                    string str = imageFile.Substring("http://", "/");

                    if ((str.Length > 3) || (tbcBuilder.SelectedTab == tbcBuilder.TabPages["tabCode"]))
                    {
                        authPic = Picpicture.AuthPic(_PicPicture[0], _PicPicture[1]); //проверка авторизации
                        if (authPic)   //если авторизация успешна
                        {
                            this.Cursor = Cursors.WaitCursor;
                            lnk = AdditionFunc.ImageLink(PathFile, "p");
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            lnk = "";
                        }
                    }
                    else
                        lnk = PathFile;
                }
            }
            catch (NullReferenceException)
            {

            }
            return lnk;
        }

        private void ShowBrowser()
        {
            //int with = SystemInformation.PrimaryMonitorSize.Width;
            //if (with > 1600)
            //    with = 1600;
            //this.MaximumSize = new Size(with, 687);
            //this.MinimumSize = new Size(with, 687);
            //this.Width = with;
            //pnlBrouser.Width = with - pnlPage1.Width;
            //pnlBrouser.Location = new Point(740, 0);
        }

        private void Preview()
        {
            //frmPreviev frm = (frmPreviev)this.Owner;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (mnuMain_View__Preview.Checked)
                {
                    //this.MaximumSize = new Size(1591, 687);
                    //this.MinimumSize = new Size(1591, 687);
                    //this.Width = 1591;
                    //webBrowser1.Width = 854;
                    //pnlBrouser.Width = 835;
                    //pnlBrouser.Location = new Point(735, 0);

                    //CodeCompleat();
                    //HTMLCompleat();
                    //tlsAlign.Location = new Point(3, 0);
                    //tlsContent.Location = new Point(173, 0);
                    //tlsFormat.Location = new Point(3, 0);
                    //pnlBbcode.Location = new Point(0, 27);

                    //frmPreviev frm = (frmPreviev)this.Owner;
                    //frm.webBrowser1.DocumentText = rtxtHtml.Text;
                    //frm.Show();
                    //webBrowser1.Width = with - pnlPage1.Width - 20;
                    CodeCompleat();
                    HTMLCompleat();
                    ShowBrowser();
                    //SetZoom((with / 1200) * 100);
                }
                else
                {
                    //this.MaximumSize = new Size(750, 687);
                    //this.MinimumSize = new Size(750, 687);
                    //this.Width = 754;

                    //pnlBbcode.Location = new Point(741, 0);
                    //pnlBbcode.Size = new Size(732, 319);
                    //pnlPage1.Location = new Point(0, 27);
                    //frmPreviev frm = (frmPreviev)this.Owner;
                    //        frm.Close();
                }
            }
            catch (Exception)
            {
                if (mnuMain_View__Preview.Checked)
                {
                    //int with = SystemInformation.PrimaryMonitorSize.Width - pnlPage1.Width - 50;
                    //this.MaximumSize = new Size(with, 687);
                    //this.MinimumSize = new Size(with, 687);
                    //this.Width = with;
                    //webBrowser1.Dock = DockStyle.None;
                    //webBrowser1.Anchor = AnchorStyles.None;
                    //webBrowser1.Width = 754;

                    //pnlBrouser.Width = 754;
                    //pnlBrouser.Location = new Point(740, 0);
                    //frmPreviev frm = new frmPreviev();
                    //frm.Show();
                }
                else
                {
                    this.MaximumSize = new Size(747, 703);
                    this.MinimumSize = new Size(75, 68);
                    this.Width = 747;
                    //frm.Close();
                }

            }
            //SetAutoScrollMargins();
            //writeSetting();
            this.Cursor = Cursors.Default;
        }

        private bool PreComplete() //Проверка заполненности полей
        {
            this.Cursor = Cursors.WaitCursor;
            string[] word = new string[20];
            string msg_str = "";
            string str = "";
            bool precompl;
            if (cmbTranslation.Text == "(выбрать)")
            {
                cmbTranslation.Text = "Без перевода";
            }
            DialogResult result = DialogResult.Yes;

            if (contr != "")
            {
                word = contr.Split(';');
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] != "")
                    {
                        word[i] = word[i] + "\n";
                        str = str + word[i];
                    }
                }
                msg_str = "Не заполнены строки:\n\n" + str + "\nВы уверены, что следует продолжить?";

                result = MessageBox.Show(msg_str, "Предупреждение!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if ((contr == "") || (result == DialogResult.Yes)) //Если нажал Да
            {
                CodeCompleat();
                HTMLCompleat();
                writeField();
                precompl = true;
            }
            else
            {
                precompl = false;
            }
            this.Cursor = Cursors.Default;

            return precompl;
        }

        private void FieldsClear()
        {
            txtName.Text = "";
            txtPoster.Text = "";
            txtName_R.Text = "";
            txtName_O.Text = "";
            txtYear.Text = "";
            txtGenre.Text = "";
            txtDirector.Text = "";
            txtStudio.Text = "";
            txtActors.Text = "";
            txtCountry.Text = "";
            txtLanguage.Text = "";
            txtVideo.Text = "";
            txtAudio.Text = "";
            txtDescriptor.Text = "";
            mtxtTime.Text = "";
            txtTimes.Text = "";
            cmbTranslation.SelectedIndex = 0;
            cmbQuality.SelectedIndex = 0;
            cmbFormat.SelectedIndex = 0;
            cmbVCodac.SelectedIndex = 0;
            cmbACodac.SelectedIndex = 0;
            cmbVoting.SelectedIndex = 0;
            if (cmbCategory.Items.Count < 1)
                CategiryFormation();
            try
            {
                cmbCategory.SelectedIndex = 0;
            }
            catch(ArgumentOutOfRangeException)
            {

            }
            cmbCategory.Text = cmbCategory.SelectedItem.ToString();
            cmbTranslation.Text = cmbTranslation.SelectedItem.ToString();
            cmbQuality.Text = cmbQuality.SelectedItem.ToString();
            cmbFormat.Text = cmbFormat.SelectedItem.ToString();
            cmbVCodac.Text = cmbVCodac.SelectedItem.ToString();
            cmbACodac.Text = cmbACodac.SelectedItem.ToString();
            cmbVoting.Text = cmbVoting.SelectedItem.ToString();
            //cmbTranslation.Text = "(выбрать)";
            //cmbQuality.Text = "(выбрать)";
            //cmbFormat.Text = "(выбрать)";
            //cmbVCodac.Text = "(выбрать)";
            //cmbACodac.Text = "(выбрать)";
            //cmbVoting.Text = "(выбрать)";
            txtScrinshot.Text = "";
            distribFile = "";
            //Times = "";
            //iTimes = "";
        }

        private void UserTags()
        {
            string start = "";
            string finish = "";
            string[] strFields = new string[_Fields.Length];
            string[] strText = new string[_Text.Length];
            string[] Replacement = new string[_Fields.Length];

            // Шрифт описания
            if (fontName != "")
            {
                s_fontName = start + "[font=" + fontName + "]";
                f_fontName = "[/font]" + finish;
            }

            //Замещение записей
            for (int i = 0; i < _Fields.Length; i++)
            {
                if (_Fields[i] == _Text[i])
                {
                    strFields[i] = "";
                    strText[i] = "";
                    Replacement[i] = _Fields[i];
                }
                else
                {
                    strFields[i] = _Fields[i];
                    strText[i] = _Text[i];
                    Replacement[i] = "";
                }
            }

            if (Replacement[0] != "")
            {
                start = start + "[color=" + Replacement[0] + "]";
                finish = "[/color]" + finish;
            }
            for (int i = 1; i < Replacement.Length; i++)
            {
                if (Replacement[i] != "")
                {
                    start = start + "[" + Replacement[i] + "]";
                    finish = "[/" + Replacement[i] + "]" + finish;
                }
            }

            s_Rep = start.Replace("[]", "");
            f_Rep = finish.Replace("[/]", "");
            start = "";
            finish = "";

            //Заголовки полей
            if (strFields[0] != "")
            {
                start = start + "[color=" + strFields[0] + "]";
                finish = "[/color]" + finish;
            }
            for (int i = 1; i < strFields.Length; i++)
            {
                if (strFields[i] != "")
                {
                    start = start + "[" + strFields[i] + "]";
                    finish = "[/" + strFields[i] + "]" + finish;
                }
            }

            //if(_Fields[0] != "")
            //{
            //    start = start + "[color=" + _Fields[0] + "]";
            //    finish = "[/color]" + finish;
            //}
            //for (int i=1; i<_Fields.Length; i++)
            //{
            //    if (_Fields[i] != "")
            //    {
            //        start = start + "[" + _Fields[i] + "]";
            //        finish = "[/" + _Fields[i] + "]" + finish;
            //    }
            //}
            s_Fields = start.Replace("[]", "");
            f_Fields = finish.Replace("[/]", "");
            start = "";
            finish = "";


            //Текст описания
            if (strText[0] != "")
            {
                start = start + "[color=" + strText[0] + "]";
                finish = "[/color]" + finish;
            }
            for (int i = 1; i < strText.Length; i++)
            {
                if (strText[i] != "")
                {
                    start = start + "[" + strText[i] + "]";
                    finish = "[/" + strText[i] + "]" + finish;
                }
            }

            //if (_Text[0] != "")
            //{
            //    start = start + "[color=" + _Text[0] + "]";
            //    finish = "[/color]" + finish;
            //}
            //for (int i = 1; i < _Text.Length; i++)
            //{
            //    if (_Text[i] != "")
            //    {
            //        start = start + "[" + _Text[i] + "]";
            //        finish = "[/" + _Text[i] + "]" + finish;
            //    }
            //}
            s_Text = start.Replace("[]", "");
            f_Text = finish.Replace("[/]", "");
            start = "";
            finish = "";

            //Заголовки разделов
            if (_Headlines[0] != "")
            {
                start = start + "[color=" + _Headlines[0] + "]";
                finish = "[/color]" + finish;
            }
            for (int i = 1; i < _Headlines.Length; i++)
            {
                if (_Headlines[i] != "")
                {
                    start = start + "[" + _Headlines[i] + "]";
                    finish = "[/" + _Headlines[i] + "]" + finish;
                }
            }

            s_Headlines = start;
            f_Headlines = finish;
            start = "";
            finish = "";

            //Описание раздачи
            if (_Description[0] != "")
            {
                start = start + "[color=" + _Description[0] + "]";
                finish = "[/color]" + finish;
            }
            for (int i = 1; i < _Description.Length; i++)
            {
                if (_Description[i] != "")
                {
                    start = start + "[" + _Description[i] + "]";
                    finish = "[/" + _Description[i] + "]" + finish;
                }
            }

            s_Description = start;
            f_Description = finish;
            start = "";
            finish = "";
        }

        private void Colored()
        {
            string[] word = new string[3];
            int r = 255;
            int g = 245;
            int b = 245;
            int dig = 0;
            int ind = 0;
            contr = "";

            duration = mtxtTime.Text.Replace(' ', '0');
            word = duration.Split(':');
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == "")
                    word[i] = "00";
                dig = Convert.ToInt32(word[i]);
                if (dig < 10)
                    word[i] = "0" + Convert.ToString(dig);
            }
            duration = word[0] + ":" + word[1] + ":" + word[2];

            if (duration == "00:00:00")
            {
                mtxtTime.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(mtxtTime.Tag) + ";";
            }
            else
            {
                mtxtTime.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(mtxtTime.Tag) + ";", "");
            }
            if (txtPoster.Text.Trim() == "")
            {
                txtPoster.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtPoster.Tag) + ";";
            }
            else
            {
                txtPoster.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtPoster.Tag) + ";", "");
            }

            if (txtName_R.Text.Trim() == "")
            {
                txtName_R.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtName_R.Tag) + ";";
            }
            else
            {
                txtName_R.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtName_R.Tag) + ";", "");
            }

            if (txtYear.Text.Trim() == "")
            {
                txtYear.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtYear.Tag) + ";";
            }
            else
            {
                txtYear.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtYear.Tag) + ";", "");
            }

            if (txtName_O.Text.Trim() == "")
            {
                txtName_O.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtName_O.Tag) + ";";
            }
            else
            {
                txtName_O.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtName_O.Tag) + ";", "");
            }

            if (txtGenre.Text.Trim() == "")
            {
                txtGenre.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtGenre.Tag) + ";";
            }
            else
            {
                txtGenre.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtGenre.Tag) + ";", "");
            }

            if (txtDirector.Text.Trim() == "")
            {
                txtDirector.BackColor = Color.FromArgb(r, g, b);
                //contr = contr + Convert.ToString(txtDirector.Tag) + ";";
            }
            else
            {
                txtDirector.BackColor = Color.White;
                //contr = contr.Replace(Convert.ToString(txtDirector.Tag) + ";", "");
            }

            if (txtStudio.Text.Trim() == "")
            {
                txtStudio.BackColor = Color.FromArgb(r, g, b);
                //contr = contr + Convert.ToString(txtStudio.Tag) + ";";
            }
            else
            {
                txtStudio.BackColor = Color.White;
                //contr = contr.Replace(Convert.ToString(txtStudio.Tag) + ";", "");
            }

            if (txtActors.Text.Trim() == "")
            {
                txtActors.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtActors.Tag) + ";";
            }
            else
            {
                txtActors.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtActors.Tag) + ";", "");
            }

            if (txtCountry.Text.Trim() == "")
            {
                txtCountry.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtCountry.Tag) + ";";
            }
            else
            {
                txtCountry.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtCountry.Tag) + ";", "");
            }

            if (txtLanguage.Text.Trim() == "")
            {
                txtLanguage.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtLanguage.Tag) + ";";
            }
            else
            {
                txtLanguage.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtLanguage.Tag) + ";", "");
            }

            if (txtVideo.Text.Trim() == "")
            {
                txtVideo.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtVideo.Tag) + ";";
            }
            else
            {
                txtVideo.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtVideo.Tag) + ";", "");
            }

            if (txtAudio.Text.Trim() == "")
            {
                txtAudio.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtAudio.Tag) + ";";
            }
            else
            {
                txtAudio.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtAudio.Tag) + ";", "");
            }

            if (txtDescriptor.Text.Trim() == "")
            {
                txtDescriptor.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtDescriptor.Tag) + ";";
            }
            else
            {
                txtDescriptor.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtDescriptor.Tag) + ";", "");
            }

            if (txtScrinshot.Text.Trim() == "")
            {
                txtScrinshot.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(txtScrinshot.Tag) + ";";
            }
            else
            {
                txtScrinshot.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(txtScrinshot.Tag) + ";", "");
            }

            if (cmbTranslation.Text == cmbTranslation.Items[0].ToString())
            {
                cmbTranslation.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(cmbTranslation.Tag) + ";";
            }
            else
            {
                cmbTranslation.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(cmbTranslation.Tag) + ";", "");
            }

            if (cmbQuality.Text == cmbQuality.Items[0].ToString())
            {
                cmbQuality.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(cmbQuality.Tag) + ";";
            }
            else
            {
                cmbQuality.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(cmbQuality.Tag) + ";", "");
            }

            if (cmbFormat.Text == cmbFormat.Items[0].ToString())
            {
                cmbFormat.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(cmbFormat.Tag) + ";";
            }
            else
            {
                cmbFormat.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(cmbFormat.Tag) + ";", "");
            }

            if (cmbVCodac.Text == cmbVCodac.Items[0].ToString())
            {
                cmbVCodac.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(cmbVCodac.Tag) + ";";
            }
            else
            {
                cmbVCodac.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(cmbVCodac.Tag) + ";", "");
            }

            if (cmbACodac.Text == cmbACodac.Items[0].ToString())
            {
                cmbACodac.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(cmbACodac.Tag) + ";";
            }
            else
            {
                cmbACodac.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(cmbACodac.Tag) + ";", "");
            }

            if ((cmbCategory.Text == cmbCategory.Items[0].ToString()) && (cmbCategory.Items.Count > 1))
            {
                cmbCategory.BackColor = Color.FromArgb(r, g, b);
                contr = contr + Convert.ToString(cmbCategory.Tag) + ";";
            }
            else if (cmbCategory.Items.Count == 1)
            {
                //var msg = MessageBox.Show(
                //    "К сожалению, сайт не отвечает и загрузить список категорий не удалось!\n\nПопытаться снова?",
                //    "Нет соединения",
                //    MessageBoxButtons.YesNo,
                //    MessageBoxIcon.Warning);

                //if (msg == DialogResult.Yes)
                //{
                //    CategiryFormation();
                //    Colored();
                //}
                //else
                //{

                //}
            }
            else
            {
                cmbCategory.BackColor = Color.White;
                contr = contr.Replace(Convert.ToString(cmbCategory.Tag) + ";", "");
            }

            if (cmbVoting.Text == cmbVoting.Items[0].ToString())
            {
                cmbVoting.BackColor = Color.FromArgb(r, g, b);
                //contr = contr + Convert.ToString(cmbVoting.Tag) + ";";
            }
            else
            {
                cmbVoting.BackColor = Color.White;
                //contr = contr.Replace(Convert.ToString(cmbVoting.Tag) + ";", "");
            }
        }

        private void Complete()
        {
            txtVideo.Text = TechParser(txtVideo.Text);
            txtAudio.Text = TechParser(txtAudio.Text);
            if (VQuality != "")
                cmbQuality.Text = VQuality;
            else
                cmbQuality.Text = "(выбрать)";
            if (VFormat != "")
                cmbFormat.Text = VFormat;
            else
                cmbFormat.Text = "(выбрать)";
            if (VCodac != "")
                cmbVCodac.Text = VCodac;
            else
                cmbVCodac.Text = "(выбрать)";
            if (ACodac != "")
                cmbACodac.Text = ACodac;
            else
                cmbACodac.Text = "(выбрать)";
        }

        private void CategiryFormation()
        {
            try
            {
                //int index = 0;
                string[,] arrStr = Kinorun.List("cat");
                if (arrStr.Length < 2)
                {
                    var msg = MessageBox.Show(
                        "К сожалению, сайт не отвечает и загрузить список категорий не удалось!\n\nПопытаться снова?",
                        "Нет соединения",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (msg == DialogResult.Yes)
                    {
                        CategiryFormation();
                        Colored();
                    }
                    //CategiryFormation();
                    //index++;
                }
                else if (arrStr.Length > 1)
                {
                    string[] name = new string[arrStr.Length / 2];
                    CatInd = new string[name.Length];

                    for (int i = 1; i < name.Length; i++)
                    {
                        CatInd[i] = arrStr[0, i];
                        name[i] = arrStr[1, i];
                    }
                    name[0] = "(выбрать)";
                    cmbCategory.Items.Clear();
                    cmbCategory.Items.AddRange(name);
                    cmbCategory.SelectedIndex = 0;
                    Array.Clear(name, 0, name.Length);
                    Array.Clear(arrStr, 0, arrStr.Length);
                }
                //else if (index > 4)
                //{
                //    cmbCategory.Items.Add("(пусто)");
                //    cmbCategory.SelectedIndex = 0;

                //    MessageBox.Show(
                //        "К сожалению, сайт не отвечает и загрузить список категорий не удалось!",
                //        "Нет соединения",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);
                //} 
            }
            catch (IndexOutOfRangeException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CodeCompleat()
        {
            if (!tbtnEdit.Checked)
            {
                string fld = "      ";
                if (FieldLine)
                    fld = "\n";

                string str = "";
                string actor = "";
                string UpAdd = "";
                string DownAdd = "";
                string[] add = txtAddition.Text.Split('|');
                UserTags();
                try
                {
                    add[0] = add[0].Trim('\n');
                    add[0] = add[0].Trim('\r');
                    add[1] = add[1].Trim('\n');
                    add[1] = add[1].Trim('\r');

                    if (txtAddition.Text != "")
                    {
                        UpAdd = add[0];
                        DownAdd = add[1];
                        //if(add[0] != "")
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    if (txtAddition.Text != "")
                    {
                        UpAdd = txtAddition.Text;
                    }

                }

                txtDirector.Text = txtDirector.Text.Trim();
                txtStudio.Text = txtStudio.Text.Trim();

                if ((txtDirector.Text.Trim('-') == "") || (txtDirector.Text.Trim('-') == " "))
                {
                    if ((txtStudio.Text.Trim('-') == "") || (txtStudio.Text.Trim('-') == " "))
                        str = "";
                    else
                        str = txtStudio.Text;
                }
                else
                {
                    if ((txtStudio.Text.Trim('-') == "") || (txtStudio.Text.Trim('-') == " "))
                        str = txtDirector.Text;
                    else
                        str = txtDirector.Text + "/" + txtStudio.Text;
                }

                if (str != "")
                    str = " [" + str + "]";

                if (txtActors.Text.Length > 5)
                    actor = " (" + txtActors.Text + ")";
                else
                    actor = "";

                txtName.Text = txtName_R.Text + " / " + txtName_O.Text + actor + str + " / " + txtYear.Text + " / " + cmbQuality.Text;
                str = "";
                txtName.Text = (txtName.Text).Replace("#", "- ");

                rtxtCode.Clear();
                rtxtCode.SelectionFont = new Font("Arial", 14, FontStyle.Regular);
                if (UpAdd != "")
                    rtxtCode.AppendText(UpAdd + "\n");

                if (chkGold.Checked)
                    rtxtCode.AppendText("[img]http://picpicture.com/image.php?id=935D_4EC9E308&gif[/img][color=Goldenrod][size=14][font=geogria]Золотой торрент !!![/color][/size][/font]\n");

                rtxtCode.SelectionColor = Color.Green;
                rtxtCode.AppendText(s_fontName + "[size=14]" + s_Description);
                rtxtCode.SelectionColor = Color.Black;
                rtxtCode.AppendText(txtDescriptor.Text);
                rtxtCode.SelectionColor = Color.Green;
                rtxtCode.AppendText(f_Description + "[/size]");
                rtxtCode.AppendText("\n");

                rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                rtxtCode.SelectionColor = Color.Green;
                rtxtCode.AppendText(s_Rep);
                if (txtName_R.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Название: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtName_R.Text + f_Text);
                }

                if (txtName_O.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Оригинал: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtName_O.Text + f_Text);
                }

                if (txtYear.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Год: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtYear.Text + f_Text);
                }

                if (txtGenre.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Жанр: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtGenre.Text + f_Text);
                }

                if (txtDirector.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Режиссер: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtDirector.Text + f_Text);
                }

                if (((txtStudio.Text == "") || (txtDirector.Text == "")) && (!FieldLine))
                {
                    rtxtCode.AppendText("\n");
                }
                else
                {
                    rtxtCode.AppendText(fld);
                }

                if (txtStudio.Text.Trim('-') != "")
                {
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Студия: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtStudio.Text + f_Text);
                }

                if (txtActors.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("В ролях: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtActors.Text + f_Text);
                }

                rtxtCode.AppendText("\n");
                rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                rtxtCode.SelectionColor = Color.Green;
                rtxtCode.AppendText("[hr]");

                if (txtCountry.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Страна: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtCountry.Text + f_Text);
                }

                if (duration != "00:00:00")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Продолжительность: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    if ((txtTimes.Text != "") && (txtTimes.Text.Length > 10))
                        str = duration + " (" + txtTimes.Text.Replace(" ", "; ") + ")";
                    else
                        str = duration;
                    rtxtCode.AppendText(s_Text + str + f_Text);
                }

                if (cmbTranslation.Text != cmbTranslation.Items[0].ToString())
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Перевод: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + cmbTranslation.Text + f_Text);
                }

                if (((txtLanguage.Text == "") || (cmbTranslation.Text == cmbTranslation.Items[0].ToString())) && (!FieldLine))
                {
                    rtxtCode.AppendText("\n");
                }
                else
                {
                    rtxtCode.AppendText(fld);
                }

                if (txtLanguage.Text.Trim('-') != "")
                {
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Язык: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtLanguage.Text + f_Text);
                }
                rtxtCode.AppendText(f_Rep);
                rtxtCode.AppendText("\n\n");

                rtxtCode.SelectionFont = new Font("Arial", 14, FontStyle.Regular);
                rtxtCode.SelectionColor = Color.Green;
                rtxtCode.AppendText(s_Headlines);
                rtxtCode.SelectionColor = Color.Teal;
                rtxtCode.AppendText("Технические данные");
                rtxtCode.SelectionColor = Color.Green;
                rtxtCode.AppendText(f_Headlines);
                rtxtCode.AppendText("\n");

                rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                rtxtCode.SelectionColor = Color.Green;
                rtxtCode.AppendText(s_Rep);

                if (cmbQuality.Text != cmbQuality.Items[0].ToString())
                {
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Качество: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + cmbQuality.Text + f_Text);
                }

                if (cmbFormat.Text != cmbFormat.Items[0].ToString())
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Формат: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + cmbFormat.Text + f_Text);
                }

                if (cmbVCodac.Text != cmbVCodac.Items[0].ToString())
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Видео кодек: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + cmbVCodac.Text + f_Text);
                }

                if (txtVideo.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Видео: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtVideo.Text + f_Text);
                }

                if (cmbACodac.Text != cmbACodac.Items[0].ToString())
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Аудио кодек: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + cmbACodac.Text + f_Text);
                }

                if (txtAudio.Text.Trim('-') != "")
                {
                    rtxtCode.AppendText("\n");
                    rtxtCode.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(s_Fields);
                    rtxtCode.SelectionColor = Color.Blue;
                    rtxtCode.AppendText("Звук: ");
                    rtxtCode.SelectionColor = Color.Green;
                    rtxtCode.AppendText(f_Fields);
                    rtxtCode.SelectionColor = Color.Black;
                    rtxtCode.AppendText(s_Text + txtAudio.Text + f_Text);
                }
                rtxtCode.AppendText(f_Rep);
                rtxtCode.AppendText(f_fontName + "\n\n");

                rtxtCode.AppendText("[spoiler=[img]pic/skrin.gif[/img]]");
                rtxtCode.AppendText(txtScrinshot.Text);
                rtxtCode.AppendText("[/spoiler]");

                rtxtCode.AppendText("\n");

                rtxtCode.AppendText(Vote(cmbVoting.SelectedIndex));

                rtxtCode.AppendText("\n");
                //rtxtCode.AppendText(txtAddition.Text);

                if (DownAdd != "")
                    rtxtCode.AppendText(DownAdd + "\n");

                rtxtCode.AppendText("[color=silver][size=5][i]KinoRun Torrent Builder[/i][/size][/color]\n");
            }
        }

        private void HTMLCompleat()
        {
            rtxtHtml.Clear();
            //rtxtHtml.AppendText("<html><head><link rel=\"stylesheet\" href=\"style.css\"></head><body>");
            rtxtHtml.AppendText("<!DOCTYPE HTML>\n<html>\n<head>\n<style type=\"text/css\"><!--\n");
            rtxtHtml.AppendText("html { background-color:#eee;padding:0px;margin:0px;min-width:1200px;font-family:Calibri, sans-serif;font-size:14px; }\n");
            rtxtHtml.AppendText("body { padding:0px;margin:0px;font:normal 14px Calibri;color:#595959;background:#efefef; }\n");
            //rtxtHtml.AppendText(".spoiler > input + .box { display: none; }\n");
            //rtxtHtml.AppendText(".spoiler > input:checked + .box { display: block; }\n");
            rtxtHtml.AppendText("input[id^=\"spoiler\"]{ display: none; }\n");
            rtxtHtml.AppendText("input[id^=\"spoiler\"] + label { display: block; width: 200px; margin: 0 auto; padding: 5px 20px; color: #fff; text-align: center; font-size: 24px; border-radius: 8px; cursor: pointer; transition: all .6s; }\n");
            rtxtHtml.AppendText("input[id^=\"spoiler\"]:checked + label { color: #333; background: #ccc; }\n");
            rtxtHtml.AppendText("input[id^=\"spoiler\"] ~ .spoiler { width: 90%; overflow: hidden; opacity: 0; margin: 10px auto 0;  padding: 10px;  background: #eee; border: 1px solid #ccc; border-radius: 8px; transition: all .6s; }\n");
            rtxtHtml.AppendText("input[id^=\"spoiler\"]:checked + label + .spoiler{  height: auto; opacity: 1; padding: 10px; }\n");

            rtxtHtml.AppendText("--></style>\n</head>\n<body>\n");

            rtxtHtml.AppendText("<p align=\"center\" size=\"20\">\n");
            rtxtHtml.AppendText("<span style=\"color: red; font-family: Verdana, Verdana, Geneva, sans-serif; font - size: 2em\">\n");
            rtxtHtml.AppendText(txtName.Text);
            rtxtHtml.AppendText("</span></p>\n");

            rtxtHtml.AppendText("<img src=\"" + txtPoster.Text + "\">\n");
            rtxtHtml.AppendText("<br><hr><br>\n");

            for (int i = 0; i < rtxtCode.Lines.Length; i++)
            {
                string s = rtxtCode.Lines[i].Replace("=[img]pic/skrin.gif[/img]", "");
                s = rtxtCode.Lines[i].Replace("     ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                rtxtHtml.AppendText(BBParser(s));
                rtxtHtml.AppendText("\n");
            }

            rtxtHtml.AppendText("</body>\n</html>");
            //string brows = rtxtHtml.Text.Replace("<img src=\"pic/skrin.gif\">", "");
            Html = rtxtHtml.Text;
            //if (tbcBuilder.SelectedTab == tbcBuilder.TabPages["tabPreview"])
            webBrowser1.DocumentText = Html;

            fView.webBrowser1.DocumentText = Html;
            //if(mnuMain_View__Preview.Checked)
            //{
            //    frmPreviev frm = new frmPreviev();
            //    frm.Owner = this;
            //    frm.webBrowser1.DocumentText = rtxtHtml.Text;
            //}
        }

        private string Vote(int indVote)
        {
            string strVote = "";

            switch (indVote)
            {
                case 0:
                    strVote = "[hr][center][color=#843BF1][size=28]ТЫКНИ В ПАЛЬЧИК!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/r2xnd6pk604e68ws5zla.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 1:
                    strVote = "[hr]&#10;[center][color=#843BF1][size=28]ПОТИСКАЙ ИХ, ОНА ЭТОГО ХОЧЕТ!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/r2xnd6pk604e68ws5zla.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 2:
                    strVote = "[hr]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/mkiye879tpziat57bt46.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 3:
                    strVote = "[hr]&#10;[center][color=#843BF1][size=28]СОРВИ ВИШЕНКУ!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/2tc12c9opvh94vk9kxm0.jpg[/img][/url][/center]&#10;[hr]";
                    break;
                case 4:
                    strVote = "[hr]&#10;[center][color=#843BF1][size=28]ПОЙМАЙ ПИНГВИНА[/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/zkwwwgf0ytqfim58vv5k.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 5:
                    strVote = "[hr]&#10;[center][color=#843BF1][size=28]ПОЙМАЙ МЫШОНКА !!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/zrw7gmj0ww7lhatd8dm.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 6:
                    strVote = "[hr][center][color=#843BF1][size=28]ТЫКНИ В ПСА!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/u8qyjebuvxzjdk1bfm3.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 7:
                    strVote = "[hr][center][color=#843BF1][size=28]ПНИ ЕЖА!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/a5x8cddn7iyr5ync766.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 8:
                    strVote = "[hr][center][color=#843BF1][size=28]ПОЙМАЙ МОРКОВКУ!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/itjim4fzkwx8470ut96s.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 9:
                    strVote = "[hr][center][color=#843BF1][size=28]ПОСЧИТАЙ БАБКИ!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/04devamlk9w01gi8xnh8.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 10:
                    strVote = "[hr][center][color=#843BF1][size=28]РАЗБУДИ КОТА!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/r5inrlbcf0yiuxa4u0b.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 11:
                    strVote = "[hr][center][color=#843BF1][size=28]ОТБЕРИ ОРЕХ У ХОМЯКА!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/0wg9ncedk1bfx6e5ylmc.gif[/img][/url][/center]&#10;[hr]";
                    break;
                case 12:
                    strVote = "[hr][center][color=#843BF1][size=28]ЗАГЛЯНИ ПОД РУЧКИ!!![/size][/color][/center]&#10;[center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/ihj5fjoh1d4av6njc47u.jpg[/img][/url][/center]&#10;[hr]";
                    break;
                case 13:
                    strVote = "[hr][center][color=#843BF1][size=28]ПИВО БУДЕШ!?[/size][/color][/center][center][url=http://www.toptracker.ru/details.php?id=329][img]http://picpicture.com/images/o7301v6m3aw5thvgs6x1.jpg[/img][/url][/center]&#10;[hr]";
                    break;
                default:
                    break;
            }
            return strVote;
        }

        private void FormImport()
        {
            string[] txt = new string[] { "Русское название: ", "Оригинальное название: ", "Год выпуска: ", "Жанры: ", "Режиссёр: ", "Студия: ", "В ролях: ", "Страна: ", "Язык: ", "Описание: ", "", "", "", "" };
            frmImport frm = new frmImport();
            //if (contr == "")
            //{

            //for (int i = 0; i < (_txt.Length + 1); i++)
            //{
            //    if (i < 10)
            //    {
            //        if (i == 7)
            //        {
            //            if (iTimes != "")
            //                frm.rtxtInfo_Video.AppendText("Продолжительность: " + iTimes + "\n");
            //            else
            //                frm.rtxtInfo_Video.AppendText("Продолжительность: " + mtxtTime.Text + "\n");
            //        }
            //        else if (i == 9)
            //            frm.rtxtInfo_Video.AppendText(txt[i] + _txt[i].Text.Replace("\n", "|") + "\n");
            //        else
            //            frm.rtxtInfo_Video.AppendText(txt[i] + _txt[i].Text + "\n");
            //    }

            //    if (i == 10)
            //        frm.txtInfo_Tech.Text = _txt[i].Text + "\n\r" + _txt[i + 1].Text.Replace("\n", "");

            //    if (i == 12)
            //        frm.txtInfo_Images.Text = _txt[i].Text + "\n\r" + _txt[i + 1].Text;
            //}
            //}
            frm.Owner = this; //Передаём вновь созданной форме её владельца.
            Imp = true;
            frm.Show();
        }

        private string TechParser(string techString)
        {
            if (techString != "")
            {
                string[] techInfo = new string[2];
                string[] techQuality = new string[2];
                int ind = 0;
                string techVar;

                techInfo[1] = "";
                techInfo = techString.Split(' ');
                techVar = techInfo[0];

                if (techVar == "Video:")
                {
                    switch (techInfo[1])
                    {
                        case "Windows":
                            ind = 5;
                            VFormat = "WMV";
                            VCodac = "WMV";
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "MPEG4":
                            ind = 4;
                            VFormat = "МР4 - МР4 File";
                            VCodac = "H264";
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "DivX":
                            ind = 3;
                            VFormat = "AVI";
                            VCodac = "DivX";
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "MPEG1":
                            ind = 3;
                            VFormat = "AVI";
                            VCodac = techInfo[1];
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "Xvid":
                            ind = 2;
                            VFormat = "AVI";
                            VCodac = "Xvid";
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        default:
                            break;
                    }
                    if (ind > 0)
                    {
                        techQuality = techInfo[ind].Split('x');
                        double resal = Convert.ToDouble(techQuality[0]) / Convert.ToDouble(techQuality[1]);
                        if (resal > 1.7f)
                        {
                            VQuality = "HD";
                            if (techQuality[0] == "720")
                                VQuality = "SD";
                            if (techQuality[1] == "720")
                                VQuality = "HD 720";
                            if (techQuality[1] == "1080")
                                VQuality = "HD 1080";
                        }
                        //if ((techQuality[0] == "720") || (techQuality[1] == "576"))
                        //    VQuality = "SD";
                        //if ((techQuality[0] == "1280") || (techQuality[1] == "720"))
                        //    VQuality = "HD 720";
                        //if ((techQuality[1] == "1920") || (techQuality[1] == "1080"))
                        //    VQuality = "HD 1080";
                    }
                }
                else if (techVar == "Audio:")
                {
                    switch (techInfo[1])
                    {
                        case "WMA":
                            ind = 3;
                            ACodac = techInfo[1] + techInfo[2];
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "Dolby":
                            ind = 3;
                            ACodac = techInfo[2];
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "MP3":
                            ind = 2;
                            ACodac = techInfo[1];
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "MPEG":
                            ind = 3;
                            ACodac = techInfo[1];
                            techString = techInfo[ind] + ", " + techInfo[ind + 1];
                            if (techInfo[ind + 2][0] != '[')
                            {
                                techString = techString + ", " + techInfo[ind + 2];
                                if (techInfo[ind + 3][0] != '[')
                                    techString = techString + ", " + techInfo[ind + 3];
                            }
                            break;
                        case "AAC":
                            goto case "MP3";
                        case "PCM":
                            goto case "MP3";
                        default:
                            break;
                    }
                }
            }
            return techString;
        }

        private string BBParser(string bbCode)
        {
            string[] bbWord = bbCode.Split(new Char[] { '[', ']' });
            string[] bbC;
            string open = "<";
            string close = ">";
            string sp = "<span style=";
            string htmString = "";
            int ind = 0;

            foreach (string s in bbWord)
            {
                string str = "";

                if (s.Trim() != "")
                {
                    bbC = s.Split(new Char[] { '=' });
                    try
                    {
                        switch (bbC[0])
                        {
                            case "b":
                                str = open + s + close;
                                break;
                            case "/b":
                                str = open + s + close;
                                break;
                            case "i":
                                goto case "b";
                            case "/i":
                                goto case "b";
                            case "u":
                                goto case "b";
                            case "/u":
                                goto case "b";
                            case "hr":
                                goto case "b";
                            case "pre":
                                goto case "b";
                            case "/pre":
                                goto case "b";
                            case "size":
                                str = sp + "\"font-size: " + bbC[1] + "\"" + close;
                                break;
                            case "/size":
                                str = "</span>";
                                break;
                            case "font":
                                str = sp + "\"font-family: " + bbC[1] + "\"" + close;
                                break;
                            case "/font":
                                str = "</span>";
                                break;
                            case "center":
                                str = open + "p align=\"center\"" + close;
                                break;
                            case "/center":
                                str = "</p>";
                                break;
                            case "color":
                                str = sp + "\"color: " + bbC[1] + "\"" + close;
                                break;
                            case "/color":
                                goto case "/size";
                            case "img":
                                str = "<img src=\"" + bbWord[ind + 1] + "\"" + close;
                                bbWord[ind + 1] = "";
                                break;
                            case "/img":
                                str = "";
                                break;
                            case "url":
                                if (bbC.Length > 1)
                                    str = "<a href=\"" + bbC[1] + "\"" + close;
                                else
                                {
                                    bbC = bbWord[ind + 1].Split(new Char[] { ' ' });
                                    bbC = bbC[0].Split(new Char[] { ':' });
                                    if (bbC[0] == "http")
                                        str = "<a href=\"" + bbC[0] + "\"" + close;
                                    else
                                    {
                                        string str1;
                                        bool c = false;
                                        if (frmInput.Input("Ввод значения", "Введите URL-адрес:", "", out str1, c))
                                        {
                                            bbC = str1.Split(new Char[] { ':' });
                                            if (bbC[0] == "http")
                                                str = "<a href=\"" + str1 + "\"" + close;
                                            else
                                                str = "<a href=\"http:\\\\" + str1 + "\"" + close;
                                        }
                                        else
                                            MessageBox.Show("Отмена действий.");
                                    }
                                }
                                //bbWord[ind + 1] = "";
                                break;
                            case "/url":
                                str = "</a>";
                                break;
                            case "/spoiler":
                                str = "</div>";
                                break;
                            case "spoiler":
                                //str = "<details><summary><img src=\"http://kinorun.com/" + bbWord[3] + "\"></summary>";
                                //str = "<div class=\"spoiler\"><input type=\"checkbox\"><img src=\"http://kinorun.com/" + bbWord[3] + "\"><div class=\"box\">";
                                str = "<input type=\"checkbox\"  id=\"spoiler2\" /><label for=\"spoiler2\" ><img src=\"http://kinorun.com/pic/skrin.gif\"></label><div class=\"spoiler\">";
                                break;
                            case "quote":
                                str = open + "blockquote" + close;
                                //str = "<div style=\"margin: 2px 2px 2px 5px\">";
                                //bbWord[ind + 1] = "";
                                break;
                            case "/quote":
                                str = "</blockquote>";
                                break;
                            case "*":
                                str = "<li>";
                                break;
                            default:
                                str = s;
                                break;
                        }
                    }
                    catch(IndexOutOfRangeException)
                    { break; }
                    htmString = htmString + str;
                }

                ind++;
            }

            return htmString = htmString + "<br />";
        }

        private string urlProxy()
        {
            string urlproxy = "https://hidemy.name/ru/proxy-list/";
            string chkUrl = "http://pornolab.net/";
            string pr = "";

            using (var net = new xNet.HttpRequest(urlproxy))
            {
                net.UserAgent = Http.ChromeUserAgent();
                CookieDictionary cookie = new CookieDictionary(false);

                net.Cookies = cookie;
                net.CharacterSet = Encoding.GetEncoding(1251);
                var urlParams = new RequestParams();
                urlParams["maxtime"] = "800";
                urlParams["ports"] = "3128";
                urlParams["type"] = "hs";
                string done = "";
                try
                {
                    done = net.Get(net.BaseAddress.AbsolutePath, urlParams).ToString();
                }
                catch (xNet.HttpException ex)
                {
                    done = ex.Message;
                }

                //pr = Regex.Matches(done, @"\d?\d?\d\.\d?\d?\d\.\d?\d?\d\.\d?\d?\d").Value;
                Regex rx = new Regex(@"\d?\d?\d\.\d?\d?\d\.\d?\d?\d\.\d?\d?\d");
                MatchCollection mch = rx.Matches(done);
                //pp = "";
                foreach (Match match in mch)
                {
                    using (var net1 = new xNet.HttpRequest(chkUrl))
                    {
                        try
                        {
                            net1.UserAgent = Http.ChromeUserAgent();
                            cookie = new CookieDictionary(false);

                            net1.Cookies = cookie;
                            net1.CharacterSet = Encoding.GetEncoding(1251);
                            //net.Proxy = HttpProxyClient.Parse("178.151.149.227:80");
                            //net.Proxy = HttpProxyClient.Parse("51.15.73.76:8080");
                            //net.Proxy = HttpProxyClient.Parse("217.196.148.177:3128");
                            net1.Proxy = HttpProxyClient.Parse(match.Value + ":3128");

                            string don = net1.Get(net1.BaseAddress.AbsolutePath).ToString();
                            pr = match.Value;
                            break;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
            return pr;
        }

        private void TextParsing(string page)
        {
            string r = string.Empty;
            string o = string.Empty;
            string p = string.Empty;
            string y = string.Empty;
            string s = page.Replace(" / ", "\r\n");
            s = s.Replace(":<br>", ":");
            s = s.Replace(":<br />", ":");
            s = s.Replace("<br>", "\r\n");
            s = s.Replace("<br />", "\r\n");
            s = s.Replace(":\r\n", ":");
            s = Regex.Replace(s, "\t", string.Empty);

            //string[] adrurl = net.Address.Authority.Split('.');
            string[] aword = {adrurl[adrurl.Length -1],
                            adrurl[adrurl.Length -2],
                            "скачать",
                            "без регистрации и рейтинга",
                            "торрент",
                            "трекер",
                            "xxx",
                            "фильм",
                            "бесплатн",
                            "ххх",
                            "порно",
                            "download",
                            "torrent",
                            "детал",
                            "релиз" };

            string title = s.Substring("<title>", "</title>");
            title = title.Replace("\n", " ");
            title = title.Replace("\r", " ");
            title = title.Replace("\"", "");

            foreach (string str in aword)
            {
                string patern = @"\b(?i)" + str + @"\w*\b";

                title = Regex.Replace(title, patern, string.Empty);
            }
            title = Regex.Replace(title, @"&[a-zA-Z0-9]+;", string.Empty); //спецсимволы HTML
            //title = Regex.Replace(title, @"[A-Za-z0-9-]+\.[A-Za-z0-9-]+", string.Empty);
            title = Regex.Replace(title, @"([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}", string.Empty); //домен сайта
            title = title.Replace("  ", " ");
            title = title.Trim(' ', '-', ';', ',', '.', ':');
            title = Regex.Replace(title, @"((([\w]+[- ])+[\w]+[,][ ])+)[\w]+[ ][\w]+", string.Empty); //домен сайта
            //title = Regex.Replace(title, @"(([ a-zA-Z]{1,}?[,][ ]){1,3}[a-zA-Z]+)+[ ]", string.Empty); //домен сайта
            o = Regex.Match(title, @"([a-zA-Z]{1,}[ ]){1,}[a-zA-Z0-9 ]+").Value;
            //o = Regex.Match(title, @"(\W[a-zA-Z0-9]+)+").Value;
            //o = Regex.Match(title, @"(\W[a-zA-Z]+([#№-][0-9]+))+").Value;
            title = o != "" ? title.Replace(o, "") : title;
            title = " " + title;
            r = Regex.Match(title, @"([а-яА-Я]{1,}[ ]){1,}?[а-яА-Я0-9 ]+").Value;
            //r = Regex.Match(title, @"(\W[а-яА-Я0-9]+)+").Value;
            //r = Regex.Match(title, @"^[A-ЯЁ][а-яё]+\s[A-ЯЁ][а-яё]+$").Value;
            y = Regex.Match(title, @"\d{4}").Value;

            txtName_R.Text = r;
            txtName_O.Text = o;
            txtYear.Text = y;

            int chInd = -1;
            int[] ind = new int[field.Length];
            for (int i = 0; i < field.Length; i++)
            {
                if ((i != 8) && (i != 6))
                {
                    int ko = s.ToLower().IndexOf(field[i]);
                    ind[i] = ko;
                }
                else
                {
                    ind[i] = -1;
                }
            }

            int sVal = 0,
                sum = 0;

            for (int i = 0; i < field.Length; i++)
            {
                int val = ind[i];
                if (val > 0)
                {
                    sVal = sVal + val;
                    sum++;
                }
            }

            sVal = sVal / sum;

            for (int i = 0; i < field.Length; i++)
            {
                if (ind[i] < 0)
                    ind[i] = ind.Max();

                int razn = ind.Max() - ind.Min();
            }

            for (int i = 0; i < field.Length; i++)
            {
                int razn = ind.Max() - ind.Min();
                if (ind[i] == ind.Min())
                {
                    if (razn > 1000)
                    {
                        ind[i] = ind.Max();

                    }
                    razn = ind.Max() - ind.Min();
                    if (razn > 1000)
                        i = -1;
                }
            }

            if (ind.Min() > sVal)
                chInd = sVal - 1200;
            else
                chInd = ind.Min() - 1000;

            string fnd = s.Substring(chInd, s.Length - chInd);

            int scrin = fnd.ToLower().IndexOf("скрин");
            if (scrin < 0)
                scrin = fnd.ToLower().IndexOf("файл");
            if (scrin < 0)
                scrin = fnd.ToLower().IndexOf("аудио");

            if (0 < scrin)
                fnd = fnd.Remove(scrin, fnd.Length - scrin);

            chInd = fnd.IndexOf(o);
            if (chInd < 0)
                chInd = fnd.IndexOf(r);

            if (chInd > 0)
                fnd = fnd.Remove(0, chInd);

            int dv = s.ToLower().IndexOf("<div"),
            tbl = s.ToLower().IndexOf("<table");

            string text = Regex.Replace(fnd, "<[^>]+>", string.Empty);
            text = text.Replace(":\r\n", ":");
            if (r.Length < 5)
                r = Regex.Match(text, @"(\W[а-яА-Я]+)+").Value;
            if (o.Length < 5)
                o = Regex.Match(text, @"(\W[a-zA-Z0-9#№]+)+").Value;

            p = Regex.Match(text, @"\d?\d\:\d?\d\:\d?\d").Value;
            if ((y != "") && ((Convert.ToInt32(y)) < 1900))
                y = Regex.Match(text, @"\d{4}").Value;

            int k = 0;
            foreach (string str in field)
            {
                string ss = text.ToLower();
                if (ss.IndexOf(str) > -1)
                    k++;
            }

            FieldComliete(text);

            string pp = "";
            if (txtTimes.Text.Length < 5)
            {
                Regex rx = new Regex(@"\d?\d\:\d?\d\:\d?\d");
                MatchCollection mch = rx.Matches(fnd);
                //pp = "";
                foreach (Match match in mch)
                {
                    GroupCollection groups = match.Groups;
                    pp = pp + "; " + match;
                }
                txtTimes.Text = pp;
                mtxtTime.Text = pp.Length > 8 ? AdditionFunc.Calculate(p) : pp;
            }
        }

        private void FieldComliete(string text)
        {
            string p = "";
            text = text.Replace("\r\n\r\n", "\r\n");
            text = text.Replace(": ", ":");
            text = Regex.Replace(text, @":[\r]", ":");
            text = Regex.Replace(text, @":[\n]", ":");
            foreach (string str in field)
            {
                string ss = text.ToLower();
                int ind = ss.IndexOf(str);
                if (ind > -1)
                {
                    string a = ss.Substring(str, "\n");
                    string sub = text.Substring(ind, a.Length + str.Length);
                    string[] line = sub.Split(':');
                    if (line.Length > 1)
                        a = line[1].Trim(' ', '\r', '\n');
                    else
                        continue;

                    if (str == "продолжительность")
                    {
                        Regex rx = new Regex(@"\d?\d\:\d?\d\:\d?\d");
                        MatchCollection mch = rx.Matches(sub);
                        foreach (Match match in mch)
                        {
                            GroupCollection groups = match.Groups;
                            p = p + "; " + match;
                        }
                        p = p.Trim(';', ' ', ';', '\r', '\n');

                        if (line.Length < 4)
                        {
                            string tm = "";
                            string hor = Regex.Match(a, @"\d?\d\s?[hч]\w+").Value;
                            a = hor != "" ? a.Replace(hor, "") : a;
                            string min = Regex.Match(a, @"\d?\d\s?[mм]\w+").Value;
                            a = min != "" ? a.Replace(min, "") : a;
                            string sec = Regex.Match(a, @"\d?\d\s?[sс]\w+").Value;

                            hor = hor != "" ? Regex.Match(hor, @"\d{1,2}").Value + ":" : "00:";
                            min = min != "" ? Regex.Match(min, @"\d{1,2}").Value + ":" : "00:";
                            sec = sec != "" ? Regex.Match(sec, @"\d{1,2}").Value : "00";

                            hor = hor.Length < 3 ? "0" + hor : hor;
                            min = min.Length < 3 ? "0" + min : min;
                            sec = sec.Length < 2 ? "0" + sec : sec;

                            p = hor + min + sec;
                        }
                    }

                    if (str == "описание")
                    {
                        string[] lines = text.Split('\n');
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i] == sub)
                            {
                                for (int j = i + 1; j < i + 6; j++)
                                {
                                    if (lines[j].Length > 50)
                                    {
                                        a = a + "\r\n" + lines[j];
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    switch (str)
                    {
                        case "название":
                            txtName_O.Text = a;
                            break;
                        case "жанр":
                            txtGenre.Text = a;
                            break;
                        case "режис":
                            txtDirector.Text = a;
                            break;
                        case "студия":
                            txtStudio.Text = a;
                            break;
                        case "сайт":
                            goto case "студия";
                        case "ролях":
                            txtActors.Text = a;
                            break;
                        case "актрисы":
                            goto case "ролях";
                        case "страна":
                            txtCountry.Text = a;
                            break;
                        case "язык":
                            txtLanguage.Text = a;
                            break;
                        case "описание":
                            txtDescriptor.Text = a;
                            break;
                        case "продолжительность":
                            txtTimes.Text = p;
                            mtxtTime.Text = p.Length > 8 ? AdditionFunc.Calculate(p) : p;
                            break;
                            //case "":
                            //goto case ;
                            //    break;
                    }

                }
            }

            if (txtDescriptor.Text.Length < 10)
            {
                string[] line = text.Split('\n');

                int ko = -1;
                int lng = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].IndexOf("Описание:") > -1)
                    {
                        ko = i;
                        break;
                    }
                    else
                    {
                        line[i] = line[i].Trim(' ', '\r');
                        line[i] = line[i].Replace("  ", "");
                        if (lng < line[i].Length)
                        {
                            lng = line[i].Length;
                            ko = i;
                        }
                    }
                }

                txtDescriptor.Text = line[ko];
            }
        }

        private void FormLoad()
        {
            //FieldsClear();
            string strApName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            string[] arrVersion = strVersion.Split('.');

            if (arrVersion[3] != "0")
            {
                if (arrVersion[2] != "0")
                    strVersion = arrVersion[0] + "." + arrVersion[1] + "." + arrVersion[2] + "." + arrVersion[3];
            }
            else
            {
                if (arrVersion[2] != "0")
                    strVersion = arrVersion[0] + "." + arrVersion[1] + "." + arrVersion[2];
                else
                    strVersion = arrVersion[0] + "." + arrVersion[1];
            }

            ExeName = strApName;
            Version = strVersion;

            this.Text = "KinoRun Torrent Builder v. " + strVersion;
            //strVersion = strVersion.Remove(5);
            //distribFile = "";

            ToolTip t = new ToolTip();
            t.SetToolTip(txtName, "Название раздачи формируется автоматически при нажатии кнопки [Сформировать код].\nДля копирования Названия раздачи в буфер обмена достаточно двойного клика на поле.");
            t.SetToolTip(txtPoster, "Вставьте сюда прямую ссылку на изображение постера раздачи.\nДля копирования из поля ссылки в буфер обмена достаточно двойного клика на поле.");
            t.SetToolTip(txtName_R, "");
            t.SetToolTip(txtName_O, "");
            t.SetToolTip(txtYear, "");
            t.SetToolTip(txtGenre, "Нажмите на кнопку рядом с полем для формирования списка жанров раздачи");
            t.SetToolTip(btnGenre, "Нажмите на эту кнопку для формирования списка жанров раздачи");
            t.SetToolTip(txtDirector, "Необязательное поле, потому как режисёр указан не у всех фильмов. Можно поставить - или просто пробел.");
            t.SetToolTip(txtStudio, "");
            t.SetToolTip(txtActors, "");
            t.SetToolTip(txtCountry, "");
            t.SetToolTip(txtLanguage, "");
            t.SetToolTip(txtVideo, "Сюда можно вставить целиком строку Видео из окна Свойств медиаплейера, например,\nVideo: MPEG4 Video (H264) 704x400 25fps 1288kbps [V: h264 high L3.1, yuv420p, 704x400, 1288 kb/s]\nПри этом будут сформированы такие поля, как Формат и Видеокодек, а также обрежется\nлишняя информация строки Видео.");
            t.SetToolTip(txtAudio, "Сюда можно вставить целиком строку Аудио из окна Свойств медиаплейера,\nнапример,Audio: AAC 48000Hz stereo 95kbps [A: aac lc, 48000 Hz, stereo, 95 kb/s]\nПри этом будет сформировано поле Аудиокодек, а также обрежется лишняя\nинформация строки Аудио.");
            t.SetToolTip(txtDescriptor, "");
            t.SetToolTip(mtxtTime, "Если вам нужно посчитать время разных видео файлов раздачи, то нажмите на кнопку рядом с полем.");
            t.SetToolTip(btnTime, "Если вам нужно посчитать время разных видео файлов раздачи, то нажмите на эту кнопку.");
            t.SetToolTip(cmbTranslation, "");
            t.SetToolTip(cmbQuality, "");
            t.SetToolTip(cmbFormat, "");
            t.SetToolTip(cmbVCodac, "");
            t.SetToolTip(cmbACodac, "");
            t.SetToolTip(cmbVoting, "");
            t.SetToolTip(cmbCategory, "");
            t.SetToolTip(txtAddition, "Вставьте сюда любой текст (BBCode допускается) для его добавления в начало или конец раздачи.\nДля добавления примечания в конец раздачи, перед ним следует поставить символ |");
            t.SetToolTip(label22, "Вставьте сюда любой текст (BBCode допускается) для его добавления в начало или конец раздачи.\nДля добавления примечания в конец раздачи, перед ним следует поставить символ |");
            t.SetToolTip(txtScrinshot, "Вставьте сюда ссылку из раздела Код среднего размера (миниатюры) со ссылкой\nполученную на Picpicture.com при заливе скриншотов.");

            if (File.Exists(ExePath + "\\" + ExeName + ExeExt)) //Проверяем и удаляем, если есть, использованный файл автообновления
                File.Delete(ExePath + "\\" + ExeName + ExeExt);

            readSetting();
            readField();

            if (sett)
            {
                writeSetting();
                writeField();
            }

            CategiryFormation();

            mtxtTime.ValidatingType = typeof(String);
            Colored();
            FieldsClear();

            //string str = frmMain.SetingPath + "design.xml";
            //if (!File.Exists(str))
            //{
            //    mnuMain_Tools_DesignOption.PerformClick();
            //}
            //readSetting();
            //readField();

            try
            {
                if (distribFile1 != "")
                {
                    if ((distribFile1 == "a") && (!sett))
                    {
                        access = distribFile1;
                        if (FileAssociations.IsAssociated)
                            FileAssociations.sRemove();
                        else
                            FileAssociations.Associate();
                    }
                    else if (distribFile1.Length > 3)
                    {
                        distribFile = distribFile1;
                        distrib = new Distrib(distribFile);
                        readSave();
                        LastFiles(distribFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка в параметрах запуска: \n\n" +
                    ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            webBrowser1.DocumentText = rtxtHtml.Text;
            menuLast();

            if ((_KinoRun[0] != "") && (_KinoRun[1] != ""))
                Userstat = Kinorun.UserStatus(_KinoRun[0], _KinoRun[1]);

            if (Userstat > 7)
            {
                mnuKinorun_Admin.Visible = true;
                chkGold.Visible = true;
            }
            else
            {
                mnuKinorun_Admin.Visible = false;
                chkGold.Visible = false;
            }
            modif = false;
            writeField();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            sett = AdditionFunc.ReSetings();
            readUpdate();

            //if ((!File.Exists(SetingPath + @"\settings.xml")) || (!File.Exists(SetingPath + @"\design.xml")))
            if (sett)
            {
                var msg = MessageBox.Show("По всей видимости это ваш первый запуск.\n" +
                    "Настройте программу, указав на вкладке Авторизация регистрационные данные, которые вы используете на трекере KinoRun и в хранилище изображений!\n" +
                    "Если вы ещё не зарегистрированы, на той же вкладке вы найдёте соответствующие ссылки или поля.",
                    "Первый запуск",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                access = distribFile1;
                if (!FileAssociations.IsAssociated)
                    FileAssociations.Associate();

                mnuMain_Tools_DesignOption.PerformClick();

                //frmDesign frm = new frmDesign();
                //frm.Owner = this; //Передаём вновь созданной форме её владельца.
                //frm.Show();
            }
            else
            {
                FormLoad();
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {

            if (tbcBuilder.SelectedTab != tbcBuilder.TabPages["tabCode"])
            {
                readSetting();
                this.BackColor = theme;
                mnuMain.BackColor = theme;
                tabForm.BackColor = theme;
                tabCode.BackColor = theme;
                tabPreview.BackColor = theme;
                if (Theme < 2)
                    chkGold.ForeColor = Color.Goldenrod;
                else
                    chkGold.ForeColor = Color.Gold;

                bGenre = iGenre.Length > 1 ? true : false;

                readField();
                Complete();

                if (First)
                {
                    if (access == "a")
                        this.Close();
                    else
                    {
                        First = false;
                        FormLoad();
                    }
                }
                //if (tbcBuilder.SelectedTab == tbcBuilder.TabPages["tabForm"])
                //    txtName_R.Focus();
                //else
                //    webBrowser1.Focus();
                //txtGenre.Text = Genre;
                //txtGenre.Tag = iGenre;
                //mtxtTime.Text = Times;
                //txtTimes.Text = iTimes;
                //if (!)
                //{
                //}
                //else
                //{
                //    if ((_KinoRun[0] != "") && (_KinoRun[1] != ""))
                //    {
                //        this.Close();
                //    }
                //else
                //{
                //    var msg = MessageBox.Show("Внесите данные регистрации на трекере KinoRun.\nБез авторизации на трекере многие функции, в том числе и заливка раздачи на трекер, будет невозможны!",
                //        "Первый запуск",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Exclamation);
                //}
                //    }
            } 
            //if (mnuMain_View__Preview.Checked)
            //{
            //    //fView.Hide();
            //    //fView.Show();
            //    //fView.Focus();
            //    //rtxtCode.Focus();
            //    //tbcBuilder.SelectedTab.Focus();
            //}
        }

        private void frmMain_LocationChanged(object sender, EventArgs e)
        {
            locX = this.Location.X + this.Width;
            locY = this.Location.Y;
            fView.Location = new Point(locX, locY);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string[] lcontr = contr.Split(';');

            writeField();
            writeUpdate();

            //if ((lcontr.Length == 1) || (lcontr.Length == 20))
            //    modif = false;
            //else
            //    modif = true;

            if (modif)
            {
                var msg = MessageBox.Show("Закрыв программу, вы можете потерять несохраннные вами данные.\nХотите перед выходом сохранить вашу раздачу?",
                    "Выход",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    mnuMain_File_Save.PerformClick();
                }
                //else if (msg == DialogResult.No)
                //{
                //    this.Close();
                //}
            }

            string filePath = frmMain.SetingPath + @"temp\";
            AdditionFunc.ClearDirTorrent(filePath);
            if (Directory.Exists(filePath))
                Directory.Delete(filePath);

            if (ClearDir)
            {
                filePath = frmMain.SetingPath + @"torrent\";

                bool b = AdditionFunc.ClearDirTorrent(filePath);
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Запуск приложения автообновления произойдёт после закрытия формы запускающего приложения.
            //access = "a";
            string UpdExe = ExePath + "\\AutoUpdater.exe";
            string arg = SetingPath + " " + access;
            string strApName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();

            if (First)
                Process.Start(Application.ExecutablePath, access);
            else
            {
                if (access != "")
                {
                    if (access == "a")
                    {
                        Process p = new Process();
                        ProcessStartInfo s = new ProcessStartInfo();
                        s.FileName = UpdExe;  //тут нужно указывать либо полный путь к запускаемой программе, либо вызывать программы быстрого запуска Windows, которые в основном покоятся в system32
                        s.CreateNoWindow = false;  //создавать ли новое окно для данного процесса?
                        s.WindowStyle = ProcessWindowStyle.Hidden;  //тут указан параметр запуска Hidden, благодаря чему вызываемая программа будет запущена скрытно
                                                                    //так же стоит отметить, что не все программы могут послушать такой аргумент запуска и открыться как соизволят сами
                                                                    //вместо hidden могут стоять и другие значения:
                                                                    //Normal - нормальное окно, самое обычное
                                                                    //Maximized - развернутое окно
                                                                    //Minimized - окно свернуто
                        s.Arguments = arg; //собственно из-за чего весь сыр-бор - параметры(или аргументы) запуска приложения, которые передаются ему сразу после запуска на выполнение
                        s.Verb = "runas"; //запуск программы с правами администратора, при отсутствие которых выйдет вездесущий UAC с соответствующим запросом
                        s.LoadUserProfile = false;//загружать ли профиль пользователя(в основном это вовсе необязательно
                        p.StartInfo = s;
                        p.Start();
                        //p.WaitForExit();
                    }
                    else
                        Process.Start(UpdExe, arg);
                }
            }
                //Process.Start(ExePath + "\\" + strApName + ".exe", "a");
        }

        private void tbtnView_Click(object sender, EventArgs e)
        {
            locX = this.Location.X + this.Width;
            locY = this.Location.Y;
            //CodeCompleat();
            try
            {
                if (tbtnView.Checked)
                {
                    if((fView == null)||(fView.IsDisposed))
                        fView = new frmView();

                    fView.Location = new Point(locX, locY);
                    HTMLCompleat();
                    //fView.webBrowser1.DocumentText = rtxtHtml.Text;
                    //frmView frm = new frmView();
                    //frm.Owner = this; //Передаём вновь созданной форме её владельца.
                    fView.Show();
                }
                else
                {
                    //frmView frm = (frmView)this.Owner;
                    fView.Hide();
                }
                mnuMain_View__Preview.Checked = tbtnView.Checked;
            }
            catch(ObjectDisposedException ex)
            {
                var msg = MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbtnEdit_Click(object sender, EventArgs e)
        {
            tbtnEdit.Image = tbtnEdit.Checked ? Properties.Resources.unlock : Properties.Resources._lock;
            rtxtCode.Enabled = (rtxtCode.Enabled) ? false : true;
            rtxtCode.ReadOnly = !rtxtCode.Enabled;

            for(int i = 0; i < _tbtn.Length; i++)
            {
                _tbtn[i].Enabled = rtxtCode.Enabled;
            }
            tbtnLink_query.Enabled = rtxtCode.Enabled;
            tcmbColor.Enabled = rtxtCode.Enabled;
            tcmbFont.Enabled = rtxtCode.Enabled;
            tcmbSize.Enabled = rtxtCode.Enabled;
        }

        private void tbtnTextFormat_Click(object sender, EventArgs e) //Кнопки форматирования BBCode в режиме Редактирования кода
        {
            var tbtn = ((ToolStripButton)sender);
            string str1, select, name, sname;
            int posicion, sellen;
            str1 = "";
            //if (selText != "")
            //{
            //    rtxtCode.SelectedText = "";
            //}
            select = rtxtCode.SelectedText;
            sellen = rtxtCode.SelectionLength;
            posicion = rtxtCode.SelectionStart;

            if ((Convert.ToString(tbtn.Tag) == "url=") && (tbtn.Checked))
            {
                bool c = false;
                if (frmInput.Input("Ввод значения", "Введите URL-адрес:", "", out str1, c))
                {
                    string[] bbC = str1.Split(new Char[] { ':' });

                    if (bbC[0] == "http")
                        tbtn.Tag = tbtn.Tag + str1; //tbtn.Tag = tbtn.Tag + "=" + str1;
                    else
                        tbtn.Tag = tbtn.Tag + "=http:\\\\" + str1;
                }
                else
                    MessageBox.Show("Действие отменено.");
            }

            if ((Convert.ToString(tbtn.Tag) == "img") && (tbtn.Checked))
            {
                //var btn = (Button)sender;

                if ((_PicPicture[0] != "") && (_PicPicture[1] != ""))   //если есть данные авторизации
                {
                    dlgOpen.Filter = "Фотографии и изображения|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|Все файлы (*.*)|*.*";
                    dlgOpen.FileName = "";
                    dlgOpen.Multiselect = false;

                    if (dlgOpen.ShowDialog() == DialogResult.OK)
                    {
                        string imgelink = dlgOpen.FileName;
                        str1 = PicUploade(imgelink);
                        //rtxtCode.SelectedText = "[" + tbtn.Tag + "]" + PicUploade(imgelink);
                        //rtxtCode.SelectedText = "[" + tbtn.Tag + "]" + PicUploade(imgelink) + "[/" + tbtn.Tag + "]";
                    }
                    else
                    {
                        str1 = "";
                        //rtxtCode.SelectedText = "[" + tbtn.Tag + "]";
                    }
                }
                else //данных нет, открываем окно авторизации
                {

                }
                tbtn.Checked = false;
                rtxtCode.SelectedText = "[" + tbtn.Tag + "]" + str1;
                //rtxtCode.SelectedText = "[" + tbtn.Tag + "]" + str1 + "[/" + tbtn.Tag + "]";
            }
            //else
            //{
            //    rtxtCode.SelectedText = "[/" + tbtn.Tag + "]";
            //}

            if ((Convert.ToString(tbtn.Tag) != "*"))
            {
                if (tbtn.Checked)
                {
                    if (rtxtCode.SelectionLength > 0)
                    {
                        string[] str = Convert.ToString(tbtn.Tag).Split('=');
                        rtxtCode.SelectedText = "[" + tbtn.Tag + "]" + select + "[/" + str[0] + "]";

                        if (str.Length > 1)
                            tbtn.Tag = str[0] + "=";
                        else
                            tbtn.Tag = str[0];

                        tbtn.Checked = false;
                    }
                    else
                    {
                        if (ind_bbc < 0)
                            ind_bbc = 0;
                        try
                        {
                            Array.Resize(ref bbcode, (ind_bbc + 1));
                        }
                        catch
                        {
                        }
                        bbcode[ind_bbc] = tbtn.Name;
                        if (ind_bbc > 0)
                        {
                            name = bbcode[ind_bbc - 1];

                            for (int i = 0; i < _tbtn.Length; i++)
                            {
                                sname = _tbtn[i].Name;
                                if (name == sname)
                                    _tbtn[i].Enabled = false;
                            }
                        }

                        ind_bbc++;
                        if (ind_bbc > 0)
                        {
                            tbtnClosetags.Enabled = true;
                        }
                        else
                            tbtnClosetags.Enabled = false;

                        rtxtCode.SelectedText = "[" + tbtn.Tag + "]";
                    }
                }
                else
                {
                    string[] str = Convert.ToString(tbtn.Tag).Split('=');
                    tbtn.Tag = str[0];

                    rtxtCode.SelectedText = "[/" + tbtn.Tag + "]";
                    if (ind_bbc > 1)
                    {
                        name = bbcode[ind_bbc - 2];
                        for (int i = 0; i < _tbtn.Length; i++)
                        {
                            sname = _tbtn[i].Name;
                            if (name == sname)
                                _tbtn[i].Enabled = true;
                        }
                    }
                    ind_bbc = ind_bbc - 1;
                    if (ind_bbc > 0)
                    {
                        Array.Resize(ref bbcode, (ind_bbc));
                        tbtnClosetags.Enabled = true;
                    }
                    else
                    {
                        tbtnClosetags.Enabled = false;
                    }

                    switch(tbtn.Tag.ToString())
                    {
                        case "color":
                            tcmbColor.SelectedIndex = 0;
                            break;
                        case "size":
                            tcmbSize.SelectedIndex = 1;
                            break;
                        case "font":
                            tcmbFont.SelectedIndex = 0;
                            break;
                    }
                }
            }
            else
            {
                rtxtCode.SelectedText = "[" + tbtn.Tag + "]" + select;
            }
        }

        private void tbtnClosetags_Click(object sender, EventArgs e)
        {

            for (int j = bbcode.Length - 1; j >= 0; j--)
            {
                string name = bbcode[j];
                for (int i = 0; i < _tbtn.Length; i++)
                {
                    string sname = _tbtn[i].Name;
                    if (name == sname)
                        _tbtn[i].PerformClick();
                }
            }

        }

        private void tcmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] sel;
            string selTag = "";
            selText = rtxtCode.SelectedText;

            //if ((selText == null) || (selText == ""))
            //{
            //    selText = rtxtCode.SelectedText;
            //}

            if ((selText != null) && (selText != ""))
            {
                selTag = selText.Trim('[', ']');
                sel = selTag.Split('=');
                sel[0] = sel[0].Replace("[", "");

                if (Convert.ToString(tcmbColor.Tag) == sel[0])
                {
                    rtxtCode.SelectedText = rtxtCode.SelectedText.Replace(rtxtCode.SelectedText, ("[" + sel[0] + "=" + color[tcmbColor.SelectedIndex] + "]"));
                    selText = "";
                }
                else
                {
                    try
                    {
                        //rtxtCode.SelectionLength = 0;
                        tbtnColor.Tag = tcmbColor.Tag + "=" + (color[tcmbColor.SelectedIndex]);
                        tbtnColor.PerformClick();
                        tbtnColor.Tag = tcmbColor.Tag;
                        tcmbColor.SelectedIndex = 0;
                    }
                    catch(IndexOutOfRangeException)
                    {

                    }
                }
            }
            else
            {
                if (tcmbColor.SelectedIndex > 0)
                {
                    tbtnColor.Tag = tcmbColor.Tag + "=" + (color[tcmbColor.SelectedIndex]);
                    tbtnColor.PerformClick();
                    tbtnColor.Tag = tcmbColor.Tag;
                }
            }
            rtxtCode.Focus();
        }

        private void tcmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] sel;
            string selTag = "";
            selText = rtxtCode.SelectedText;

            if (selText != "")
            {
                selTag = selText.Trim('[', ']');
                sel = selTag.Split('=');
                sel[0] = sel[0].Replace("[", "");

                if (Convert.ToString(tcmbSize.Tag) == sel[0])
                {
                    rtxtCode.SelectedText = rtxtCode.SelectedText.Replace(rtxtCode.SelectedText, ("[" + sel[0] + "=" + tcmbSize.Text + "]"));
                    selText = "";
                }
                else
                {
                    //rtxtCode.SelectionLength = 0;
                    tbtnSize.Tag = tcmbSize.Tag + "=" + (size[tcmbSize.SelectedIndex]);
                    tbtnSize.PerformClick();
                    tbtnSize.Tag = tcmbSize.Tag;
                    tcmbSize.SelectedIndex = 1;
                }
            }
            else
            {
                if (tcmbColor.SelectedIndex != 1)
                {
                    tbtnSize.Tag = tcmbSize.Tag + "=" + (size[tcmbSize.SelectedIndex]);
                    tbtnSize.PerformClick();
                    tbtnSize.Tag = tcmbSize.Tag;
                }
            }
            rtxtCode.Focus();
        }

        private void tcmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] sel;
            string selTag = "";
            selText = rtxtCode.SelectedText;

            if (selText != "")
            {
                selTag = selText.Trim('[', ']');
                sel = selTag.Split('=');
                sel[0] = sel[0].Replace("[", "");

                if (Convert.ToString(tcmbFont.Tag) == sel[0])
                {
                    rtxtCode.SelectedText = rtxtCode.SelectedText.Replace(rtxtCode.SelectedText, ("[" + sel[0] + "=" + tcmbFont.Text + "]"));
                    selText = "";
                }
                else
                {
                    //rtxtCode.SelectionLength = 0;
                    tbtnFont.Tag = tcmbFont.Tag + "=" + (tcmbFont.Text);
                    tbtnFont.PerformClick();
                    tbtnFont.Tag = tcmbFont.Tag;
                    tcmbFont.SelectedIndex = 0;
                }
            }
            else
            {
                if (tcmbFont.SelectedIndex > 0)
                {
                    tbtnFont.Tag = tcmbFont.Tag + "=" + (tcmbFont.Text);
                    tbtnFont.PerformClick();
                    tbtnFont.Tag = tcmbFont.Tag;
                }
            }
            rtxtCode.Focus();
        }

        private void tbtnFont_CheckedChanged(object sender, EventArgs e)
        {
            if (selText != "")
            {
                selText = "";
                tbtnFont.Checked = false;
            }
            else
            {
                tcmbFont.Enabled = !tbtnFont.Checked;
            }
            //if (!tbtnFont.Checked)
            //{
            //    tcmbFont.Text = tcmbFont.Items[0].ToString();
            //}
        }

        private void tbtnColor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbtnFileBBCode_Click(object sender, EventArgs e) //Открыть, Сохранить, Сохранить как в режиме Редактирования кода
        {
            ToolStripButton tbtn = ((ToolStripButton)sender);
            //string file = "";

            if (tbtn.Name == "tbtnOpen") //Открыть файл в редакторе
            {
                dlgOpen.InitialDirectory = PathSave;
                dlgOpen.Filter = "Текст в формате RTF|*.rtf|Обычный текст TXT|*.txt|Все файлы|*.*";
                //dlgOpen.Filter = "Текст в формате RTF|*.rtf|Обычный текст|*.txt";
                dlgOpen.Title = "Открыть текстовый файл";
                dlgOpen.FileName = "";
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    rtf = dlgOpen.FileName;
                    string[] _rtf = rtf.Split('.');

                    if (File.Exists(rtf))
                    {
                        if (_rtf[_rtf.Length - 1] == "rtf")
                            rtxtCode.LoadFile(rtf);
                        else
                            rtxtCode.Text = File.ReadAllText(rtf, Encoding.UTF8); ;
                    }

                    rtxtModif = false;
                }
            }
            else if (tbtn.Name == "tbtnSave") //Сохранить в файл
            {
                string FName = "BBCode.rtf";

                if ((!Directory.Exists(PathSave)) && (PathSave != ""))
                {
                    var msg = MessageBox.Show(
                                "Путь, указанный в настройках не существует. Восстанговить этот путь?",
                                "Неверный путь",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);
                    if (msg == DialogResult.Yes)
                    {
                        Directory.CreateDirectory(PathSave);
                    }
                    else
                    {
                        PathSave = "";
                        SaveDefault = false;
                    }
                }

                if ((SaveDefault) && (Directory.Exists(PathSave)))
                {
                    rtf = PathSave + "\\" + FName;
                    rtxtModif = false;
                    rtxtCode.SaveFile(rtf);
                }
                else
                {
                    dlgSave.InitialDirectory = PathSave;
                    dlgSave.Title = "Сохранить текст в файл";
                    dlgSave.Filter = "Текст в формате RTF|*.rtf|Обычный текст TXT|*.txt";
                    if ((rtf == "") || (rtf == "openFileDialog1"))
                    {
                        dlgSave.FileName = FName;

                        if (dlgSave.ShowDialog() == DialogResult.OK)
                        {
                            rtf = dlgSave.FileName;
                            string[] str = rtf.Split('.');
                            if (str[str.Length - 1] == "rtf")
                            {
                                //if (rtf.IndexOf(str) < (rtf.Length - 5))
                                //    rtf = rtf + str;
                                rtxtCode.SaveFile(rtf);
                            }
                            else
                            {
                                File.WriteAllText(rtf, rtxtCode.Text, Encoding.UTF8);
                            }
                            rtxtModif = false;
                        }
                    }
                    else
                    {
                        rtxtModif = false;
                        dlgSave.FileName = rtf;
                        rtxtCode.SaveFile(rtf);
                        MessageBox.Show("Сохранение прошло успешно!", "Сохранение", MessageBoxButtons.OK);
                    }
                }

            }
            else if (tbtn.Name == "tbtnSaveAs") //Сохранить открытый файл под другим именем
            {
                rtf = "";
                tbtnSave.PerformClick();
            }
        }

        private void tbtnSize_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbtnCopy_Click(object sender, EventArgs e)
        {
            if (rtxtCode.Text != "")
                Clipboard.SetText(rtxtCode.Text);
        }

        private void tcmbFont_EnabledChanged(object sender, EventArgs e)
        {
            if (tcmbFont.Enabled)
            {
                tcmbFont.Text = tcmbFont.Items[0].ToString();
            }
        }

        private void tcmbColor_EnabledChanged(object sender, EventArgs e)
        {
            if (tcmbColor.Enabled)
            {
                tcmbColor.Text = tcmbColor.Items[0].ToString();
            }
        }

        private void tcmbSize_EnabledChanged(object sender, EventArgs e)
        {
            var tcmb = (ToolStripComboBox)sender;

            if (tcmbSize.Enabled)
            {
                tcmbSize.Text = tcmbSize.Items[1].ToString();
            }
        }

        private void btnTime_Click(object sender, EventArgs e)
        {
            iTimes = txtTimes.Text;
            //txtTimes.Text = "";
            frmTimes frm = new frmTimes();
            frm.Owner = this; //Передаём вновь созданной форме её владельца.
            frm.Show();
        }

        private void btnGenre_Click(object sender, EventArgs e)
        {
            frmGenre frm = new frmGenre();
            frm.Owner = this; //Передаём вновь созданной форме её владельца.
            frm.Show();
        }

        private void mtxtTime_Leave(object sender, EventArgs e)
        {
            //Times = mtxtTime.Text;
        }

        private void rtxtCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var rtxt = (RichTextBox)sender;
            int posit = rtxt.SelectionStart;
            int sel = rtxt.SelectionLength;

            //if (sel < 1)
            //    sel = 1;

            if (e.KeyCode == Keys.Delete)
            {
                if ((posit > 0) && (posit < rtxt.Text.Length))
                {
                    rtxt.Text = rtxt.Text.Remove(posit, sel);
                    rtxt.SelectionStart = posit;
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

        private void rtxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string ss = rtxtCode.SelectedText;
            int k = rtxtCode.SelectionStart;

            if ((rtxtCode.SelectionStart > 0) && (code == "") && (rtxtCode.Text[rtxtCode.SelectionStart - 1] == '['))
            {
                pos = rtxtCode.SelectionStart;
                code = "[" + e.KeyChar.ToString();
            }
            else if ((rtxtCode.SelectionStart - pos == 1) && (code != "") && (rtxtCode.Text.Length - rtxtCode.SelectionStart > 0))
            {
                if (rtxtCode.Text[rtxtCode.SelectionStart + 1] == ']')
                {
                    pos = rtxtCode.SelectionStart;
                    code = "";
                }
            }
            else if (pos > 1)
            {
                pos = 0;
                rtxtCode.SelectionColor = Color.Black;
                code = "";
            }

            if (e.KeyChar == '[')
            {
                rtxtCode.SelectionColor = Color.Blue;
                code = e.KeyChar.ToString();
            }
            else if ((code != "") && (e.KeyChar == '='))
            {
                code = code + e.KeyChar.ToString();
                rtxtCode.SelectionColor = Color.Brown;
            }
            else if ((code != "") && (e.KeyChar != ']'))
            {
                code = code + e.KeyChar.ToString();
                rtxtCode.SelectionColor = Color.BlueViolet;
            }
            else if ((code != "") && (e.KeyChar == ']'))
            {
                code = code + e.KeyChar.ToString();
                //rtxtCode.Select(rtxtCode.SelectionStart + 1 - code.Length, code.Length);
                //rtxtCode.SelectionStart = rtxtCode.SelectionStart + 1 - code.Length;
                //rtxtCode.SelectionLength = code.Length + 1;
                rtxtCode.SelectionColor = Color.Blue;
                code = "";
            }
            else
            {
                rtxtCode.SelectionColor = Color.Black;
            }
        }

        private void rtxtCode_TextChanged(object sender, EventArgs e)
        {
            if (tbtnView.Checked)
            {
                HTMLCompleat();
            }

            //bool tg = false;
            //int posit = rtxtCode.SelectionStart;
            //Color clr = Color.Black;

            //rtxtCode.SelectionColor = clr;
            //for (int i = rtxtCode.Text.Length - 1; i > 0; i--)
            //{
            //    char chr = rtxtCode.Text[i];
            //    rtxtCode.SelectionColor = clr;
            //    if (chr == ']')
            //    {
            //        clr = Color.BlueViolet;
            //    }
            //    else if(chr == '[')
            //    {
            //        clr = Color.Blue;
            //    }
            //}
        }

        private void rtxtCode_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Times++;
            if (oper == "TorrentDown")
            {
                if ((Kinorun.TorrentDown(_KinoRun[0], _KinoRun[1], link1)) || (Times == 3))
                {
                    timer1.Enabled = false;
                    link1 = "";
                }
            }

            if (oper == "Auth")
            {
                if ((Kinorun.Auth(_KinoRun[0], _KinoRun[1])))
                {
                    timer1.Enabled = false;
                    mnuKinorun_Upload.PerformClick();
                }
            }

            oper = "";
        }

        private void rtxtHtml_TextChanged(object sender, EventArgs e)
        {
            Html = rtxtHtml.Text;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (webBrowser1.DocumentText.Length < 50)
            //    webBrowser1.DocumentText = rtxtHtml.Text;
        }

        private void cmb_Leave(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
                //int k = cmb.Items.IndexOf(cmb.Text);
            if ((cmb.Items.IndexOf(cmb.Text) < 0) || (cmb.Text == ""))
                txtBox.Text = cmb.Text;

            txtBox.AutoCompleteCustomSource.Clear();

            var values = cmb.Items.Cast<string>().ToArray();
            txtBox.AutoCompleteCustomSource.AddRange(values);

            int ind = -1;
            string str = txtBox.Text.Trim();
            ind = txtBox.AutoCompleteCustomSource.IndexOf(str);

            if (str != "")
            {
                if (ind < 0)
                {
                    txtBox.AutoCompleteCustomSource.Add(cmb.Text);
                }
                //else
                //{
                //    textBox.AutoCompleteCustomSource.Remove(textBox.Text);
                //    textBox.AutoCompleteCustomSource.Add(textBox.Text);
                //}
                //if (ind > -1)
                //{
                //    txtBox.AutoCompleteCustomSource.Remove(cmb.Text);
                //}
                string[] list = new string[1];
                list[0] = cmb.Text;
                for (int i = 0; i < txtBox.AutoCompleteCustomSource.Count; i++)
                {
                    int k = Array.IndexOf(list, txtBox.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtBox.AutoCompleteCustomSource[i] != ""))
                    {
                        Array.Resize(ref list, list.Length + 1);
                        list[list.Length - 1] = txtBox.AutoCompleteCustomSource[i];
                    }
                }
                //Array.Resize(ref list, list.Length + 1);
                //list[list.Length - 1] = cmb.Text;
                txtBox.Text = cmb.Text;
                txtBox.AutoCompleteCustomSource.Clear();
                txtBox.AutoCompleteCustomSource.AddRange(list);
                writeField();
            }

            tabForm.Controls.Remove(cmb);
            cmb.Dispose();
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            txtBox.Text = cmb.Text;
        }

        private void cmb_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    int k = cmb.Items.IndexOf(cmb.Text);
            //    if (cmb.Text != "")
            //    {
            //        cmb.Items.Remove(cmb.SelectedText);
            //        try
            //        {
            //            cmb.SelectedIndex = k;
            //        }
            //        catch (ArgumentOutOfRangeException)
            //        {
            //            cmb.SelectedIndex = k - 1;
            //        }
            //    }
            //}
        }

        private void cmb_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_TextUpdate(object sender, EventArgs e)
        {
            //txtBox.Text = e.ToString();
        }

        private void txtBox_DoubleClick(object sender, EventArgs e)
        {
            int k = tabForm.Controls.IndexOf(cmb);
            int leingh = 0;

            if (k < 0)
            {
                txtBox = (TextBox)sender;
                cmb = new ComboBox();
                cmb.Name = "cmb";
                cmb.Width = txtBox.Width;
                cmb.Location = new Point(txtBox.Location.X, txtBox.Location.Y);

                string str = txtBox.Text.Trim();
                k = txtBox.AutoCompleteCustomSource.IndexOf(str);
                if (k < 0)
                {
                    txtBox.AutoCompleteCustomSource.Add(txtBox.Text);
                }

                string[] list = new string[0];
                //for (int i = txtBox.AutoCompleteCustomSource.Count -1; i > -1 ; i--)
                for (int i = 0; i < txtBox.AutoCompleteCustomSource.Count; i++)
                {
                    k = Array.IndexOf(list, txtBox.AutoCompleteCustomSource[i]);
                    if ((k < 0) && (txtBox.AutoCompleteCustomSource[i] != ""))
                    {
                        Array.Resize(ref list, list.Length + 1);
                        list[list.Length - 1] = txtBox.AutoCompleteCustomSource[i];

                        int leingh1 = txtBox.AutoCompleteCustomSource[i].Length;

                        if (leingh < leingh1)
                            leingh = leingh1;
                    }
                }

                cmb.Items.AddRange(list);
                leingh = leingh * 5;
                if (leingh > 500)
                    leingh = 500;
                else if (leingh < 80)
                    leingh = 80;

                if (leingh > cmb.Width)
                    cmb.DropDownWidth = leingh;
                //else
                //    cmb.DropDownWidth = (leingh*2) + cmb.Width;
                cmb.Sorted = false;
                cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb.AutoCompleteSource = AutoCompleteSource.ListItems;

                cmb.SelectedIndex = cmb.Items.IndexOf(txtBox.Text);
                cmb.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);
                cmb.Leave += new EventHandler(cmb_Leave);
                cmb.TextUpdate += new EventHandler(cmb_TextUpdate);
                cmb.TextChanged += new EventHandler(cmb_TextChanged);
                cmb.KeyDown += new KeyEventHandler(cmb_KeyDown);

                tabForm.Controls.Add(cmb);
                cmb.BringToFront();
            }

            cmb.Focus();
        }

        private void txtName_DoubleClick(object sender, EventArgs e)
        {
            mnuMain_Copy_Name.PerformClick();
        }

        private void txtFormFields_TextChanged(object sender, EventArgs e) //При изменении значения поля
        {
            try
            {
                var obj = ((Control)sender);
                if (obj is ComboBox)
                {
                    if (obj.Name == "cmbQuality")
                        VQuality = cmbQuality.Text;
                    if (obj.Name == "cmbFormat")
                        VFormat = cmbFormat.Text;
                    if (obj.Name == "cmbVCodac")
                        VCodac = cmbVCodac.Text;
                    if (obj.Name == "cmbACodac")
                        ACodac = cmbACodac.Text;
                }
            }
            catch (Exception)
            { }
            finally
            {
                var obj = ((Control)sender);
                string str = obj.Text.Trim();
                if ((obj.Name != "cmbCategory") && (str != ""))
                {
                    modif = true;
                    //string[] lcontr = contr.Split(';');
                    //for (int i = 0; i < lcontr.Length; i++)
                    //{
                    //    if (lcontr[i] != "")
                    //    {
                    //        modif = false;
                    //    }
                    //    else
                    //    {
                    //        modif = true;
                    //        break;
                    //    }
                    //}
                }
                else
                {
                    modif = false;
                }
                Colored();
            }
        }

        private void txtFormFields_Leave(object sender, EventArgs e) //Когда поле теряет фокус
        {
            int ind = -1;
            try
            {
                var textBox = ((TextBox)sender);
                if ("txtGenre" == textBox.Name)
                    iGenre = AdditionFunc.TagBuild(txtGenre.Text);

                //for (int i = 0; i < textBox.AutoCompleteCustomSource.Count; i++)
                //{
                //    if ((str != "") | (str != "0"))
                //    {
                //        if (textBox.Text == textBox.AutoCompleteCustomSource[i])
                //        {
                //            ind = i + 1;
                //        }
                //    }
                //}
                string str = textBox.Text.Trim();
                ind = textBox.AutoCompleteCustomSource.IndexOf(str);

                if (str != "")
                {
                    //if (ind < 0)
                    //{
                    //    textBox.AutoCompleteCustomSource.Add(textBox.Text);
                    //}
                    //else
                    //{
                    //    textBox.AutoCompleteCustomSource.Remove(textBox.Text);
                    //    textBox.AutoCompleteCustomSource.Add(textBox.Text);
                    //}
                    if (ind > -1)
                    {
                        textBox.AutoCompleteCustomSource.Remove(textBox.Text);
                    }
                    string[] list = new string[1];
                    list[0] = textBox.Text;
                    for(int i = 0; i < textBox.AutoCompleteCustomSource.Count; i++)
                    {
                        int k = Array.IndexOf(list, textBox.AutoCompleteCustomSource[i]);
                        if(k < 0)
                        {
                            Array.Resize(ref list, list.Length + 1);
                            list[list.Length - 1] = textBox.AutoCompleteCustomSource[i];
                        }
                    }
                    textBox.AutoCompleteCustomSource.Clear();
                    textBox.AutoCompleteCustomSource.AddRange(list);
                    writeField();
                }

                VQuality = cmbQuality.Text;
                VFormat = cmbFormat.Text;
                VCodac = cmbVCodac.Text;
                ACodac = cmbACodac.Text;
                if ((textBox.Name == "txtVideo") || (textBox.Name == "txtAudio"))
                {
                    textBox.Text = TechParser(textBox.Text);
                    cmbQuality.Text = VQuality;
                    cmbFormat.Text = VFormat;
                    cmbVCodac.Text = VCodac;
                    cmbACodac.Text = ACodac;
                }
                //HTMLCompleat();
                //if (mnuMain_View__Preview.Checked)
                //{
                //}
            }
            catch (InvalidCastException)
            {
                int time = 0;
                var obj = ((Control)sender);

                if (obj is MaskedTextBox)
                {
                    string[] mtb = mtxtTime.Text.Split(':');
                    int b = Convert.ToInt32(mtb[1]);
                    int c = Convert.ToInt32(mtb[2]);

                    if(Convert.ToInt32(mtb[1]) > 59)
                        time =  time + 1;

                    if(Convert.ToInt32(mtb[2]) > 59)
                        time = time + 2;

                    if(time > 0)
                    {
                        string s = time == 1 ? "минуты" : "секунды";
                        s = time == 3 ? "минуты и секунды" : s;
                        var msg = MessageBox.Show("Вы не верно указали время продолжительности!\nОбратите внимание, что " + s + " не могут быть больше 59.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        mtxtTime.Focus();
                    }
                }
            }
            finally
            {
                CodeCompleat();
                HTMLCompleat();
            }
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            dlgOpen.Filter = "Фотографии и изображения|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|Все файлы (*.*)|*.*";
            dlgOpen.FileName = "";

            if (btn.Name == "btnScreenshot")
            {
                try
                {
                    if (imageFiles[0].Length == 0)
                    {
                        dlgOpen.Multiselect = true;

                        if (dlgOpen.ShowDialog() == DialogResult.OK)
                        {
                            imageFiles = dlgOpen.FileNames;

                            if ((_PicPicture[0] != "") && (_PicPicture[1] != ""))   //если есть данные авторизации
                            {
                                PicUploade();
                            }
                            else //данных нет, открываем окно авторизации
                            {
                                frmAuthorization frm = new frmAuthorization("PicPicture.com");
                                frm.Owner = this; //Передаём вновь созданной форме её владельца.
                                frm.Show();
                            }
                        }
                    }
                    else
                    {
                        PicUploade();
                    }
                }
                catch (NullReferenceException)
                {
                    dlgOpen.Multiselect = true;

                    if (dlgOpen.ShowDialog() == DialogResult.OK)
                    {
                        imageFiles = dlgOpen.FileNames;

                        if ((_PicPicture[0] != "") && (_PicPicture[1] != ""))   //если есть данные авторизации
                        {
                            PicUploade();
                        }
                        else //данных нет, открываем окно авторизации
                        {
                            frmAuthorization frm = new frmAuthorization("PicPicture.com");
                            frm.Owner = this; //Передаём вновь созданной форме её владельца.
                            frm.Show();
                        }
                    }
                }
            }
            else
            {
                dlgOpen.Multiselect = false;
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                    txtPoster.Text = dlgOpen.FileName;


                //try
                //{
                //    if (imageFile.Length == 0)
                //    {
                //        dlgOpen.Multiselect = false;

                //        if (dlgOpen.ShowDialog() == DialogResult.OK)
                //        {
                //            imageFile = dlgOpen.FileName;

                //            if ((_PicPicture[0] != "") && (_PicPicture[1] != ""))   //если есть данные авторизации
                //            {
                //                authPic = Picpicture.AuthPic(_PicPicture[0], _PicPicture[1]); //проверка авторизации
                //                PicUploade();
                //            }
                //            else //данных нет, открываем окно авторизации
                //            {
                //                frmAuthorization frm = new frmAuthorization("PicPicture.com");
                //                frm.Owner = this; //Передаём вновь созданной форме её владельца.
                //                frm.Show();
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if ((_PicPicture[0] != "") && (_PicPicture[1] != ""))   //если есть данные авторизации
                //        {
                //            authPic = Picpicture.AuthPic(_PicPicture[0], _PicPicture[1]); //проверка авторизации
                //            PicUploade();
                //        }
                //        else //данных нет, открываем окно авторизации
                //        {
                //            frmAuthorization frm = new frmAuthorization("PicPicture.com");
                //            frm.Owner = this; //Передаём вновь созданной форме её владельца.
                //            frm.Show();
                //        }
                //    }
                //}
                //catch (NullReferenceException)
                //{
                //    dlgOpen.Multiselect = false;

                //    if (dlgOpen.ShowDialog() == DialogResult.OK)
                //    {
                //        imageFile = dlgOpen.FileName;

                //        if ((_PicPicture[0] != "") && (_PicPicture[1] != ""))   //если есть данные авторизации
                //        {
                //            authPic = Picpicture.AuthPic(_PicPicture[0], _PicPicture[1]); //проверка авторизации
                //            PicUploade();
                //        }
                //        else //данных нет, открываем окно авторизации
                //        {
                //            frmAuthorization frm = new frmAuthorization("PicPicture.com");
                //            frm.Owner = this; //Передаём вновь созданной форме её владельца.
                //            frm.Show();
                //        }
                //    }
                //}
            }

        }

        private void txtScrinshot_Leave(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            if (obj.Name == "txtPoster")
            {
                string[] str = obj.Text.Split('/');
                try
                {
                    if (str[2] != "picpicture.com")
                    {
                        imageFile = obj.Text.Trim();
                        PicUploade();
                    }
                    
                }
                catch (IndexOutOfRangeException)
                {

                }
            }

            if (obj.Name == "txtScrinshot")
            {
                string substr = "";
                try
                {
                    substr = txtScrinshot.Text.Substring("[url=", "image/");
                }
                catch (ArgumentNullException)
                {
                }

                if ((txtScrinshot.Text != "") & (substr != "http://picpicture.com/"))
                {
                    txtScrinshot.Text = txtScrinshot.Text.Replace("\r", "");
                    txtScrinshot.Text = txtScrinshot.Text.Trim('\n');

                    string[] str = obj.Text.Split('\n');
                    Array.Resize(ref imageFiles, str.Length);
                    Array.Resize(ref imageFiles_text, str.Length);
                    for (int i = 0; i < str.Length; i++)
                    {
                        string[] line = str[i].Split('/');

                        if ((line[0] == "http:") || (line[0] == "https:"))
                        {
                            imageFiles[i] = str[i].Trim('\r');
                            imageFiles_text[i] = "";
                        }
                        else
                        {
                            imageFiles[i] = "";
                            imageFiles_text[i] = str[i].Trim('\r');
                        }
                    }

                    txtScrinshot.Text = "";
                    PicUploade();
                }
            }
        }

        private void txtAddition_Leave(object sender, EventArgs e)
        {
            props.Fields.Addition = txtAddition.Text;
            props.WriteXml();
        }

        private void tbcBuilder_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(tbcBuilder.SelectedTab == tbcBuilder.TabPages["tabForm"])
            {
                //mnuMain_View_Editor.Enabled = true;
                //mnuMain_View_Form.Enabled = false;
            }
            else if(tbcBuilder.SelectedTab == tbcBuilder.TabPages["tabCode"])
            {
                //if (PreComplete()) //Если нажал Да
                //{
                //    mnuMain_View_Editor.Enabled = false;
                //    mnuMain_View_Form.Enabled = true;
                //}
                //else
                //    mnuMain_View__Preview.PerformClick();
                CodeCompleat();
                writeField();
            }
            else
            {
                CodeCompleat();
                HTMLCompleat();
            }

            Preview();
        }

        private void mnuContext_Copy_Click(object sender, EventArgs e)
        {
            if (txtName_O.Focused)
            {
                if (txtName_O.SelectedText.Length > 0)
                    Clipboard.SetText(txtName_O.SelectedText);
            }
            else if (txtDescriptor.Focused)
            {
                if (txtDescriptor.SelectedText.Length > 0)
                    Clipboard.SetText(txtDescriptor.SelectedText);
            }
            else if (rtxtCode.Focused)
            {
                if (rtxtCode.SelectedText.Length > 0)
                    Clipboard.SetText(rtxtCode.SelectedText);
            }
        }

        private void mnuContext_Past_Click(object sender, EventArgs e)
        {
            if (txtName_O.Focused)
            {
                txtName_O.SelectedText = Clipboard.GetText();
            }
            else if (txtDescriptor.Focused)
            {
                txtDescriptor.SelectedText = Clipboard.GetText();
            }
            else if (rtxtCode.Focused)
            {
                rtxtCode.SelectedText = Clipboard.GetText();
            }
        }

        private void mnuContext_Select_Click(object sender, EventArgs e)
        {
            if (txtName_O.Focused)
            {
                txtName_O.SelectAll();
            }
            else if (txtDescriptor.Focused)
            {
                txtDescriptor.SelectAll();
            }
            else if (rtxtCode.Focused)
            {
                rtxtCode.SelectAll();
            }
        }

        private void mnuContext_Cut_Click(object sender, EventArgs e)
        {
            if (txtName_O.Focused)
            {
                if (txtName_O.SelectedText.Length > 0)
                {
                    Clipboard.SetText(txtName_O.SelectedText);
                    txtName_O.SelectedText = "";
                }
            }
            else if (txtDescriptor.Focused)
            {
                if (txtDescriptor.SelectedText.Length > 0)
                {
                    Clipboard.SetText(txtDescriptor.SelectedText);
                    txtDescriptor.SelectedText = "";
                }
            }
            else if (rtxtCode.Focused)
            {
                if (rtxtCode.SelectedText.Length > 0)
                {
                    Clipboard.SetText(rtxtCode.SelectedText);
                    rtxtCode.SelectedText = "";
                }
            }
        }

        private void mnuContext_Del_Click(object sender, EventArgs e)
        {
            if (txtName_O.Focused)
            {
                if (txtName_O.SelectedText.Length > 0)
                    txtName_O.SelectedText = "";
                else
                    txtName_O.Text = "";
            }
            else if (txtDescriptor.Focused)
            {
                if (txtDescriptor.SelectedText.Length > 0)
                    txtDescriptor.SelectedText = "";
                else
                    txtDescriptor.Text = "";
            }
            else if (tbcBuilder.SelectedTab == tbcBuilder.TabPages["tabCode"])
            {
                if (rtxtCode.SelectedText.Length > 0)
                    rtxtCode.SelectedText = "";
                else
                    rtxtCode.Text = "";
            }
        }

        private void mnuContext_Translate_Click(object sender, EventArgs e)
        {
            string link = "https://translate.google.ru/#auto/ru/";
            mnuContext_Translate.Visible = true;

            if (txtName_O.Focused)
            {
                if (txtName_O.SelectedText.Length > 0)
                    link = link + HttpUtility.HtmlDecode(txtName_O.SelectedText);
                else
                    link = link + HttpUtility.HtmlDecode(txtName_O.Text);
            }
            else if (txtDescriptor.Focused)
            {
                if (txtDescriptor.SelectedText.Length > 0)
                    link = link + HttpUtility.HtmlDecode(txtDescriptor.SelectedText);
                else
                    link = link + HttpUtility.HtmlDecode(txtDescriptor.Text);
            }
            else if (rtxtCode.Focused)
            {
                mnuContext_Translate.Visible = false;
            }

            Process.Start(link);
        }

        private void mnuDescription_Copy_Click(object sender, EventArgs e)
        {
            if (txtDescriptor.SelectedText.Length > 0)
                Clipboard.SetText(txtDescriptor.SelectedText);
        }

        private void mnuDescription_Past_Click(object sender, EventArgs e)
        {
            txtDescriptor.SelectedText = Clipboard.GetText();
        }

        private void mnuDescription_Select_Click(object sender, EventArgs e)
        {
            txtDescriptor.SelectAll();
        }

        private void mnuDescription_Cut_Click(object sender, EventArgs e)
        {
            if (txtDescriptor.SelectedText.Length > 0)
            {
                Clipboard.SetText(txtDescriptor.SelectedText);
                txtDescriptor.SelectedText = "";
            }
        }

        private void mnuDescription_Dalete_Click(object sender, EventArgs e)
        {
            txtDescriptor.SelectedText = "";
        }

        private void mnuDescription_Translate_Click(object sender, EventArgs e)
        {
            string str = "https://translate.google.ru/#auto/ru/" + HttpUtility.HtmlDecode(txtDescriptor.Text);

            if(txtDescriptor.SelectedText != "")
                str = "https://translate.google.ru/#auto/ru/" + HttpUtility.HtmlDecode(txtDescriptor.SelectedText);

            Process.Start(str);
        }

        private void mnuMain_File_New_Click(object sender, EventArgs e)
        {
            if (modif)
            {
                var msg = MessageBox.Show("Закрыв раздачу, вы можете потерять несохраннные вами данные.\nХотите перед выходом сохранить вашу раздачу?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (msg != DialogResult.Cancel)
                {
                    if (msg == DialogResult.Yes)
                    {
                        mnuMain_File_Save.PerformClick();
                        modif = false;
                    }
                    else
                    {
                        modif = false;
                    }
                    writeField();
                    FieldsClear();
                }
            }
            else
            {
                writeField();
                FieldsClear();
                modif = false;
            }
            //rtxtHtml.Text = "";
            CodeCompleat();
            HTMLCompleat();

            webBrowser1.DocumentText = rtxtHtml.Text;
        }

        private void mnuMain_File_Open_Click(object sender, EventArgs e)
        {
            if (modif)
            {
                var msg = MessageBox.Show("Закрыв раздачу, вы можете потерять несохраннные вами данные.\nХотите перед выходом сохранить вашу раздачу?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (msg != DialogResult.Cancel)
                {
                    if (msg == DialogResult.Yes)
                    {
                        mnuMain_File_Save.PerformClick();

                        modif = false;
                    }
                    else
                    {
                        modif = false;
                    }

                    writeField();
                    FieldsClear();
                }
            }
            else
            {
                dlgOpen.InitialDirectory = PathSave;
                dlgOpen.Filter = "Описание раздачи (*.knr)|*.knr";
                dlgOpen.FileName = "";
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    distribFile = dlgOpen.FileName;
                    if (File.Exists(distribFile))
                    {
                        distrib = new Distrib(distribFile);
                        readSave();
                        LastFiles(distribFile);
                    }
                    modif = false;
                }
            }
        }

        private void mnuMain_File_Last_Click(object sender, EventArgs e)
        {
            var btn = (ToolStripMenuItem)sender;

            distribFile = Convert.ToString(btn.Tag);

            if (modif)
            {
                var msg = MessageBox.Show("Закрыв раздачу, вы можете потерять несохраннные вами данные.\nХотите перед выходом сохранить вашу раздачу?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (msg != DialogResult.Cancel)
                {
                    if (msg == DialogResult.Yes)
                    {
                        mnuMain_File_Save.PerformClick();
                        modif = false;
                    }
                    else
                    {
                        modif = false;
                    }
                    writeField();
                    FieldsClear();
                    btn.PerformClick();
                }
            }
            else
            {
                    if (File.Exists(distribFile))
                    {
                        distrib = new Distrib(distribFile);
                        readSave();
                    }
                    modif = false;
            }
        }

        private void mnuMain_File_Save_Click(object sender, EventArgs e)
        {
            string FName = txtName_R.Text + " (" + txtName_O.Text + ").knr";

            if ((!Directory.Exists(PathSave)) && (PathSave != ""))
            {
                var msg = MessageBox.Show(
                            "Путь, указанный в настройках не существует. Восстанговить этот путь?",
                            "Неверный путь",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
                if(msg == DialogResult.Yes)
                {
                    Directory.CreateDirectory(PathSave);
                }
                else
                {
                    PathSave = "";
                    SaveDefault = false;
                }
            }

            if ((SaveDefault) && (Directory.Exists(PathSave)))
            {
                        distribFile = PathSave + "\\" + FName;
                        modif = false;
                        distrib = new Distrib(distribFile);
                        writeSave();
            }
            else
            {
                dlgSave.InitialDirectory = PathSave;
                if ((distribFile == "") || (distribFile == "openFileDialog1"))
                {
                    dlgSave.FileName = FName;

                    if (dlgSave.ShowDialog() == DialogResult.OK)
                    {
                        distribFile = dlgSave.FileName;
                        string str = ".knr";
                        if (distribFile.IndexOf(str) < (distribFile.Length - 5))
                            distribFile = distribFile + str;

                        modif = false;
                        distrib = new Distrib(distribFile);
                        writeSave();
                    }
                }
                else
                {
                    modif = false;
                    dlgSave.FileName = distribFile;
                    distrib = new Distrib(distribFile);
                    writeSave();
                    MessageBox.Show("Сохранение прошло успешно!", "Сохранение", MessageBoxButtons.OK);
                }
            }

            LastFiles(distribFile);
        }

        private void mnuMain_File_SaveAs_Click(object sender, EventArgs e)
        {
            distribFile = "";
            mnuMain_File_Save.PerformClick();
        }

        private void mnuMain_File_Close_Click(object sender, EventArgs e)
        {
            if (modif)
            {
                var msg = MessageBox.Show("Закрыв программу, вы можете потерять несохраннные вами данные.\nХотите перед выходом сохранить вашу раздачу?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    mnuMain_File_Save.PerformClick();
                    modif = false;
                    this.Close();
                }
                else if (msg == DialogResult.No)
                {
                    modif = false;
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void mnuMain_View_Overall_Click(object sender, EventArgs e)
        {
            if (mnuMain_View_Overall.Checked)
            {
                TopMost = false;
                mnuMain_View_Overall.Checked = false;
            }
            else
            {
                TopMost = true;
                mnuMain_View_Overall.Checked = true;
            }

        }

        private void mnuMain_View_Form_Click(object sender, EventArgs e)
        {
            tbcBuilder.SelectedTab = tbcBuilder.TabPages["tabForm"];
        }

        private void mnuMain_View_Editor_Click(object sender, EventArgs e)
        {
            tbcBuilder.SelectedTab = tbcBuilder.TabPages["tabCode"];
        }

        private void mnuMain_View__Preview_Click(object sender, EventArgs e)
        {
            //tbcBuilder.SelectedTab = tbcBuilder.TabPages["tabPreview"];
            //Preview();
            tbtnView.PerformClick();
        }

        private void mnuMain_View_Themes_Light_Click(object sender, EventArgs e)
        {
            var mnu = ((ToolStripMenuItem)sender);

            if (mnu.Text == "Светлая")
                Theme = 0;
            if (mnu.Text == "Нейтральная")
                Theme = 1;
            if (mnu.Text == "Тёмная")
                Theme = 2;

            switch (Theme)
            {
                case 0:
                    theme = Color.GhostWhite;
                    mnuMain_View_Themes_Light.Checked = true;
                    mnuMain_View_Themes_Neutral.Checked = false;
                    mnuMain_View_Themes_Dark.Checked = false;
                    break;
                case 1:
                    theme = Color.Gainsboro;
                    mnuMain_View_Themes_Light.Checked = false;
                    mnuMain_View_Themes_Neutral.Checked = true;
                    mnuMain_View_Themes_Dark.Checked = false;
                    break;
                case 2:
                    theme = Color.DarkGray;
                    mnuMain_View_Themes_Light.Checked = false;
                    mnuMain_View_Themes_Neutral.Checked = false;
                    mnuMain_View_Themes_Dark.Checked = true;
                    break;
            }
            if (Theme < 2)
                chkGold.ForeColor = Color.Goldenrod;
            else
                chkGold.ForeColor = Color.Gold;

            this.BackColor = theme;
            mnuMain.BackColor = theme;
            tabForm.BackColor = theme;
            tabCode.BackColor = theme;
            tabPreview.BackColor = theme;
            writeSetting();
        }

        private void mnuMain_Tools_Ganre_Click(object sender, EventArgs e)
        {
            btnGenre.PerformClick();
        }

        private void mnuMain_Tools_TimeCalc_Click(object sender, EventArgs e)
        {
            btnTime.PerformClick();
        }

        private void mnuMain_Tools_Parsing_Click(object sender, EventArgs e)
        {
            string Url;
            bool prx = false;
            mnuMain_File_New.PerformClick();
            if (frmInput.Input("Ввод значения", "Введите URL-адрес:", "Использовать прокси", out Url, prx))
            {
                string r = string.Empty;
                string o = string.Empty;
                string p = string.Empty;
                string y = string.Empty;
                //string ga = string.Empty;
                //string st = string.Empty;
                //string re = string.Empty;
                //string lang = string.Empty;
                //string countr = string.Empty;
                //string act = string.Empty;
                //string desc = string.Empty;
                //string err = string.Empty;
                this.Cursor = Cursors.WaitCursor;

                //string[] field = {"название",
                //            "жанр",
                //            "режис",
                //            "студия",
                //            "сайт",
                //            "ролях",
                //            "актрисы",
                //            "страна",
                //            "язык",
                //            "описание" };
                if (Url.IndexOf("http") > -1)
                {
                    var urlParams = new RequestParams();
                    string pp = "";
                    //string opt = "";
                    string[] url = Url.Split('?');
                    string[] urlp = url[0].Split('/');
                    if (url.Length > 1)
                    {
                        string[] prms = url[1].Split('&');

                        for (int i = 0; i < prms.Length; i++)
                        {
                            string[] prm = prms[i].Split('=');

                            urlParams[prm[0]] = prm[1];
                        }
                    }

                    string page = urlp[urlp.Length - 1];

                    using (var net = new xNet.HttpRequest(url[0]))
                    {
                        net.UserAgent = Http.ChromeUserAgent();
                        CookieDictionary cookie = new CookieDictionary(false);

                        net.Cookies = cookie;
                        net.CharacterSet = Encoding.GetEncoding(65001);
                        if (prx)
                            net.Proxy = HttpProxyClient.Parse(urlProxy() + ":3128");

                        string done = "";
                        try
                        {
                            string text1 = net.Get(net.BaseAddress.AbsolutePath, urlParams).ToString();
                            if (text1.IndexOf("nap.rkn.gov.ru") > 0)
                            {
                                net.Proxy = HttpProxyClient.Parse(urlProxy() + ":3128");
                                text1 = net.Get(net.BaseAddress.AbsolutePath, urlParams).ToString();
                            }
                            string charset = text1.Substring("charset=", "\"");
                            if (charset.ToLower() == "windows-1251")
                            {
                                net.CharacterSet = Encoding.GetEncoding(1251);
                                text1 = net.Get(net.BaseAddress.AbsolutePath, urlParams).ToString();
                            }

                            done = text1;
                            adrurl = net.Address.Authority.Split('.');

                            TextParsing(text1);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message,
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Импорт инфы завершён!\nПроверьте верность данных полей формы.",
                            "Готово",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void mnuMain_Tools_Import_Click(object sender, EventArgs e)
        {
            if (modif)
            {
                var msg = MessageBox.Show("Создать новую раздачу?",
                    "Импорт",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    mnuMain_File_New.PerformClick();
                    FormImport();
                }
                else if (msg == DialogResult.No)
                {
                    FormImport();
                }
            }
            else
            {
                FormImport();
            }
        }

        private void mnuMain_Tools_DigConverter_Click(object sender, EventArgs e)
        {
            string str = Clipboard.GetText();
            frmDigConverter frm = new frmDigConverter();
            frm.Owner = this; //Передаём вновь созданной форме её владельца.
            frm.Show();

            //if (frmInput.Input("Конвертация буфера", "Верное значение:", out str))
            //{
            //    Clipboard.SetText(str);
            //}
            //else
            //    MessageBox.Show("Действие отменено.");
        }

        private void mnuMain_Tools_ClearDirTorrent_Click(object sender, EventArgs e)
        {
            string filePath = frmMain.SetingPath + @"torrent\";

            if (AdditionFunc.ClearDirTorrent(filePath))
            {
                var msg = MessageBox.Show("Операция завершена успешно.\nВременная папка с торрент-файлами удалена.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void mnuMain_Tools_DesignOption_Click(object sender, EventArgs e)
        {
            frmDesign frm = new frmDesign();
            frm.Owner = this; //Передаём вновь созданной форме её владельца.
            frm.Show();
        }

        private void mnuMain_Copy_Name_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
                Clipboard.SetText(txtName.Text);
        }

        private void mnuMain_Copy_Poster_Click(object sender, EventArgs e)
        {
            if (txtPoster.Text != "")
                Clipboard.SetText(txtPoster.Text);
        }

        private void mnuMain_Copy_Code_Click(object sender, EventArgs e)
        {
            if (PreComplete()) //Если нажал Да
            {
                tbtnCopy.PerformClick();
            }
        }

        private void mnuKinorun_Search_Click(object sender, EventArgs e)
        {
            frmKinSearch frm = new frmKinSearch();
            frm.Owner = this;
            frm.Show();
        }

        private void mnuKinorun_Admin_List_Click(object sender, EventArgs e)
        {

        }

        private void mnuKinorun_Admin_Moders_Click(object sender, EventArgs e)
        {
            //TimeSpan times;
            //DateTime time = DateTime.Now;

            //times = time - TimeAutoriz;

            Cursor = Cursors.WaitCursor;

            frmModerSearch frm = new frmModerSearch();
            frm.Owner = this;
            frm.Show();

            Cursor = Cursors.Default;
            //if (times.Seconds > 20)
            //{
            //}
            //else
            //{
            //    MessageBox.Show(
            //    "Не удалось загрузить данные, так как время от авторизации прошло менеьше 20 сек. Попробуйте открыть данное окно позднее!",
            //    "Преждевременный вход",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Asterisk);
            //}
        }

        private void mnuKinorun_Cheking_Click(object sender, EventArgs e)//Проверка на дубликат
        {
            bool ok = false;
            string str = "";
            Int64 calc = 0;

            ToolStripMenuItem btn = (ToolStripMenuItem)sender;
            if (btn.Name == "mnuKinorun_Cheking_torrent")
            {
                dlgOpen.Title = "Выберите торрент-файл раздачи";
                dlgOpen.FileName = "";
                dlgOpen.Filter = "Торрент-файл (*.torrent)|*.torrent";
                dlgOpen.Multiselect = false;
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    FileBEncoding Change = new FileBEncoding(dlgOpen.FileName);
                    string strList = Change.ToString();
                    string line = strList.Substring("info => ", "piece length =>");
                    Int64[] list = new Int64[0];
                    int ind = 0;

                    while (line.Length > 0)
                    {
                        string s = line.Substring("length => ", "\n");
                        int i = line.IndexOf(s);
                        if (s == "")
                            i = line.IndexOf("ength => ");
                        if (s.Length > 0)
                        {
                            ind++;
                            Array.Resize(ref list, ind);
                            list[ind - 1] = Convert.ToInt64(s);
                            calc = calc + Convert.ToInt64(s);
                        }

                        line = line.Remove(0, i);
                        str = line.Substring("length => ", "\n");
                        if (str.Length == 0)
                            line = "";
                    }

                    str = calc.ToString();
                    ok = true;
                }
            }
            else
            {
                dlgOpen.Title = "Выберите файлы раздачи";
                dlgOpen.FileName = "";
                dlgOpen.Filter = "Видео (*.vob;*.avi;*.mpg;*.wmv;*.mkv;*.flv;*.mov;*.asf;*.mp4;*.ogm;*.rmvb;*.qt;*.mpeg;*.m1v)|*.vob;*.avi;*.mpg;*.wmv;*.mkv;*.flv;*.mov;*.asf;*.mp4;*.ogm;*.rmvb;*.qt;*.mpeg;*.m1v|Фотографии и изображения (*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff)|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|Аудио (*.mp3;*.wma, *.wav)|*.mp3;*.wma, *.wav|Все файлы (*.*)|*.*";
                dlgOpen.Multiselect = true;
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    if (dlgOpen.FileNames.Length > 1)
                    {
                        double sum = 0;
                        this.Cursor = Cursors.WaitCursor;
                        for (int i = 0; i < dlgOpen.FileNames.Length; i++)
                        {
                            sum = sum + Convert.ToDouble(AdditionFunc.GetFileSize(new System.IO.FileInfo(dlgOpen.FileNames[i]), 0));
                        }

                        str = Convert.ToString(sum);
                        this.Cursor = Cursors.Default;

                    }
                    else
                    {
                        str = AdditionFunc.GetFileSize(new System.IO.FileInfo(dlgOpen.FileName), 0);
                    }
                    ok = true;
                }
            }

            if (ok)
            {
                if (str != "0")
                {

                    string line = frmKinSearch.startDoc + Kinorun.Searching("", "1", "0", str, str) + frmKinSearch.finishDoc;
                    //if((line.IndexOf("Ничего не найдено")) < 0)
                    //{
                    //    var msg = MessageBox.Show("Ваша раздача дублирует одну из уже загруженных.",
                    //        "Неудача",
                    //        MessageBoxButtons.OK,
                    //        MessageBoxIcon.Hand);
                    //}
                    //else
                    //{
                    //    var msg = MessageBox.Show("Дубликатов не обнаружено.",
                    //        "Успех",
                    //        MessageBoxButtons.OK,
                    //        MessageBoxIcon.Asterisk);
                    //}

                    tbcBuilder.SelectedTab = tbcBuilder.TabPages["tabPreview"];
                    line = line.Replace("Ничего не найдено", "<h2 style=\"vertical - align: middle; color: rgb(0, 217, 0)\"><span style=\"text-align: center; display: block;height: 100%;width:100%; position: absolute;\">Можете смело оформлять раздачу<br>Похожих раздач не найдено!</span></h2>");

                    webBrowser1.DocumentText = line;
                }
                else
                {
                    var msg = MessageBox.Show("Возможно файл повреждён или имеет несколько иной формат, незнакомый программе.\nПопробуйте осуществить поиск по содержимому раздачи.",
                        "Неудача",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                }
            }
        }

        private void mnuKinorun_Upload_Click(object sender, EventArgs e)
        {
            if (mnuKinorun_Upload.Checked)
            {
                //mnuKinorun_Upload.Checked = false;
                //tabForm.Enabled = true;
                tbcBuilder.SelectedTab = tbcBuilder.TabPages["tabForm"];
            }
            else
            {
                DialogResult msg;

                if (txtPoster.Text.Length > 3)
                {
                    Random rnd = new Random();
                    int ind = 0 + rnd.Next(14);
                    if (cmbVoting.SelectedIndex == 0)
                        cmbVoting.SelectedIndex = ind;

                    string title = dlgOpen.Title;
                    string torrent = "";
                    dlgOpen.Title = "Выберите торрент-файл заливаемой раздачи";
                    dlgOpen.Filter = "Торрент файл (*.torrent)|*.torrent";
                    dlgOpen.FileName = "";
                    if (!bGenre)
                    {
                        string strGanre = "";
                        string[] ganre = txtGenre.Text.Split(',');
                        int res = 0;

                        for (int i = 0; i < ganre.Length; i++)
                        {
                            if (Array.IndexOf(frmGenre.strGenre, ganre[i].Trim()) > -1)
                            {
                                strGanre = strGanre + ", " + ganre[i].Trim();
                                res++;
                            }
                        }
                        strGanre = strGanre.Trim();

                        iGenre = AdditionFunc.TagBuild(strGanre.Trim());
                        if (res > 1)
                            bGenre = true;
                    }
                    if ((dlgOpen.ShowDialog() == DialogResult.OK) & (File.Exists(dlgOpen.FileName)))
                    {
                        int fn = 0;
                        fn = dlgOpen.FileName.IndexOf('#');

                        if (fn > -1)
                        {
                            msg = MessageBox.Show("В имени файла замечен недопустимый символ #, такой торрент тркере не примет.\n\n" +
                                "Переименовать торрент-файл автоматически?",
                                "Внимание!",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (msg == DialogResult.Yes)
                                fn = -1;
                        }

                        if ((PreComplete()) && (fn < 0))
                        {
                            torrent = AdditionFunc.Translitter(dlgOpen.FileName);

                            if (cmbCategory.SelectedIndex > 0)

                                //if ((_KinoRun[0] != "") && (!Kinorun.isAuth()))
                                if ((_KinoRun[0] != "") && (!Kinorun.isAuth()))
                                    authKin = Kinorun.Auth(_KinoRun[0], _KinoRun[1]);

                            if (!authKin)
                            {
                                oper = "Auth";
                                timer1.Interval = 25000;
                                timer1.Enabled = true;
                            }

                            if ((bGenre) && (authKin))
                            {
                                iGenre = AdditionFunc.TagBuild(txtGenre.Text);
                                string[] str = Tags.Split(',');
                                for (int i = 0; i < str.Length; i++)
                                {
                                    if (str[i].Length > 0)
                                    {
                                        iGenre = iGenre.Replace(str[i], "");
                                        iGenre = iGenre.Replace(",,", ",");
                                    }
                                }

                                iGenre = ".knr," + iGenre.Trim(',');
                                iGenre = iGenre.Replace(",,", ",");

                                iGenre = (Tags.Length > 1) ? iGenre + "," + Tags : iGenre + "";

                                //tabForm.Enabled = false;
                                //mnuKinorun_Upload.Checked = true;
                                //string link = Kinorun.UplTorrent(dlgOpen.FileName, txtName.Text, textBox1.Text, rtxtCode.Text, iGenre, CatInd[cmbCategory.SelectedIndex]);

                                string link = Kinorun.UplTorrent(torrent, txtName.Text, txtPoster.Text, rtxtCode.Text, iGenre, CatInd[cmbCategory.SelectedIndex]);

                                try
                                {
                                    AdditionFunc.ClearDirTorrent(SetingPath + "temp");
                                    Directory.Delete(SetingPath + "temp");
                                }
                                catch (IOException)
                                {
                                    msg = MessageBox.Show("Временные файлы не удалены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                if (link == "Такой торрент уже загружен!")
                                {
                                    msg = MessageBox.Show(link, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    //mnuKinorun_Upload.PerformClick();
                                }
                                else
                                {
                                    tbcBuilder.SelectedTab = tbcBuilder.TabPages["tabPreview"];
                                    webBrowser1.DocumentText = link;
                                    ShowBrowser();
                                    Times = 0;
                                    link1 = link.Substring("download.php?", "\">");
                                    if (link1 == "")
                                    {
                                        //var msg = MessageBox.Show("Раздачу залить не удалась.\nПроверьте поля формы и повторите отправку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        if (!Kinorun.TorrentDown(_KinoRun[0], _KinoRun[1], link1))
                                        {
                                            oper = "TorrentDown";
                                            timer1.Enabled = true;
                                        }
                                    }
                                }

                                //Kinorun.DownTorrent(_KinoRun[0], _KinoRun[1], link1);"http://kinorun.com/download.php?" + 

                                //link1 = link1.Replace(" ", "%20");
                                //if (0 < link1.Length)
                                //{
                                //    Kinorun.DownTorrent(_KinoRun[0], _KinoRun[1], link.Substring("download.php?", "\">"););

                                //    //if (Kinorun.Auth(_KinoRun[0], _KinoRun[1]))
                                //    //{
                                //    //    //Process.Start("http://kinorun.com/download.php?" + link);
                                //    //}
                                //}

                            }
                            else if (!bGenre)
                            {
                                msg = MessageBox.Show("Проверьте правильность указания тегов!\nРаздачу залить не удалась.\nВыберите теги и повторите отправку.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                btnGenre.PerformClick();
                            }
                            else
                            {
                                msg = MessageBox.Show("Неудалось залогиниться на KinoRun.com!\nРаздачу залить не удалась.\n\n" +
                                    "Проверьте правильность данных авторизации и повторите отправку.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                authKin = false;
                                mnuMain_Tools_DesignOption.PerformClick();
                            }
                        }
                        else if(fn > -1) //Замечен запрещённый символ
                        {
                            msg = MessageBox.Show("Имя торрент-файла содержит запрещённый символ #!\nРаздачу залить не удалась.\n\nПереименуйте файл и повторите отправку.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                        }
                        else
                        {
                            msg = MessageBox.Show("Не выбрана категория!\nРаздачу залить не удалась.\n\nВыберите категорию и повторите отправку.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                        }
                    }
                    else if (File.Exists(distribFile))
                    {
                        msg = MessageBox.Show("Торрент-файл не существует!\nРаздачу залить не удалась.\n\nВыберите существующий файл и повторите отправку.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                    }

                    dlgOpen.Title = title;
                }
                else
                {
                    msg = MessageBox.Show("Не указан путь нахождения постера!\nРаздачу залить не удалась.\n\nВыберите изображение постера и повторите отправку.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                    btnPoster.PerformClick();
                }
            }
        }

        private void mnuHelp_Wizard_Click(object sender, EventArgs e)
        {
            frmWizard frm = new frmWizard();
            frm.Owner = this;
            frm.Show();
        }

        private void mnuHelp_Site_Click(object sender, EventArgs e)
        {
            Process.Start("http://altrec.h1n.ru/");
        }

        private void mnuHelp_Treker_Click(object sender, EventArgs e)
        {
            Process.Start("http://kinorun.online/");
        }

        private void mnuHelp_Forum_Click(object sender, EventArgs e)
        {
            Process.Start("http://altrec.ucoz.net/forum/2");
        }

        private void mnuHelp_Update_Click(object sender, EventArgs e)
        {
            if (File.Exists(ExePath + "\\AutoUpdater.exe"))
            {
                //writeSetting();

                //if (ExeExt == "zip")
                //{
                //    File.Copy(ExePath + "\\AutoUpdater.exe", ExePath + "\\_AutoUpdater.exe");
                //    File.Delete(ExePath + "\\AutoUpdater.exe");
                //    Process.Start(ExePath + "\\_AutoUpdater.exe");
                //}
                //else
                //    Process.Start(ExePath + "\\AutoUpdater.exe");

                //File.Copy(ExePath + "\\AutoUpdater.exe", ExePath + "\\_AutoUpdater.exe");
                //File.Delete(ExePath + "\\AutoUpdater.exe");
                //Process.Start(ExePath + "\\AutoUpdater.exe", SetingPath);

                //var startInfo = new ProcessStartInfo(ExePath + "\\AutoUpdater.exe"); //Путь
                //startInfo.Arguments = SetingPath;
                //startInfo.Verb = "runas";
                //Process.Start(startInfo);

                string pathsys = Environment.GetEnvironmentVariable("PROGRAMFILES(x86)") + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
                string[] sys = ExePath.Split('\\');

                if (sys[1] == "Program Files (x86)")
                    access = "a";
                else
                    access = "u";

                access = "a";
                Close();
            }
            else
                Process.Start("http://altrec.h1n.ru/");
        }

        private void mnuHelp_About_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.Owner = this;
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (Kinorun.Auth(_KinoRun[0], _KinoRun[1]))
            //    Kinorun.TorrentDown(_KinoRun[0], _KinoRun[1], textBox1.Text);
            //else
            //    Kinorun.TorrentDown(_KinoRun[0], _KinoRun[1], textBox1.Text);
            Kinorun.TorrentDown(_KinoRun[0], _KinoRun[1], textBox1.Text);
        }
    }
}
