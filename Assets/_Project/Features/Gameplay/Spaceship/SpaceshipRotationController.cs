using _Project.Core.Input;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Tools;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipRotationController
    {
        private bool _isSetup;
        private CustomVector2 _lookPoint;
        private float _rotateAngle;
        private CustomVector2 _rotateDirection;
        private IFireInputService _fireInputService;
        private MovementModel _movementModel;
        private ScreenService _screenService;


        [Inject]
        private void Construct(
            IFireInputService fireInputService,
            ScreenService screenService)
        {
            _fireInputService = fireInputService;
            _screenService = screenService;
        }

        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _isSetup = true;
        }


        public void UpdatePhysics(float deltaTime)
        {
            if (!_isSetup) return;
            
            RotateSpaceship();
        }
        
        private void RotateSpaceship()
        {
            _lookPoint = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            _rotateDirection = _lookPoint - _movementModel.Position;
            _rotateAngle = CustomMath.Atan2(_rotateDirection.y, _rotateDirection.x) * CustomMath.Rad2Deg;
            _movementModel.UpdateRotationAngle(_rotateAngle - 90); 
        }

        public void Reset()
        {
            _isSetup = false;
            _movementModel = null;
        }
    }
}