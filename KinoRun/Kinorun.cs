using System;
using xNet;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace KinoRun
{
    class Kinorun
    {
        //static Timer myTimer = new Timer();
        //static int alarmCounter = 1;
        //static bool exitFlag = false;

        //private readonly Timer tmrShow;
        public static string done = "";
        //public static string url_viewer, images, thumb;
        static string login, password;
        //static bool auth;
        const string site = "http://kinorun.online/"; //"http://kinorun.com/"; // 
        static CookieDictionary cookie = frmMain.cookie;

        public class ImageUrl
        {
            public int status_code;
            public Image image { get; set; }
            public class Image
            {
                public string url { get; set; }
                public string url_viewer { get; set; }
                public Thumb thumb { get; set; }
                public class Thumb
                {
                    public string url { get; set; }
                }
            }
        }

        static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = (date - origin);
            return (Math.Round(diff.TotalSeconds, 3) * 1000);
        }

        class CursorEx : IDisposable
        {
            public CursorEx()
            {
                Cursor.Current = Cursors.WaitCursor;
            }
            public void Dispose()
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private static void TimerAuth()
        {
            if (frmMain.TimeAutoriz.ToString() != "01.01.0001 0:00:00")
            {
                DateTime time = DateTime.Now;
                TimeSpan times = time - frmMain.TimeAutoriz;

                new CursorEx();
                int tm = 100 - times.Milliseconds;
                Thread.Sleep(tm);
            }
        }

        private static string AuthKin(string Login, string Password)
        {
            if ((Login != null) || (Login != ""))
            {
                TimerAuth();
                using (var net = new xNet.HttpRequest(site))
                {
                    net.UserAgent = Http.ChromeUserAgent();
                    //cookie = new CookieDictionary(false);

                    net.Cookies = cookie;
                    net.CharacterSet = Encoding.GetEncoding(1251);
                    //done = net.Get("/").ToString();
                    //        cookie = net.Cookies;
                    try
                    {
                        done = "";
                        done = net.Get("/").ToString();
                            cookie = net.Cookies;
                        string str = done.Substring("<input type=\"submit\" value=\"", "\" class=");
                        if ("Вход!" == str)
                        {
                            var urlParams = new RequestParams();
                            urlParams["username"] = Login;
                            urlParams["password"] = Password;

                            done = net.Post("/takelogin1.php", urlParams).ToString();
                            cookie = net.Cookies;
                            frmMain.cookie = net.Cookies;
                            str = done.Substring("<b>Ошибка входа</b><br />", "</div>");
                            if (str == "Последняя попытка входа произведена менее 20 секунд назад.")
                            {
                                done = str;
                                //tmrShow.Enabled = true;
                            }
                        }
                    }
                    catch (xNet.HttpException ex)
                    {
                        var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        done = "";
                    }
                }

                //frmMain.TimeAutoriz = DateTime.Now;
            }
            else
            {
                done = "";
            }
            return done;
        }

        public static bool Auth(string Login, string Password)
        {
            bool rep = false;
            login = Login;
            password = Password;
            //Timer tmrShow = new Timer();
            //tmrShow.Interval = 25000;
            //tmrShow.Tick += tmrShow_Tick;

            if (Login != "")
            {
                string resp = AuthKin(login, password);
                //tmrShow.Enabled = false;

                if (resp == "Последняя попытка входа произведена менее 20 секунд назад.")
                {
                    var msg = MessageBox.Show(resp + "Подождите 20 секунд и повторите попытку.", "Система безопасности сайта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rep = false;
                }
                else
                {
                    string str = "";
                    //str = done.Substring("Ошибка входа</b><br />", "</div>");
                    str = resp.Substring("Привет, ", "</div>");

                    //if (str.Length == 5)
                    int pr = resp.IndexOf(login);

                    if (pr > -1)
                    {
                        rep = true;
                        frmMain.TimeAutoriz = DateTime.Now;
                    }
                    else
                    {
                        //string msgstr = "";
                        //pr = resp.IndexOf("Имя пользователя или пароль неверны");
                        //if (pr < 0)
                        //    pr = resp.IndexOf("Вы не зарегистрированы в системе");
                        //else
                        //    msgstr = ;

                        var msg = MessageBox.Show("Неверное имя пользователя или пароль.\nЕсли Вы зарегистрировались, но забыли активировать\nучётную запись, то сделайте это и повторите попытку.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        rep = false;
                    }
                }
            }
            else
            {
                rep = false;
            }

            frmMain.authKin = rep;
            return rep;
        }

        public static bool isAuth()
        {
            bool rep = false;

            using (var net = new xNet.HttpRequest(site))
            {
                string str = "";
                net.UserAgent = Http.ChromeUserAgent();
                //cookie = new CookieDictionary(false);

                net.Cookies = cookie;
                net.CharacterSet = Encoding.GetEncoding(1251);
                try
                {
                    done = "";
                    done = net.Get("/").ToString();
                    str = done.Substring("Привет, ", "</div>");
                    if (str.Length == 5)
                        rep = false;
                    else
                        rep = true;
                }
                catch (xNet.HttpException ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    rep = false;
                }
            }

            frmMain.authKin = rep;
            return rep;
        }

        public static int UserStatus(string Login, string Password)
        {
            //TimerAuth();

            int rep = 0;
            if ((Login != null) || (Login != ""))
            {
                if (frmMain.cookie.Count < 4)
                    AuthKin(Login, Password);

                using (var net = new xNet.HttpRequest(site))
                {
                    login = Login;
                    password = Password;
                    net.UserAgent = Http.ChromeUserAgent();
                    //cookie = new CookieDictionary(false);

                    net.Cookies = cookie;
                    net.CharacterSet = Encoding.GetEncoding(1251);
                    try
                    {
                        done = "";
                        done = net.Get("/").ToString();
                        string str = done.Substring("<input type=\"submit\" value=\"", "\" class=");
                        if ("Вход!" == str)
                        {
                            //var urlParams = new RequestParams();
                            //urlParams["username"] = Login;
                            //urlParams["password"] = Password;
                            //done = net.Post("/takelogin1.php", urlParams).ToString();

                            AuthKin(Login, Password);

                            done = net.Get("/").ToString();
                        }

                        //var urlParams = new RequestParams();
                        //urlParams["username"] = Login;
                        //urlParams["password"] = Password;

                        //done = net.Post("/takelogin1.php", urlParams).ToString();

                        str = "";
                        str = done.Substring("Ошибка входа</b><br />", "</div>");

                        if (str.Length > 0)
                        {
                            var msg = MessageBox.Show("Сервер вернул ошибку авторизации!\nЭто может быть следствием попытки авторизации\nранее чем через 20 секунд после последней.\nНеверное имя пользователя или пароль.\nЕсли Вы зарегистрировались, но забыли активировать\nучётную запись, то сделайте это и повторите попытку.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            rep = 0;
                        }
                        else
                        {
                            str = "";
                            str = done.Substring("<img src=\"pic/warning.gif\" title=\"", "\" alt=");

                            if (str == "Любитель")
                                rep = 1;
                            else if (str == "Профессионал")
                                rep = 2;
                            else if (str == "Эксперт")
                                rep = 3;
                            else if (str == "Порномастер")
                                rep = 4;
                            else if (str == "Киноруновец")
                                rep = 5;
                            else if (str == "VIP")
                                rep = 6;
                            else if (str == "Модер")
                                rep = 7;
                            else if (str == "Админ")
                                rep = 8;
                            else if (str == "Директор")
                                rep = 9;
                            else
                                rep = 0;

                        }
                    }
                    catch (xNet.HttpException ex)
                    {
                        var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        rep = 0;
                    }
                }
            }

            if (rep == 0)
                frmMain.authKin = false;
            else
            {
                frmMain.authKin = true;
            }

            //frmMain.TimeAutoriz = DateTime.Now;
            return rep;
        }

        public static string Searching(string search, string incldead, string cat, string sizeOn, string sizeOff)
        {
            //string link = "";
            string str = "";
            using (var net = new xNet.HttpRequest(site))
            {
                net.UserAgent = Http.ChromeUserAgent();

                //cookie = new CookieDictionary(false);
                net.Cookies = cookie;

                var urlString = HttpUtility.UrlEncode(search, Encoding.GetEncoding(1251));
                //net.CharacterSet = Encoding.GetEncoding("Windows-1251");
                net.AddUrlParam("search", urlString).AddUrlParam("incldead", incldead).AddUrlParam("cat", cat).AddUrlParam("size11", sizeOn).AddUrlParam("size12", "1").AddUrlParam("size21", sizeOff).AddUrlParam("size22", "1");
                string data = net.Get("/browse.php").ToString();

                str = data;
                str = data.Substring("<div id=\"torrents_go\">", "</td>\n\t<!--ЦЕНТР-->\n\t<!--ПРАВАЯ КОЛОНКА-->\n\t");
                //str = data.Substring("страниц</div>\n\t</td></tr>\n</table>\n</td></tr>\n<script type=\"text/javascript\" src=\"js/wz_tooltip.js\"></script>\n", "</div>\n\t</td>\n\t<!--ЦЕНТР-->\n\t<!--ПРАВАЯ КОЛОНКА-->\n\t");
                //link = str.Substring("<script", "</script>\n");
                //str = str.Replace("<script" + link + "</script>\n", "");
                //str = str.Replace(" onmouseout=\"UnTip()\"", "");
                //link = str.Substring("\n<td onmouseover=\"Tip", "align=\"left\">");
                //str = str.Replace("\n<td onmouseover=\"Tip" + link + "align=\"left\">", "");
                str = str.Replace("./", site);
                str = str.Replace("src=\"pic", "src=\"" + site + "pic");
                string html = System.Text.RegularExpressions.Regex.Replace(data, "<[^>]+>", string.Empty);
                return str;
            }

        }

        public static string[,] Admins()
        {
            //TimerAuth();
            string[,] strArr = new string[3,10];
            string str = "";
            using (var net = new xNet.HttpRequest(site))
            {
                //if (!isAuth())
                //{
                //    bool b = Auth(frmMain._KinoRun[0], frmMain._KinoRun[1]);
                //}
                net.UserAgent = Http.ChromeUserAgent();

                //cookie = new CookieDictionary(false);
                net.Cookies = cookie;
                string data = net.Get("/uthzy.php").ToString();
                str = data.Substring("<tr><td class=embedded colspan=11><b>" + "Модер", "<tr><td class=embedded colspan=11><b>Супер аплоадер");
                if (str == "")
                {
                    string str1 = data.Substring("<input type=\"submit\" value=\"", "\" class=");
                    if ("Вход!" == str1)
                    {
                        //var urlParams = new RequestParams();
                        //urlParams["username"] = frmMain._KinoRun[0];
                        //urlParams["password"] = frmMain._KinoRun[1];
                        //done = net.Post("/takelogin1.php", urlParams).ToString();
                        //str = done.Substring("<b>Ошибка входа</b><br />", "</div>");
                        //if (str == "Последняя попытка входа произведена менее 20 секунд назад.")
                        //{
                        //    done = str;
                        //    //tmrShow.Enabled = true;
                        //}
                        //else
                        //    frmMain.TimeAutoriz = DateTime.Now;

                        AuthKin(frmMain._KinoRun[0], frmMain._KinoRun[1]);

                        data = net.Get("/uthzy.php").ToString();
                        str = data.Substring("<tr><td class=embedded colspan=11><b>" + "Модер", "<tr><td class=embedded colspan=11><b>Супер аплоадер");
                    }
                    //else
                    //    frmMain.TimeAutoriz = DateTime.Now;

                }

                strArr = new string[3,10];
                for (int i = 0; i < 3; i++)
                {
                    string[] stat = new string[] { "Директор", "Админ", "Модер", "Супер аплоадер" };
                    //string cab = "";
                    //switch (i)
                    //{
                    //    case 0:
                    //        str = data.Substring("<tr><td class=embedded colspan=11><b>" + stat[i], stat[i + 1]);
                    //        break;
                    //    case 1:
                    //        str = "Админ";
                    //        break;
                    //    case 2:
                    //        str = "Модер";
                    //        break;
                    //}

                    str = data.Substring("<tr><td class=embedded colspan=11><b>" + stat[i], stat[i + 1]);

                    //if (i > 0)
                    //    str = data.Substring("<tr><td class=embedded colspan=11><b>" + stat[i], "<tr><td class=embedded colspan=11><b>Супер аплоадер");
                    //else
                    //    str = data.Substring("<tr><td class=embedded colspan=11><b>" + stat[i], "<tr><td class=embedded colspan=11><b>Супер аплоадер");
                    string strA = "";

                    for (int j = 0; j < 10; j++)
                    {
                        strA = str.Substring("title=", "/b></a></td>");
                        str = str.Replace("title=" + strA + "/b></a></td>", "");
                        string[] strL = strA.Split('"');

                        if (strL.Length > 1)
                        {
                            if (strL[1] == stat[i])
                                strA = strL[2].Substring(">", "<");
                            else
                            {
                                string[] strM = strL[strL.Length - 1].Split('<');
                                string str1 = strM[0].Trim('>', '<');
                                if (str1 != "")
                                    strA = str1;
                                else
                                    strA = strL[3];
                            }
                            strArr[i,j] = strA;
                        }
                        else
                        {
                            strArr[i,j] = "";
                            continue;
                        }
                    }
                }
            return strArr;
            }
        }

        public static string ModerSearching(string search)
        {
            string str = "";
            string str1 = "";
            string pages = "";
            int pgs = 0;
            using (var net = new xNet.HttpRequest(site))
            {
                net.UserAgent = Http.ChromeUserAgent();

                //cookie = new CookieDictionary(false);
                net.Cookies = cookie;

                var urlString = HttpUtility.UrlEncode(search, Encoding.GetEncoding(1251));
                net
                    .AddUrlParam("tag", urlString);
                string data = net.Get("/browse.php").ToString();

                str = data.Substring("<div class=\"paginator_pages\">", " страниц");

                if (str != "")
                    pgs = Convert.ToInt32(str);
                else
                    pgs = 0;

                //if (pgs > 5)
                //    pgs = 5;

                if (pgs > 1)
                {
                    for(int i=0; i < pgs; i++)
                    {
                        urlString = HttpUtility.UrlEncode(search, Encoding.GetEncoding(1251));
                        net
                            .AddUrlParam("tag", urlString)
                            .AddUrlParam("size11", "-1")
                            .AddUrlParam("size12", "1")
                            .AddUrlParam("size21", "-1")
                            .AddUrlParam("size22", "1")
                            .AddUrlParam("page", Convert.ToString(i));
                        data = net.Get("/browse.php").ToString();
                        str = data.Substring("<div id=\"torrents_go\">", "</td>\n\t<!--ЦЕНТР-->\n\t<!--ПРАВАЯ КОЛОНКА-->\n\t");
                        pages = pages + str;
                        str1 = pages.Substring("<td class=\"index\" colspan=\"12\">\n\n<script type=\"text/javascript\">\n\t$j = jQuery;\n</script>\n\n<script type=\"text/javascript\">\n\t\t$j(document).ready(function (){\n\t\tpaginator_example = new Paginator(\n\t\t\t'paginator_div_1',                           // id контейнера, куда ляжет пагинатор\n\t\t\t", "Раздает</a></td>\n</tr>\n");
                        pages = pages.Replace("<td class=\"index\" colspan=\"12\">\n\n<script type=\"text/javascript\">\n\t$j = jQuery;\n</script>\n\n<script type=\"text/javascript\">\n\t\t$j(document).ready(function (){\n\t\tpaginator_example = new Paginator(\n\t\t\t'paginator_div_1',                           // id контейнера, куда ляжет пагинатор\n\t\t\t" + str1 + "Раздает</a></td>\n</tr>\n", "");
                    }
                }
                else
                {
                    pages = data.Substring("<div id=\"torrents_go\">", "</td>\n\t<!--ЦЕНТР-->\n\t<!--ПРАВАЯ КОЛОНКА-->\n\t");
                }

                str = "<div class=\"paginator_pages\">" + Convert.ToString(pgs) + " страниц</div>";
                str1 = "<div class=\"paginator_pages\">" + Convert.ToString(pgs + 1) + " страниц</div>";
                pages = pages.Replace(str, "");
                pages = pages.Replace(str1, "");
                pages = pages.Replace("<td onmouseover=\"Tip('<img src=http://picpicture.com/images/2016/09/01/046eefb52d6a78b0fffd23c55afe7f28.jpg width=200>', 300, 600, PADDING, 1, 'red', 'red')\" onmouseout=\"UnTip()\" align=\"left\"><a href=\"  ./torrent-68543-sladkie-i-jarkie-sweetness-and-light-b-skowskow-for-girlfriends-films-2014-dvdrip.html\"><b>Сладкие и Яркие / Sweetness And Light [B. Skow/Skow for Girlfriends Films] / 2014 / DVDRip</b></a> \n    <script language=\"JavaScript\" type=\"text/javascript\">\n    /*<![CDATA[*/\n    (function($){\n        bookmark = function(id){\n            $.post(\"bookmark.php\",{\"id\":id},function(response){\n                alert(response);\n            });\n        }\n    })(jQuery);\n    /*]]>*/\n    </script>\n    <a href=\"javascript:bookmark('68543')\">\n        <img border=\"0\" src=\"pic/bookmark.gif\" alt=\"В закладки\" title=\"В закладки\" />\n    </a>\n", "");
                pages = pages.Replace("./", site);
                pages = pages.Replace("src=\"pic", "src=\"" + site + "pic");

                return pages;
            }

        }

        public static string[,] List(string Searching)
        {
            int count = 0;
            count = ArraySize(Searching);
            string[,] list = new string[2,count];
            string str, data;

            using (var net = new xNet.HttpRequest(site))
            {
                net.UserAgent = Http.ChromeUserAgent();

                //cookie = new CookieDictionary(false);
                net.Cookies = cookie;

                net.AddUrlParam("search").AddUrlParam("x", "34").AddUrlParam("y", "6");
                data = net.Get("/browse.php").ToString();

                str = data.Substring("<select name=\"" + Searching + "\">", "</select>");
                str = str.Replace("</option>", "");
                str = str.Replace("<option value=\"", "");
                str = str.Replace("\n0", "0");
                str = str.Replace("&nbsp;", " ");
                str = str.Replace(">", "");
                str = str.Replace(" selected", "");

                string[] sstr = str.Split('\n');

                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        string[] lst = sstr[i].Split('"');
                        list[0,i] = lst[0];
                        list[1,i] = lst[1];
                    }
                    catch
                    {

                    }
                }

                return list;
            }
        }

        private static int ArraySize(string Searching)
        {
            //int count = 0;
            string str;

            using (var net = new xNet.HttpRequest(site))
            {
                net.UserAgent = Http.ChromeUserAgent();

                //cookie = new CookieDictionary(false);
                net.Cookies = cookie;

                net.AddUrlParam("search").AddUrlParam("x", "34").AddUrlParam("y", "6");
                string data = net.Get("/browse.php").ToString();

                str = data.Substring("<select name=\"" + Searching +"\">", "</select>");
                str = str.Replace("</option>", "");
                str = str.Replace("<option value=\"", "");
                str = str.Replace("\n0", "0");
                str = str.Replace("&nbsp;", " ");
                str = str.Replace(">", "");
                str = str.TrimEnd('\n');

                string[] sstr = str.Split('\n');

                return sstr.Length;
            }
        }

        public static string UplTorrent(string Torrent, string Name, string Poster, string Description, string Tags, string Category)
        {
            string data = "<!DOCTYPE HTML>\n<html>\n <head> \n  <meta charset=\"utf - 8\"><style type=\"text/css\"><!--\nhtml { background-color:#eee;padding:0px;margin:0px;min-width:1200px;font-family:Calibri, sans-serif;font-size:14px; }\nbody { padding:0px;margin:0px;font:normal 14px Calibri;color:#595959;background:#efefef; }\n--></style></head><body><table  style=\"width: 100%\">\n<h2 style=\"vertical - align: middle; color: rgb(255, 0, 51)\"><span style=\"text-align: center; display: block;height: 100%;width:100%; position: absolute;\">";
            string depAnswer = "no";
            string Deposit = "300";

            //auth = Auth;
            //login = Login;
            //password = Password;

            using (var net = new xNet.HttpRequest(site))
            {
                net.UserAgent = Http.ChromeUserAgent();

                net.Cookies = cookie;
                string[] torr_f = Torrent.Split('\\');
                //string timestamp = Convert.ToString(ConvertToUnixTimestamp(DateTime.UtcNow));
                //var multipartContent = new MultipartContent();

                //if(Deposit != "")
                //{
                //    depAnswer = "yes";
                //    Deposit = "300";
                //}
                //var name = HttpUtility.UrlEncode(Name, Encoding.GetEncoding(1251));
                //var description = HttpUtility.UrlEncode(Description, Encoding.GetEncoding(1251));

                //Encoding srcEncodingFormat = Encoding.GetEncoding("UTF-8");
                //byte[] originalByteString = srcEncodingFormat.GetBytes(Name);
                //var name = Encoding.Default.GetString(originalByteString);
                //var name = UTF8ToWin1251(Name);
                //name = HttpUtility.UrlEncode(Name, Encoding.GetEncoding(1251));

                net.CharacterSet = Encoding.GetEncoding(1251);

                //var name = Win1251ToUTF8(Name);
                //var name = HttpUtility.UrlEncode(Name, Encoding.GetEncoding(1251));

                //string description = HttpUtility.UrlEncode(Description, Encoding.GetEncoding(1251));
                try
                {
                    //var multipartContent = new MultipartContent()
                    //{
                    //    {new StringContent("1000000"), "MAX_FILE_SIZE"},
                    //    {new FileContent(Torrent), "tfile", torr_f[torr_f.Length - 1], "application/x-bittorrent"},
                    //    {new StringContent(Name), "name"},
                    //    {new StringContent(Poster), "image0"},
                    //    //{new FileContent(""), "image0", "", "application/octet-stream"},
                    //    {new StringContent(AdditionFunc.UTF8ToWin1251(Description)), "descr"},
                    //    {new StringContent(""), "oldtags"},
                    //    {new StringContent(Tags), "tags"},
                    //    {new StringContent(Category), "type"},
                    //    {new StringContent(depAnswer), "setZ"},
                    //    {new StringContent(Deposit), "zoll"}
                    //};
                    //data = net.Post("/takeupload.php", multipartContent).ToString();
                    string str = Poster.Substring("http://", "/");
                    if (str.Length > 3)
                    {
                        net
                            .AddField("MAX_FILE_SIZE", "1000000")
                            .AddFile("tfile", Torrent)
                            .AddField("name", Name)
                            .AddField("image0", Poster)
                            .AddField("descr", Description)
                            .AddField("oldtags", "")
                            .AddField("tags", Tags)
                            .AddField("type", Category)
                            .AddField("setZ", depAnswer)
                            .AddField("zoll", Deposit);
                    }
                    else
                    {
                        net
                            .AddField("MAX_FILE_SIZE", "1000000")
                            .AddFile("tfile", Torrent)
                            .AddField("name", Name)
                            .AddField("image0", "")
                            .AddFile("image0", Poster)
                            .AddField("descr", Description)
                            .AddField("oldtags", "")
                            .AddField("tags", Tags)
                            .AddField("type", Category)
                            .AddField("setZ", depAnswer)
                            .AddField("zoll", Deposit);
                    }

                    data = net.Post("/takeupload.php").ToString();
                    data = data.Replace("./", site);
                    data = data.Replace("src=\"pic", "src=\"" + site + "pic");
                    data = data.Replace("href=\"" + site + "\"", "href=\"");
                    data = data.Replace("href=\"/", "href=\"");
                    data = data.Replace("href=\"", "href=\"" + site);
                    //data = data.Replace("href=\"viewimage.php", "href=\"" + site + "viewimage.php");
                    data = data.Replace("viewimage.php", site + "viewimage.php");
                    data = data.Replace("thumbnail.php", site + "thumbnail.php");
                    //data = data.Substring("<script type=\"text/javascript\" src=\"http://pketred.com/90lca/54d7835e9e7/269/fb.js\"></script>", "<script type=\"text/javascript\">\nfunction SE_SayThanks(id)");

                    str = data.Substring("Такой торрент уже загружен!", "\n");
                    if (str == "</p>")
                        data = "Такой торрент уже загружен!";
                }
                catch (ArgumentNullException ex)
                {
                     var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    data = data + "О Ш И Б К А ! <br><br>" + ex.Message + "</span></h2>" + "\n</tadle>\n</body>\n</html>";
               }
                catch (ArgumentException ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    data = data + "О Ш И Б К А ! <br><br>" + ex.Message + "</span></h2>" + "\n</tadle>\n</body>\n</html>";
                }
                catch (xNet.HttpException ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    data = data + "О Ш И Б К А ! <br><br>" + ex.Message + "</span></h2>" + "\n</tadle>\n</body>\n</html>";
                }
                catch (Exception ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    data = data + "О Ш И Б К А ! <br><br>" + ex.Message + "\n</tadle>\n</body>\n</html>";
                }

                return data;
            }
        }

        //public static void DownTorrent(string Login, string Password, string Link)
        //{
        //    using (var net = new xNet.HttpRequest(site))
        //    {
        //        login = Login;
        //        password = Password;
        //        string filePath = Application.StartupPath + "\\torrent\\";

        //        if (!Directory.Exists(filePath))
        //        {
        //            DirectoryInfo di = Directory.CreateDirectory(filePath);
        //        }

        //        string id = Link.Substring("id=", "&");
        //        string name = Link.Substring("name=", "torrent") + "torrent";
        //        string fileName = name.Replace("%20", " ");

        //        net.UserAgent = Http.ChromeUserAgent();
        //        cookie = new CookieDictionary(false);

        //        net.Cookies = cookie;

        //        net.AddUrlParam("id", id);
        //        done = net.Get("/details.php").ToString();
        //        string str = done.Substring("<input type=\"submit\" value=\"", "\" class=");
        //        if("Вход!" == str)
        //        {
        //            var urlParams = new RequestParams();
        //            urlParams["username"] = Login;
        //            urlParams["password"] = Password;

        //            net.Post("/takelogin1.php", urlParams).None();
        //        }

        //        net.AddUrlParam("id", id).AddUrlParam("name", name);

        //        Stream response = net.Get("/download.php").ToMemoryStream();
        //        using (Stream file = File.Create(filePath + fileName))
        //        {
        //            response.Position = 0;
        //            response.CopyTo(file);
        //            file.Close();
        //        }

        //        string commandText = filePath + fileName;
        //        var proc = new System.Diagnostics.Process();
        //        proc.StartInfo.FileName = commandText;
        //        proc.StartInfo.UseShellExecute = true;
        //        proc.Start();
        //    }
        //}

        public static bool TorrentDown(string Login, string Password, string Link)
        {
            //TimerAuth();
            bool bl = false;
            if (Link != "")
            {
                using (var net = new xNet.HttpRequest(site))
                {
                    login = Login;
                    password = Password;
                    string id = Link.Substring("id=", "&");
                    string name = Link.Substring("name=", "torrent") + "torrent";
                    string fileName = name.Replace("%20", " ");
                    fileName = fileName.Replace("%28", "(");
                    fileName = fileName.Replace("%29", ")");
                    fileName = fileName.Replace("%26", "&");
                    fileName = fileName.Replace("%2C", ",");
                    fileName = fileName.Replace("%2D", "-");
                    fileName = fileName.Replace("%2E", ".");

                    string filePath = frmMain.SetingPath + @"torrent\";

                    if (!Directory.Exists(filePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                    }

                    net.UserAgent = Http.ChromeUserAgent();
                    //cookie = new CookieDictionary(false);
                    net.Cookies = cookie;
                    net.CharacterSet = Encoding.GetEncoding(1251);

                    done = "";
                    net.AddUrlParam("id", id);
                    done = net.Get("/details.php").ToString();
                    string str = done.Substring("<input type=\"submit\" value=\"", "\" class=");

                    var urlParams = new RequestParams();
                    if ("Вход!" == str)
                    {
                        //urlParams["username"] = Login;
                        //urlParams["password"] = Password;
                        AuthKin(Login, Password);
                        done = net.Post("/takelogin1.php", urlParams).ToString();

                        net.AddUrlParam("id", id);
                        done = net.Get("/details.php").ToString();
                    }

                    str = done.Substring("<input type=\"submit\" value=\"", "\" class=");
                    if ("Вход!" != str)
                    {
                        net.AddUrlParam("id", id).AddUrlParam("name", name);

                        Stream response = net.Get("/download.php").ToMemoryStream();
                        using (Stream file = File.Create(filePath + fileName))
                        {
                            response.Position = 0;
                            response.CopyTo(file);
                            file.Close();
                        }

                        string commandText = filePath + fileName;
                        var proc = new System.Diagnostics.Process();
                        proc.StartInfo.FileName = commandText;
                        proc.StartInfo.UseShellExecute = true;
                        proc.Start();
                        bl = true;
                    }
                    else
                    {
                        var msg = MessageBox.Show("Не удалось загрузить торрент файл в клиент.\n\nПовторить попытку?", 
                            "Ошибка", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Warning);
                        if (msg == DialogResult.Yes)
                        {
                            AuthKin(Login, Password);
                            TorrentDown(Login, Password, Link);
                        }
                        else
                        {
                        msg = MessageBox.Show("Не удалось загрузить торрент файл в клиент. Перейдите на страницу своей раздачи и скачайте его вручную", 
                            "Ошибка", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning);
                        }
                    }
                    //str = done.Substring("<input type=\"submit\" value=\"", "\" class=");

                    //if ("Вход!" == str)
                    //{
                    //    urlParams = new RequestParams();
                    //    urlParams["username"] = Login;
                    //    urlParams["password"] = Password;
                    //    done = net.Post("/takelogin1.php", urlParams).ToString();
                    //    net.AddUrlParam("id", id);
                    //    done = net.Get("/details.php").ToString();
                    //}
                    //else
                    //{

                    //    net.AddUrlParam("id", id).AddUrlParam("name", name);

                    //    Stream response = net.Get("/download.php").ToMemoryStream();
                    //    using (Stream file = File.Create(filePath + fileName))
                    //    {
                    //        response.Position = 0;
                    //        response.CopyTo(file);
                    //        file.Close();
                    //    }

                    //    string commandText = filePath + fileName;
                    //    var proc = new System.Diagnostics.Process();
                    //    proc.StartInfo.FileName = commandText;
                    //    proc.StartInfo.UseShellExecute = true;
                    //    proc.Start();
                    //    bl = true;
                    //}
                }
            }
            return bl;
        }

        private static void tmrShow_Tick(object sender, EventArgs e)
        {
            AuthKin(login, password);
        }
    }
}
