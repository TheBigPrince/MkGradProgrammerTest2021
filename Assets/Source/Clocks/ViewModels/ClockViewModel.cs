using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;

namespace Protodroid.Clocks.ViewModels
{
    public abstract class ClockViewModel : BaseViewModel
    {
        // Model - Definition
        protected ClockModel model;

        #region Fields
        private string title = "";
        private string category = "";

        #endregion
        
        #region Properties
        public string Title
        {
            get => title;
            set => OnPropertyChanged(ref title, value, onSetTitle);
        }
        
        public string Category
        {
            get => category;
            set => OnPropertyChanged(ref category, value, onSetCategory);
        }
        #endregion

        
        public ClockViewModel(ClockModel clockModel)
        {
            model = clockModel;
            UpdateViewModel(model);
            model.OnModelUpdated
                .Subscribe(UpdateViewModel);
        }

        public void EditClock(Unit _)
        {
            EditClockManager.instance.EditClock(model);
        }
        
        public override void NotifyView()
        {
            onSetTitle?.OnNext(title);
            onSetCategory?.OnNext(category);
        }

        private void UpdateViewModel(ClockModel clockModel)
        {
            Title = model.Title;
            Category = model.ClockCategory;
        }
        

        #region Events

        private Subject<string> onSetTitle = new Subject<string>();
        private Subject<string> onSetCategory = new Subject<string>();
        public IObservable<string> OnSetTitle => onSetTitle;
        public IObservable<string> OnSetCategory => onSetCategory;

        #endregion
    }
}