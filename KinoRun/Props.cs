using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace KinoRun
{
    //Класс определяющий какие настройки есть в программе
    public class PropsFields
    {
        //Чтобы добавить настройку в программу просто добавьте суда строку вида -
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        //public string fontName = "";
        //public string[] Fields = new string[] { "", "b", "", "" };
        //public string[] Text = new string[] { "", "", "", "" };
        //public string[] Headlines = new string[] { "Teal", "b", "i", "u" };
        //public string[] Description = new string[] { "Blue", "b", "i", "" };

        //public string Name = "Последняя раздача";
        public string[] Cookies = new string[4];
        public string Addition = "";
        public string[] Name_R = new string[] { };
        public string[] Name_O = new string[] { };
        public string[] Year = new string[] { "2015", "2016" };
        public string[] Genre = new string[] { "All sex" };
        public string[] Director = new string[] {  };
        public string[] Studio = new string[] {  };
        public string[] Actors = new string[] {  };
        public string[] Country = new string[] { "Россия", "USA", "Europe" };
        public string[] Language = new string[] { "English", "Русский" };
        public string[] Video = new string[] {  };
        public string[] Audio = new string[] {  };
        public string[] LastName = new string[] { };
        public string[] LastPath = new string[] { };
        //public string[] Translate = new string[] {  };
        //public string[] Qualiti = new string[] {  };
        //public string[] Format_V = new string[] {  };
        //public string[] Codec_V = new string[] {  };
        //public string[] Codec_A = new string[] {  };
        //public string[] Vote = new string[] {  };
        //public string[] Category = new string[] {  };

        //public string[] Description = new string[] {  };
        //public string[] Duration = new string[] {  };
        //public string[] Annotation = new string[] {  };
        //public string[] Screanshots = new string[] {  };
        //public string[] BBCode = new string[] {  };

        //public DateTime DateValue = new DateTime(2011, 1, 1);
        //public Decimal DecimalValue = 555;
        //public Boolean BoolValue = true;
    }

    //Класс работы с настройками
    public class Props
    {
        //Путь до файла настроек
        static string dirsetings = frmMain.SetingPath;
        public String XMLFileName = dirsetings + "settings.xml";
        //public String XMLFileName = Environment.CurrentDirectory + "\\settings.xml";
        public PropsFields Fields;

        public Props()
        {
            Fields = new PropsFields();
        }

        //Запист настроек в файл
        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
            TextWriter writer = new StreamWriter(XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }
        //Чтение настроек из файла
        public void ReadXml()
        {
            if (File.Exists(XMLFileName))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                    TextReader reader = new StreamReader(XMLFileName);
                    Fields = ser.Deserialize(reader) as PropsFields;
                    reader.Close();
                }
                catch(FileNotFoundException)
                {

                }
            }
            //else{можно написать вывод какова то сообщения если файла не существует}}
        }
    }
}
