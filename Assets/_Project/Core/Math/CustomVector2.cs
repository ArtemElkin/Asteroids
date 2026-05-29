using UnityEngine;

namespace _Project.Core.Math
{
    public struct CustomVector2
    {
        public float x;
        public float y;
        public float sqrMagnitude => x * x + y * y;
        public float magnitude => Mathf.Sqrt(sqrMagnitude);
        
        public CustomVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static CustomVector2 zero => new CustomVector2(0f, 0f);
        public CustomVector2 normalized
        {
            get
            {
                float mag = magnitude;
                if (mag > 0.00001f)
                {
                    return new CustomVector2(x / mag, y / mag);
                }
                
                return zero;
            }
        }
        
        public static implicit operator CustomVector2(Vector2 v) 
        {
            return new CustomVector2(v.x, v.y);
        }
        public static implicit operator Vector2(CustomVector2 v) 
        {
            return new Vector2(v.x, v.y);
        }
        
        public static CustomVector2 operator +(CustomVector2 a, CustomVector2 b) => new (a.x + b.x, a.y + b.y);

        public static CustomVector2 operator -(CustomVector2 a, CustomVector2 b) => new (a.x - b.x, a.y - b.y);

        public static CustomVector2 operator *(CustomVector2 a, CustomVector2 b) => new (a.x * b.x, a.y * b.y);

        public static CustomVector2 operator /(CustomVector2 a, CustomVector2 b) => new (a.x / b.x, a.y / b.y);

        public static CustomVector2 operator -(CustomVector2 a) => new (-a.x, -a.y);

        public static CustomVector2 operator *(CustomVector2 a, float d) => new (a.x * d, a.y * d);

        public static CustomVector2 operator *(float d, CustomVector2 a) => new (a.x * d, a.y * d);

        public static CustomVector2 operator /(CustomVector2 a, float d) => new (a.x / d, a.y / d);

        public static bool operator ==(CustomVector2 lhs, CustomVector2 rhs)
        {
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            return num1 * num1 + num2 * num2 < 9.999999439624929E-11;
        }
        public static bool operator !=(CustomVector2 lhs, CustomVector2 rhs) => !(lhs == rhs);

        public static implicit operator CustomVector2(CustomVector3 v) => new (v.x, v.y);

        public static implicit operator CustomVector3(CustomVector2 v) => new (v.x, v.y, 0.0f);
    }
}