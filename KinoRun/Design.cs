using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace KinoRun
{

    //Класс определяющий какие настройки есть в программе
    public class DesignFields
    {
        //Путь до файла настроек

        //Чтобы добавить настройку в программу просто добавьте суда строку вида -
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        public string fontName = "";
        public bool FieldLine = false;
        public string[] Fields = new string[] { "", "b", "", "" };
        public string[] Text = new string[] { "", "", "", "" };
        public string[] Headlines = new string[] { "Teal", "b", "i", "u" };
        public string[] Description = new string[] { "Blue", "b", "i", "" };

        public string PathSave = "";
        public bool SaveDefault = false;
        public string[] PicPicture = new string[] { "", "" };
        public string[] KinoRun = new string[] { "", "" };

        public string Tags = "";
        public bool ClearDir = false;

        public int Theme = 0;
        public bool Beta = false;
    }

    //Класс работы с настройками
    public class Design
    {
        static string dirsetings = frmMain.SetingPath;
        public String XMLFileName = dirsetings + "design.xml";
        //public String XMLFileName = Environment.CurrentDirectory + "\\design.xml";
        public DesignFields Fields;

        public Design()
        {
            Fields = new DesignFields();
        }

        //Запист настроек в файл
        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(DesignFields));
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
                    XmlSerializer ser = new XmlSerializer(typeof(DesignFields));
                    TextReader reader = new StreamReader(XMLFileName);
                    Fields = ser.Deserialize(reader) as DesignFields;
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
