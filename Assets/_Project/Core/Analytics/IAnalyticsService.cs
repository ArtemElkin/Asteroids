namespace _Project.Core.Analytics
{
    public interface IAnalyticsService
    {
        void Init();
        void LogEvent(string eventName);
    }
}