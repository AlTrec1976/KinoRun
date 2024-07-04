using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace KinoRun
{
    //Класс определяющий какие настройки есть в программе
    public class DistribFields
    {
        //Путь до файла настроек
        //public string XMLFileName;

        //Чтобы добавить настройку в программу просто добавьте суда строку вида -
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        public string Name;
        public string Poster;
        public string Name_R;
        public string Name_O;
        public string Year;
        public string Genre;
        public string Genre_tag;
        public string Director;
        public string Studio;
        public string Actors;
        public string Country;
        public string Language;
        public string Video;
        public string Audio;

        public bool Gold;

        public string Description;
        public string Times;
        public string Duration;
        public string Translate;
        public string Qualiti;
        public string Format_V;
        public string Codec_V;
        public string Codec_A;
        public string Annotation;
        public string Vote;
        public string Category;
        public string Screanshots;
        public string BBCode;
    }

    //Класс работы с настройками
    public class Distrib
    {
        string XMLFileName = "";

        public DistribFields Fields;

        public Distrib(string SaveFileName)
        {
            XMLFileName = SaveFileName;
            Fields = new DistribFields();
        }

        //Запист настроек в файл
        public void WriteSave()
        {
            XmlSerializer ser = new XmlSerializer(typeof(DistribFields));
            TextWriter writer = new StreamWriter(XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }
        //Чтение настроек из файла
        public void ReadSave()
        {
            if (XMLFileName != "openFileDialog1")
            {
                XmlSerializer ser = new XmlSerializer(typeof(DistribFields));
                TextReader reader = new StreamReader(XMLFileName);
                Fields = ser.Deserialize(reader) as DistribFields;
                reader.Close();
            }
        }
    }
}
