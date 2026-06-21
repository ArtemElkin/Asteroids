using System;

namespace _Project.Core.Player
{
    public class PlayerModel
    {
        public event Action<int> CurrentScoreChanged;
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
                    CurrentScoreChanged?.Invoke(value);
                }
            }
        }

        public void IncreaseCurrentScore(int value)
        {
            CurrentScore += value;
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