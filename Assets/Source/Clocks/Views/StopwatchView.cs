using System;
using Protodroid.Clocks.ViewModels;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks.Views
{
    public class StopwatchView : ClockView<StopwatchViewModel>
    {
        [SerializeField]
        private Button startStopButton = null;

        [SerializeField]
        private Button resetButton = null;

        [SerializeField]
        private Sprite playImage = null, pauseImage = null;

        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();

            startStopButton.OnClickAsObservable().Subscribe(ViewModel.ToggleStopwatchState).AddTo(Disposer);
            resetButton.OnClickAsObservable().Subscribe(ViewModel.ResetStopwatch).AddTo(Disposer);
            ViewModel.OnStopwatchActive.Subscribe(ChangeStartStopButttonImage).AddTo(Disposer);
            ViewModel.OnUpdateTime.Subscribe(UpdateStopwatchDisplay).AddTo(Disposer);
        }

        private void ChangeStartStopButttonImage(bool active)
        {
            if (active) startStopButton.image.sprite = pauseImage;
            else startStopButton.image.sprite = playImage;
        }

        private void UpdateStopwatchDisplay(float currentTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            displayTMP.text = $"{time.Hours}h {time.Minutes}m {time.Seconds}s";
        }
    }
}