using _Project.Core.EventBus;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Spaceship.Weapon.LaserBeam
{
    public class LaserBeamFacade : IFacade
    {
        public IDrawable Drawable { get; }
        private readonly Timer _timer;
        private readonly IEventBus _eventBus;


        public LaserBeamFacade(
            IDrawable drawable,
            float aliveTime,
            Timer timer,
            IEventBus eventBus)
        {
            Drawable = drawable;
            _timer = timer;
            _eventBus = eventBus;

            _timer.Elapsed += OnTimerElapsed;
            _timer.Start(aliveTime);
        }

        private void OnTimerElapsed()
        {
            _eventBus.Publish(new DespawnRequestedEvent<LaserBeamFacade>(this));
        }

        public void Dispose()
        {
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Dispose();
        }
    }
}