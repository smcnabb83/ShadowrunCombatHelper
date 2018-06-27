using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using System.Windows.Media;

namespace ShadowrunCombatHelper.Globals
{
    public static class AffiliationList
    {
        public static List<Affiliation> Affiliations = new List<Affiliation>() { new Affiliation() { Name="Player", BackgroundColor = new int[] {0,0,255 }, ForegroundColor = new int[] {0,0,0 } },
                                                                                 new Affiliation() { Name="Enemy", BackgroundColor = new int[] {255,0,0 }, ForegroundColor = new int[] {0,0,0 } } };


        
    }
}
