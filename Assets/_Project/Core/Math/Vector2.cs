namespace _Project.Core.Math
{
    public struct Vector2
    {
        public float x;
        public float y;
        public float sqrMagnitude => x * x + y * y;
        public float magnitude => (float)System.Math.Sqrt(sqrMagnitude);
        
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2 zero => new Vector2(0f, 0f);
        public Vector2 normalized
        {
            get
            {
                float mag = magnitude;
                if (mag > 0.00001f)
                {
                    return new Vector2(x / mag, y / mag);
                }
                
                return zero;
            }
        }
        
        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            float num = -2f * Dot(inNormal, inDirection);
            return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
        }
        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }
        
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            t = Math.Clamp01(t);
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            float num1 = target.x - current.x;
            float num2 = target.y - current.y;
            float d = (float) ((double) num1 * (double) num1 + (double) num2 * (double) num2);
            if ((double) d == 0.0 || (double) maxDistanceDelta >= 0.0 && (double) d <= (double) maxDistanceDelta * (double) maxDistanceDelta)
                return target;
            float num3 = (float) System.Math.Sqrt((double) d);
            return new Vector2(current.x + num1 / num3 * maxDistanceDelta, current.y + num2 / num3 * maxDistanceDelta);
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxMagnitude)
        {
            if (vector.magnitude > maxMagnitude)
            {
                return vector.normalized * maxMagnitude;
            }
            return vector;
        }
        
        public static Vector2 operator +(Vector2 a, Vector2 b) => new (a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new (a.x - b.x, a.y - b.y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new (a.x * b.x, a.y * b.y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new (a.x / b.x, a.y / b.y);
        public static Vector2 operator -(Vector2 a) => new (-a.x, -a.y);
        public static Vector2 operator *(Vector2 a, float d) => new (a.x * d, a.y * d);
        public static Vector2 operator *(float d, Vector2 a) => new (a.x * d, a.y * d);
        public static Vector2 operator /(Vector2 a, float d) => new (a.x / d, a.y / d);
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            return num1 * num1 + num2 * num2 < 9.999999439624929E-11;
        }
        public static bool operator !=(Vector2 lhs, Vector2 rhs) => !(lhs == rhs);

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}