using _Project.Core.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Lifecycle
{
    public class GameplayStarter : MonoBehaviour
    {
        private ISignalBus _signalBus;


        private void Awake()
        {
            _signalBus.Fire<InitializeGameSignal>();
        }

        private void Start()
        {
            _signalBus.Fire<StartGameSignal>();
        }

        [Inject]
        private void Construct(ISignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    }
}