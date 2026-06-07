using _Project.Core.Config;
using _Project.Features.Spaceship.Weapon.Config;

namespace _Project.Features.Spaceship.Config
{
    public class SpaceshipConfig : IConfig
    {
        public int maxHp;
        public bool hasClones;
        public SpaceshipMovementConfig movementConfig;
        public WeaponConfig weaponConfig;
    }
}