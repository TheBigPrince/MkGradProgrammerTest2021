namespace Protodroid.Clocks.Models
{
    public class TimerModel : ClockModel
    {
        private float countdownTime = 10f;

        public float CountdownTime
        {
            get => countdownTime;
            set => OnPropertyChanged(ref countdownTime, value);
        }
        
        public TimerModel(float timeInSeconds) : base()
        {
            ClockCategory = "Timer";
            CountdownTime = timeInSeconds;
        }
    }
}