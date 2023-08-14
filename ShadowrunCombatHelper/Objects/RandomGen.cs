using ShadowrunCombatHelper.Interfaces;
using System;

namespace ShadowrunCombatHelper.Objects
{
    internal class RandomGen : IRandomGenerator
    {
        private readonly Random actualGen = new Random();

        public RandomGen()
        {
        }

        public int Next(int low, int high)
        {
            return actualGen.Next(low, high);
        }
    }
}