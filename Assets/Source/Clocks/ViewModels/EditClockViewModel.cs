using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;

namespace Protodroid.Clocks.ViewModels
{
    public class EditClockViewModel : BaseViewModel
    {
        protected ClockModel model { get; set; }
        
        private string title = "";
        
        public string Title
        {
            get => title;
            set => OnPropertyChanged(ref title, value, onTitleChanged);
        }
        
        public EditClockViewModel(ClockModel model)
        {
            this.model = model;
            Title = model.Title;
        }

        public virtual void SaveToModel()
        {
            model.Title = title;
            EditClockManager.Instance.CloseAll();
            onCloseDialog?.OnNext(Unit.Default);
        }

        public void Cancel(Unit _)
        {
            EditClockManager.Instance.CloseAll();
            onCloseDialog?.OnNext(Unit.Default);
        }

        private Subject<string> onTitleChanged = new Subject<string>();
        public IObservable<string> OnTitleChanged => onTitleChanged;

        private Subject<Unit> onCloseDialog = new Subject<Unit>();
        public IObservable<Unit> OnCloseDialog => onCloseDialog;
        
        
        public override IView View { get; set; }
        public override void NotifyView()
        {
            onTitleChanged?.OnNext(Title);
        }
    }
}