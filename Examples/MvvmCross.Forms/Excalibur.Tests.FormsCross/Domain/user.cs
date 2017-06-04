using Excalibur.Shared.Storage;

namespace Excalibur.Tests.FormsCross.Domain
{
    public class User : StorageDomain<int>
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
