using System;
using _Project.Core.Physics;

namespace _Project.Features.Common.Bounds
{
    public class BoundsChecker
    {
        public event Action OutOfBounds;
        public event Action EnteredGameArea;
        public bool IsEnteredGameAreaAfterSpawn { get; private set; }
        private readonly IHasPosition _position;
        private readonly BoundsService _boundsService;


        public BoundsChecker(
            BoundsService boundsService,
            IHasPosition position,
            bool isEnteredGameAreaAfterSpawn = false)
        {
            _boundsService = boundsService;
            _position = position;
            IsEnteredGameAreaAfterSpawn = isEnteredGameAreaAfterSpawn;
        }

        public void CheckOutOfBounds()
        {
            if (_boundsService.IsOutOfBounds(_position.Position) && IsEnteredGameAreaAfterSpawn)
            {
                OutOfBounds?.Invoke();
            }
            else if (!IsEnteredGameAreaAfterSpawn && !_boundsService.IsOutOfBounds(_position.Position))
            {
                IsEnteredGameAreaAfterSpawn = true;
                EnteredGameArea?.Invoke();
            }
        }
    }
}