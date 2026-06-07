using System;
using _Project.Core.Physics;
using _Project.Features.Projectile;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.UnityRender
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionHandler : MonoBehaviour, ICollidable
    {
        public event Action<Vector2> OnCollided;
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
            if (_timeLeftAfterLastCollision > CooldownTime)
            {
                if (collision.gameObject.TryGetComponent(out ICollidable _))
                {
                    if (collision.gameObject.TryGetComponent(out IProjectile _)) return;
                    var normal = collision.contacts[0].normal;
                    OnCollided?.Invoke(normal.ToCore());
                }
            }
        }
    }
}