namespace _Project.Core.Math
{
    public static class Math
    {
        public const float Rad2Deg = 57.29578f;
        public static float Atan2(float y, float x) => (float) System.Math.Atan2(y, x);
        public static float Clamp01(float value)
        {
            if (value < 0.0)
                return 0.0f;
            return value > 1.0 ? 1f : value;
        }
    }
}