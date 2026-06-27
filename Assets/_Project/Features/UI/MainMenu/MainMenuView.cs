using Plugins.MVVM.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Data("MaxScore")]
        public TMP_Text maxScore;
        [Data("OnStartClick")]
        public Button startButton;
    }
}