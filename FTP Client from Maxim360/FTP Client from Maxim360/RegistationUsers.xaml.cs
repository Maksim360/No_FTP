using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FTP_Client_from_Maxim360
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class RegistationUsers : Window
    {
        SaveData saveData = new SaveData(); //экземпляр класса с настройками 

        public RegistationUsers()
        {
            InitializeComponent();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.ActualHeight) / 2;
            this.Left = (screenWidth - this.ActualWidth) / 2;
        }

        private void RegistrationServer()
        {
            string isSaveIP = "0";
            if (!IsValid(Login.Text, 6, 30, "Логин") || !IsValid(Password.Password, 8, 30, "Пароль")) return;
            if (!isLicenseChek.IsChecked.Value) { Message("INFO", "Вы не согласились с лицензиями"); return; }

            if (IsSaveIp.IsChecked.Value) isSaveIP = "1";
            else isSaveIP = "0";

            string[] NameComand = { "login", "password", "IsSaveIP" , "email", "phone"};
            string[] SendComand = { Login.Text, Password.Password, isSaveIP, email.Text, phone.Text};

            string result = Send("MainRegistration", NameComand, SendComand, Config.url[0]);
            if (string.Compare(result, "64677") == 0)
            {
                WriteSetting();
                AuthorizationUsers authorizationUsers = new AuthorizationUsers();
                authorizationUsers.Show();
                this.Hide();
            }
            else
            {
                Message("REG INFO", result);
            }
        }

        bool IsValid(string value, int min, int max, string field) // валидация имени и пароля
        {
            if (value.Length < min)
            {
                Message("INFO", "В поле [ " + field + " ] недостаточно символов, нужно минимум [ " + min + " ]");
                return false;
            }
            else if (value.Length > max)
            {
                Message("INFO", "В поле [ " + field + " ] допустимый максимум символов, не более [ " + max + " ]");
                return false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(value, @"[.@]"))
            {
                Message("INFO", "В поле [ " + field + " ] содержаться недопустимые символы.");
                return false;
            }

            return true;
        }


        void Message(string Caption, string message)
        {
            System.Windows.Forms.MessageBox.Show(message, Caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }


        public static string Send(string command, string[] NameComand, string[] SendComand, string url)
        {
            WebClient web = new WebClient();
            NameValueCollection dataToSend = new NameValueCollection();

            //dataToSend["PublicApiKey"] = publicApiKey;
            dataToSend["command"] = command;

            for (int x = 0; x < SendComand.Length; x++)
            {
                dataToSend[NameComand[x]] = SendComand[x];
            }

            //for (int x = 0; x < url.Length; x++) Encoding.UTF8.GetString(wc.UploadValues(@url[x], dataToSend)); //это для того, если 1-н хост забанят, то все останется на втором и третьем хосте
            return Encoding.UTF8.GetString(web.UploadValues(url, dataToSend));
        }

        #region Settings action
        //Запись в файл...
        private void WriteSetting()
        {
            try
            {
                saveData.ReadXml();
                saveData.Fields.Login = Login.Text; //Сохраняем логин
                saveData.Fields.Password = Password.Password; //Сохраняем пароль
                saveData.WriteXml();
            }
            catch
            { }
        }
        #endregion


        private void IsReg_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationUsers authorizationUsers = new AuthorizationUsers();
            authorizationUsers.Show();
            this.Hide();
        }

        private void RegBttn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationServer();
        }

    }
}
