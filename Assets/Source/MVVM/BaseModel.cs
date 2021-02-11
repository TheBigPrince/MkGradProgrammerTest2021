using System;
using UniRx;
using UnityEngine;

namespace Protodroid.MVVM
{
    public abstract class BaseModel<T> where T : BaseModel<T>
    {
        public bool enableNotify { get; set; } = true;
        
        protected void OnPropertyChanged<U>(ref U field, U value)
        {
            field = value;
            if(enableNotify) onModelUpdated?.OnNext((T)this);
        }

        private Subject<T> onModelUpdated = new Subject<T>();
        public IObservable<T> OnModelUpdated => onModelUpdated;
    }
}