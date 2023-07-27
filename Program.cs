﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Controllers;
using TelegramBot.Models;
using TelegramBot.Services;
using TelegramBot.Configuration;
using TelegramBot.Extentions;

namespace TelegramBot{
    public class Program{
        
        static AppSettings BuildAppSettings()
        {
            
            using (StreamReader sr = new StreamReader("BotToken.txt"))
            {
                return new AppSettings() 
                {
                    DownloadsFolder = "C:\\Users\\Владимир\\Downloads",
                    BotToken = sr.ReadLine(),  
                    AudioFileName = "audio",
                    InputAudioFormat = "ogg",
                    OutputAudioFormat = "wav",
                    AudioBitrate = 64000f
                    
                };  
            }
        }

        public static async Task Main()
            {
                Console.OutputEncoding = Encoding.Unicode;
 
                // Объект, отвечающий за постоянный жизненный цикл приложения
                var host = new HostBuilder()
                    .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                    .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                    .Build(); // Собираем
 
                Console.WriteLine("Сервис запущен");
                // Запускаем сервис
                await host.RunAsync();
                Console.WriteLine("Сервис остановлен");
            }
 
        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());
            // Подключаем контроллеры
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<IFileHandler, AudioFileHandler>();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
            Console.WriteLine(DirectoryExtentions.GetSolutionRoot());
        }
    }
}
