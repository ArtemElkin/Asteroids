using System;
using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Render
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionHandler : MonoBehaviour, ICollidable
    {
        public MovementModel MovementModel { get; private set; }
        public event Action<ICollidable, Vector2> OnCollided;
        private const float CooldownTime = 0.5f;
        private float _timeLeftAfterLastCollision;
        private Collider2D _collider;


        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        
        private void Update()
        {
            if (_timeLeftAfterLastCollision < CooldownTime)
                _timeLeftAfterLastCollision += Time.deltaTime;
        }
        
        public void Setup(MovementModel movementModel)
        {
            MovementModel = movementModel;
        }

        public void Reset()
        {
            MovementModel = null;
        }

        public void ActivateCollision()
        {
            _collider.enabled = true;
        }

        public void DeactivateCollision()
        {
            _collider.enabled = false;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (MovementModel == null) return;
            
            if (_timeLeftAfterLastCollision > CooldownTime)
            {
                if (collision.gameObject.TryGetComponent(out ICollidable other))
                {
                    if (collision.gameObject.TryGetComponent(out IProjectile _)) return;
                    OnCollided?.Invoke(other, collision.contacts[0].normal.ToCore());
                }
            }
        }
    }
}