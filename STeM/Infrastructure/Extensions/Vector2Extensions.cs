using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace STeM.Infrastructure.Extensions
{
    public static class Vector2Extensions
    {
        private const double WHITE_NOISE_THRESHOLD = 5;
        public static bool IsWhiteNoise(this IEnumerable<Vector2> dataPoints)
        {
            var xVariance = dataPoints.Select(x => x.X).GetVariance();
            var yVariance = dataPoints.Select(x => x.Y).GetVariance();
            return xVariance < WHITE_NOISE_THRESHOLD || yVariance < WHITE_NOISE_THRESHOLD;
        }

        public static double GetVariance(this IEnumerable<float> dataPoints) 
        {
            var mean = dataPoints.Average();
            var s = dataPoints.Sum(x => Math.Pow((x - mean), 2));
            var variance = s / (dataPoints.Count() - 1);
            return variance;
        }
    }
}
