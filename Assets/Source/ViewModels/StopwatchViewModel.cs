using Protodroid.Clocks.Models;
using Protodroid.MVVM;

namespace Protodroid.Clocks.ViewModels
{
    public class StopwatchViewModel : ClockViewModel, IClock
    {
        public StopwatchViewModel(StopwatchModel model) : base(model)
        {
            
        }

        public override IView View { get; set; }
    }
}