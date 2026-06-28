using Plugins.MVVM.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.Settings
{
    public class SettingsView  : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [Data("OnBackToMenuClick")]
        public Button backToMenuButton;
        [Data("OnNextPageClick")]
        public Button nextPageButton;
        [Setter("Active")]
        public bool Active
        {
            set => _window.gameObject.SetActive(value);
        }
    }
}