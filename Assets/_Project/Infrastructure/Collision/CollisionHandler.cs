using System;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Movement;
using _Project.Features.Common.Hit;
using _Project.Infrastructure.UnityServices;
using UnityEngine;

namespace _Project.Infrastructure.Collision
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionHandler : MonoBehaviour, ICollidable
    {
        public MovementModel MovementModel { get; private set; }
        public event Action<CollisionData> OnCollided;
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
            _timeLeftAfterLastCollision = CooldownTime;
        }

        public void Reset()
        {
            MovementModel = null;
            ActivateCollision();
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
            
            if (_timeLeftAfterLastCollision >= CooldownTime)
            {
                if (collision.gameObject.TryGetComponent(out ICollidable other) && other.MovementModel != null)
                {
                    if (collision.gameObject.TryGetComponent(out IHitSource _)) return;
                    var collisionData = new CollisionData(
                        MovementModel, 
                        other.MovementModel, 
                        collision.contacts[0].normal.ToCore(), 
                        collision.contacts[0].point.ToCore());
                    OnCollided?.Invoke(collisionData);
                }
            }
        }
    }
}