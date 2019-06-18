using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IMessageService
    {
        Task SendMessage(MessageModel message);
    }
}
