using System;
using _Project.Features.Common.Hit;
using UnityEngine;

namespace _Project.Infrastructure.Collision
{
    public class HitSource : MonoBehaviour, IHitSource
    {
        public event Action OnHit;


        private void OnCollisionEnter2D(Collision2D other)
        {
            OnHit?.Invoke();
        }
    }
}