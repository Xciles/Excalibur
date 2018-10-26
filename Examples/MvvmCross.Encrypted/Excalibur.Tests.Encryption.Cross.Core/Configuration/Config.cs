namespace Excalibur.Tests.Encrypted.Cross.Core.Configuration
{
    public class Config
    {
        public string Email { get; set; }
        public int UserId { get; set; }
        public bool Authenticated { get; set; }
        public int? PinAttempts { get; set; }
        public string Pin { get; set; }
    }
}