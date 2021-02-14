using System;
using System.Collections.Generic;
using System.Linq;
using Protodroid.Clocks.ViewModels;
using Protodroid.MVVM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Protodroid.Clocks.Views
{
    public abstract class EditClockView<T> : BaseView<T> where T : EditClockViewModel
    {
        [SerializeField]
        private TMP_InputField titleInput = null;

        [SerializeField]
        private Button saveButton = null, cancelButton = null;

        private List<Selectable> selectables = new List<Selectable>();
        private int currentlySelectedIndex = 0;


        private void Start()
        {
            selectables = GetComponentsInChildren<Selectable>().ToList();
            SetAllSelectableColorsTo(new Color(215,215,215,255));
        }

        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();
            EventSystem.current.SetSelectedGameObject(null);

            FocusFirstSelectable();

            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Tab))
                .Subscribe(CycleSelectableFocus)
                .AddTo(Disposer);

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

        private void SetAllSelectableColorsTo(Color inColor)
        {
            foreach (Selectable selectable in selectables)
            {
                ColorBlock colorBlock = selectable.colors;
                colorBlock.selectedColor = inColor;
                colorBlock.highlightedColor = inColor;
                selectable.colors = colorBlock;
            }
        }
        
        private async void FocusFirstSelectable()
        {
            await new WaitForEndOfFrame();
            currentlySelectedIndex = 0;
            selectables[currentlySelectedIndex].Select();
        }

        private void CycleSelectableFocus(long _)
        {
            currentlySelectedIndex++;
            if (currentlySelectedIndex >= selectables.Count) currentlySelectedIndex = 0;
            
            if(selectables[currentlySelectedIndex].IsInteractable()) 
                selectables[currentlySelectedIndex].Select();
            else CycleSelectableFocus(0);
        }
    }
}