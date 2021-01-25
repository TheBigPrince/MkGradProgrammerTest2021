using System;
using System.Collections;
using System.Collections.Generic;
using Protodroid.MVVM;
using UniRx;
using UnityEngine;

namespace Protodroid.Clocks.Models
{
    public class ClockModel : BaseModel<ClockModel>
    {
        #region Fields
        
        private string id;
        private string title = "New Clock";
        private string clockCategory = "Clock";
        
        #endregion

        #region Properties
        
        public string ID
        {
            get => id;
            private set => OnPropertyChanged(ref id, value);
        }

        public string Title
        {
            get => title;
            set => OnPropertyChanged(ref title, value);
        }
        
        public string ClockCategory
        {
            get => clockCategory;
            set => OnPropertyChanged(ref clockCategory, value);
        }
        #endregion
        
        
        public ClockModel()
        {
            id = Guid.NewGuid().ToString();
        }
    }
}
