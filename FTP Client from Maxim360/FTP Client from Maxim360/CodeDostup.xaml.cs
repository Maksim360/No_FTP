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
    /// Логика взаимодействия для CodeDostup.xaml
    /// </summary>
    public partial class CodeDostup : Window
    {
        SaveData saveData = new SaveData(); //экземпляр класса с настройками 

        public CodeDostup()
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

        static void Message(string Caption, string message)
        {
            System.Windows.Forms.MessageBox.Show(message, Caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        private void AuthorizationServer()
        {
            if (!IsValid(CodeDostupe.Password, 5, 5, "Код доступа")) return;

            saveData.ReadXml();

            string[] NameComand = { "CodeDostup", "login", "password" };
            string[] SendComand = { CodeDostupe.Password, saveData.Fields.Login, saveData.Fields.Password };

            string result = Send("RegistrationCodeDostupe", NameComand, SendComand, Config.url[0]);
            if (string.Compare(result, "98568") == 0)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
            else
            {
                Message("Authorization", result);
            }
        }

        public static string Send(string command, string[] NameComand, string[] SendComand, string url)
        {
            try
            {
                WebClient web = new WebClient();
                NameValueCollection dataToSend = new NameValueCollection();

                dataToSend["command"] = command;

                for (int x = 0; x < SendComand.Length; x++)
                {
                    dataToSend[NameComand[x]] = SendComand[x];
                }

                //for (int x = 0; x < url.Length; x++) Encoding.UTF8.GetString(wc.UploadValues(@url[x], dataToSend)); //это для того, если 1-н хост забанят, то все останется на втором и третьем хосте
                return Encoding.UTF8.GetString(web.UploadValues(url, dataToSend));
            }
            catch(Exception e) { return e.ToString(); }
        }
        private void OkBttn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationServer();
        }

        private void ExitBttn_Click(object sender, RoutedEventArgs e)
        {
            saveData.ReadXml();
            saveData.Fields.AutoAuthorization = false;
            saveData.WriteXml();
            AuthorizationUsers windowAuthorization = new AuthorizationUsers();
            windowAuthorization.Show();
            this.Hide();
        }
    }
}
