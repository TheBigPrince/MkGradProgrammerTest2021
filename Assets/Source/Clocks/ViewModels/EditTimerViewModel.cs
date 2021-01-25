using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;

namespace Protodroid.Clocks.ViewModels
{
    public class EditTimerViewModel : EditClockViewModel
    {
        private float countdownTime = 0f;

        public float CountdownTime
        {
            get => countdownTime;
            set => OnPropertyChanged(ref countdownTime, value, onCountdownTimeChanged);
        }
        
        public EditTimerViewModel(TimerModel model) : base(model)
        {
            this.model = model;
            CountdownTime = model.CountdownTime;
        }

        public override void SaveToModel()
        {
            base.SaveToModel();
            TimerModel timerModel = (TimerModel) model;
            timerModel.CountdownTime = CountdownTime;
        }

        private Subject<float> onCountdownTimeChanged = new Subject<float>();
        public IObservable<float> OnCountdownTimeChanged => onCountdownTimeChanged;
        
        public override void NotifyView()
        {
            base.NotifyView();
            onCountdownTimeChanged?.OnNext(CountdownTime);
        }
        
        public override IView View { get; set; }
    }
}