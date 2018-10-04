using Excalibur.Base.Storage;
using Excalibur.Cross.Storage;

namespace Excalibur.Tests.Cross.Core.Domain
{
    public class Todo : StorageDomain<int>
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
