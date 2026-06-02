using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public class BounceController : IBouncable
    {
        private readonly MovementModel _movementModel;
        
        
        public BounceController(MovementModel movementModel) => _movementModel = movementModel;
        
        
        public void Bounce(Vector2 normal)
        {
            var velocity = _movementModel.Velocity;
            var reflectedVelocity = Vector2.Reflect(velocity, normal);
            _movementModel.UpdateVelocity(reflectedVelocity);
        }
    }
}