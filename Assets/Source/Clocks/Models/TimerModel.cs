namespace Protodroid.Clocks.Models
{
    public class TimerModel : ClockModel
    {
        public float CountdownTime { get; set; } = 10f;
        
        public TimerModel(float timeInSeconds) : base()
        {
            ClockCategory = "Timer";
            CountdownTime = timeInSeconds;
        }
    }
}