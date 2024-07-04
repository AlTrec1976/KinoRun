//File 1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace TorrentFile
{
    #region BDictionary

    /// <summary>
    /// Словарь
    /// начало - 'd'
    /// конец - 'e'
    /// Пример - d2:hi7:goodbyee => ("hi" => "goodbye")
    /// </summary>
    public class BDictionary
    {
        protected List<BElement> FirstItem = null;
        protected List<BElement> SecondItem = null;

        public BDictionary()
        {
            FirstItem = new List<BElement>();
            SecondItem = new List<BElement>();
        }

        public int Count
        {
            get
            {
                return FirstItem.Count;
            }
        }
        /// <summary>
        /// индекс
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BElement[] this[int index]
        {
            get
            {
                if (FirstItem.Count > index)
                {
                    BElement[] Items = new BElement[2];
                    Items[0] = FirstItem[index];
                    Items[1] = SecondItem[index];
                    return Items;
                }
                return new BElement[2];
            }
            set
            {
                if (FirstItem.Count > index)
                {
                    FirstItem[index] = value[0];
                    SecondItem[index] = value[1];
                }
                else
                {
                    FirstItem.Add(value[0]);
                    SecondItem.Add(value[1]);
                }
            }
        }
        /// <summary>
        /// Добавляет новый элемент в словарь
        /// </summary>
        /// <param name="First">First Item</param>
        /// <param name="Second">Second Item</param>
        public void Add(BElement First, BElement Second)
        {
            FirstItem.Add(First);
            SecondItem.Add(Second);
        }
    }
    #endregion
    #region BList
    /// <summary>
    /// Список
    /// начало - 'l'
    /// конец - 'e'
    /// Пример - l5:helloi145e => ("hello",145)
    /// </summary>
    public class BList
    {
        List<BElement> Items = null;

        public BList()
        {
            Items = new List<BElement>();
        }

        public BElement this[int index]
        {
            get
            {
                if (Items.Count > index)
                {

                    return Items[index];
                }
                return new BElement();
            }
            set
            {
                if (Items.Count > index)
                {
                    Items[index] = value;
                }
                else
                {
                    Items.Add(value);
                }
            }
        }
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        /// <summary>
        /// Добавляет новый элемент в словарь
        /// </summary>
        /// <param name="First">Первый элемент</param>
        /// <param name="Second">Второй элемент</param>
        public void Add(BElement inf)
        {
            Items.Add(inf);
        }
    }
    #endregion
    #region BItem
    /// <summary>
    /// Целое значение
    /// начало - 'i'
    /// конец - 'e'
    /// Пример - i145e => 145
    /// Строковое значение
    /// начало - length
    /// конец -
    /// Пример 5:hello => "hello"
    /// </summary>
    public class BItem
    {
        protected string strValue = "";
        protected Int64 intValue = 0;
        protected bool IsInt = true;
        public bool isInt
        {
            get
            {
                return IsInt;
            }
        }
        public BItem(string A)
        {
            strValue = A;
            IsInt = false;
        }
        public BItem(int A)
        {
            IsInt = true;
            intValue = A;
        }
        public string ToString()
        {
            if (IsInt)
                return intValue.ToString();
            return strValue;
        }
    }
    #endregion
    #region BElement
    /// <summary>
    /// Universal bencode store
    /// </summary>
    public class BElement
    {
        public BItem STDItem = null;
        public BList LSTItem = null;
        public BDictionary DICItem = null;
        /// <summary>
        /// Создание и чтение простого типа данных string\integer
        /// </summary>
        /// <param name="Reader">поток чтения</param>
        /// <param name="CurrentCode">Считанный заранее символ</param>
        public void AddToBItem(StreamReader Reader, char CurrentCode)
        {
            char C;
            if (CurrentCode == 'i')
            {//считывание числа
                string Value = "";
                C = (char)Reader.Read();
                while (C != 'e')
                {//конец
                    Value += C;
                    C = (char)Reader.Read();
                }
                try
                {
                    int Res = Int32.Parse(Value);
                    STDItem = new BItem(Res);
                }
                catch (OverflowException ex)
                {
                    Int64 Res = Int64.Parse(Value);
                    STDItem = new BItem(Res.ToString());
                }
                catch (Exception ex)
                {
                    //Здесь можно вызвать throw ошибку. А можно сделать объект null'ом
                    STDItem = null;
                }
                return;
            }
            int length = (int)CurrentCode - (int)'0';
            C = (char)Reader.Read();
            while (C != ':' && (C >= '0' && C <= '9'))
            {
                length = length * 10 + (int)C - (int)'0';
                C = (char)Reader.Read();
            }
            if (C != ':')
            {//Можно вызвать ошибку (так же как выше, просто обнулим объект, вместо throw new Exception("Неверно задан объект");
                // так как второй способ throw требует обработки выше по коду... а пока пишем только класс =)
                STDItem = null;
                return;
            }
            string value = "";
            for (int CurrentCount = 0; CurrentCount < length; CurrentCount++)
            {
                value += (char)Reader.Read();
            }
            STDItem = new BItem(value);

        }
        /// <summary>
        /// список. Считаем, что l была считана
        /// </summary>
        /// <param name="Reader">ридер файла</param>
        public void AddToBList(StreamReader Reader)
        {
            LSTItem = new BList();
            BElement Temp = GetNewBElement(Reader);
            while (Temp != null)
            {
                LSTItem.Add(Temp);
                Temp = GetNewBElement(Reader);
            }
            if (LSTItem.Count == 0) LSTItem = null;//опять же - здесь можно генерировать ошибку о неверной структуре файла.
        }
        /// <summary>
        /// Считывание словаря
        /// </summary>
        /// <param name="Reader">поток чтения файла</param>
        public void AddToBDic(StreamReader Reader)
        {
            DICItem = new BDictionary();
            BElement FirstTemp = GetNewBElement(Reader);
            BElement SecondTemp = GetNewBElement(Reader);
            while (FirstTemp != null || SecondTemp != null)
            {
                DICItem.Add(FirstTemp, SecondTemp);
                FirstTemp = GetNewBElement(Reader);
                SecondTemp = GetNewBElement(Reader);
            }
            if (DICItem.Count == 0) DICItem = null;//Либо писать об ошибке в структуре файла
        }

        /// <summary>
        /// Определяем тип следующего элемента. Запускаем создание
        /// </summary>
        /// <param name="Reader">поток чтения</param>
        /// <returns>Новый элемент</returns>
        public static BElement GetNewBElement(StreamReader Reader)
        {
            char C = (char)Reader.Read();
            switch (C)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case 'i':
                    {//простой тип данных
                        BElement STDElement = new BElement();
                        STDElement.AddToBItem(Reader, C);
                        return STDElement;
                    }
                case 'l':
                    {//список
                        BElement LSTElement = new BElement();
                        LSTElement.AddToBList(Reader);
                        return LSTElement;
                    }

                case 'd':
                    {//словарь
                        BElement DICElement = new BElement();
                        DICElement.AddToBDic(Reader);
                        return DICElement;
                    }
                default://("e")
                    return null;
            }
        }
    }
    #endregion
    #region FileBEncoding

    public class FileBEncoding
    {
        List<BElement> BenItems;//Если подразумеваем только один элемент на файл, то пишем BElement BenItem

        BElement this[int index]
        {
            get
            {
                if (BenItems.Count > index)
                    return BenItems[index];
                return null;
            }
            set
            {
                if (BenItems.Count > index)
                {
                    BenItems[index] = value;
                }
                else throw new Exception("Выход за пределы. Ничего не записано");
            }
        }

        public FileBEncoding(string Path)
        {
            if (!File.Exists(Path)) return;
            BenItems = new List<BElement>();
            StreamReader Reader = new StreamReader(Path, Encoding.ASCII);
            while (!Reader.EndOfStream)
            {
                BElement temp = BElement.GetNewBElement(Reader);
                if (temp != null)
                    BenItems.Add(temp);
            }
            Reader.Close();
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////
        #region StringBuild

        private string BElementToSTR(BElement CurrentElement, int TabCount, bool Ignore = true)
        {
            //для корректной обработки некорректных файлов
            if (CurrentElement == null) return "";//Так как выше по ходу, при неверное структуре файла не выдавали исключение.

            string Result = "";//строка результата
            if (Ignore)//табуляция строки для словаря не нужна
                PasteTab(ref Result, TabCount);
            if (CurrentElement.STDItem != null)
            {
                Result += CurrentElement.STDItem.ToString();
                return Result;
            }
            if (CurrentElement.LSTItem != null)
            {//обработка списка
                Result += "List{\n";
                for (int i = 0; i < CurrentElement.LSTItem.Count; i++)
                    Result += BElementToSTR(CurrentElement.LSTItem[i], TabCount + 1) + '\n';
                PasteTab(ref Result, TabCount);
                Result += "}List\n";
                return Result;
            }
            if (CurrentElement.DICItem != null)
            {//обработка словаря
                Result += "Dict{\n";
                for (int i = 0; i < CurrentElement.DICItem.Count; i++)
                {
                    Result += BElementToSTR(CurrentElement.DICItem[i][0], TabCount + 1) + " => " + BElementToSTR(CurrentElement.DICItem[i][1], TabCount + 1, false) + '\n';
                }
                PasteTab(ref Result, TabCount);
                Result += "}Dict\n";
                return Result;
            }
            return "";//Если все элементы null, то возвратим пустую строку
        }

        private string PasteTab(ref string STR, int count)
        {//табуляция
            for (int i = 0; i < count; i++)
                STR += '|';
                //STR += '\t';
            return STR;
        }

        public string ToString()
        {
            string Result = "";
            for (int i = 0; i < BenItems.Count; i++)
            {
                Result += BElementToSTR(BenItems[i], 0) + "\n\n";
            }
            return Result;
        }
        #endregion
        #region XMLBuild
        private void BElementToXML(BElement Current, XmlWriter Writer, int order = 0)
        {
            if (Current == null) return;//корректная обработка некорректных файлов
            if (Current.STDItem != null)
            {//простой элемент запишем как аттрибут
                Writer.WriteAttributeString("STDType" + '_' + order.ToString(), Current.STDItem.ToString());
                return;
            }
            if (Current.LSTItem != null)
            {//список
                Writer.WriteStartElement("List");//данный метод записывает ноду <List>
                for (int i = 0; i < Current.LSTItem.Count; i++)
                    BElementToXML(Current.LSTItem[i], Writer, order);//рекурсивное раскручивание
                Writer.WriteEndElement();//закрываем ноду </List>
                return;
            }
            if (Current.DICItem != null)
            {//словарь (аналогично списку)
                Writer.WriteStartElement("Dictionary");
                for (int i = 0; i < Current.DICItem.Count; i++)
                {
                    Writer.WriteStartElement("Dictionary_Items");
                    BElementToXML(Current.DICItem[i][0], Writer, order);
                    BElementToXML(Current.DICItem[i][1], Writer, order + 1);
                    Writer.WriteEndElement();
                }
                Writer.WriteEndElement();
                return;
            }
            return;
        }
        public void ToXMLFile(string path)
        {
            using (XmlTextWriter XMLwr = new XmlTextWriter(path, System.Text.Encoding.Unicode))
            {
                XMLwr.Formatting = Formatting.Indented;
                XMLwr.WriteStartElement("Bencode_to_XML");
                foreach (BElement X in BenItems)
                {
                    XMLwr.WriteStartElement("BenItem");
                    BElementToXML(X, XMLwr);
                    XMLwr.WriteEndElement();
                }
                XMLwr.WriteEndElement();
            }
        }

        #endregion
    }
}