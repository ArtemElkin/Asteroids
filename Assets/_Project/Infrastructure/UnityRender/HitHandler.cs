using System;
using _Project.Features.Common;
using _Project.Features.Projectile;
using UnityEngine;

namespace _Project.Infrastructure.UnityRender
{
    public class HitHandler : MonoBehaviour, IHitable
    {
        public event Action OnHit;
        private const float CooldownTime = 0.5f;
        private float _timeLeftAfterLastHit;
        
        
        private void Update()
        {
            if (_timeLeftAfterLastHit < CooldownTime)
                _timeLeftAfterLastHit += Time.deltaTime;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_timeLeftAfterLastHit > CooldownTime)
            {
                if (collision.gameObject.TryGetComponent(out IProjectile _))
                {
                    OnHit?.Invoke();
                }
            }
        }
    }
}