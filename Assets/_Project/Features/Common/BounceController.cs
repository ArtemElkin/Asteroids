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
            var reflectedVelocity = Vector2.Reflect(_movementModel.Velocity, normal);
            _movementModel.UpdateVelocity(reflectedVelocity);
        }
    }
}