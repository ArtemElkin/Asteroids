namespace _Project.Core.Factories
{
    public interface IFactory<in TData, TEntity>
    {
        TEntity Create(TData data);
        void Release(TEntity facade);
    }
}