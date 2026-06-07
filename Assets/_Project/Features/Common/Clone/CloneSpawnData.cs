using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Common.Clone
{
    public struct CloneSpawnData
    {
        public readonly MovementModel originMovementModel;
        public readonly Vector2 cloneOffset;
        public readonly IDrawable originDrawable;

        public CloneSpawnData(
            MovementModel originMovementModel,
            Vector2 cloneOffset,
            IDrawable originDrawable)
        {
            this.originMovementModel = originMovementModel;
            this.cloneOffset = cloneOffset;
            this.originDrawable = originDrawable;
        }
    }
}