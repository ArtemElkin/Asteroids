using System;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Signals;


namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsWarper : IDisposable
    {
        private readonly IScreenService _screenService;
        private readonly BoundsService _boundsService;
        private readonly ISignalBus _signalBus;


        public BoundsWarper(
            IScreenService screenService,
            BoundsService boundsService,
            ISignalBus signalBus)
        {
            _screenService = screenService;
            _boundsService = boundsService;
            _signalBus = signalBus;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OutOfBoundsSignal>(OnOutOfBounds);
        }

        private void OnOutOfBounds(OutOfBoundsSignal signal)
        {
            var warpable = signal.warpable;
            var oldPos = signal.position;
            var newPos = oldPos;
            if (_boundsService.TryGetCrossedBounds(oldPos, out var crossedBounds))
            {
                if ((crossedBounds & BoundType.Top) != 0) newPos.y = _screenService.BottomEdgeY;
                if ((crossedBounds & BoundType.Bottom) != 0) newPos.y = _screenService.TopEdgeY;
                if ((crossedBounds & BoundType.Left) != 0) newPos.x = _screenService.RightEdgeX;
                if((crossedBounds & BoundType.Right) != 0) newPos.x = _screenService.LeftEdgeX;
                warpable.Warp(newPos);
            }
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<OutOfBoundsSignal>(Initialize);
            _signalBus.Unsubscribe<OutOfBoundsSignal>(OnOutOfBounds);
        }
    }
}