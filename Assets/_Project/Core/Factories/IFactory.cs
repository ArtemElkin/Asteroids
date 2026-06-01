namespace _Project.Core.Factories
{
    public interface IFactory<in TData, out TEntity>
    {
        TEntity Create(TData data);
    }
}