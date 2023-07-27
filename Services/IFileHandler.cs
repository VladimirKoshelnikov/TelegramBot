using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot.Services
{
    public interface IFileHandler
    {
        public Task Download(string fileId, CancellationToken ct);
        public string Process(string languageCode);
    }
}