using System;
using UniRx;
using UnityEngine;

namespace Protodroid.MVVM
{
    public class BaseView<T> : MonoBehaviour, IView where T : BaseViewModel
    {
        protected CompositeDisposable Disposer = new CompositeDisposable();

        protected T ViewModel { get; set; } = null;

        public void SetViewModel(T vm)
        {
            ViewModel = vm;
            ViewModel.View = this;
            Disposer?.Clear();
            InitialiseBindings();
            ViewModel.NotifyView();
        }

        private void OnDestroy()
        {
            Disposer?.Clear();
        }

        protected virtual void InitialiseBindings() { }
    }
}