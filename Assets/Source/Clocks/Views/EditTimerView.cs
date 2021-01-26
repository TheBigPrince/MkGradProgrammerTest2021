using System;
using Protodroid.Clocks.ViewModels;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Protodroid.Helper;

namespace Protodroid.Clocks.Views
{
    public class EditTimerView : EditClockView<EditTimerViewModel>
    {
        [SerializeField]
        private TMP_InputField hoursInput = null;
        
        [SerializeField]
        private TMP_InputField minutesInput = null;
        
        [SerializeField]
        private TMP_InputField secondsInput = null;

        private float inHours = 0;
        private float inMinutes = 0;
        private float inSeconds = 0;

        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();

            hoursInput.ObserveEveryValueChanged(field => field.text)
                .Where(text => text.Length > 0)
                .Where(_ => hoursInput.IsNumber())
                .Select(text => hoursInput.Between(0.0, 99.0))
                .Subscribe(num =>
                {
                    inHours = (float)num;
                    OnSetTime();
                })
                .AddTo(Disposer);
            
            minutesInput.ObserveEveryValueChanged(field => field.text)
                .Where(text => text.Length > 0)
                .Where(_ => minutesInput.IsNumber())
                .Select(text => minutesInput.Between(0.0, 59.0))
                .Subscribe(num =>
                {
                    inMinutes = (float)num;
                    OnSetTime();
                })
                .AddTo(Disposer);
            
            secondsInput.ObserveEveryValueChanged(field => field.text)
                .Where(text => text.Length > 0)
                .Where(_ => secondsInput.IsNumber())
                .Select(text => secondsInput.Between(0.0, 59.0))
                .Subscribe(num =>
                {
                    inSeconds = (float)num;
                    OnSetTime();
                })
                .AddTo(Disposer);
        }

        private void OnSetTime()
        {
            ViewModel.CountdownTime = inHours * 3600f + inMinutes * 60f + inSeconds;
        }
        
        protected override void ClearInputs(Unit _)
        {
            base.ClearInputs(_);
            hoursInput.text = "";
            minutesInput.text = "";
            secondsInput.text = "";
        }
    }
}