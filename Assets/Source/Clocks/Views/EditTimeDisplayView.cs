using System;
using Protodroid.Clocks.ViewModels;
using Protodroid.Helper;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks.Views
{
    public class EditTimeDisplayView : EditClockView<EditTimeDisplayViewModel>
    {
        [SerializeField]
        private Toggle useLocalTimeToggle = null;

        [SerializeField]
        private TMP_InputField hoursInput = null;
        
        [SerializeField]
        private TMP_InputField minutesInput = null;
        
        [SerializeField]
        private TMP_InputField secondsInput = null;

        [SerializeField]
        private TMP_Dropdown timeFormatsDropdown = null;
        
        private float inHours = 0;
        private float inMinutes = 0;
        private float inSeconds = 0;

        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();
            
            AddTimeFormatDropdownOptions();
            InitialiseTimeInputBindings();
            
            useLocalTimeToggle.OnValueChangedAsObservable()
                .Subscribe(toggleOn => ViewModel.UseCustomTime = !toggleOn)
                .AddTo(Disposer);

            ViewModel.OnUseCustomTimeChanged
                .Subscribe(ToggleCustomTimeInputs)
                .AddTo(Disposer);
            
            ToggleCustomTimeInputs(ViewModel.UseCustomTime);

            timeFormatsDropdown.ObserveEveryValueChanged(dropdown => dropdown.value)
                .Select(valueIndex => timeFormatsDropdown.options[valueIndex].text)
                .Subscribe(value => ViewModel.TimeFormat = ViewModel.LookupTimeFormat(value))
                .AddTo(Disposer);
        }
        
        private void OnSetTime()
        {
            DateTime current = DateTime.Now;
            ViewModel.TimeToSet = new DateTime(current.Year, current.Month, current.Day, (int)inHours, (int)inMinutes, (int)inSeconds);
        }

        private void ToggleCustomTimeInputs(bool customTimeActive)
        {
            hoursInput.interactable = customTimeActive;
            minutesInput.interactable = customTimeActive;
            secondsInput.interactable = customTimeActive;
            hoursInput.text = "";
            minutesInput.text = "";
            secondsInput.text = "";
        }

        protected override void ClearInputs(Unit _)
        {
            base.ClearInputs(_);
            hoursInput.text = "";
            minutesInput.text = "";
            secondsInput.text = "";
        }

        private void AddTimeFormatDropdownOptions()
        {
            if (timeFormatsDropdown.options.Count > 0)
                return;
            
            foreach (string timeFormatExample in ViewModel.GetTimeFormatExamples())
            {
                timeFormatsDropdown.options.Add(new TMP_Dropdown.OptionData(timeFormatExample));   
            }
        }

        private void InitialiseTimeInputBindings()
        {
            hoursInput.ObserveEveryValueChanged(field => field.text)
                .Where(text => text.Length > 0)
                .Where(_ => hoursInput.IsNumber())
                .Select(text => hoursInput.Between(0.0, 23.0))
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
    }
}