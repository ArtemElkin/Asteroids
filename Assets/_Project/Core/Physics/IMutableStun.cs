namespace _Project.Core.Physics
{
    public interface IMutableStun : IHasStun
    {
        void SetStunned(bool isStunned);
    }
}