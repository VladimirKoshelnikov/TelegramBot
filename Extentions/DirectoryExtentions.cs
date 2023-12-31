using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot.Extentions
{
    public static class DirectoryExtentions
    {
        /// <summary>
        /// Получаем путь до каталога с .sln файлом
        /// </summary>

        public static string GetSolutionRoot(){
            // var dir = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            // var fullname = Directory.GetParent(dir).FullName;
            // var projectRoot = fullname.Substring(0, fullname.Length - 4);
            // return Directory.GetParent(projectRoot)?.FullName;
            return Directory.GetCurrentDirectory();
        }
    }
}