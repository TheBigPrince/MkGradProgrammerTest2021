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

        private bool stopwatchActive = false;
        public bool StopwatchActive
        {
            get => stopwatchActive;
            set => OnPropertyChanged(ref stopwatchActive, value, onStopwatchActive);
        }
        
        private float time = 0f;
        public float Time
        {
            get => time;
            set => OnPropertyChanged(ref time, value, onUpdateTime);
        }

        public StopwatchViewModel(StopwatchModel model) : base(model)
        {
            ResetStopwatch(Unit.Default);

            Observable.EveryUpdate()
                .Where(_ => StopwatchActive)
                .Subscribe(IncrementStopwatch)
                .AddTo(disposer);
        }

        private void IncrementStopwatch(long _)
        {
            Time += UnityEngine.Time.deltaTime;
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
        

        private Subject<bool> onStopwatchActive = new Subject<bool>();
        public IObservable<bool> OnStopwatchActive => onStopwatchActive;
        
        private Subject<float> onUpdateTime = new Subject<float>();
        public IObservable<float> OnUpdateTime => onUpdateTime;

    }
}