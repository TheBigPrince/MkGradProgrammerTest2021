using Protodroid.Clocks.Models;
using Protodroid.MVVM;

namespace Protodroid.Clocks.ViewModels
{
    public class TimerViewModel : ClockViewModel
    {
        public TimerViewModel(TimerModel model) : base(model)
        {
            
        }

        public override IView View { get; set; }
    }
}