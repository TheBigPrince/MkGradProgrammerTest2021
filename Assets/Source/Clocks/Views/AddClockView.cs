using Protodroid.Clocks.Models;
using Protodroid.Clocks.ViewModels;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks.Views
{
    public class AddClockView : BaseView<AddClockViewModel>
    {
        [SerializeField]
        private Button addClockButton = null;

        [SerializeField]
        private Button addTimeDisplayButton = null, addStopwatchButton = null, addTimerButton = null;

        [SerializeField]
        private GameObject addClockContainer = null, buttonsContainer = null;


        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();
            ButtonBindings();

            ViewModel.OnAddClockButtonActive
                .Subscribe(active => addClockButton.gameObject.SetActive(active));

            ViewModel.OnClockButtonsActive
                .Subscribe(active => buttonsContainer.gameObject.SetActive(active));
        }




        private void ButtonBindings()
        {
            addClockButton
                .OnClickAsObservable()
                .Subscribe(_ => ViewModel.AddClockButtonPressed());

            addTimeDisplayButton
                .OnClickAsObservable()
                .Subscribe(_ => ViewModel.CreateClock(new TimeDisplayModel()));
            
            addStopwatchButton
                .OnClickAsObservable()
                .Subscribe(_ => ViewModel.CreateClock(new StopwatchModel()));
            
            addTimerButton
                .OnClickAsObservable()
                .Subscribe(_ => ViewModel.CreateClock(new TimerModel(30)));
        }
    }
}