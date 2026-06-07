using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Common.Signals
{
    public class CloneCollidedSignal<TOriginFacade> where TOriginFacade : IFacade
    {
        public Vector2 normal;


        public CloneCollidedSignal(Vector2 normal)
        {
            Debug.Log("Clone collided signal");
            this.normal = normal;
        }
    }
}