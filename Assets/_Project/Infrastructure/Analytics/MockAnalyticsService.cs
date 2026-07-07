using _Project.Core.Analytics;
using UnityEngine;

namespace _Project.Infrastructure.Analytics
{
    public class MockAnalyticsService : IAnalyticsService
    {
        public void Init() { }

        public void LogEvent(string eventName) => Debug.Log("[Mock Analytics Service] LogEvent: " + eventName);
    }
}