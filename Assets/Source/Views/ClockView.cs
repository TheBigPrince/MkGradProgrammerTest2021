using System.Collections.Generic;
using Protodroid.Clocks.ViewModels;
using Protodroid.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Protodroid.Clocks.Views
{
    public class ClockView<T> : BaseView<T> where T : ClockViewModel
    {
        #region Inspector Exposed

        [SerializeField]
        private TextMeshProUGUI titleTMP;
        
        [SerializeField]
        private TextMeshProUGUI categoryTMP;    
        
        [SerializeField]
        private TextMeshProUGUI displayTMP;

        [SerializeField]
        private List<Button> operationalButtons;

        [SerializeField]
        private Button editButton;

        [SerializeField]
        private Button deleteButton;

        #endregion

        protected override void InitialiseBindings()
        {
            ViewModel.OnSetTitle.Subscribe(title => titleTMP.text = title).AddTo(Disposer);
            ViewModel.OnSetCategory.Subscribe(category => categoryTMP.text = category).AddTo(Disposer);
        }

        protected void OperationalButtonsActive(bool isActive)
        {
            operationalButtons.ForEach(button => button.gameObject.SetActive(isActive));
        }
    }
}