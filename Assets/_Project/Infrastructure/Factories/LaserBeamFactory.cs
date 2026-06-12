using _Project.Core.Physics;
using _Project.Core.Render;
using _Project.Features.Spaceship.Weapon.LaserBeam;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class LaserBeamFactory : AbstractFactory<LaserBeamSpawnData, LaserBeamFacade>
    {
        public LaserBeamFactory(IInstantiator instantiator, TransformView prefab, Transform parentTransform) : base(instantiator, prefab, parentTransform)
        {
        }
        
        public override LaserBeamFacade Create(LaserBeamSpawnData data)
        {
            TransformView view = (TransformView)_viewPool.Get();
            IDrawable drawable = view;
            drawable.Setup(data.initialPosition, data.initialRotationAngle);
            LaserBeamFacade facade = CreateComponent<LaserBeamFacade>(drawable, data.aliveTime);
            return facade;
            
        }
    }
}