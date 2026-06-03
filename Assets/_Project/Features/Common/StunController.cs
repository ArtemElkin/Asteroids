using System.Threading.Tasks;
using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public class StunController
    {
        private readonly IStunable _stunable;
        private readonly ICollidable _collidable;


        public StunController(IStunable stunable, ICollidable collidable)
        {
            _stunable = stunable;
            _collidable = collidable;
        }

        public async Task ApplyStun(float duration)
        {
            _stunable.SetStunned(true);
            _collidable.DeactivateCollision();
            
            await Task.Delay((int)(duration * 1000));

            _stunable?.SetStunned(false);
            _collidable?.ActivateCollision();
        }
    }
}