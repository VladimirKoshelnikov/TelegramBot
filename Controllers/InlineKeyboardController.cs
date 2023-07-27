using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Services;
using TelegramBot.Configuration;

using Telegram.Bot.Types.ReplyMarkups;



namespace TelegramBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct){
             if (callbackQuery?.Data == null)
                return;
            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            string languageText = callbackQuery.Data switch
            {
                "ru" => "Русский",
                "en" => "Английский",
                _ => String.Empty
            };
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b>Язык аудио - {languageText}.</b> {Environment.NewLine}" + 
            $"{Environment.NewLine} Можно поменять в главном меню", cancellationToken: ct, parseMode: ParseMode.Html);
        
            
            
            Console.WriteLine($"Контроллер {GetType().Name} обнаружил нажатие на кнопку {callbackQuery.Data}" );
        }
    }
}