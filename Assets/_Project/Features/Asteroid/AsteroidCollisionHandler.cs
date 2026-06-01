using System;
using _Project.Features.Common;
using UnityEngine;

namespace _Project.Features.Asteroid
{
    [RequireComponent(typeof(Collider2D))]
    public class AsteroidCollisionHandler : MonoBehaviour, IHitable
    {
        public event Action OnHit;
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ProjectileCollisionHandler projectile))
            {
                OnHit?.Invoke();
            }
        }
    }
}