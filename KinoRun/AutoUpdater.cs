using System;
using System.IO;
using System.Xml.Serialization;

namespace KinoRun
{
    //Класс определяющий какие настройки есть в программе
    public class UpdateFields
    {
        //Чтобы добавить настройку в программу просто добавьте суда строку вида -
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        //public string fontName = "";
        //public string[] Fields = new string[] { "", "b", "", "" };
        //public string[] Text = new string[] { "", "", "", "" };
        //public string[] Headlines = new string[] { "Teal", "b", "i", "u" };
        //public string[] Description = new string[] { "Blue", "b", "i", "" };

        public string ExeName = "";
        public string ExeExt = "";
        public string UpdateServer = "http://altrec.h1n.ru/";
        public string Version = "";
        //public string  = "";
    }

    //Класс работы с настройками
    class AutoUpdater
    {
        //Путь до файла настроек
        public String XMLFileName = frmMain.SetingPath + "updater.xml";

        public UpdateFields Fields;

        public AutoUpdater()
        {
            Fields = new UpdateFields();
        }

        //Запист настроек в файл
        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(UpdateFields));
            TextWriter writer = new StreamWriter(XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }
        //Чтение настроек из файла
        public void ReadXml()
        {
            XMLFileName = frmMain.SetingPath + "updater.xml";

            if (File.Exists(XMLFileName))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(UpdateFields));
                    TextReader reader = new StreamReader(XMLFileName);
                    Fields = ser.Deserialize(reader) as UpdateFields;
                    reader.Close();
                }
                catch (FileNotFoundException)
                {

                }
            }
            //else{можно написать вывод какова то сообщения если файла не существует}}
        }
    }
}
