using TelegramBot.Configuration;
using Telegram.Bot;
using TelegramBot.Utilities;

namespace TelegramBot.Services
{
    public class AudioFileHandler : IFileHandler
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramBotClient;

        public AudioFileHandler(ITelegramBotClient telegramBotClient, AppSettings appSettings){
            _appSettings = appSettings;
            _telegramBotClient = telegramBotClient;
        }

        
        public async Task Download(string fileId, CancellationToken ct){
            // Генерируем полный путь к файлу
            string inputAudioFilePath = Path.Combine(_appSettings.DownloadsFolder,$"{_appSettings.AudioFileName}.{_appSettings.InputAudioFormat}");
            using (FileStream destinationStream = File.Create(inputAudioFilePath)){
                var file = await _telegramBotClient.GetFileAsync(fileId, ct);
                if (file.FilePath == null){
                    return;
                }
                    // скачиваем файл
                await _telegramBotClient.DownloadFileAsync(file.FilePath, destinationStream, ct);
                Console.WriteLine("Файл загружен");
                
            }
        }
        public string Process(string languageCode){
            string inputAudioPath = Path.Combine(_appSettings.DownloadsFolder, $"{_appSettings.AudioFileName}.{_appSettings.InputAudioFormat}");
            string outputAudioPath = Path.Combine(_appSettings.DownloadsFolder, $"{_appSettings.AudioFileName}.{_appSettings.OutputAudioFormat}");
            Console.WriteLine("Конвертация началась");
            AudioConverter.TryConvert(inputAudioPath, outputAudioPath);
            Console.WriteLine("Конвертация закончилась");

            return  VoiceDetection.GetTextMessageFromVoice(outputAudioPath,_appSettings.AudioBitrate, languageCode);;
        }
    }
}