using System;
using System.Collections.Generic;
using Protodroid.Clocks.Models;
using Protodroid.Clocks.ViewModels;
using Protodroid.Clocks.Views;
using UniRx;
using UnityEngine;

namespace Protodroid.Clocks
{
    public class ClockFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform clockContainer;

        [SerializeField]
        private StopwatchView stopwatchPrefab;

        [SerializeField]
        private TimerView timerPrefab;

        [SerializeField]
        private TimeDisplayView timeDisplayPrefab;
        

        public StopwatchViewModel Create(StopwatchModel model)
        {
            StopwatchViewModel vm = new StopwatchViewModel(model);
            StopwatchView view = Instantiate(stopwatchPrefab, clockContainer);
            view.SetViewModel(vm);
            onClockCreated?.OnNext(vm);
            return vm;
        }
        
        public TimerViewModel Create(TimerModel model)
        {
            TimerViewModel vm = new TimerViewModel(model);
            TimerView view = Instantiate(timerPrefab, clockContainer);
            view.SetViewModel(vm);
            onClockCreated?.OnNext(vm);
            return vm;
        }
        
        public TimeDisplayViewModel Create(TimeDisplayModel model)
        {
            TimeDisplayViewModel vm = new TimeDisplayViewModel(model);
            TimeDisplayView view = Instantiate(timeDisplayPrefab, clockContainer);
            view.SetViewModel(vm);
            onClockCreated?.OnNext(vm);
            return vm;
        }


        private Subject<ClockViewModel> onClockCreated = new Subject<ClockViewModel>();
        public IObservable<ClockViewModel> OnClockCreated => onClockCreated;
    }
}