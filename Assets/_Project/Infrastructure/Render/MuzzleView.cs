using _Project.Core.Physics;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Render
{
    public class MuzzleView : MonoBehaviour, IHasPosition
    {
        public Vector2 Position => transform.position.ToCore();
    }
}