using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heznek.Common.Email;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Models;
using Heznek.Services.Providers;

namespace Heznek.Services.Implementation
{
    public class MessageService : AbstractService, IMessageService
    {
        private readonly IEmailSender _emailSender;

        public MessageService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser, IEmailSender emailSender) : base(unitOfWork, authUser)
        {
            _emailSender = emailSender;
        }

        public async Task SendMessage(MessageModel message)
        {
            var emails = _unitOfWork.Repository<UserEntity>().Set
                .Where(x => message.SendToWho.Contains(x.Profile.Status) && x.Role != Common.Enums.RoleEnum.Admin)
                .Select(x => x.Email)
                .ToList();
            var attachments = new Dictionary<string, byte[]>();
            if (!string.IsNullOrEmpty(message.File?.FileName))
            {
                using (var stream = new MemoryStream())
                {
                    await message.File.CopyToAsync(stream);
                    attachments.Add(message.File.FileName, stream.ToArray());
                }
            }
            foreach (var email in emails)
            {
                try
                {
                    await _emailSender.SendEmail(email, message.Topic, message.Content, attachments);
                }
                catch { }
            }

        }
    }
}
