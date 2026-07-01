using _Project.Core.Math;
using _Project.Core.Render.VFX;

namespace _Project.Core.Factories
{
    public interface IEffectFactory<TMarker> : IFactory<Vector2, IEffect>, IReleaser<IEffect>
    {
    }
}