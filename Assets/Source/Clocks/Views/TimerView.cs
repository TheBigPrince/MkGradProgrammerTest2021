using System;
using Protodroid.Clocks.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks.Views
{
    public class TimerView : ClockView<TimerViewModel>
    {
        [SerializeField]
        private Button startStopButton = null;
        
        [SerializeField]
        private Button resetButton = null;
        
        [SerializeField]
        private Sprite playImage = null, pauseImage = null;

        [SerializeField]
        private AudioSource timerAudio = null;


        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();
            
            startStopButton.OnClickAsObservable().Subscribe(ViewModel.ToggleTimerState).AddTo(Disposer);
            resetButton.OnClickAsObservable().Subscribe(ViewModel.ResetTimer).AddTo(Disposer);
            ViewModel.OnTimerActive.Subscribe(ChangeStartStopButttonImage).AddTo(Disposer);
            ViewModel.OnUpdateTime.Subscribe(UpdateTimerDisplay).AddTo(Disposer);
            ViewModel.OnTimerComplete.Subscribe(_ => timerAudio.Play()).AddTo(Disposer); // TO-DO: add play sounds logic
        }
        
        private void ChangeStartStopButttonImage(bool active)
        {
            if (active) startStopButton.image.sprite = pauseImage;
            else startStopButton.image.sprite = playImage;
        }
        
        private void UpdateTimerDisplay(float currentTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            displayTMP.text = $"{time.Hours}h {time.Minutes}m {time.Seconds}s";
        }
    }
}