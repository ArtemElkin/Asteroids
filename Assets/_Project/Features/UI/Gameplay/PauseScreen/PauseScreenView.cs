using Plugins.MVVM.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.Gameplay.PauseScreen
{
    public class PauseScreenView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [Data("Title")]
        public TMP_Text title;
        [Data("Score")]
        public TMP_Text currentScoreText;
        [Data("MaxScore")]
        public TMP_Text maxScore;
        [Data("OnRestartClick")]
        public Button restartButton;
        [Data("OnResumeClick")]
        public Button resumeButton;
        [Data("OnMainMenuClick")]
        public Button mainMenuButton;

        [Setter("Active")]
        public bool Active
        {
            set => _canvas.gameObject.SetActive(value);
        }

        [Setter("IsGameOver")]
        public bool IsGameOver
        {
            set
            {
                restartButton.gameObject.SetActive(value);
                resumeButton.gameObject.SetActive(!value);
            }
        }
    }
}