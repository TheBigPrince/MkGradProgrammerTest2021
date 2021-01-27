using System;
using UniRx;

namespace Protodroid.Clocks
{
    public class Ticker
    {
        private Subject<Unit> onOneSecondTick = new Subject<Unit>();
        public IObservable<Unit> OnOneSecondTick => onOneSecondTick;
        
        public Ticker()
        {
            Observable.Interval(TimeSpan.FromSeconds(1f))
                .Subscribe(_ => onOneSecondTick?.OnNext(Unit.Default));
        }
    }
}