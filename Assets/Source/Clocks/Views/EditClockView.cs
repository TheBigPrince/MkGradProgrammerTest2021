using Protodroid.Clocks.ViewModels;
using Protodroid.MVVM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Protodroid.Clocks.Views
{
    public abstract class EditClockView<T> : BaseView<T> where T : EditClockViewModel
    {
        [SerializeField]
        private TMP_InputField titleInput = null;

        [SerializeField]
        private Button saveButton = null, cancelButton = null;

        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();
            
            titleInput.ObserveEveryValueChanged(field => field.text)
                .Where(text => text.Length > 0)
                .Subscribe(title => ViewModel.Title = title)
                .AddTo(Disposer);

            saveButton.OnClickAsObservable()
                .Subscribe(_ => ViewModel.SaveToModel())
                .AddTo(Disposer);
            
            cancelButton.OnClickAsObservable()
                .Subscribe(ViewModel.Cancel)
                .AddTo(Disposer);

            ViewModel.OnCloseDialog
                .Subscribe(ClearInputs)
                .AddTo(Disposer);
        }


        protected virtual void ClearInputs(Unit _)
        {
            titleInput.text = "";
        }
    }
}