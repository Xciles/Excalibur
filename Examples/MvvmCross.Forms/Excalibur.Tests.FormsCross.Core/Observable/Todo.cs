using Excalibur.Base.Observable;
using Excalibur.Cross.Observable;

namespace Excalibur.Tests.FormsCross.Core.Observable
{
    public class Todo : ObservableBase<int>
    {
        private int _userId;
        private string _title;
        private bool _completed;

        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool Completed
        {
            get { return _completed; }
            set { SetProperty(ref _completed, value); }
        }
    }
}
