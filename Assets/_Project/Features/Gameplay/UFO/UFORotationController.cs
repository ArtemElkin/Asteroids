using _Project.Core.Math;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.UFO
{
    public class UFORotationController : BaseRotationController
    {
        protected override void UpdateAngleOnRotate()
        {
            if (_movementModel.Velocity.sqrMagnitude < 0.001f) return;
            var rotateAngle = Math.Atan2(_movementModel.Velocity.y, _movementModel.Velocity.x) * Math.Rad2Deg;
            _movementModel.UpdateRotationAngle(rotateAngle);
        }
    }
}