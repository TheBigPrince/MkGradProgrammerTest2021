using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;

namespace Protodroid.Clocks.ViewModels
{
    public class AddClockViewModel : BaseViewModel
    {
        private bool addClockButtonActive = true;
        public bool AddClockButtonActive
        {
            get => addClockButtonActive;
            set => OnPropertyChanged(ref addClockButtonActive, value, onAddClockButtonActive);
        }
        
        private bool clockButtonsActive = false;
        public bool ClockButtonsActive
        {
            get => clockButtonsActive;
            set => OnPropertyChanged(ref clockButtonsActive, value, onClockButtonsActive);
        }
        

        public void AddClockButtonPressed()
        {
            AddClockButtonActive = false;
            ClockButtonsActive = true;
        }
        
        public void CreateClock(ClockModel model)
        {
            AddClockButtonActive = true;
            ClockButtonsActive = false;
            ClocksManager.Instance.AddClock(model);
        }



        private Subject<bool> onAddClockButtonActive = new Subject<bool>();
        public IObservable<bool> OnAddClockButtonActive => onAddClockButtonActive;
        
        private Subject<bool> onClockButtonsActive = new Subject<bool>();
        public IObservable<bool> OnClockButtonsActive => onClockButtonsActive;

        public override IView View { get; set; }
        public override void NotifyView()
        {
            onAddClockButtonActive?.OnNext(true);
            onClockButtonsActive?.OnNext(false);
        }
    }
}