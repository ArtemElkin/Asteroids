namespace _Project.Core.StaticData
{
    public static class FileNames
    {
        public static class Config
        {
            public const string Game = "GameConfig";
            public const string Awards = "AwardsConfig";
            public const string AdUnits = "AdUnitsIdsConfig";

            public static class Entities
            {
                public const string Spaceship = "SpaceshipConfig";
                public const string Asteroid = "AsteroidConfig";
                public const string Ufo = "UFOConfig";
            }
        }

        public static class Save
        {
            public const string Player = "PlayerSave";
            public const string GameSettings = "GameSettings";
        }

        public static class Scene
        {
            public const string MainMenu = "MainMenu";
            public const string Gameplay = "Game";
        }
    }
}