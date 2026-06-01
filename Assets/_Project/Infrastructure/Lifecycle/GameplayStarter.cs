using _Project.Core.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Lifecycle
{
    public class GameplayStarter : MonoBehaviour
    {
        private ISignalBus _signalBus;


        private void Start()
        {
            _signalBus.Fire<GameInitializeSignal>();
            _signalBus.Fire<GameStartSignal>();
        }

        [Inject]
        private void Construct(ISignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    }
}