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
        
        [SerializeField]
        private Color displayCompleteColor = Color.red;


        private Color originalTimeDisplayColor = Color.black;
        private IDisposable flashDisposable;


        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();

            originalTimeDisplayColor = displayTMP.color;
            
            startStopButton.OnClickAsObservable().Where(_ => !ViewModel.TimerComplete).Subscribe(ViewModel.ToggleTimerState).AddTo(Disposer);
            resetButton.OnClickAsObservable().Subscribe(ViewModel.ResetTimer).AddTo(Disposer);
            ViewModel.OnTimerActive.Subscribe(ChangeStartStopButttonImage).AddTo(Disposer);
            ViewModel.OnUpdateTime.Subscribe(UpdateTimerDisplay).AddTo(Disposer);
            ViewModel.OnTimerComplete.Subscribe(_ => timerAudio.Play()).AddTo(Disposer);
            
            ViewModel.OnTimerComplete.Where(complete => complete).Subscribe(complete => SetTimerDisplayColor(!complete)).AddTo(Disposer);
            ViewModel.OnResetTimer.Subscribe(_ => SetTimerDisplayColor(true)).AddTo(Disposer);
            
            ViewModel.OnTimerComplete.Where(complete => complete).Subscribe(SetTimerDisplayFlashing).AddTo(Disposer);
            ViewModel.OnResetTimer.Subscribe(_ => SetTimerDisplayFlashing(false)).AddTo(Disposer);
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

        private void SetTimerDisplayColor(bool normalColor)
        {
            if (normalColor) displayTMP.color = originalTimeDisplayColor;
            else displayTMP.color = displayCompleteColor;
        }
        
        private void SetTimerDisplayFlashing(bool flashOn)
        {
            if (!flashOn)
            {
                displayTMP.enabled = true;
                flashDisposable?.Dispose();
            }
            else
            {
                flashDisposable?.Dispose();
                
                flashDisposable = Observable.Interval(TimeSpan.FromSeconds(0.5f))
                    .Subscribe(_ =>
                    {
                        displayTMP.enabled = !displayTMP.enabled;
                    }).AddTo(Disposer);
            }
        }
    }
}