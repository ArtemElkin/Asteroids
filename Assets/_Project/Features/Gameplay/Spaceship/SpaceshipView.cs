using _Project.Core.Physics;
using _Project.Features.Gameplay.Common;
using _Project.Infrastructure.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipView : MonoBehaviour, IDrawable
    {
        private Rigidbody2D _rb;
        private IReadOnlyPositionable _positionable;
        private IReadOnlyRotatable _rotatable;


        [Inject]
        private void Construct(IReadOnlyPositionable positionable, IReadOnlyRotatable rotatable)
        {
            _positionable = positionable;
            _rotatable = rotatable;
            transform.position = _positionable.Position.ToUnity();
            _rb = GetComponent<Rigidbody2D>();
            _rb.position = transform.position;
        }
        
        public void Draw()
        {
            _rb.MovePosition(_positionable.Position.ToUnity());
            var rotation = Quaternion.Euler(0, 0, _rotatable.RotationAngle);
            _rb.MoveRotation(rotation);
        }
    }
}