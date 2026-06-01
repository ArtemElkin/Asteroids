namespace _Project.Features.Asteroid.Signals
{
    public class DespawnRequestedSignal
    {
        public AsteroidFacade asteroidFacade;


        public DespawnRequestedSignal(AsteroidFacade asteroidFacade)
        {
            this.asteroidFacade = asteroidFacade;
        }
    }
}