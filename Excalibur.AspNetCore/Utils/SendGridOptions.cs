namespace Excalibur.AspNetCore.Utils
{
    /// <summary>
    /// Class that provides all required information when sending emails via SendGrid
    /// </summary>
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}