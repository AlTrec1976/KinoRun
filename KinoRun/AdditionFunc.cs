using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using xNet;
using System.Security.Cryptography;

namespace KinoRun
{
    class AdditionFunc
    {
        public static string Calculate(string strTimes)
        {
            string[] sTime = new string[10];
            string iTimes = "";
            string[] sStr = new string[3];
            string str = "";
            string h = "";
            string m = "";
            string s = "";
            int hore = 0;
            int minute = 0;
            int secund = 0;

            iTimes = "";

            strTimes = strTimes.Replace("; ", " ");
            strTimes = strTimes.Replace(";", " ");
            if (strTimes.Length > 0)
            {
                str = strTimes.Trim();
                sTime = str.Split(' ');

                for (int i = 0; i < sTime.Length; i++)
                {
                    try
                    {
                        sStr = sTime[i].Split(':');

                        if (i > sStr.Length)
                            Array.Resize(ref sStr, sTime.Length);

                        secund = secund + Convert.ToInt32(sStr[2]);
                        if (secund > 60)
                        {
                            secund = secund - 60;
                            minute = minute + 1;
                        }

                        minute = minute + Convert.ToInt32(sStr[1]);
                        if (minute > 60)
                        {
                            minute = minute - 60;
                            hore = hore + 1;
                        }

                        hore = hore + Convert.ToInt32(sStr[0]);

                        if (iTimes != "")
                            iTimes = iTimes + "; " + sTime[i];
                        else
                            iTimes = sTime[i];
                    }
                    catch
                    {
                        if (!frmMain.Imp)
                            MessageBox.Show("Проверьте, пожалуйста, разделители\nи замените их на пробелы.\nТаже возможно, что вместо цифр во времени\nиспользованы буквы или другие символы.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }

                }
                if (hore < 10)
                    h = "0" + Convert.ToString(hore);
                else
                    h = Convert.ToString(hore);

                if (minute < 10)
                    m = "0" + Convert.ToString(minute);
                else
                    m = Convert.ToString(minute);

                if (secund < 10)
                    s = "0" + Convert.ToString(secund);
                else
                    s = Convert.ToString(secund);

                str = h + ":" + m + ":" + s;
            }
            return str;
        }

        public static string DigitalCompl(string Line)
        {
            int i = 0;
            string strInp = Line.Replace(" ", "");
            strInp = "";
            foreach (char ch in Line)
            {
                int number;
                if (Int32.TryParse(Convert.ToString(ch), out number))
                    strInp = strInp + ch;
                i++;
            }
            return strInp;
        }

        /// <summary>
        /// Этот метод передаёт ссылку на картинку и возвращает готовую ссылку picpicture.com для оформления раздачи.
        /// </summary>
        /// <param name="Link">Ссылка на картинку или путь до неё</param>
        /// <param name="Type">Тип возвращаемой ссылки ("p" для поста и что угодно для скриншотов)</param>
        /// <returns>Готовая ссылка на картинку, залитую на picpicture.com</returns>

        public static string ImageLink(string Link, string Type)
        {
            string type = " постера";
            string[] lnk = new string[3];
            string[] name = Link.Split('\\');
            string[] name1 = Link.Split('\\');
            string str = name[name.Length - 1];

            //if (name.Length > 1) //транслитерация имени файла
            //{
            //    str = Link.Replace(Link, Translitter(Link));
            //    Link = Translitter(Link);
            //}
            //name[name.Length - 1] = Translitter(str);
            //Array.Clear(name, 0, name.Length);
            //name = str.Split('.');
            //string ras = name[name.Length - 1];
            //str = str.Replace(ras, "");
            //str = str.Replace(".", "_");
            //str = str.Trim('_') + "." + ras;

            //Link = Translitter(Link.Replace(name1[name1.Length - 1], str));
            Link = Translitter(Link);
            //Link = Link.Replace(Link, str);

            str = "";

            if ((null != Link) || (Link.Length > 10))
            {
                lnk = Picpicture.UplPic(Link);
                if (Type == "p")
                {
                    if (lnk[0] != null)
                        str = lnk[0];
                }
                else
                {
                    if (lnk[0] != null)
                        str = "[url=" + lnk[1] + "][img]" + lnk[2] + "[/img][/url]";
                    type = "(ов) скриншотов";
                }
            }
            if (str == "")
            {
                var msg = MessageBox.Show("Загрузка файла" + type + " неудачна!\nУстраните имеющуюся причину и повторите загрузку снова.",
                    "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
            return str;
        }

        //public static String[] ImageLinks(string[] Links)
        //{
        //    return 
        //}

        public static string Win1251ToUTF8(string source)
        {

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;

        }

        public static string UTF8ToWin1251(string sourceStr)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] utf8Bytes = utf8.GetBytes(sourceStr);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            return win1251.GetString(win1251Bytes);
        }

        //public static bool CleanFilesAndDirectories(System.IO.DirectoryInfo directory)
        //{
        //    bool del = false;
        //    foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
        //    foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        //    if(!directory.Exists)
        //    {
        //        del = true;
        //    }
        //    return del;
        //}

        public static bool DirCopy(string begin_dir, string end_dir)
        {
            bool rez = false;
            //Берём нашу исходную папку
            DirectoryInfo dir_inf = new DirectoryInfo(begin_dir);
            try
            {
                //Перебираем все внутренние папки
                foreach (DirectoryInfo dir in dir_inf.GetDirectories())
                {
                    //Проверяем - если директории не существует, то создаём;
                    if (Directory.Exists(end_dir + "\\" + dir.Name) != true)
                    {
                        Directory.CreateDirectory(end_dir + "\\" + dir.Name);
                    }

                    //Рекурсия (перебираем вложенные папки и делаем для них то-же самое).
                    DirCopy(dir.FullName, end_dir + "\\" + dir.Name);
                }

                //Перебираем файлики в папке источнике.
                foreach (string file in Directory.GetFiles(begin_dir))
                {
                    //Определяем (отделяем) имя файла с расширением - без пути (но с слешем "\").
                    string filik = file.Substring(file.LastIndexOf('\\'), file.Length - file.LastIndexOf('\\'));
                    //Копируем файлик с перезаписью из источника в приёмник.
                    File.Copy(file, end_dir + "\\" + filik, true);
                }
                rez = true;
            }
            catch(Exception ex)
            {
                var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rez = false;
            }

            return rez;
        }

        //Составляет список папок в указанной директории
        public static string[] ListDir(string Dir)
        {
            string[] list = new string[1] { "Выбрать" };
            int k = 0;

            System.IO.DirectoryInfo dir_inf = new System.IO.DirectoryInfo(Dir);

            if (System.IO.Directory.Exists(Dir))
            {
                foreach (System.IO.DirectoryInfo dir in dir_inf.GetDirectories())
                {
                    k++;
                    Array.Resize(ref list, k + 1);
                    list[k] = dir.Name;
                }
            }

            return list;
        }

        public static bool ClearDirTorrent(string Directory)
        {
            bool del = false;

            try
            {
                if (System.IO.Directory.Exists(Directory))
                {
                    System.IO.Directory.Delete(Directory, true);
                    del = true;
                }
                else
                    del = true;

                if (!System.IO.Directory.Exists(Directory))
                    System.IO.Directory.CreateDirectory(Directory);
            }
            catch (Exception ex)
            {
                var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                del = false;
            }

            try
            {
                if (!System.IO.Directory.Exists(Directory))
                {
                    del = true;
                }
            }
            catch (Exception ex)
            {
                var msg = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                del = false;
            }

            return del;
        }

        public static string TagBuild(string TagsStr)
        {
            string tags = "";
            string[] strGenre = new string[190] { ".+18", ".boss1970", ".delikates", ".everlast", ".liza_1", ".nn99nn", "1080 HD", "3dporn", "4k", "720 HD", "All Girl", "All Sex", "Amateur", "Anal", "Anilingus", "Animals (Zoo)", "Anime", "Asian", "Ass Licking", "Ass to Mouth", "Bbw", "Bdsm", "Bdwc", "Bestiality", "Bi (Bisexual)", "Big Ass", "Big Boobs", "Big Cock", "Big Dick", "Big Tits", "Binding", "Bisexual", "Bizarre", "Black", "Blondes", "Blowjob", "Bondage", "Brunette", "Bukkake", "Busty", "Casting", "Classic", "Compilation", "Copro", "Cosplay", "Couples", "Creampie", "Cumschot", "Deep Throat", "Deepthroat", "Defloration", "Dildo", "Doctor", "Documentary XXX", "Doggystyle", "Dogs", "Domination", "Double Anal Penetration", "Double Oral", "Double Penetration", "Drunk", "Erotic", "Facesitting", "Facial", "Family Roleplay", "Fat", "Feature", "Femdom", "Fetish", "Fingering", "Fisting", "Flog", "Foot Fetish", "Footjob", "Foto", "Foursome", "Fuck Machine", "Fucking Machines", "Gags", "Gang Bang", "Gaping", "Gay (Homosexual)", "Girl/Girl (Lesbian)", "Goat", "Gonzo", "Grany", "Group Footjob", "Group Sex", "Gyno", "Hairy", "Hardcore", "Home Sex", "Horror", "Horse", "Incest", "Indian", "Interracial", "Latinos", "Lesbian", "Lingerie", "Massage", "Masturbation", "Mature", "Medical", "Midgets", "Milf", "Natural Tits", "Oiled", "Old", "Old Fart", "Oral", "Orgy", "Outdoor", "Pack", "Pantyhose", "Parody", "Peeping", "Perversion", "Photosession", "Pickup", "Pin Up Porno", "Pippin", "Piss", "Pornostar", "Posing", "Pov", "Pregnant", "Publik", "Pumping", "Pussy Licking", "Pygmy", "Rape", "Retro", "Romance", "Russian", "Scat", "Scuba Sex", "Sex Machines", "Sexmachines", "Shemale", "Sleeping", "Small", "Small Boobs", "Solo", "Spanking", "Sports Sex", "Squirt", "Squirting", "Straight", "Strap-On", "Strapping", "Striptease", "Students", "Swallowing", "Swingers", "Taboo", "Tattooed", "Teeny", "Thin", "Threesome", "Tit Fucking", "Titjob", "Titty Fucking", "Torture", "Toys", "Training", "Tranny", "Triple Anal Penetration", "Triple Penetration", "Underwater Sex", "Uniform", "Uniforms", "Vintage", "Virgin", "Virgins", "Voyeurism", "War Porn", "Wives", "X-Mas", "Zooskool", "Жены", "Массаж", "Подглядывание", "Порно с русским переводом", " ", " ", " ", " ", " ", " " };
            //string[] strGenre = new string[9] { ".+18", ".boss1970", ".delikates", "teens", ".liza_1", ".nn99nn", "1080 HD", "oral", "4k" };
            string[] tagGenre = new string[190] { ".+18", ".boss1970", ".delikates", ".everlast", ".liza_1", ".nn99nn", "1080 hd", "3dporn", "4k", "720 hd", "all girl", "all sex", "amateur", "anal", "anilingus", "animals (zoo)", "anime", "asian", "ass licking", "ass to mouth", "bbw", "bdsm", "bdwc", "bestiality", "bi (bisexual)", "big ass", "big boobs", "big cock", "big dick", "big tits", "binding", "bisexual", "bizarre", "black", "blondes", "blowjob", "bondage", "brunette", "bukkake", "busty", "casting", "classic", "compilation", "copro", "cosplay", "couples", "creampie", "cumschot", "deep throat", "deepthroat", "defloration", "dildo", "doctor", "documentary xxx", "doggystyle", "dogs", "domination", "double anal penetration", "double oral", "double penetration", "drunk", "erotic", "facesitting", "facial", "family roleplay", "fat", "feature", "femdom", "fetish", "fingering", "fisting", "flog", "foot fetish", "footjob", "foto", "foursome", "fuck machine", "fucking machines", "gags", "gang bang", "gaping", "gay (homosexual)", "girl/girl (lesbian)", "goat", "gonzo", "grany", "group footjob", "group sex", "gyno", "hairy", "hardcore", "home sex", "horror", "horse", "incest", "indian", "interracial", "latinos", "lesbian", "lingerie", "massage", "masturbation", "mature", "medical", "midgets", "milf", "natural tits", "oiled", "old", "old fart", "oral", "orgy", "outdoor", "pack", "pantyhose", "parody", "peeping", "perversion", "photosession", "pickup", "pin up porno", "pippin", "piss", "pornostar", "posing", "pov", "pregnant", "publik", "pumping", "pussy licking", "pygmy", "rape", "retro", "romance", "russian", "scat", "scuba sex", "sex machines", "sexmachines", "shemale", "sleeping", "small", "small boobs", "solo", "spanking", "sports sex", "squirt", "squirting", "straight", "strap-on", "strapping", "striptease", "students", "swallowing", "swingers", "taboo", "tattooed", "teeny", "thin", "threesome", "tit fucking", "titjob", "titty fucking", "torture", "toys", "training", "tranny", "triple anal penetration", "triple penetration", "underwater sex", "uniform", "uniforms", "vintage", "virgin", "virgins", "voyeurism", "war porn", "wives", "x-mas", "zooskool", "жены", "массаж", "подглядывание", "порно с русским переводом", " ", " ", " ", " ", " ", " " };
            string[] pars = TagsStr.Split(',');

            for(int i = 0; i < pars.Length; i++)
            {
                //string str = "";
                int ind = 0;
                pars[i] = pars[i].Trim();

                do 
                {
                    if (ind < strGenre.Length - 1)
                        ind++;
                    else
                        break;
                } while (pars[i] != strGenre[ind]);

                if (pars[i] == strGenre[ind])
                    tags = tags + "," + tagGenre[ind];
            }
            tags = tags.Replace(",,", ",");

            return tags.Trim(',');
        }

        public static string GetFileSize(System.IO.FileInfo file, int Unit)
        {
            string str = "";
            try
            {
                double sizeinbytes = file.Length;
                double sizeinkbytes = Math.Round((sizeinbytes / 1024), 2);
                double sizeinmbytes = Math.Round((sizeinkbytes / 1024), 2);
                double sizeingbytes = Math.Round((sizeinmbytes / 1024), 2);
                if (Unit == 0)
                    str = Convert.ToString(sizeinbytes); //размер в гигабайтах
                else if (Unit == 1)
                    str = Convert.ToString(sizeinkbytes); //возвращает размер в мегабайтах, если размер файла менее одного гигабайта
                else if (Unit == 2)
                    str = Convert.ToString(sizeinmbytes); //возвращает размер в килобайтах, если размер файла менее одного мегабайта
                else
                    str = Convert.ToString(sizeingbytes); //возвращает размер в байтах, если размер файла менее одного килобайта
            }
            catch { MessageBox.Show("Ошибка получения размера файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); } //перехват ошибок и возврат сообщения об ошибке

            return str;
        }

        public static string[] AccCoding(string[] AccountData, int Code)
        {
            string login = AccountData[0];
            string pass = AccountData[1];
            string[] RetData = new string[2];

            SHA1 sha = new SHA1CryptoServiceProvider();
            if(Code == 0)
            {

                for (int i = 0; i < AccountData.Length; i++)
                {
                    try
                    {
                        byte[] buffer = Encoding.Unicode.GetBytes(AccountData[i]);
                        RetData[i] = Convert.ToBase64String(buffer);
                    }
                    catch(Exception)
                    { }
                }
            }
            else
            {
                for(int i = 0; i < AccountData.Length; i++)
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(AccountData[i]);
                        login = Encoding.UTF8.GetString(bytes);
                        RetData[i] = Encoding.Unicode.GetString(bytes);
                    }
                    catch (ArgumentNullException)
                    {
                        RetData[i] = "";
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return RetData;
        }

        public static bool ReSetings() //Проверка на наличие файлов настроек в программной папке и перенос их в папку AppDate
        {
            string dirsetings = frmMain.SetingPath;
            string curdir = Application.StartupPath + "\\";
            bool bag = false;

            if (!Directory.Exists(dirsetings))
                Directory.CreateDirectory(dirsetings);

            //string strUpd = @"\updater.xml";
            string files = curdir + "updater.xml";
            if (File.Exists(files))
            {
                if (File.Exists(dirsetings + "updater.xml"))
                    File.Delete(dirsetings + "updater.xml");
                File.Copy(files, dirsetings + "updater.xml");
                File.Delete(files);
            }

            if ((!File.Exists(dirsetings + "design.xml")) || (!File.Exists(dirsetings + "settings.xml")))
                bag = true;

            files = curdir + "design.xml";
            if (File.Exists(files))
            {
                if (File.Exists(dirsetings + "design.xml"))
                    File.Delete(dirsetings + "design.xml");
                File.Copy(files, dirsetings + "design.xml");
                File.Delete(files);
            }

            files = curdir + "settings.xml";
            if (File.Exists(files))
            {
                if (File.Exists(dirsetings + "settings.xml"))
                    File.Delete(dirsetings + "settings.xml");
                File.Copy(files, dirsetings + "settings.xml");
                File.Delete(files);
            }

            files = curdir + "torrent\\";
            if (!File.Exists(dirsetings + "torrent\\"))
                Directory.CreateDirectory(dirsetings + "torrent\\");
            if (Directory.Exists(files))
            {
                AdditionFunc.DirCopy(files, dirsetings + "torrent\\");
                Directory.Delete(files, true);
            }
            return bag;
        }

        public static string Translitter(string Str) //Транслитерация кириллицы
        {
            //string treker = "(kinorun).";
            string str = "";
            string fileName = "";
            string[] file = Str.Split('\\');
            string[] except = Str.Split('.');

                if (file.Length > 0)
                    fileName = file[file.Length - 1];
                else
                    fileName = Str;

            if ((fileName.Any(wordByte => wordByte > 127)) || (Str.IndexOf('#') > -1))
            {
                str = frmMain.SetingPath + "temp\\" + Translit.GetTranslit(fileName);

                if (!Directory.Exists(frmMain.SetingPath + "temp"))
                    Directory.CreateDirectory(frmMain.SetingPath + "temp");

                if (File.Exists(str))
                    File.Delete(str);

                Array.Clear(file, 0, file.Length);
                file = str.Split('\\');
                int ind = file[file.Length - 1].IndexOf("kinorun");
                //if ((ind < 0) && ((except[except.Length - 1]).ToLower() == "torrent"))
                //{
                //    str = str.Replace("." + except[except.Length - 1], treker + except[except.Length - 1]);
                //}
                //str = str.Replace("__", "_");

                //if (File.Exists(str))
                //    File.Delete(str);

                File.Copy(Str, str);
            }
            else
            {
                str = Str;
            }

            return str;
        }

        public static CookieDictionary CookiesExport (string[] Cookies)
        {
            CookieDictionary cookie = new CookieDictionary(false);

            for (int i = 0; i < Cookies.Length; i++)
            {
                try
                {
                    string[] line = Cookies[i].Split(',');
                    cookie.Add(line[0], line[1]);
                }
                catch
                { }
            }

            return cookie;
        }

        public static string[] CookiesImport (CookieDictionary Cookies)
        {
            string[] cookie = new string[4];

            string[] lines = Cookies.ToString().Split(';');
            for (int i = 0; i < Cookies.Count; i++)
            {
                string[] line = lines[i].Split('=');
                cookie[i] = line[0].Trim()+","+line[1].Trim();
            }

            return cookie;
        }
    }

}
