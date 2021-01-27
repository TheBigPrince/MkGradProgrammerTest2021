using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;

namespace Protodroid.Clocks.ViewModels
{
    public class TimeDisplayViewModel : ClockViewModel
    {
        private TimeDisplayModel timeDisplayModel;
        
        #region Fields

        private bool useCustomTime = false;
        private DateTime currentTime = DateTime.Now;
        private bool twentyFourHourTime = true;
        private string timeFormat = "hh:mm tt";

        #endregion
        
        #region Properties

        public bool UseCustomTime
        {
            get => useCustomTime;
            set => OnPropertyChanged(ref useCustomTime, value, onUseCustomTimeChanged);
        }
        
        public DateTime CurrentTime
        {
            get => currentTime;
            set => OnPropertyChanged(ref currentTime, value, onChangeCurrentTime);
        }
        
        public bool TwentyFourHourTime
        {
            get => twentyFourHourTime;
            set => OnPropertyChanged(ref twentyFourHourTime, value, onTwentyFourHourTimeChanged);
        }
        
        public string TimeFormat
        {
            get => timeFormat;
            set => OnPropertyChanged(ref timeFormat, value, onTimeFormatChanged);
        }

        #endregion
        
        public TimeDisplayViewModel(TimeDisplayModel model) : base(model)
        {
            timeDisplayModel = model;
            UpdateViewModel(timeDisplayModel);

            timeDisplayModel.OnModelUpdated
                .Subscribe(UpdateViewModel)
                .AddTo(disposer);
            
            ClocksManager.instance.TimeTicker
                .OnOneSecondTick
                .Select(_ => UseCustomTime)
                .Subscribe(SetTime)
                .AddTo(disposer);

            onTimeFormatChanged
                .Subscribe(_ => ProcessTimeFormat())
                .AddTo(disposer);

            onTwentyFourHourTimeChanged
                .Subscribe(_ =>
                {
                    ProcessTimeFormat();
                    TimeFormat = timeFormat;
                })
                .AddTo(disposer);

            ProcessTimeFormat();
        }
        
        private void UpdateViewModel(ClockModel model)
        {
            TimeDisplayModel timeDisplayModel = (TimeDisplayModel) model;
            CurrentTime = timeDisplayModel.CurrentTime;
            UseCustomTime = timeDisplayModel.UseCustomTime;
            TwentyFourHourTime = timeDisplayModel.TwentyFourHourTime;
            TimeFormat = timeDisplayModel.TimeFormat;
        }

        private void SetTime(bool usingCustomTime)
        {
            if(usingCustomTime) CurrentTime = CurrentTime.AddSeconds(1);
            else CurrentTime = DateTime.Now;
        }

        private void ProcessTimeFormat()
        {
            if (!TwentyFourHourTime)
                timeFormat = TimeFormat.ToLower();
            else
                timeFormat = TimeFormat.Replace("h", "H");
        }

        private CompositeDisposable disposer = new CompositeDisposable();

        #region Notifications
        
        private Subject<DateTime> onChangeCurrentTime = new Subject<DateTime>();
        private Subject<bool> onUseCustomTimeChanged = new Subject<bool>();
        private Subject<bool> onTwentyFourHourTimeChanged = new Subject<bool>();
        private Subject<string> onTimeFormatChanged = new Subject<string>();
        public IObservable<DateTime> OnChangeCurrentTime => onChangeCurrentTime;
        public IObservable<bool> OnUseCustomTimeChanged => onUseCustomTimeChanged;
        public IObservable<bool> OnTwentyFourHourTimeChanged => onTwentyFourHourTimeChanged;
        public IObservable<string> OnTimeFormatChanged => onTimeFormatChanged;
        
        public override void NotifyView()
        {
            base.NotifyView();
            onChangeCurrentTime?.OnNext(CurrentTime);
            onUseCustomTimeChanged?.OnNext(UseCustomTime);
            onTwentyFourHourTimeChanged?.OnNext(TwentyFourHourTime);
            onTimeFormatChanged?.OnNext(TimeFormat);
        }
        
        #endregion

        public override IView View { get; set; }
    }
}