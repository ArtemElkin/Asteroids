using Plugins.MVVM.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [Data("MaxScore")]
        public TMP_Text maxScore;
        [Data("OnStartClick")]
        public Button startButton;
        [Data("OnSettingsClick")]
        public Button settingsButton;
        [Setter("Active")]
        public bool Active
        {
            set => _window.gameObject.SetActive(value);
        }
    }
}