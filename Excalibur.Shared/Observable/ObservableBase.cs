namespace Excalibur.Shared.Observable
{
    public class ObservableBase<TId> : ObservableObjectBase
    {
        private TId _id = default(TId);

        public TId Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }
    }
}
