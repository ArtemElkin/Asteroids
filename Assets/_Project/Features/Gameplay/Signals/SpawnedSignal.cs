namespace _Project.Features.Gameplay.Signals
{
    public class SpawnedSignal<T> where T : class
    {
        public T spawnedObj;
        
        
        public SpawnedSignal(T spawnedObj) =>  this.spawnedObj = spawnedObj;
    }
}