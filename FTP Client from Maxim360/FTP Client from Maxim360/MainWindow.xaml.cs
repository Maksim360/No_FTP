using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
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


using Ionic.Zip;

using System.Windows.Media.Animation;
using System.Drawing;
using System.Collections.Specialized;

namespace FTP_Client_from_Maxim360
{



    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SaveData saveData = new SaveData(); //экземпляр класса с настройками 


        string[] _PathFolder;
        string[] _PathFile;
        string _TempPath;
        string loginPC = Environment.UserName;

        string TextTime = "";
        float timerS = 0;
        float timerM = 0;
        float timerH = 0;

        int[] minSecund = { 60, 300, 600, 900, 1800, 3600, 5400, 7200, 14400, 21600, 43200, 86400 };

        public MainWindow()
        {
            InitializeComponent();


            saveData.ReadXml();
            _PathFolder = saveData.Fields._PathFolder;
            _PathFile = saveData.Fields._PathFile;
            _TempPath = saveData.Fields._TempFolder;

            StartMetod();
           


            StartSettings();
        }

        void StartMetod()
        {
            try
            {
                CopyFiles();
                SaveZip();
                UploadFile();
                DelAllFile();
            }
            catch { }
        }

        private void timerTick(object sender, EventArgs e)
        {
            StartMetod();
        }
        private void timer2Tick(object sender, EventArgs e)
        {
            //timerS= DateTime.;
            //AutoUpdateToMin.Content= "Следущие автоматическое обновление через: "+ text;
        }



        void Log(int WhatComand, string message)
        {
            switch (WhatComand)
            {
                case 1:
                    log.Items.Add(DateTime.Now + " Файловая система (OK) - " + message);
                    break;
                case 2:
                    log.Items.Add(DateTime.Now + " Файловая система (ERROR) - " + message);
                    break;

                case 3:
                    log.Items.Add(DateTime.Now + " Серверная система (OK) - " + message);
                    break;
                case 4:
                    log.Items.Add(DateTime.Now + " Серверная система (ERROR) - " + message);
                    break;
                case 9:
                    log.Items.Add("---------------------------------------------------------------------------------------------------------------------");
                    break;
                case 10:
                    log.Items.Add("--------------------------------------------------------ERROR--------------------------------------------------------");
                    break;
                default:
                    log.Items.Add(DateTime.Now + " Произошла неизвесная ошибка : " + message);
                    break;
            }
        }

        void CopyFiles()
        {
            string temp = "";
            if (!Directory.Exists(_TempPath)) Directory.CreateDirectory(_TempPath);
            Log(9, "");
            for (int x = 0; x < _PathFolder.Length; x++)
            {
                try
                {
                    //Создать идентичную структуру папок
                    foreach (string dirPath in Directory.GetDirectories(_PathFolder[x], "*", SearchOption.AllDirectories))
                    {

                        Directory.CreateDirectory(dirPath.Replace(_PathFolder[x], _TempPath + @"\" + new DirectoryInfo(_PathFolder[x]).Name));
                        Log(1, "Пака " + dirPath + " Удачно скопирована");
                        temp = dirPath;

                    }
                }
                catch (Exception e)
                {
                    Log(10, "");
                    Log(2, "Пака " + temp + " не скопирована из-за ошибки : " + e.ToString());
                    Log(10, "");
                }

                try
                {
                    //Копировать все файлы и перезаписать файлы с идентичным именем
                    foreach (string newPath in Directory.GetFiles(_PathFolder[x], "*.*", SearchOption.AllDirectories))
                    {

                        temp = newPath;
                        File.Copy(newPath, newPath.Replace(_PathFolder[x], _TempPath + @"\" + new DirectoryInfo(_PathFolder[x]).Name), true);
                        Log(1, "Файл " + newPath + " Удачно скопирован");
                    }
                }
                catch (Exception e)
                {
                    Log(10, "");
                    Log(2, "Файл " + temp + " не скопирован изза ошибки : " + e.ToString());
                    Log(10, "");
                }
            } //копируем паки
            for (int x = 0; x < _PathFile.Length; x++)    //копируем файлы
            {
                try
                {
                    if (File.Exists(_PathFile[x])) File.Copy(_PathFile[x], _TempPath + @"\" + new FileInfo(_PathFile[x]).Name, true);
                    Log(1, "Файл " + new DirectoryInfo(_PathFolder[x]).Name + " Удачно скопирован");
                }
                catch (Exception e)
                { Log(10, ""); Log(2, "Файл " + new DirectoryInfo(_PathFolder[x]).Name + " не скопирован изза ошибки : " + e.ToString()); Log(10, ""); }
            }
            Log(9, "");
        }
        void SaveZip()
        {
            try
            {
                ZipFile zip = new ZipFile();

                zip.AddDirectory(_TempPath);

                zip.Save(_TempPath + @"/" + loginPC + ".rar"); //создаем архив и запысываем все в него..
                Log(1, "RAR архив по пути `" + _TempPath + "` был удачно создан");
            }
            catch (Exception e) { Log(10, ""); Log(2, "RAR архив по пути `" + _TempPath + "` не создался изза ошибки : " + e.ToString()); Log(10, ""); }
            Log(9, "");
        }
        void UploadFile()
        {
            WebClient webClient = new WebClient();
            try
            {
                //кидаем архив на сервер
                webClient.Headers.Add("Content-Type", "binary/octet-stream");
                webClient.UploadFile(saveData.Fields._url[0], _TempPath + @"/" + loginPC + ".rar");
                Log(3, "Rar архив удачно отправлен на сервер");
            }
            catch (Exception e)
            { Log(10, ""); Log(4, "Rar архив не может быть отправлен на сервер изза ошибки : " + e.ToString()); Log(10, ""); }
            Log(9, "");
        }
        void DelAllFile()
        {
            try
            {
                Directory.Delete(_TempPath, true);
                Log(1, "Удаление временной папки `" + _TempPath + "` прошло успешно");
            }
            catch (Exception e) { Log(10, ""); Log(1, "При удаление временной папки `" + _TempPath + "` произошла ошибка : " + e.ToString()); Log(10, ""); }
            // System.Windows.Application.Current.Shutdown();//Закрываем приложение
            Log(9, "");
        }

        private void UpdateFiles_Click(object sender, RoutedEventArgs e)
        {
            StartMetod();
        }

        private void SettingsBttn_Click(object sender, RoutedEventArgs e)
        {
            TabControls.SelectedIndex = 1;
        }

        private void LogBttn_Click(object sender, RoutedEventArgs e)
        {
            TabControls.SelectedIndex = 2;
        }

        private void HomeBttn_Click(object sender, RoutedEventArgs e)
        {
            TabControls.SelectedIndex = 0;
        }


        void StartSettings()
        {


            //Пути к папкам 
            try {
                for (int x = 0; x < _PathFolder.Length; x++)
                    PatshListBox.Items.Add(_PathFolder[x]);
            }
            catch { }
            //Пути к  файлам
            try {
                for (int x = 0; x < _PathFile.Length; x++)
                    PatshListBox.Items.Add(_PathFile[x]);
            }
            catch { }

            //Скрипты сервера

            try
            {
                for (int x = 0; x < saveData.Fields._url.Length; x++)
                    LinkServerListBox.Items.Add(saveData.Fields._url[x]);
            }
            catch { }

            //Папка временных данных...
            try
            {
                TempFolderText.Text = saveData.Fields._TempFolder;
            }
            catch { }

        }

        private void AddPathBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(PathText.Text)) { }
                else if (System.IO.Directory.Exists(PathText.Text)) { }
                else { MessageBox.Show("Такого пути не существует!"); return; }
                saveData.ReadXml();
                PatshListBox.Items.Add(PathText.Text);

                string[] s = { };
                string[] sc = { };
                for (int i = 0; i < PatshListBox.Items.Count; i++)
                {
                    s[i] = PatshListBox.Items[i].ToString();
                    if (System.IO.File.Exists(PatshListBox.Items[i].ToString()))
                    {
                        s[i] = PatshListBox.Items[i].ToString();
                    }
                    else if (System.IO.Directory.Exists(PatshListBox.Items[i].ToString()))
                    {
                        sc[i] = PatshListBox.Items[i].ToString();
                    }
                    saveData.WriteXml();
                }
            }
            catch { saveData.WriteXml(); }
        }

        private void AddLink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] NameComand = { "Link" };
                string[] SendComand = { UrlToPHPscript.Text };
                string message = Send("ChekLink", NameComand, SendComand, UrlToPHPscript.Text);
                if (string.Compare(message, "46436") == 0) { } else { MessageBox.Show("Данная ссылка не действительна!"); return; }
                try
                {
                    if (saveData.Fields._url.Contains(UrlToPHPscript.Text))
                    {
                        MessageBox.Show("Данная ссылка уже есть в списке!"); return;
                    }
                }
                catch { }
                saveData.ReadXml();
                int x = 0;
                try
                {
                    LinkServerListBox.Items.Add(UrlToPHPscript.Text);

                    string[] s = new string[LinkServerListBox.Items.Count];
                    for (int i = 0; i < LinkServerListBox.Items.Count; i++)
                    {
                        s[i] = LinkServerListBox.Items[i].ToString();
                    }
                    saveData.Fields._url = s;
                    saveData.WriteXml();
                }
                catch { saveData.WriteXml(); }
            }
            catch { saveData.WriteXml(); }
        }


        public static string Send(string command, string[] NameComand, string[] SendComand, string url)
        {
            try
            {
                WebClient web = new WebClient();
                NameValueCollection dataToSend = new NameValueCollection();

                //dataToSend["PublicApiKey"] = publicApiKey;
                dataToSend["command"] = command;

                for (int x = 0; x < SendComand.Length; x++)
                {
                    dataToSend[NameComand[x]] = SendComand[x];
                }

                return Encoding.UTF8.GetString(web.UploadValues(url, dataToSend));
            }
            catch (Exception e) { return e.ToString(); }
        }

        private void AutoUpdateTimeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int c = AutoUpdateTimeBox.SelectedIndex;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();


            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, minSecund[c]);

            System.Windows.Threading.DispatcherTimer timer2 = new System.Windows.Threading.DispatcherTimer();
            timer2.Tick += new EventHandler(timer2Tick);
            timer2.Interval = new TimeSpan(0, 0, 1);
            timerS = minSecund[c];
            timer.Start();
            timer2.Start();

        }

        private void AddTempFolderBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                saveData.ReadXml();
                if (System.IO.File.Exists(TempFolderText.Text)) { MessageBox.Show("Это не папка, а файл!"); return; }
                else if (System.IO.Directory.Exists(TempFolderText.Text)) saveData.Fields._TempFolder = TempFolderText.Text;
                else { MessageBox.Show("Такого пути не существует!"); return; }
                saveData.WriteXml();
            }
            catch { saveData.WriteXml(); }
        }
    }
}
