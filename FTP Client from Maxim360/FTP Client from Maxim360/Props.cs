using System;
using System.Xml.Serialization;
using System.IO;

namespace FTP_Client_from_Maxim360_and_CosmicGuy
{
    //Класс определяющий какие настройки есть в программе
    public class PropsFields
    {
        public String XMLFileName = Environment.CurrentDirectory + "\\config.ini";
        //Чтобы добавить настройку в программу просто добавьте туда строку вида -
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        //public DateTime DateValue = new DateTime(2011, 1, 1);
        //public Decimal DecimalValue = 555;
        #region Save LoginForm
        public Boolean AutoUpdate = false;
        public Boolean AutoOpenSet = false;
        string LinkForSite = "http://site.ru";
        #endregion
    }
    //Класс работы с настройками
    public class Props
    {
        public PropsFields Fields;

        public Props()
        {
            Fields = new PropsFields();
        }
        //Запись настроек в файл
        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));

            TextWriter writer = new StreamWriter(Fields.XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }
        //Чтение насроек из файла
        public void ReadXml()
        {
            if (File.Exists(Fields.XMLFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(Fields.XMLFileName);
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
            else
            {
                //можно написать вывод сообщения если файла не существует
            }
        }
    }
}