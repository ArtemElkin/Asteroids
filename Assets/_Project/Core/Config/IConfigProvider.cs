namespace _Project.Core.Config
{
    public interface IConfigProvider
    {
        T GetConfig<T>(string path) where T : IConfig;
    }
}