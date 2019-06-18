using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Common.Email
{
    public interface IEmailSender
    {
        Task SendEmail(string to, string subject, string html, Dictionary<string, byte[]> Attachments = null);
        Task SendEmail(List<string> to, string subject, string html, Dictionary<string, byte[]> attachments = null);
        Task SendEmail<TModel>(string to, string subject, string templateName, TModel model);
    }

    public sealed class EmailSenderException : Exception
    {
        public EmailSenderException()
        {
        }

        public EmailSenderException(string message)
            : base(message)
        {
        }

        public EmailSenderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
