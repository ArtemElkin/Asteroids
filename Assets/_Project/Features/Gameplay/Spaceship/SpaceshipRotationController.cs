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
            IFireInputService fireInputService,
            IScreenService screenService)
        {
            _fireInputService = fireInputService;
            _screenService = screenService;
        }
        
        protected override void Rotate()
        {
            var lookPoint = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            var rotateDirection = lookPoint - _movementModel.Position;
            var rotateAngle = Math.Atan2(rotateDirection.y, rotateDirection.x) * Math.Rad2Deg;
            _movementModel.UpdateRotationAngle(rotateAngle - 90); 
        }

        public override void Reset()
        {
            base.Reset();
            _fireInputService = null;
            _screenService = null;
        }
    }
}