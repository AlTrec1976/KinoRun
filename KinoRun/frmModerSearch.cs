using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace KinoRun
{
    public partial class frmModerSearch : Form
    {
        const string startDoc = "<!DOCTYPE HTML>\n<html>\n <head> \n  <meta charset=\"utf - 8\"><style type=\"text/css\"><!--\nhtml { background-color:#eee;padding:0px;margin:0px;min-width:1200px;font-family:Calibri, sans-serif;font-size:14px; }\nbody { padding:0px;margin:0px;font:normal 14px Calibri;color:#595959;background:#efefef; }\n--></style></head><body><table  style=\"width: 100%\">\n";
        const string finishDoc = "</body>\n</html>";

        string[] ListUser = new string[17];
        string[] ListTag = new string[17];
        string[,] ListCabs = new string[3,10];
        string[] ListModers = new string[0];
        string[] ListAdmins = new string[0];
        string[] ListDirs = new string[0];
        //string[] ListModersTag = new string[0];
        //string[] ListAdminsTag = new string[0];
        //string[] ListDirsTag = new string[0];
        string line;
        string listing;
        string SearchPage;

        public frmModerSearch()
        {
            InitializeComponent();
            ListUser = new string[] { "выбрать", "Kross", "Priest", "SvenSD", "Скорпион", "ALEX SS", "AlTrec76", "aturai", "Doberm@n", "FIKS.a", "BOSS1970", "delikates", "Everlast", "kalligula", "nn99nn", "Liza_1", "ВОЖДЬ", "farzoy513" };
            ListTag = new string[] { "", ".КРОСС", ".priest", ".svensd", ".СКОРПИОН", ".alexss", ".altrec76", ".aturai", ".ДОБЕРМАН", ".ФИКС.а", ".boss1970", ".delikates", ".everlast", ".kalligula", ".nn99nn", ".liza_1", ".вождь", ".farzoy513" };
        }

        private void frmModerSearch_Load(object sender, EventArgs e)
        {
            ListCabs = Kinorun.Admins();
            Cursor = Cursors.WaitCursor;

            for(int i = 0; i < 3; i++)
            {
                string[] lst = new string[0];

                for (int j = 0; j < 10; j++)
                {
                    if (ListCabs[i, j].Length > 1)
                    {
                        Array.Resize(ref lst, lst.Length + 1);
                        lst[j] = ListCabs[i, j];
                    }
                }

                switch (i)
                {
                    case 0:
                        Array.Resize(ref ListDirs, lst.Length);
                        ListDirs = lst;
                        break;
                    case 1:
                        Array.Resize(ref ListAdmins, lst.Length);
                        ListAdmins = lst;
                        break;
                    case 2:
                        Array.Resize(ref ListModers, lst.Length);
                        ListModers = lst;
                        break;
                }
            }

            //if (ListModers.Length > 1)
            //{
            //    cmbTag.Items.Clear();
            //    cmbTag.Items.AddRange(ListModers);
            //}
            //else
            //{
            //    cmbTag.Items.AddRange(ListUser);
            //    Array.Resize(ref ListModersTag, cmbTag.Items.Count);
            //}

            //for (int i = 0; i < cmbTag.Items.Count; i++)
            //{
            //    cmbTag.SelectedIndex = i;
            //    string str = cmbTag.SelectedItem.ToString();
            //    ListModersTag[i] = "." + str.ToLower();
            //}

            string[] years = new string[3];
            int year = Convert.ToInt32(DateTime.Now.Year);
            years = new string[] { Convert.ToString(year), Convert.ToString(year - 1), Convert.ToString(year - 2) };

            cmbYear.Items.AddRange(years);
            try
            {
                cmbStat.SelectedIndex = 0;
                cmbTag.SelectedIndex = 0;
                cmbYear.SelectedIndex = 0;
                cmbMonth.SelectedIndex = Convert.ToInt32(DateTime.Now.Month);
                cmbStat.Text = cmbStat.SelectedItem.ToString();
                cmbTag.Text = cmbTag.SelectedItem.ToString();
                cmbYear.Text = cmbYear.SelectedItem.ToString();
                //cmbYear.Text = Convert.ToString(DateTime.Now.Year);
                cmbMonth.Text = cmbMonth.SelectedItem.ToString();
            }
            catch
            {
                this.Close();
            }
            cmbTag.SelectedIndex = 0;
            Sorting();
            webBrowser1.DocumentText = listing;
            Cursor = Cursors.Default;
        }

        private void Sorting()
        {
            string[] torrents = new string[0];
            string list = "." + cmbTag.Text.ToLower();
            string mont = "";

            this.Cursor = Cursors.WaitCursor;
            if (cmbMonth.SelectedIndex < 1)
                cmbMonth.SelectedIndex = DateTime.Now.Month;

            mont = Convert.ToString(cmbMonth.SelectedIndex);
            if (cmbMonth.SelectedIndex < 10)
                mont = "0" + Convert.ToString(cmbMonth.SelectedIndex);

            line = startDoc + SearchPage + finishDoc;

            line = line.Replace("Ничего не найдено", "<h2 style=\"vertical - align: middle; color: rgb(255, 0, 51)\"><span style=\"text-align: center; display: block;height: 100%;width:100%; position: absolute;\">К сожалению, поиск не дал результата</span></h2>");
            //line = line.Replace("<tr >", "<tr>");
            int tor = 0;
            int ustor = 0;
            string line1 = "<!DOCTYPE HTML>\n<html>\n <head> \n  <meta charset=\"utf - 8\"><style type=\"text/css\"><!--\nhtml { background-color:#eee;padding:0px;margin:0px;min-width:1200px;font-family:Calibri, sans-serif;font-size:14px; }\nbody { padding:0px;margin:0px;font:normal 14px Calibri;color:#595959;background:#efefef; }\n--></style></head><body><table  style=\"width: 100%\">\n\n<table class=\"embedded\" cellspacing=\"0\" cellpadding=\"5\" width=\"100%\">\n";
            line1 = line1 + "<tr>\n<td class=\"index\" colspan=\"12\">\n\n<script type=\"text/javascript\">\n\t$j = jQuery;\n</script>\n\n<script type=\"text/javascript\">\n\t\t$j(document).ready(function (){\n\t\tpaginator_example = new Paginator(\n\t\t\t'paginator_div_1',                           // id контейнера, куда ляжет пагинатор\n\t\t\t1,                      // общее число страниц\n\t\t\t20,            // число страниц, видимых одновременно\n\t\t\t1,                     // номер текущей страницы\n\t\t\t'browse.php?tag=.alexss&amp;&size11=-1&size12=1&size21=-1&size22=1&page='  // url страниц\n\t\t);\n\t});\n\t$j(document).ready(function (){\n\t\tpaginator_example = new Paginator(\n\t\t\t'paginator_div_2',                           // id контейнера, куда ляжет пагинатор\n\t\t\t1,                      // общее число страниц\n\t\t\t20,            // число страниц, видимых одновременно\n\t\t\t1,                     // номер текущей страницы\n\t\t\t'browse.php?tag=.alexss&amp;&size11=-1&size12=1&size21=-1&size22=1&page='  // url страниц\n\t\t);\n\t});\n</script>\n\n<tr><td class=\"index\" colspan=\"12\">\n<table border=\"1\" align=\"center\">\n\t<tr><td bgcolor=\"#e35c2c\">\n\t\t<div class=\"paginator\" id=\"paginator_div_1\"></div>\n\t\t\n\t</td></tr>\n";
            int j = 0;
            string str1 = SearchPage.Substring("<tr><td class=\"index\" colspan=\"12\">", "</td></tr>");
            if (str1 != "Ничего не найдено")
            {
                for (int i = 0; i < 200; i++)
                {
                    string str = "";
                    string dat = "";
                    string user = "";
                    str = line.Substring("<tr >\n", "</tr>\n");
                    str = "<tr >\n" + str + "</tr>\n";
                    //if (i > 0)
                    line = line.Replace(str, "");

                    dat = str.Substring("<i>", " ");
                    string[] datstr = dat.Split('-');
                    try
                    {
                        //int mont = Convert.ToInt32(datstr[1]);
                        if (datstr.Length == 3)
                        {
                            str1 = datstr[0] + "-" + datstr[1];
                            //if ((datstr[1] == mont) && (datstr[0] == cmbYear.SelectedText.ToString()))
                            if ((datstr[1] == mont) && (datstr[0] == cmbYear.Text))
                            {
                                Array.Resize(ref torrents, torrents.Length + 1);
                                torrents[j] = str;
                                //line = line.Replace(str, "");
                                tor++;
                                user = str.Substring(cmbTag.Text, "</b></a></td>\n");
                                if (user.Length > 20)
                                {
                                    user = user.Replace(list, "") + "</b></a></td>\n";
                                    user = user.Replace(list.ToUpper(), "");
                                    user = user.Substring(cmbTag.Text, "</b></a></td>\n");
                                }

                                if (user == "</span>")
                                    ustor++;
                                line1 = line1 + str;
                                j++;
                            }
                            else if((Convert.ToInt32(datstr[1]) < Convert.ToInt32(mont)) || (Convert.ToInt32(datstr[0]) < Convert.ToInt32(cmbYear.Text)))
                                { break; }
                        }
                        //else
                        //{
                        //    torrents[j] = str;
                        //    //line = line.Replace(str, "");
                        //    tor++;
                        //    user = str.Substring(cmbTag.Text, "</b></a></td>\n");
                        //    if (user == "</span>")
                        //        ustor++;
                        //    line1 = line1 + str;
                        //    j++;
                        //    Array.Resize(ref torrents, j + 1);
                        //}
                    }
                    catch (Exception)
                    { break; }
                    //if ((mont < 9) || (datstr[0] != "2016"))
                    //    break;
                }
            }
            else
            {
                line1 = line1 + "Ничего не найдено";
            }
            if (tor == 0)
                line1 = line1 + "Ничего не найдено";
            line1 = line1 + "</tbody><tr><td class=\"index\" colspan=\"12\">\n<table border=\"1\" align=\"center\">\n\t<tr><td bgcolor=\"#e45c28\">\n\t\t<div class=\"paginator\" id=\"paginator_div_2\"></div>\n\t\t\n\t</td></tr>\n</table>\n</td></tr>\n</table></body>\n</html>";
            //lblReporp.Text = "Найдено раздач - " + tor + "\nиз них:\nличных - " + ustor + "\nпроверенных - " + (tor - ustor);

            lblTotal.Text = Convert.ToString(tor);
            lblPrivate.Text = Convert.ToString(ustor);
            lblApproved.Text = Convert.ToString(tor - ustor);

            listing = line1;

            webBrowser1.DocumentText = listing;
            this.Cursor = Cursors.Default;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sorting();
        }

        private void cmbTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";

            this.Cursor = Cursors.WaitCursor;
            if (cmbTag.SelectedIndex > -1)
            {
                if (cmbTag.Text == "Kross")
                    str = "КРОСС";
                else
                    str = cmbTag.Text.ToLower();

                SearchPage = Kinorun.ModerSearching("." + str);
                Sorting();
            }
            else
            {

            }
            this.Cursor = Cursors.Default;
        }

        private void cmbStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbStat.SelectedIndex)
            {
                case 0:
                    cmbTag.Items.Clear();
                    cmbTag.Items.AddRange(ListDirs);
                    break;
                case 1:
                    cmbTag.Items.Clear();
                    cmbTag.Items.AddRange(ListAdmins);
                    break;
                case 2:
                    cmbTag.Items.Clear();
                    cmbTag.Items.AddRange(ListModers);
                    break;
            }
            try
            {
                cmbTag.SelectedIndex = 0;
                cmbTag.Text = cmbTag.SelectedItem.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show(
                "Не удалось загрузить данные, так как время от авторизации прошло менеьше 20 сек. Попробуйте открыть данное окно позднее!",
                "Преждевременный вход",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk);
                this.Close();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.DocumentText.Length < 50)
                //if (listing != null)
                webBrowser1.DocumentText = listing;
        }
    }
}
