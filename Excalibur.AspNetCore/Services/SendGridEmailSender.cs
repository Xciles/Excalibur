using System.Threading.Tasks;
using Excalibur.AspNetCore.Services.Interfaces;
using Excalibur.AspNetCore.Utils;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Excalibur.AspNetCore.Services
{
    /// <summary>
    /// SendGrid based email sender based on the <see cref="IEmailSender"/>
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridOptions _options;

        public SendGridEmailSender(IOptions<SendGridOptions> options)
        {
            _options = options.Value;
        }

        /// <inheritdoc />
        public Task SendEmail(string email, string subject, string message)
        {
            return ExecuteAsync(_options.ApiKey, email, subject, message);
        }

        private Task ExecuteAsync(string apiKey, string email, string subject, string message)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_options.FromEmail, _options.FromName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));

            return client.SendEmailAsync(msg);
        }
    }
}
