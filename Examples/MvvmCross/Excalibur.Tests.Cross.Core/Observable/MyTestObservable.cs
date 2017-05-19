using Excalibur.Shared.Observable;

namespace Excalibur.Tests.Cross.Core.Observable
{
    public class MyTestObservable : ObservableBase<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
