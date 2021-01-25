using System;
using UniRx;

namespace Protodroid.MVVM
{
    public abstract class BaseModel<T> where T : BaseModel<T>
    {
        protected void OnPropertyChanged<U>(ref U field, U value)
        {
            field = value;
            onModelUpdated?.OnNext((T)this);
        }

        private Subject<T> onModelUpdated = new Subject<T>();
        public IObservable<T> OnModelUpdated => onModelUpdated;
    }
}