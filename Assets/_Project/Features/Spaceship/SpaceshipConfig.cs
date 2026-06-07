using _Project.Core.Config;
using _Project.Features.Spaceship.Weapon;

namespace _Project.Features.Spaceship
{
    public class SpaceshipConfig : IConfig
    {
        public int maxHp;
        public bool hasClones;
        public SpaceshipMovementConfig movementConfig;
        public WeaponConfig weaponConfig;
    }
}