using _Project.Core.Input;
using _Project.Core.Math;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Features.Common.Movement;

namespace _Project.Features.Spaceship
{
    public class SpaceshipRotationController : BaseRotationController
    {
        private readonly IFireInputService _fireInputService;
        private readonly IScreenService _screenService;


        public SpaceshipRotationController(
            MovementModel movementModel,
            IFireInputService fireInputService,
            IScreenService screenService) : base(movementModel)
        {
            _fireInputService = fireInputService;
            _screenService = screenService;
        }
        
        protected override void UpdateAngleOnRotate()
        {
            if (_movementModel.IsStunned) return;
            
            var lookPoint = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            var rotateDirection = lookPoint - _movementModel.Position;
            var rotateAngleRad = Math.Atan2(rotateDirection.y, rotateDirection.x);
            var rotationAngleDeg = Math.RadiansToDegrees(rotateAngleRad);
            _movementModel.UpdateRotationAngle(rotationAngleDeg - 90); 
        }
    }
}