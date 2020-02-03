using Excalibur.Cross.Providers;

namespace Excalibur.Tests.Cross.Core.Domain
{
    public class Todo : ProviderDomain<int>
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
