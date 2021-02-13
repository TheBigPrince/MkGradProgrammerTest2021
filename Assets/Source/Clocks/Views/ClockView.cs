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

        private Animator animator;

        protected override void InitialiseBindings()
        {
            animator = GetComponent<Animator>();
            ViewModel.OnSetTitle.Subscribe(title => titleTMP.text = title).AddTo(Disposer);
            ViewModel.OnSetCategory.Subscribe(category => categoryTMP.text = category).AddTo(Disposer);
            deleteButton.OnClickAsObservable().Subscribe(OnDeleteButtonPressed).AddTo(Disposer);
            editButton.OnClickAsObservable().Subscribe(ViewModel.EditClock).AddTo(Disposer);
        }

        private void OnDeleteButtonPressed(Unit _)
        {
            if (!ClocksManager.Instance.IsLastClock(ViewModel))
                animator.SetTrigger("Delete");
        }
        
        public void Delete()
        {
            ClocksManager.Instance.DeleteClock(ViewModel);
        }
    }
}