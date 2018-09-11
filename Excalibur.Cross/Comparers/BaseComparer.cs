using System.Collections.Generic;

namespace Excalibur.Cross.Comparers
{
    /// <inheritdoc />
    public abstract class BaseComparer<TObservable> : IComparer<TObservable>
    {
        /// <inheritdoc />
        public abstract int Compare(TObservable x, TObservable y);
    }
}
