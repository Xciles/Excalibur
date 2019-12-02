using System.Threading.Tasks;

namespace Excalibur.AspNetCore.Services.Interfaces
{
    /// <summary>
    /// Interface for sending emails
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Method sends an email to just the one email address
        /// </summary>
        /// <param name="toEmail">The to email address</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="message">The actual message of the email</param>
        Task SendEmail(string toEmail, string subject, string message);
    }
}
