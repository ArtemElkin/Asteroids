using UnityEngine;

namespace _Project.Infrastructure.UnityRender
{
    public class SpaceshipView : MovableView
    {
        [SerializeField] private MuzzleView _muzzleView;

        public MuzzleView GetMuzzleView() => _muzzleView;

    }
}