using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOMovementController : BaseMovementController
    {
        private float _accelerationMultiplier;
        private float _inertiaMultiplier;


        public void Setup(MovementModel movementModel, float accelerationMultipler, float inertiaMultiplier)
        {
            _accelerationMultiplier = accelerationMultipler;
            _inertiaMultiplier = inertiaMultiplier;
            base.Setup(movementModel);
        }

        protected override void UpdateVelocity(float deltaTime)
        {
            var velocity = _movementModel.Velocity; 
            velocity = _movementModel.MoveDirection == Vector2.zero ? 
                (Physics.ApplyInertia(velocity, _inertiaMultiplier, deltaTime)) :
                Physics.ApplyAcceleration(velocity, _accelerationMultiplier, _movementModel.MoveDirection, deltaTime);
            if (velocity.sqrMagnitude > 1)
            {
                velocity = velocity.normalized * _movementModel.Speed;
            }
            _movementModel.UpdateVelocity(velocity);
        }
    }
}