using System;
using _Project.Core.Physics;

namespace _Project.Features.Common.Bounds
{
    public class BoundsChecker
    {
        public event Action OutOfBounds;
        public event Action EnteredGameAreaAfterSpawn;
        public bool IsEnteredGameAreaAfterSpawn { get; private set; }
        private readonly IReadOnlyPositionable _positionable;
        private readonly BoundsService _boundsService;


        public BoundsChecker(
            BoundsService boundsService,
            IReadOnlyPositionable positionable,
            bool isEnteredGameAreaAfterSpawn = false)
        {
            _boundsService = boundsService;
            _positionable = positionable;
            IsEnteredGameAreaAfterSpawn = isEnteredGameAreaAfterSpawn;
        }

        public void CheckOutOfBounds()
        {
            if (_boundsService.IsOutOfBounds(_positionable.Position) && IsEnteredGameAreaAfterSpawn)
            {
                OutOfBounds?.Invoke();
            }
            else if (!IsEnteredGameAreaAfterSpawn && !_boundsService.IsOutOfBounds(_positionable.Position))
            {
                IsEnteredGameAreaAfterSpawn = true;
                EnteredGameAreaAfterSpawn?.Invoke();
            }
        }
    }
}