using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Render;

namespace _Project.Features.Common.ScreenWrapClone
{
    public struct ScreenWrapCloneSpawnData
    {
        public readonly MovementModel originMovementModel;
        public readonly Vector2 cloneOffset;
        public readonly IDrawable originDrawable;

        public ScreenWrapCloneSpawnData(
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