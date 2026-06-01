namespace _Project.Infrastructure.Factories
{
    public interface IFactory<in TData, out TEntity>
    {
        TEntity Create(TData data);
    }
}