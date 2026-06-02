using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Features.Common;

namespace _Project.Features.UFO
{
    public class UFOMovementController : BaseMovementController, IBouncable
    {
        private float _accelerationMultiplier;
        private float _inertiaMultiplier;


        public UFOMovementController(
            MovementModel movementModel,
            UFOSpawnData spawnData) : base(
            movementModel)
        {
            _accelerationMultiplier = spawnData.accelerationMultiplier;
            _inertiaMultiplier = spawnData.inertiaMultiplier;
        }

        protected override void UpdateVelocityOnMove(float deltaTime)
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
        
        public void Bounce(Vector2 normal)
        {
            var velocity = _movementModel.Velocity;
            var reflectedVelocity = Vector2.Reflect(velocity, normal);
            _movementModel.UpdateVelocity(reflectedVelocity);
        }
    }
}