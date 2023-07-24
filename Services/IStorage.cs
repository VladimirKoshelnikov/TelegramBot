using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TelegramBot.Models;

namespace TelegramBot.Services
{
    public interface IStorage
    {
        public Session GetSession(long chatId);
    }
    
}