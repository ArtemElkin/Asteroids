using System.Threading.Tasks;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Features.Common.Effect;

namespace _Project.Features.Spaceship.Stun
{
    public class StunController
    {
        private readonly IStunable _stunable;
        private readonly ICollidable _collidable;
        private readonly IEffect _stunEffect;


        public StunController(
            IStunable stunable, 
            ICollidable collidable,
            IEffect stunEffect)
        {
            _stunable = stunable;
            _collidable = collidable;
            _stunEffect = stunEffect;
        }

        public async Task ApplyStun(float duration)
        {
            _stunable.SetStunned(true);
            _collidable.DeactivateCollision();
            _stunEffect.Play();
            
            await Task.Delay((int)(duration * 1000));
            
            _stunable?.SetStunned(false);
            _collidable?.ActivateCollision();
            _stunEffect?.Stop();
        }
    }
}