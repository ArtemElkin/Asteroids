using Plugins.MVVM.Attributes;
using UnityEngine;

namespace _Project.Features.UI.Settings
{
    public abstract class BaseSettingsPageView : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [Setter("Active")]
        public bool Active
        {
            set => _window.gameObject.SetActive(value);
        }
    }
}