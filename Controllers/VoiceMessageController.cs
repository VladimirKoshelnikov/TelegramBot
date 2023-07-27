using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Configuration;
using TelegramBot.Services;
using TelegramBot.Models;

namespace TelegramBot.Controllers
{
    public class VoiceMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly AppSettings _appSettings;
        private readonly IFileHandler _audioFileHandler;

        private readonly IStorage _memoryStorage;

        public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramBotClient,  IFileHandler audioFileHandler, IStorage memoryStorage) 
        {
            _telegramClient = telegramBotClient;
            _appSettings = appSettings;
            _audioFileHandler = audioFileHandler;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;
            await _audioFileHandler.Download(fileId, ct);
            Session session = _memoryStorage.GetSession(message.From.Id);

            string textMessage = _audioFileHandler.Process(session.LanguageCode);
            if (String.IsNullOrEmpty(textMessage)){
                textMessage = "Не удалось распознать голос";
            }
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, text: textMessage, cancellationToken: ct);

        }
    }
}