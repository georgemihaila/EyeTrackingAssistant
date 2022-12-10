using STeM.Infrastructure.Models;

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
        public static Vector2 GetAverage(this IEnumerable<Vector2> dataPoints) => new Vector2(dataPoints.Select(x => x.X).Average(), dataPoints.Select(x => x.Y).Average());
        public static Vector2 GetAverage(this IEnumerable<TimedVector2> dataPoints) => dataPoints.Select(x => x.Vector2).GetAverage();
        public static Vector2 GetVariance(this IEnumerable<TimedVector2> dataPoints) => new Vector2((float)dataPoints.Select(x=>x.Vector2.X).GetVariance(), (float)dataPoints.Select(x=>x.Vector2.Y).GetVariance());
        public static double GetVariance(this IEnumerable<float> dataPoints) 
        {
            var mean = dataPoints.Average();
            var s = dataPoints.Sum(x => Math.Pow((x - mean), 2));
            var variance = s / (dataPoints.Count() - 1);
            return variance;
        }
    }
}
