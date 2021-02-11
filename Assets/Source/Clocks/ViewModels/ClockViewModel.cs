using System;
using System.Reflection;
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

        protected virtual void SaveToModel()
        {
            model.Title = Title;
            model.ClockCategory = Category;
        }

        public void EditClock(Unit _)
        {
            model.enableNotify = false;
            SaveToModel();
            EditClockManager.Instance.EditClock(model);
            model.enableNotify = true;
        }
        
        public override void NotifyView()
        {
            onSetTitle?.OnNext(title);
            onSetCategory?.OnNext(category);
        }

        private void UpdateViewModel(ClockModel clockModel)
        {
            SetProperty(nameof(Title), model.Title);
            SetProperty(nameof(Category), model.ClockCategory);
        }

        protected void SetProperty<T>(string propertyName, T targetValue)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);

            T currentValue = (T)property.GetValue(this, null);
            
            if (!currentValue.Equals(targetValue))
                property.SetValue(this, targetValue);
        }
        

        #region Events

        private Subject<string> onSetTitle = new Subject<string>();
        private Subject<string> onSetCategory = new Subject<string>();
        public IObservable<string> OnSetTitle => onSetTitle;
        public IObservable<string> OnSetCategory => onSetCategory;

        #endregion
    }
}