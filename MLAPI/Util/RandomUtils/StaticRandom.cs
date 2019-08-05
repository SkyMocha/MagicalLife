﻿using System;
using System.Threading;

namespace MLAPI.Util.RandomUtils
{
    public static class StaticRandom
    {
        private static int seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        /// <summary>
        /// Returns a random number.
        /// </summary>
        /// <param name="min">Inclusive minimum.</param>
        /// <param name="max">Exclusive maximum.</param>
        /// <returns></returns>
        public static int Rand(int min, int max)
        {
            return random.Value.Next(min, max);
        }

        public static double Rand(double min, double max)
        {
            //if (   min < 0 
            //    || max < 0 
            //    || min > 1 
            //    || max > 1)
            //{
            //    throw new ArgumentOutOfRangeException();
            //}

            return (random.Value.NextDouble() * (max - min)) + min;
        }
    }
}