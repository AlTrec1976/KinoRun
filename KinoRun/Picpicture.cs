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

namespace KinoRun
{
    class Picpicture
    {
        public static string done = "";
        //public static string url_viewer, images, thumb;
        static bool access = true;
        static string login, password, token;
        const string pic = "http://picpicture.com/";
        static CookieDictionary cookie;
        string[] _PicPicture = new string[2];

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

        public static bool AuthPic(string Login, string Password)
        {
            using (var net = new HttpRequest(pic))
            {
                login = Login;
                password = Password;
                net.UserAgent = Http.ChromeUserAgent();
                cookie = new CookieDictionary(false);

                net.Cookies = cookie;
                try
                {
                    string data = net.Get("/").ToString();
                    token = data.Substring("name=\"auth_token\" value=\"", "\"");

                    var urlParams = new RequestParams();

                    urlParams["auth_token"] = token;
                    urlParams["login-subject"] = Login;
                    urlParams["password"] = Password;

                    done = net.Post("/login", urlParams).ToString();

                    string str = done.Substring("Неверное имя пользователя или пароль");

                    if(str.Length > 0)
                    {
                        var msg = MessageBox.Show("Неверное имя пользователя или пароль.\nЕсли Вы зарегистрировались, но забыли активировать\nучётную запись, то сделайте это и повторите попытку.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch(HttpException ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

            }
        }

        public static bool RegPic(string eMail, string Login, string Password)
        {
            bool rep = false;
            using (var net = new HttpRequest(pic))
            {
                string[] error = new string[3];
                login = Login;
                password = Password;
                net.UserAgent = Http.ChromeUserAgent();
                cookie = new CookieDictionary(false);

                net.Cookies = cookie;
                try
                {
                    string data = net.Get("/").ToString();
                    token = data.Substring("name=\"auth_token\" value=\"", "\"");

                    var urlParams = new RequestParams();

                    urlParams["auth_token"] = token;
                    urlParams["email"] = eMail;
                    urlParams["username"] = Login;
                    urlParams["password"] = Password;

                    done = net.Post("/signup", urlParams).ToString();

                    string str = done.Substring("Ваша учетная запись почти готова");

                    if (str.Length > 0)
                    {
                        var msg = MessageBox.Show("Ваша учетная запись почти готова.\nИнструкции по активации вашего аккаунта были отправлены на " + eMail + ".\nСсылка для активации действительна в течение 48 часов.\nЕсли вы не получили сообщение проверьте папку Спам.", "Регистрация успешна.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        rep = true;
                    }
                    else
                    {
                        error[0] = data.Substring("input-warning red-warning\">", "</span");
                        str = done.Substring("input-warning red-warning\">");
                        error[1] = str.Substring("input-warning red-warning\">", "</span");
                        str = str.Substring("input-warning red-warning\">");
                        error[2] = str.Substring("input-warning red-warning\">", "</span");

                        str = ((error[0] != "") ? (error[0] + "\n") : "") + ((error[1] != "") ? (error[1] + "\n") : "") + ((error[2] != "") ? (error[2] + "\n") : "");
                    
                        var msg = MessageBox.Show("Сервер вернул следующие ошибки:\n" + str + "Исправьте указанные ошибки и повтормте снова.\n\nТакже можете попробывать зарегистрироваться на сайте\nhttp://picpicture.com/signup", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        net.Close();
                        rep = false;
                    }
                }
                catch (HttpException ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    rep = false;
                }

            }
            return rep;
        }

        public static string[] UplPic(string Url)
        {
            string[] link = new string[3];
            string  upl = "";
            using (var net = new HttpRequest(pic))
            {
                net.UserAgent = Http.ChromeUserAgent();

                net.Cookies = cookie;

                string[] url_pr = Url.Split('/');
                string[] url_f = Url.Split('\\');
                string timestamp = Convert.ToString(ConvertToUnixTimestamp(DateTime.UtcNow));
                var multipartContent = new MultipartContent();
                try
                {
                    if (url_pr.Length > 1)
                    {
                        multipartContent = new MultipartContent()
                        {
                            {new StringContent("url"), "type"},
                            {new StringContent("upload"), "action"},
                            {new StringContent("public"), "privacy"},
                            {new StringContent(timestamp), "timestamp"},
                            {new StringContent(token), "auth_token"},
                            {new StringContent("null"), "category_id"},
                            {new StringContent("0"), "nsfw"},
                            {new StringContent(Url), "source"}
                        };
                    }
                    else
                    {
                        multipartContent = new MultipartContent()
                        {
                            {new StringContent("file"), "type"},
                            {new StringContent("upload"), "action"},
                            {new StringContent("public"), "privacy"},
                            {new StringContent(timestamp), "timestamp"},
                            {new StringContent(token), "auth_token"},
                            {new StringContent("null"), "category_id"},
                            {new StringContent("0"), "nsfw"},
                            {new FileContent(Url), "source", url_f[url_f.Length - 1], "image/png"}
                        };
                    }
                }
                catch (ArgumentNullException ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (ArgumentException ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (Exception ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                try
                {
                    upl = net.Post("/json", multipartContent).ToString();
                    //f = Convert.ToString(net.Response.StatusCode);

                    var json = JsonConvert.DeserializeObject<ImageUrl>(upl);

                    if (json.status_code == 200)
                    {
                        link[0] = json.image.url;
                        link[1] = json.image.url_viewer;
                        link[2] = json.image.thumb.url;

                    }
                }
                catch (HttpException ex)
                {
                    var req = net.Response.ToString();
                    req = req.Substring("\"error\":{", "}");
                    string[] st = req.Split(',');
                    req = req.Substring("\"message\":\"", "\",");
                    req = System.Text.RegularExpressions.Regex.Unescape(req);

                    if (req == "Повторяющаяся загрузка")
                    {
                        var msg = MessageBox.Show("Вы пытаетесь повторно загрузить файл изображения.\nОбращаем Ваше внимание на то, что проверка на дубликаты скриптом Хранилища изображений выполняется не по имени файла, а по контрольной сумме, что исключает ошибку и захламление хранилища.\n\nПерейдите в свой профиль на http://picpicture.com/ и используйте коды от соответствующего изображения, либо удалите там его файл для повторной загрузки через KinoRun Torrent Builder.",
                            req,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                    }
                    else
                    {
                        var msg = MessageBox.Show(req,
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                    }
                    //var msg = MessageBox.Show("Вы пытаетесь повторно загрузить файл изображения.\nПерейдите в свой профиль на http://picpicture.com/ и используйте коды от соответствующего изображения, либо удалите там его файл для повторной загрузки через KinoRun Torrent Builder.\nОбращаем Ваше внимание на то, что проверка на дубликаты скриптом Хранилища изображений выполняется не по имени файла, а по контрольной сумме, что исключает ошибку и захламление хранилища.\n\nВы уверены, что загружаемый файл не является дубликатом?",
                    //    "Ошибка",
                    //    MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Stop);
                    //if (msg == DialogResult.Yes)
                    //{
                    //    string fpath = "";
                    //    string[] fname = url_f[url_f.Length - 1].Split('.');
                    //    if (fname.Length > 2)
                    //    {
                    //        string sstr = "";
                    //        foreach (string s in fname)
                    //        {
                    //            if ((s != fname[0]) && (s != fname[fname.Length - 1]))
                    //                sstr = sstr + s;
                    //        }
                    //        fname[0] = fname[0] + sstr;
                    //    }
                    //    //else
                    //    //    ;

                    //    string rand = DateTime.Now.ToString();
                    //    rand = rand.Replace(".", "");
                    //    rand = rand.Replace(":", "");
                    //    rand = rand.Replace(" ", "");

                    //    fpath = fname[0] + rand + "." + fname[fname.Length - 1];
                    //    fpath = Url.Replace(url_f[url_f.Length - 1], fpath);
                    //    File.Copy(Url, fpath);
                    //    File.Delete(Url);

                    //    access = false;
                    //    UplPic(fpath);
                    //}
                }
                catch (Exception ex)
                {
                    var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    AuthPic(login, password);
                    UplPic(Url);
                }
                return link;
            }
        }
    }
}
