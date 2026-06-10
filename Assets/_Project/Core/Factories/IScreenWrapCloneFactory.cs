using _Project.Core.Render;

namespace _Project.Core.Factories
{
    public interface IScreenWrapCloneFactory<in TSpawnData, TOriginEntity>
    {
        IDrawable Create(TSpawnData data);
        void Release(IDrawable screenWrapCloneDrawable);
    }
}