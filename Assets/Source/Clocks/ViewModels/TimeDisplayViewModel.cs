using Protodroid.Clocks.Models;
using Protodroid.MVVM;

namespace Protodroid.Clocks.ViewModels
{
    public class TimeDisplayViewModel : ClockViewModel
    {
        public TimeDisplayViewModel(TimeDisplayModel model) : base(model)
        {
            
        }

        public override IView View { get; set; }
    }
}