using UnityEngine;

namespace _Project.Core.Math
{
    public struct CustomVector3
    {
        public float x;
        public float y;
        public float z;

        public CustomVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static implicit operator CustomVector3(Vector3 v) 
        {
            return new CustomVector3(v.x, v.y, v.z);
        }
        
        public static implicit operator Vector3(CustomVector3 v) 
        {
            return new Vector3(v.x, v.y, v.z);
        }
        
        public static implicit operator CustomVector2(CustomVector3 v) => new (v.x, v.y);

        public static implicit operator CustomVector3(CustomVector2 v) => new (v.x, v.y, 0.0f);
    }
}