using Excalibur.Shared.Storage;

namespace Excalibur.Tests.Cross.Core.Domain
{
    public class MyTestDomain : StorageDomain<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
