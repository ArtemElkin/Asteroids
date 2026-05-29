using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Gameplay.UFO
{
    public class UFORotationController
    {
        private bool _isSetup;
        private float _rotateAngle;
        private MovementModel _movementModel;


        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _isSetup = true;
        }

        public void UpdatePhysics()
        {
            if (!_isSetup) return;
            
            RotateUFO();
        }
        
        private void RotateUFO()
        {
            if (_movementModel.Velocity.sqrMagnitude < 0.001f) return;
            _rotateAngle = CustomMath.Atan2(_movementModel.Velocity.y, _movementModel.Velocity.x) * CustomMath.Rad2Deg;
            _movementModel.UpdateRotationAngle(_rotateAngle);
        }

        public void Reset()
        {
            _isSetup = false;
            _rotateAngle = 0;
            _movementModel = null;
        }
    }
}