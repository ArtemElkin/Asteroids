namespace _Project.Core.Factories
{
    public interface IFactory<in TSpawnData, out TEntity>
    {
        TEntity Create(TSpawnData data);
    }
}