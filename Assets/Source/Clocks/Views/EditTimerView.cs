using System;
using Protodroid.Clocks.ViewModels;
using TMPro;
using UniRx;
using UnityEngine;

namespace Protodroid.Clocks.Views
{
    public class EditTimerView : EditClockView<EditTimerViewModel>
    {
        [SerializeField]
        private TMP_InputField countdownInput = null;
        
        [SerializeField]
        private TMP_InputField hoursInput = null;
        
        [SerializeField]
        private TMP_InputField minutesInput = null;
        
        [SerializeField]
        private TMP_InputField secondsInput = null;

        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();
            
            countdownInput.ObserveEveryValueChanged(field => field.text)
                .Where(text => text.Length > 0)
                .Subscribe(text =>
                {
                    float seconds = (float)Convert.ToDouble(text);
                    ViewModel.CountdownTime = seconds;
                })
                .AddTo(Disposer);
            
            //text => ViewModel.CountdownTime = (float) Convert.ToDouble(text)
        }
        protected override void ClearInputs(Unit _)
        {
            base.ClearInputs(_);
            countdownInput.text = "";
        }
    }
}