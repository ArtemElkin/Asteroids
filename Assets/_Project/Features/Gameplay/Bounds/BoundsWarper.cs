using System;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsWarper : IInitializable, IDisposable
    {
        private readonly ScreenService _screenService;
        private readonly BoundsService _boundsService;
        private readonly SignalBus _signalBus;


        public BoundsWarper(
            ScreenService screenService,
            BoundsService boundsService,
            SignalBus signalBus)
        {
            _screenService = screenService;
            _boundsService = boundsService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OutOfBoundsSignal>(OnOutOfBounds);
        }

        private void OnOutOfBounds(OutOfBoundsSignal signal)
        {
            var warpable = signal.warpable;
            var oldPos = warpable.GetLastPosition();
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
            _signalBus.Unsubscribe<OutOfBoundsSignal>(OnOutOfBounds);
        }
    }
}