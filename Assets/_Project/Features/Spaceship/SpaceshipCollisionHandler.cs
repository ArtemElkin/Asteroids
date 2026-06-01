using System;
using _Project.Features.Common;
using UnityEngine;

namespace _Project.Features.Spaceship
{
    public class SpaceshipCollisionHandler : MonoBehaviour, IHitable
    {
        public event Action OnHit;
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnHit?.Invoke();
        }
    }
}