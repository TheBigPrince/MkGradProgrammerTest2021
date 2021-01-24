using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;

namespace Protodroid.Clocks.ViewModels
{
    public class StopwatchViewModel : ClockViewModel
    {
        public override IView View { get; set; }

        #region Fields

        private bool stopwatchActive = false;
        private float time = 0f;

        #endregion

        #region Properties

        public bool StopwatchActive
        {
            get => stopwatchActive;
            set => OnPropertyChanged(ref stopwatchActive, value, onStopwatchActive);
        }

        public float Time
        {
            get => time;
            set => OnPropertyChanged(ref time, value, onUpdateTime);
        }
        
        #endregion
        

        public StopwatchViewModel(StopwatchModel model) : base(model)
        {
            ResetStopwatch(Unit.Default);

            Observable.Interval(TimeSpan.FromSeconds(1f))
                .Where(_ => StopwatchActive)
                .Subscribe(IncrementStopwatch)
                .AddTo(disposer);
        }

        private void IncrementStopwatch(long _)
        {
            Time += 1f;
        }

        public void ToggleStopwatchState(Unit _)
        {
            StopwatchActive = !stopwatchActive;
        }

        public void ResetStopwatch(Unit _)
        {
            StopwatchActive = false;
            Time = 0f;
        }

        private CompositeDisposable disposer = new CompositeDisposable();
        
        
        #region Notifications
        
        private Subject<bool> onStopwatchActive = new Subject<bool>();
        private Subject<float> onUpdateTime = new Subject<float>();
        
        public IObservable<bool> OnStopwatchActive => onStopwatchActive;
        public IObservable<float> OnUpdateTime => onUpdateTime;
        
        public override void NotifyView()
        {
            base.NotifyView();
            onStopwatchActive?.OnNext(StopwatchActive);
            onUpdateTime?.OnNext(Time);
        }
        
        #endregion
        
        
    }
}