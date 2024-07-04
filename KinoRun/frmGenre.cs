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
    public partial class frmGenre : Form
    {
        string iGenre = "";
        string[] sGenre;
        public static string[] strGenre = new string[190] { ".+18", ".boss1970", ".delikates", ".everlast", ".liza_1", ".nn99nn", "1080 HD", "3dporn", "4k", "720 HD", "All Girl", "All Sex", "Amateur", "Anal", "Anilingus", "Animals (Zoo)", "Anime", "Asian", "Ass Licking", "Ass to Mouth", "Bbw", "Bdsm", "Bdwc", "Bestiality", "Bi (Bisexual)", "Big Ass", "Big Boobs", "Big Cock", "Big Dick", "Big Tits", "Binding", "Bisexual", "Bizarre", "Black", "Blondes", "Blowjob", "Bondage", "Brunette", "Bukkake", "Busty", "Casting", "Classic", "Compilation", "Copro", "Cosplay", "Couples", "Creampie", "Cumschot", "Deep Throat", "Deepthroat", "Defloration", "Dildo", "Doctor", "Documentary XXX", "Doggystyle", "Dogs", "Domination", "Double Anal Penetration", "Double Oral", "Double Penetration", "Drunk", "Erotic", "Facesitting", "Facial", "Family Roleplay", "Fat", "Feature", "Femdom", "Fetish", "Fingering", "Fisting", "Flog", "Foot Fetish", "Footjob", "Foto", "Foursome", "Fuck Machine", "Fucking Machines", "Gags", "Gang Bang", "Gaping", "Gay (Homosexual)", "Girl/Girl (Lesbian)", "Goat", "Gonzo", "Grany", "Group Footjob", "Group Sex", "Gyno", "Hairy", "Hardcore", "Home Sex", "Horror", "Horse", "Incest", "Indian", "Interracial", "Latinos", "Lesbian", "Lingerie", "Massage", "Masturbation", "Mature", "Medical", "Midgets", "Milf", "Natural Tits", "Oiled", "Old", "Old Fart", "Oral", "Orgy", "Outdoor", "Pack", "Pantyhose", "Parody", "Peeping", "Perversion", "Photosession", "Pickup", "Pin Up Porno", "Pippin", "Piss", "Pornostar", "Posing", "Pov", "Pregnant", "Publik", "Pumping", "Pussy Licking", "Pygmy", "Rape", "Retro", "Romance", "Russian", "Scat", "Scuba Sex", "Sex Machines", "Sexmachines", "Shemale", "Sleeping", "Small", "Small Boobs", "Solo", "Spanking", "Sports Sex", "Squirt", "Squirting", "Straight", "Strap-On", "Strapping", "Striptease", "Students", "Swallowing", "Swingers", "Taboo", "Tattooed", "Teeny", "Thin", "Threesome", "Tit Fucking", "Titjob", "Titty Fucking", "Torture", "Toys", "Training", "Tranny", "Triple Anal Penetration", "Triple Penetration", "Underwater Sex", "Uniform", "Uniforms", "Vintage", "Virgin", "Virgins", "Voyeurism", "War Porn", "Wives", "X-Mas", "Zooskool", "Жены", "Массаж", "Подглядывание", "Порно с русским переводом", " ", " ", " ", " ", " ", " " };
        string[] tagGenre = new string[190] { ".+18", ".boss1970", ".delikates", ".everlast", ".liza_1", ".nn99nn", "1080 hd", "3dporn", "4k", "720 hd", "all girl", "all sex", "amateur", "anal", "anilingus", "animals (zoo)", "anime", "asian", "ass licking", "ass to mouth", "bbw", "bdsm", "bdwc", "bestiality", "bi (bisexual)", "big ass", "big boobs", "big cock", "big dick", "big tits", "binding", "bisexual", "bizarre", "black", "blondes", "blowjob", "bondage", "brunette", "bukkake", "busty", "casting", "classic", "compilation", "copro", "cosplay", "couples", "creampie", "cumschot", "deep throat", "deepthroat", "defloration", "dildo", "doctor", "documentary xxx", "doggystyle", "dogs", "domination", "double anal penetration", "double oral", "double penetration", "drunk", "erotic", "facesitting", "facial", "family roleplay", "fat", "feature", "femdom", "fetish", "fingering", "fisting", "flog", "foot fetish", "footjob", "foto", "foursome", "fuck machine", "fucking machines", "gags", "gang bang", "gaping", "gay (homosexual)", "girl/girl (lesbian)", "goat", "gonzo", "grany", "group footjob", "group sex", "gyno", "hairy", "hardcore", "home sex", "horror", "horse", "incest", "indian", "interracial", "latinos", "lesbian", "lingerie", "massage", "masturbation", "mature", "medical", "midgets", "milf", "natural tits", "oiled", "old", "old fart", "oral", "orgy", "outdoor", "pack", "pantyhose", "parody", "peeping", "perversion", "photosession", "pickup", "pin up porno", "pippin", "piss", "pornostar", "posing", "pov", "pregnant", "publik", "pumping", "pussy licking", "pygmy", "rape", "retro", "romance", "russian", "scat", "scuba sex", "sex machines", "sexmachines", "shemale", "sleeping", "small", "small boobs", "solo", "spanking", "sports sex", "squirt", "squirting", "straight", "strap-on", "strapping", "striptease", "students", "swallowing", "swingers", "taboo", "tattooed", "teeny", "thin", "threesome", "tit fucking", "titjob", "titty fucking", "torture", "toys", "training", "tranny", "triple anal penetration", "triple penetration", "underwater sex", "uniform", "uniforms", "vintage", "virgin", "virgins", "voyeurism", "war porn", "wives", "x-mas", "zooskool", "жены", "массаж", "подглядывание", "порно с русским переводом", " ", " ", " ", " ", " ", " " };
        CheckBox[] myBtn;

        public frmGenre()
        {
            InitializeComponent();
        }

        private void btnCompleat_Click(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)this.Owner;
            frm.txtGenre.Text = txtGenre.Text;
            frm.iGenre = AdditionFunc.TagBuild(txtGenre.Text);
            if (iGenre != "")
                frm.bGenre = true;
            else
                frm.bGenre = false;

            this.Close();
        }

        private void frmGenre_Load(object sender, EventArgs e)
        {
            int widthBtn = 0;
            int heightBtn = 0;
            int yy = 0;
            string genre = "";
            myBtn = new CheckBox[strGenre.Length];
            frmMain frm = (frmMain)this.Owner;
            iGenre = frm.iGenre;
            txtGenre.Text = frm.txtGenre.Text;
            sGenre = txtGenre.Text.Split(',');

            for (int i = 0; i < myBtn.Length; i++)
            {
                myBtn[i] = new CheckBox();
                myBtn[i].Width = 100;
                myBtn[i].Name = "btnGenere" + i;
                myBtn[i].Appearance = Appearance.Button;
                myBtn[i].Tag = tagGenre[i];
                myBtn[i].Text = strGenre[i];

                yy = panel1.Width - (myBtn[i].Width + widthBtn);
                if(yy <= 0)
                {
                    heightBtn = myBtn[i].Height + heightBtn + 1;
                    widthBtn = 0;
                }
                myBtn[i].Location = new Point(widthBtn, heightBtn);
                widthBtn = myBtn[i].Width + widthBtn + 1;

                for (int j = 0; j < sGenre.Length; j++)
                {
                    sGenre[j] = sGenre[j].Trim();

                    if (sGenre[j] == myBtn[i].Text)
                    {
                        myBtn[i].Checked = true;
                        if (genre == "")
                            genre = sGenre[j];
                        else
                            genre = genre + ", " + sGenre[j];
                    }
                }

                txtGenre.Text = genre.Trim();
                myBtn[i].CheckedChanged += new EventHandler(myClickFunction);

                panel1.Height = heightBtn;
                this.Height = 115 + panel1.Height;
                btnCancel.Location = new Point(btnCancel.Location.X, this.Height - (45 + btnCancel.Height));
                btnComplete.Location = new Point(btnComplete.Location.X, this.Height - (45 + btnComplete.Height));
                this.Refresh();
                panel1.Controls.Add(myBtn[i]);

            }
        }

        private void myClickFunction(object sender, EventArgs e)
        {
            var _myBtn = (CheckBox)sender;

            if (_myBtn.Text != " ")
            {
                if (_myBtn.Checked)
                {
                    if (txtGenre.Text != "")
                    {
                        txtGenre.Text = txtGenre.Text + ", " + _myBtn.Text;
                        iGenre = iGenre + "," + _myBtn.Tag;
                    }
                    else
                    {
                        txtGenre.Text = _myBtn.Text;
                        iGenre = Convert.ToString(_myBtn.Tag);
                    }
                }
                else
                {
                    txtGenre.Text = txtGenre.Text.Replace(", " + _myBtn.Text, string.Empty);
                    txtGenre.Text = txtGenre.Text.Replace(_myBtn.Text, string.Empty);
                    iGenre = iGenre.Replace("," + _myBtn.Text, string.Empty);
                    iGenre = iGenre.Replace(Convert.ToString(_myBtn.Tag), string.Empty);
                }

                txtGenre.Text = txtGenre.Text.TrimStart(',');
                txtGenre.Text = txtGenre.Text.Trim();
                iGenre = iGenre.TrimStart(',');
                iGenre = iGenre.Trim();

            }
            else
                _myBtn.Checked = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmMain frm = (frmMain)this.Owner;
            this.Close();
        }
    }
}
