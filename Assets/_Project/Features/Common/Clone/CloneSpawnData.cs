using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Common.Clone
{
    public struct CloneSpawnData
    {
        public readonly Vector2 cloneOffset;
        public readonly IReadOnlyPositionable _originPositionable;
        public readonly IReadOnlyRotationable _originRotationable;
        public readonly IDrawable drawable;

        public CloneSpawnData(
            Vector2 cloneOffset,
            IReadOnlyPositionable originPositionable,
            IReadOnlyRotationable originRotationable,
            IDrawable drawable)
        {
            this.cloneOffset = cloneOffset;
            this._originPositionable = originPositionable;
            this._originRotationable = originRotationable;
            this.drawable = drawable;
        }
    }
}