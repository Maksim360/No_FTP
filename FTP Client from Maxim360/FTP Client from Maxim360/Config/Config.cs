using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Client_from_Maxim360
{
    class Config
    {
        public static string[] url =
        {
        "http://varsquad.ru/no_ftp/api/LoginAndReg/",//авторизация и регистрация
        "http://varsquad.ru/no_ftp/api/update",//проверка на обновление и обновление
        "http://varsquad.ru/no_ftp/api//Chek.php",//Проверка сервера (off или on)
        ""
        };

        public static string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string PathFolderDefaut = @"/Data/";
    }
}
