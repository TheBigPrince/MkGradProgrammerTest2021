using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protodroid.Clocks.Models
{
    public class ClockModel
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
            private set => id = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }
        
        public string ClockCategory
        {
            get => clockCategory;
            set => clockCategory = value;
        }

        #endregion
        
        
        public ClockModel()
        {
            id = Guid.NewGuid().ToString();
        }
        
    }
}
