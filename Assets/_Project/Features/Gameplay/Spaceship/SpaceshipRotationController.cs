using System.Collections.Generic;
using _Project.Core.Input;
using _Project.Core.Tools;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipRotationController : IInitializable, ITickable
    {
        private float _rotateAngle;
        private Vector2 _lookPoint;
        private Vector2 _rotateDirection;
        private IFireInputService _fireInputService;
        private Dictionary<Rigidbody2D, Vector2> _clonesRigidbodiesOffsets;
        private Rigidbody2D _rb;
        private ScreenService _screenService;


        [Inject]
        private void Construct(
            IFireInputService fireInputService,
            ScreenService screenService)
        {
            _fireInputService = fireInputService;
            _screenService = screenService;
        }

        public void Initialize()
        {
            _clonesRigidbodiesOffsets = new Dictionary<Rigidbody2D, Vector2>();
        }
        
        public void Setup(Rigidbody2D rb)
        {
            _rb = rb;
        }
        
        public void AddClone(Rigidbody2D clone, Vector2 offset)
        {
            _clonesRigidbodiesOffsets[clone] = offset;
        }

        public void Tick()
        {
            RotateSpaceship();
        }
        
        private void RotateSpaceship()
        {
            _lookPoint = _screenService.ScreenPointToWorldPoint(_fireInputService.GetScreenPointerPosition());
            _rotateDirection = _lookPoint - _rb.position;
            _rotateAngle = Mathf.Atan2(_rotateDirection.y, _rotateDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, _rotateAngle - 90); 
            _rb.MoveRotation(rotation);
            foreach (var clone in _clonesRigidbodiesOffsets.Keys)
            {
                clone.MoveRotation(rotation);
            }
        }
    }
}