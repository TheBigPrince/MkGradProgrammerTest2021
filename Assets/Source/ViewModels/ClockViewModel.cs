using System;
using Protodroid.Clocks.Models;
using Protodroid.MVVM;
using UniRx;

namespace Protodroid.Clocks.ViewModels
{
    public abstract class ClockViewModel : BaseViewModel
    {
        // Model - Definition
        private ClockModel clockModel;

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
            set => OnPropertyChanged(ref category, value, onSetTitle);
        }
        #endregion

        
        public ClockViewModel(ClockModel clockModel)
        {
            this.clockModel = clockModel;
            Title = clockModel.Title;
            Category = clockModel.ClockType;
        }

        
        public override void NotifyView()
        {
            onSetTitle?.OnNext(title);
            onSetCategory?.OnNext(category);
        }

        #region Events

        private Subject<string> onSetTitle = new Subject<string>();
        private Subject<string> onSetCategory = new Subject<string>();
        public IObservable<string> OnSetTitle => onSetTitle;
        public IObservable<string> OnSetCategory => onSetCategory;

        #endregion
    }
}