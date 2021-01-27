using System;
using System.Collections.Generic;
using Protodroid.Clocks.Models;
using Protodroid.Clocks.Views;
using Protodroid.MVVM;
using UniRx;

namespace Protodroid.Clocks.ViewModels
{
    public class EditTimeDisplayViewModel : EditClockViewModel
    {
        private DateTime timeToSet;
        private bool useCustomTime;
        private string timeFormat;
        public DateTime TimeToSet
        {
            get => timeToSet;
            set => OnPropertyChanged(ref timeToSet, value, onTimeToSetChanged);
        }
        
        public bool UseCustomTime
        {
            get => useCustomTime;
            set => OnPropertyChanged(ref useCustomTime, value, onUseCustomTimeChanged);
        }
        
        public string TimeFormat
        {
            get => timeFormat;
            set => OnPropertyChanged(ref timeFormat, value, onTimeFormatChanged);
        }
        
        public EditTimeDisplayViewModel(TimeDisplayModel model) : base(model)
        {
            this.model = model;
            UseCustomTime = model.UseCustomTime;
        }

        public List<string> GetTimeFormatExamples()
        {
            List<string> examples = new List<string>();
            TimeDisplayModel timeDisplayModel = (TimeDisplayModel) model;

            foreach (string example in timeDisplayModel.TimeFormatLookup.Keys)
                examples.Add(example);

            return examples;
        }

        public string LookupTimeFormat(string key)
        {
            TimeDisplayModel timeDisplayModel = (TimeDisplayModel) model;
            return timeDisplayModel.TimeFormatLookup[key];
        }
        
        
        public override void SaveToModel()
        {
            base.SaveToModel();
            TimeDisplayModel timeModel = (TimeDisplayModel) model;
            timeModel.CurrentTime = TimeToSet;
            timeModel.UseCustomTime = UseCustomTime;
            timeModel.TimeFormat = TimeFormat;
        }


        private Subject<DateTime> onTimeToSetChanged = new Subject<DateTime>();
        private Subject<bool> onUseCustomTimeChanged = new Subject<bool>();
        private Subject<string> onTimeFormatChanged = new Subject<string>();
        
        public IObservable<DateTime> OnTimeToSetChanged => onTimeToSetChanged;
        public IObservable<bool> OnUseCustomTimeChanged => onUseCustomTimeChanged;
        public IObservable<string> OnTimeFormatChanged => onTimeFormatChanged;

        public override void NotifyView()
        {
            base.NotifyView();
            onTimeToSetChanged?.OnNext(TimeToSet);
            onTimeFormatChanged?.OnNext(TimeFormat);
        }

        public override IView View { get; set; }
    }
}