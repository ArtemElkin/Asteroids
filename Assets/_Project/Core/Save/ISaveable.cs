namespace _Project.Core.Save
{
    public interface ISaveable<TSave> where TSave : ISave
    {
        TSave GetSave();
        void LoadSave(TSave save);
    }
}