using System;
using _Project.Core.Physics;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.UnityRender
{
    public class CollisionHandler : MonoBehaviour, ICollidable
    {
        public event Action<Vector2> OnCollided;
        private const float CooldownTime = 0.5f;
        private float _timeLeftAfterLastCollision;


        private void Update()
        {
            if (_timeLeftAfterLastCollision < CooldownTime)
                _timeLeftAfterLastCollision += Time.deltaTime;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_timeLeftAfterLastCollision > CooldownTime)
            {
                if (collision.gameObject.TryGetComponent(out ICollidable _))
                {
                    var normal = collision.contacts[0].normal;
                    OnCollided?.Invoke(normal.ToCore());
                }
            }
        }
    }
}