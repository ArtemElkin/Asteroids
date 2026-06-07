using _Project.Core.Physics;

namespace _Project.Features.Common.Signals
{
    public class CollisionDetectedSignal
    {
        public readonly CollisionData collisionData;


        public CollisionDetectedSignal(CollisionData collisionData)
        {
            this.collisionData = collisionData;
        }
        
    }
}