using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot.Extentions
{
    public static class StringExtentions
    {
        /// <summary>
        /// Преобразуем строку, чтобы она начиналась с заглавной буквы
        /// </summary>

        public static string UppercaseFirst(string s){
            if (string.IsNullOrEmpty(s)){
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}