using _Project.Core.Save;

namespace _Project.Core.Player
{
    public class PlayerSave : ISave
    {
        public int maxScore = 0;

        public PlayerSave Clone()
        {
            return new PlayerSave
            {
                maxScore = maxScore,
            };
        }
    }
}