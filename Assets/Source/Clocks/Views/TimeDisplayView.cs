using System;
using System.Collections.Generic;
using Protodroid.Clocks.ViewModels;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks.Views
{
    public class TimeDisplayView : ClockView<TimeDisplayViewModel>
    {
        [SerializeField]
        private Button twentyFourHourButton = null;

        private TextMeshProUGUI twentyFourHourButtonText = null;

        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();

            twentyFourHourButtonText = twentyFourHourButton.GetComponentInChildren<TextMeshProUGUI>();
            
            SetTwentyFourHourButtonText();
            
            ViewModel.OnChangeCurrentTime.Subscribe(UpdateTimeDisplay).AddTo(Disposer);

            twentyFourHourButton.OnClickAsObservable()
                .Subscribe(_ => ViewModel.TwentyFourHourTime = !ViewModel.TwentyFourHourTime)
                .AddTo(Disposer);

            ViewModel.OnTwentyFourHourTimeChanged
                .Subscribe(_ => SetTwentyFourHourButtonText());
        }


        private void SetTwentyFourHourButtonText()
        {
            twentyFourHourButtonText.text = ViewModel.TwentyFourHourTime ? "24hr" : "12hr";
        }


        private void UpdateTimeDisplay(DateTime dateTime)
        {
            displayTMP.text = dateTime.ToString(ViewModel.TimeFormat); //$"{dateTime.Hour}:{dateTime.Minute} {dateTime.Second}s";
        }
    }
}