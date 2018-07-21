using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunCombatHelper.Interfaces
{
    public interface IRandomGenerator
    {
        int Next(int low, int high);
    }
}
