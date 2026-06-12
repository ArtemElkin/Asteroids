using _Project.Core.EventBus;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common;
using _Project.Features.Common.Event;
using _Project.Features.Spaceship.Weapon.Config;

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

            _timer.Elapsed += Elapsed;
            _timer.Start(aliveTime);
        }

        private void Elapsed()
        {
            _eventBus.Publish(new DespawnRequestedEvent<LaserBeamFacade>(this));
        }

        public void Dispose()
        {
            _timer.Elapsed -= Elapsed;
            _timer.Dispose();
        }
    }
}