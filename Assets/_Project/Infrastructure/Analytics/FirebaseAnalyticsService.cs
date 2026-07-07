using _Project.Core.Analytics;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace _Project.Infrastructure.Analytics
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        private bool _isReady;
        
        
        public void Init()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                DependencyStatus dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    Debug.Log("[Firebase Analytics Service] Successfully initialize analytics!");
                    _isReady = true;
                }
                else
                {
                    Debug.LogError($"[Firebase Analytics Service] Cannot resolve dependencies: {dependencyStatus}");
                }
            });
        }
        
        public void LogEvent(string eventName)
        {
            if (!_isReady) return;
            
            FirebaseAnalytics.LogEvent(eventName);
        }
    }
}