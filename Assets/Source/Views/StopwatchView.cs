using Protodroid.Clocks.ViewModels;

namespace Protodroid.Clocks.Views
{
    public class StopwatchView : ClockView<StopwatchViewModel>
    {
        protected override void InitialiseBindings()
        {
            base.InitialiseBindings();
            OperationalButtonsActive(false);
        }
    }
}