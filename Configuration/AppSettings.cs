using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot.Configuration
{
 public class AppSettings
    {
        /// <summary>
        /// Токен телеграм апи
        /// </summary>
        public string BotToken { get; set; }

        /// <summary>
        /// Папка загрузки аудио файлов
        /// </summary>
        public string DownloadsFolder{ get; set;}
        /// <summary>
        /// Наименование аудиофайла
        /// </summary>
        public string AudioFileName{ get; set;}
        /// <summary>
        /// Формат входного аудиофайла
        /// </summary>
        public string InputAudioFormat{ get; set;}
        
        /// <summary>
        /// Формат выходного аудиофайла
        /// </summary>
        public string OutputAudioFormat{ get; set;}

        /// <summary>
        /// Битрейт аудиосообщения
        /// </summary>
        public float AudioBitrate{ get; set;}
        
    }
}