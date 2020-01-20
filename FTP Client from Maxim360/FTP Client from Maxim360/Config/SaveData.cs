using System;
using System.Xml.Serialization;
using System.IO;

namespace FTP_Client_from_Maxim360
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
        public String Login = "";
        public String Password = "";
        public Boolean SavePassword = false;
        public Boolean AutoAuthorization = false;
        #endregion
        #region Save Files
        public String[] _PathFolder = { };
        public String[] _PathFile = { };
        public String _TempFolder = @"C:\Users\Public\Documents\Temp\";
        public String[] _url = { };
        #endregion
        #region Settings
        public Boolean AutoSave = true;
        public String _DateUpdate;
        #endregion
    }
    //Класс работы с настройками
    public class SaveData
    {
        public PropsFields Fields;

        public SaveData()
        {
            Fields = new PropsFields();
        }
        //Запись настроек в файл
        public void WriteXml()
        {
            try {
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));

            TextWriter writer = new StreamWriter(Fields.XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
            }
            catch { }
        }
        //Чтение насроек из файла
        public void ReadXml()
        {
            try { 
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
           catch { }
        }
    }
}