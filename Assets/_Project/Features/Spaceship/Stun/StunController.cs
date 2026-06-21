using System.Threading.Tasks;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Render.VFX;

namespace _Project.Features.Spaceship.Stun
{
    public class StunController
    {
        private readonly IStunnable _stunnable;
        private readonly ICollidable _collidable;
        private readonly IEffect _stunEffect;


        public StunController(
            IStunnable stunnable, 
            ICollidable collidable,
            IEffect stunEffect)
        {
            _stunnable = stunnable;
            _collidable = collidable;
            _stunEffect = stunEffect;
        }

        public async Task ApplyStun(float duration)
        {
            _stunnable.SetStunned(true);
            _collidable.DeactivateCollision();
            _stunEffect.Play();
            
            await Task.Delay((int)(duration * 1000));
            
            _stunnable?.SetStunned(false);
            _collidable?.ActivateCollision();
            _stunEffect?.Stop();
        }
    }
}