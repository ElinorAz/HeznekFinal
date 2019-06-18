using Heznek.Common.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Common.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IRazorViewRenderer _viewRenderer;
        private readonly IOptions<EmailOptions> _emailOptions;

        public EmailSender(
            IRazorViewRenderer viewRenderer,
            IOptions<EmailOptions> emailOptions)
        {
            _viewRenderer = viewRenderer;
            _emailOptions = emailOptions;
        }

        public async Task SendEmail(List<string> to, string subject, string html, Dictionary<string, byte[]> attachments = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailOptions.Value.From));
            message.Bcc.AddRange(to.Select(x => new MailboxAddress(x)));
            message.Subject = subject ?? String.Empty;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = html
            };
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.Key, attachment.Value);
                }

            }

            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_emailOptions.Value.Host, _emailOptions.Value.Port, false).ConfigureAwait(false);
                    await client.AuthenticateAsync(_emailOptions.Value.UserName, _emailOptions.Value.Password).ConfigureAwait(false);
                    await client.SendAsync(message).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);

                }
            }
            catch (Exception e)
            {
                throw new EmailSenderException("Failed to send message", e);
            }
        }

        public async Task SendEmail(string to, string subject, string html, Dictionary<string, byte[]> attachments = null)
        {
            await SendEmail(new List<string> { to }, subject, html, attachments);
        }
        public async Task SendEmail<TModel>(string to, string subject, string templateName, TModel model)
        {
            string view = await _viewRenderer.Render(templateName, model);
            await SendEmail(new List<string>{ to }, subject, view);
        }
    }
}
