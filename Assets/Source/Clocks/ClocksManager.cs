using System;
using System.Collections;
using System.Collections.Generic;
using Protodroid.Clocks.Models;
using Protodroid.Clocks.ViewModels;
using Protodroid.Clocks.Views;
using Protodroid.Helper;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks
{
    public class ClocksManager : MonoSingleton<ClocksManager>
    {
        
        [SerializeField]
        public ClockFactory clockFactory = null;

        [SerializeField]
        private AddClockView addClockButtonView = null;

        private List<ClockViewModel> clocks = new List<ClockViewModel>();
        private Ticker timeTicker;

        public Ticker TimeTicker => timeTicker;
        

        protected override void InitialiseOnAwake()
        {
            timeTicker = new Ticker();
        }

        private void Start()
        {
            AddClockViewModel addClockViewModel = new AddClockViewModel();
            addClockButtonView.SetViewModel(addClockViewModel);

            clockFactory.OnClockCreated?
                .Subscribe(vm => clocks.Add(vm))
                .AddTo(gameObject);
            
            TimeDisplayModel model = new TimeDisplayModel {Title = "My Time Display"};
            AddClock(model);
        }


        public void AddClock(ClockModel model)
        {
            switch (model)
            {
                case StopwatchModel stopwatchModel:
                    clockFactory.Create(stopwatchModel);
                    break;
                
                case TimeDisplayModel timeDisplayModel:
                    clockFactory.Create(timeDisplayModel);
                    break;
                
                case TimerModel timerModel:
                    clockFactory.Create(timerModel);
                    break;
            }

            int addButtonSiblingIndex = addClockButtonView.transform.GetSiblingIndex() + 1;
            addClockButtonView.transform.SetSiblingIndex(addButtonSiblingIndex);
        }
        
        public void DeleteClock(ClockViewModel viewModel)
        {
            if (clocks.Contains(viewModel) && clocks.Count > 1)
            {
                clocks.Remove(viewModel);
                Destroy(viewModel.View.GameObject);
            }
        }
    }
}


