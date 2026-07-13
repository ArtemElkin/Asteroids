using _Project.Core.Input;
using _Project.Infrastructure.Input.MobileInput;
using _Project.Infrastructure.Input.StandaloneInput;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private bool _useMobileInputInEditor;
        [SerializeField] private GameObject _standaloneInputHandler;
        [SerializeField] private GameObject _mobileInputHandler;
        
        
        public override void InstallBindings()
        {
            switch (InputTypeSelector.Select(_useMobileInputInEditor))
            {
                case InputType.Mobile:
                    BindMobileInput(_mobileInputHandler);
                    break;
                case InputType.Standalone:
                    BindStandaloneInput(_standaloneInputHandler);
                    break;
            }
        }
        
        private void BindStandaloneInput(GameObject inputHandler)
        {
            Container
                .BindInterfacesTo<StandaloneInputHandler>()
                .FromComponentOn(inputHandler)
                .AsSingle()
                .NonLazy();
            
            inputHandler.SetActive(true);
        }

        private void BindMobileInput(GameObject inputHandler)
        {
            Container
                .BindInterfacesTo<MobileInputHandler>()
                .FromComponentOn(inputHandler)
                .AsSingle()
                .NonLazy();
            
            inputHandler.SetActive(true);
        }
    }
}