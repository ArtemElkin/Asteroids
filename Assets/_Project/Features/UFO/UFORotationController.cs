using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Physics.Movement;
using _Project.Features.Common;
using _Project.Features.Common.Movement;

namespace _Project.Features.UFO
{
    public class UFORotationController : BaseRotationController
    {
        public UFORotationController(MovementModel movementModel) : base(movementModel) { }

        protected override void UpdateAngleOnRotate()
        {
            if (_movementModel.Velocity.sqrMagnitude < 0.001f) return;
            var rotateAngleRad = Math.Atan2(_movementModel.Velocity.y, _movementModel.Velocity.x);
            var rotateAngleDeg = Math.RadiansToDegrees(rotateAngleRad);
            _movementModel.UpdateRotationAngle(rotateAngleDeg);
        }
    }
}