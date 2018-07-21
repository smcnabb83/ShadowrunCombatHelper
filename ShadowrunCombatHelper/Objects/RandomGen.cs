using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Interfaces;

namespace ShadowrunCombatHelper.Objects
{
    class RandomGen : IRandomGenerator
    {
        Random actualGen = new Random();

        public RandomGen()
        {

        }

        public int Next(int low, int high)
        {
            return actualGen.Next(low, high);
        }
    }
}
