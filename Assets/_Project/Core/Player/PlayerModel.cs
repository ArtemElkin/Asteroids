using System;
using _Project.Core.Save;

namespace _Project.Core.Player
{
    public class PlayerModel : ISaveable<PlayerSave>
    {
        public event Action<int> CurrentScoreChanged;
        public event Action<int> MaxScoreChanged;
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
                MaxScoreChanged?.Invoke(newMaxScore);
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