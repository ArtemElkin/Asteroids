namespace _Project.Core.Save
{
    public interface ISaveService
    {
        void Save(ISave save);
        T Load<T>() where T : ISave;
    }
}