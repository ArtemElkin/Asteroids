using _Project.Core.Physics;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay
{
    public class BoundsChecker
    {
        private bool _isSetup;
        private bool _isEnteredGameAreaAfterSpawn;
        private IWarpable _warpable;
        private IReadOnlyPositionable _positionable;
        private readonly SignalBus _signalBus;
        private readonly BoundsService _boundsService;


        public BoundsChecker(
            SignalBus signalBus,
            BoundsService boundsService)
        {
            _signalBus = signalBus;
            _boundsService = boundsService;
        }

        public void Setup(IReadOnlyPositionable  positionable, IWarpable warpable)
        {
            _positionable = positionable;
            _warpable = warpable;
            _isSetup = true;
        }

        public void CheckOutOfBounds()
        {
            if (!_isSetup) return;
            
            if (_boundsService.IsOutOfBounds(_positionable.Position) && _isEnteredGameAreaAfterSpawn)
            {
                _signalBus.Fire(new OutOfBoundsSignal(_warpable, _positionable.Position));
            }
            else if (!_isEnteredGameAreaAfterSpawn && !_boundsService.IsOutOfBounds(_positionable.Position))
            {
                _isEnteredGameAreaAfterSpawn = true;
            }
        }

        public void Reset()
        {
            _isSetup = false;
            _positionable = null;
            _warpable = null;
        }
    }
}