using System;
using System.Collections;
using System.Collections.Generic;
using Protodroid.Clocks.Models;
using Protodroid.Clocks.ViewModels;
using Protodroid.Clocks.Views;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks
{
    public class ClocksManager : MonoBehaviour
    {
        #region Singleton
        
        public static ClocksManager instance = null;

        private void CreateSingleton()
        {
            if (instance == null) instance = this;
            else Destroy(this);
        }

        #endregion
        

        [SerializeField]
        public ClockFactory clockFactory = null;

        [SerializeField]
        private AddClockView addClockButtonView = null;

        private List<ClockViewModel> clocks = new List<ClockViewModel>();


        private void Awake()
        {
            CreateSingleton();
        }

        private void Start()
        {
            AddClockViewModel addClockViewModel = new AddClockViewModel();
            addClockButtonView.SetViewModel(addClockViewModel);

            clockFactory.OnClockCreated?
                .Subscribe(vm => clocks.Add(vm))
                .AddTo(gameObject);
            
            CreateStopWatch();
        }


        public void AddClock(ClockModel model)
        {
            switch (model)
            {
                case StopwatchModel stopwatchModel:
                    clockFactory.Create(stopwatchModel);
                    break;
                
                case TimeDisplayModel timeDisplayModel:
                    // clockFactory.Create(timeDisplayModel);
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

        public void EditClock(ClockViewModel viewModel)
        {
            // open up edit dialogue window
        }

        [ContextMenu("Create Stopwatch")]
        public void CreateStopWatch()
        {
            StopwatchModel model = new StopwatchModel {Title = "My Clock", ClockCategory = "Stop Watch"};
            AddClock(model);
        }
    }
}


