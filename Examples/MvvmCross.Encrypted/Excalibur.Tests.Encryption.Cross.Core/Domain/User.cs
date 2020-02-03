using Excalibur.Cross.Providers;

namespace Excalibur.Tests.Encrypted.Cross.Core.Domain
{
    public class User : ProviderDomain<int>
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
