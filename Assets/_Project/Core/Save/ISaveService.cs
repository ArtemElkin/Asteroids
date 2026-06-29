namespace _Project.Core.Save
{
    public interface ISaveService
    {
        void Save(ISave save, string fileName);
        T Load<T>(string fileName) where T : ISave;
    }
}