using _Project.Infrastructure.Audio;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI.Global
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _audioServicePrefab;


        public override void InstallBindings()
        {
            BindAudioService(_audioServicePrefab);
        }
        
        private void BindAudioService(GameObject audioServicePrefab)
        {
            Container
                .BindInterfacesAndSelfTo<AudioService>()
                .FromComponentInNewPrefab(audioServicePrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}