using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Save;
using Zenject;


namespace _Project.Core.Player
{
    public class PlayerModel : IInitializable
    {
        public event Action<int> OnCurrentScoreChanged;
        private int _currentScore;
        private PlayerSave _playerSave = new();
        public int MaxScore => _playerSave.maxScore;
        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                if (value != _currentScore)
                {
                    _currentScore = value;
                    OnCurrentScoreChanged?.Invoke(value);
                }
            }
        }

        public void Initialize()
        {
            ResetCurrentScore();
        }

        public void IncreaseCurrentScore()
        {
            CurrentScore++;
        }

        public void ResetCurrentScore()
        {
            CurrentScore = 0;
        }

        public void TryUpdateMaxScore(int newMaxScore)
        {
            if (newMaxScore > MaxScore)
            {
                _playerSave.maxScore = newMaxScore;
            }
        }

        public PlayerSave GetSave() => _playerSave.Clone();

        public void LoadSave(PlayerSave loadedSave)
        {
            if (loadedSave == null) return;
            _playerSave = loadedSave.Clone();
        }
    }
}