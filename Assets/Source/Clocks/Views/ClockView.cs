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
        protected TextMeshProUGUI titleTMP;
        
        [SerializeField]
        protected TextMeshProUGUI categoryTMP;    
        
        [SerializeField]
        protected TextMeshProUGUI displayTMP;

        [SerializeField]
        protected Button editButton;

        [SerializeField]
        protected Button deleteButton;

        #endregion
        
        protected override void InitialiseBindings()
        {
            ViewModel.OnSetTitle.Subscribe(title => titleTMP.text = title).AddTo(Disposer);
            ViewModel.OnSetCategory.Subscribe(category => categoryTMP.text = category).AddTo(Disposer);
            deleteButton.OnClickAsObservable().Subscribe(_ => ClocksManager.instance.DeleteClock(ViewModel)).AddTo(Disposer);
            editButton.OnClickAsObservable().Subscribe(_ => ClocksManager.instance.EditClock(ViewModel)).AddTo(Disposer);
        }
    }
}