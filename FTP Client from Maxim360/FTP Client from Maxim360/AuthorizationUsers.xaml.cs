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
using System.Windows.Shapes;

namespace FTP_Client_from_Maxim360
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationUsers.xaml
    /// </summary>
    public partial class AuthorizationUsers : Window
    {
        SaveData saveData = new SaveData(); //экземпляр класса с настройками 

        public AuthorizationUsers()
        {
            InitializeComponent();
            //update(); //Надо фиксить(проблема с получение данных)...
            ReadSetting();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.ActualHeight) / 2;
            this.Left = (screenWidth - this.ActualWidth) / 2;
        }

        private void AuthorizationServer()
        {
            if (!IsValid(Login.Text, 6, 30, "Логин") || !IsValid(Password.Password, 8, 30, "Пароль")) return;

            string[] NameComand = { "login", "password" };
            string[] SendComand = { Login.Text, Password.Password };

            string result = Send("MainAuthorization", NameComand, SendComand, Config.url[0]);
            if (string.Compare(result, "login") == 0)
            {
                WriteSetting();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
            else if (string.Compare(result, "Неверно введен пароль!") == 0) { WriteSetting(); Message("Authorization", result); }
            else if (string.Compare(result, "53674") == 0)
            {
                Message("Authorization", "У вас не активирован аккаунт, для активации нажмите 'ок'.");
                CodeDostup codeDostup = new CodeDostup();
                codeDostup.Show();
                this.Hide();
            }
            else
            {
                Message("Authorization", result);
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
        private void AuthorizationBtnn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationServer();
        }

        void update()
        {
            string[] NameComand = {
                "version"
            };
            string[] SendComand = {
               Config.version
            };
            string VersionServer = Send("CheckUpdate", NameComand, SendComand, Config.url[1]);
            string DescriptionVersionServer = Send("CheckUpdate_Description", NameComand, SendComand, Config.url[1]);
            string LinkNewVersion = Send("CheckUpdate_Link", NameComand, SendComand, Config.url[1]);
            MessageBox.Show(VersionServer + " "+ DescriptionVersionServer + " " + LinkNewVersion);
            if (VersionServer == Config.version) { ReadSetting(); }
            else
            {
                if (System.Windows.Forms.MessageBox.Show("Новая версия - " + VersionServer + "n/" + "---Описание---" + "n/" + DescriptionVersionServer, "Update", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {

                    WebClient client = new WebClient();
                    client.DownloadFile(LinkNewVersion, "Temp/No_FTP.exe");
                    ReadSetting();
                }
                else ReadSetting(); //Продолжаем обнову..
                                   //System.Diagnostics.Process.Start("Roles Play Program.exe");//Запускаем Updater

            }

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
                saveData.Fields.Login = Login.Text; //Сохраняем логин
                if (SavePassword.IsChecked.Value)
                {
                    saveData.Fields.Password = Password.Password; //и пароль
                }
                else
                { saveData.Fields.Password = ""; }
                saveData.Fields.SavePassword = SavePassword.IsChecked.Value;
                saveData.Fields.AutoAuthorization = AutoAuthorization.IsChecked.Value;
                saveData.WriteXml();
            }
            catch
            { }
        }
        //Чтение настроек
        private void ReadSetting()
        {
            saveData.ReadXml();
            SavePassword.IsChecked = saveData.Fields.SavePassword;
            AutoAuthorization.IsChecked = saveData.Fields.AutoAuthorization;

            Login.Text = saveData.Fields.Login;
            Password.Password = saveData.Fields.Password;

            if (AutoAuthorization.IsChecked.Value)
            {
                AuthorizationServer();
            }
        }
        #endregion

        private void IsReg_Click(object sender, RoutedEventArgs e)
        {
            RegistationUsers registationUsers = new RegistationUsers();
            registationUsers.Show();
            this.Hide();
        }
    }
}
