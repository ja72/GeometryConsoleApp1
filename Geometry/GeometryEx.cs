using System;
using System.Numerics;

namespace JA.Geometry
{
    public static class GeometryEx
    {
        public static Random RNG { get; } = new Random();

        public static Vector4 RandomVector4(float minValue = 0, float maxValue = 1)
            => new Vector4(
                minValue + (maxValue-minValue) * (float)RNG.NextDouble(),
                minValue + (maxValue-minValue) * (float)RNG.NextDouble(),
                minValue + (maxValue-minValue) * (float)RNG.NextDouble(),
                minValue + (maxValue-minValue) * (float)RNG.NextDouble());

    }
}
