using _Project.Core.Input;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipRotationController : BaseRotationController
    {
        private IFireInputService _fireInputService;
        private IScreenService _screenService;


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
            var lookPoint = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            var rotateDirection = lookPoint - _movementModel.Position;
            var rotateAngle = Math.Atan2(rotateDirection.y, rotateDirection.x) * Math.Rad2Deg;
            _movementModel.UpdateRotationAngle(rotateAngle - 90); 
        }
    }
}