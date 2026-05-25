using System.Collections.Generic;
using _Project.Core.Infrastructure.Save;


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