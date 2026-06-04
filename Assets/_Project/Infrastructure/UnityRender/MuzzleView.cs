using _Project.Core.Physics;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.UnityRender
{
    public class MuzzleView : MonoBehaviour, IReadOnlyPositionable
    {
        public Vector2 Position => transform.position.ToCore();
    }
}