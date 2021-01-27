using System;
using System.Collections.Generic;

namespace Protodroid.Clocks.Models
{
    public class TimeDisplayModel : ClockModel
    {
        private DateTime currentTime = DateTime.Now;
        private bool useCustomTime = false;
        private bool twentyFourHourTime = true;
        private string timeFormat = "hh:mm";
        
        
        public DateTime CurrentTime
        {
            get => currentTime;
            set => OnPropertyChanged(ref currentTime, value);
        }

        public bool UseCustomTime
        {
            get => useCustomTime;
            set => OnPropertyChanged(ref useCustomTime, value);
        }
        
        public bool TwentyFourHourTime
        {
            get => twentyFourHourTime;
            set => OnPropertyChanged(ref twentyFourHourTime, value);
        }
        
        public string TimeFormat
        {
            get => timeFormat;
            set => OnPropertyChanged(ref timeFormat, value);
        }
        
        
        public TimeDisplayModel()
        {
            ClockCategory = "Time";
        }

        public Dictionary<string, string> TimeFormatLookup => new Dictionary<string, string>()
        {
            {"05:50", "hh:mm"},
            {"05:50 AM", "hh:mm tt"},
            {"5:50", "h:mm"},
            {"5:50 AM", "h:mm tt"},
            {"05:50:06", "hh:mm:ss"},
            {"05:50:06 AM", "hh:mm:ss tt"}
        };
    }
}