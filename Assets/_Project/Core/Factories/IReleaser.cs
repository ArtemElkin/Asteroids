namespace _Project.Core.Factories
{
    public interface IReleaser<in TEntity>
    {
        void Release(TEntity entity);
    }
}