namespace _Project.Core.Math
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static implicit operator Vector2(Vector3 v) => new (v.x, v.y);

        public static implicit operator Vector3(Vector2 v) => new (v.x, v.y, 0.0f);
    }
}