using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;

namespace Protodroid.Clocks.ViewModels
{
    public class TimerViewModel : ClockViewModel
    {
        public override IView View { get; set; }

        private TimerModel timerModel;

        #region Fields

        private bool timerActive = false;
        private float time = 0f;

        #endregion

        #region Properties

        public bool TimerActive
        {
            get => timerActive;
            set => OnPropertyChanged(ref timerActive, value, onTimerActive);
        }

        public float Time
        {
            get => time;
            set => OnPropertyChanged(ref time, value, onUpdateTime);
        }
        
        public bool TimerComplete => !TimerActive && Time <= 0f;

        #endregion
        
        
        public TimerViewModel(TimerModel model) : base(model)
        {
            timerModel = model;
            
            UpdateViewModel(model);

            timerModel.OnModelUpdated
                .Subscribe(UpdateViewModel)
                .AddTo(disposer);
            
            ResetTimer(Unit.Default);
            Observable.Interval(TimeSpan.FromSeconds(1f))
                .Where(_ => TimerActive && Time >= 0f)
                .Subscribe(CountdownTimer)
                .AddTo(disposer);
        }
        
        private void CountdownTimer(long _)
        {
            if (Time <= 0)
            {
                onTimerComplete?.OnNext(true);
                TimerActive = false;
            }
            else Time -= 1f;
        }
        
        public void ToggleTimerState(Unit _)
        {
            TimerActive = !TimerActive;
        }

        public void ResetTimer(Unit _)
        {
            UpdateViewModel(timerModel);
        }

        private void UpdateViewModel(ClockModel model)
        {
            TimerModel timerModel = (TimerModel) model;
            Time = timerModel.CountdownTime;
            TimerActive = false;
        }



        private CompositeDisposable disposer = new CompositeDisposable();

        #region Notifications
        
        private Subject<bool> onTimerActive = new Subject<bool>();
        private Subject<float> onUpdateTime = new Subject<float>();
        private Subject<bool> onTimerComplete = new Subject<bool>();
        
        public IObservable<bool> OnTimerActive => onTimerActive;
        public IObservable<float> OnUpdateTime => onUpdateTime;
        public IObservable<bool> OnTimerComplete => onTimerComplete;
        
        public override void NotifyView()
        {
            base.NotifyView();
            onTimerActive?.OnNext(TimerActive);
            onUpdateTime?.OnNext(Time);
        }
        
        #endregion
    }
}