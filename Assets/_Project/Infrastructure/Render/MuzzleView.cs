using System;
using _Project.Core.Physics;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Render
{
    public class MuzzleView : MonoBehaviour, IReadOnlyPosition
    {
        public Vector2 Position => transform.position.ToCore();
        public event Action<Vector2> PositionChanged;
    }
}