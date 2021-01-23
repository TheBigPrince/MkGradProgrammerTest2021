using System;
using System.Collections;
using System.Collections.Generic;
using Protodroid.Clocks.Models;
using Protodroid.Clocks.ViewModels;
using Protodroid.Clocks.Views;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;

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

        private List<ClockViewModel> clocks = new List<ClockViewModel>();


        private void Awake()
        {
            CreateSingleton();
        }

        private void Start()
        {
            clockFactory.OnClockCreated?
                .Subscribe(vm => clocks.Add(vm))
                .AddTo(gameObject);
            
            StopwatchModel defaultModel = new StopwatchModel {Title = "My Clock", ClockType = "Stop Watch"};
            StopwatchViewModel defaultViewModel = clockFactory.Create(defaultModel);
        }


        [ContextMenu("Create Stopwatch")]
        public void CreateStopWatch()
        {
            StopwatchModel model = new StopwatchModel {Title = "My Clock", ClockType = "Stop Watch"};
            StopwatchViewModel vm = clockFactory.Create(model);
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


