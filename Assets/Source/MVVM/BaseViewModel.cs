using System;
using UniRx;

namespace Protodroid.MVVM
{
    public abstract class BaseViewModel
    {
        public abstract IView View { get; set; }

        protected void OnPropertyChanged<T>(ref T field, T value, Subject<T> observable)
        {
            field = value;
            observable?.OnNext(value);
        }

        public abstract void NotifyView();
        
    }
}