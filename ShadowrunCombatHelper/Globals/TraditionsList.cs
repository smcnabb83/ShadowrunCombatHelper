using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class TraditionsList
    {
        private List<MagicTradition> traditionList = new List<MagicTradition>();

        public List<MagicTradition> TraditionList
        {
            get { return traditionList; }
            set { traditionList = value; }
        }

        static TraditionsList()
        {

        }

        private TraditionsList()
        {
            TraditionList.Add(new MagicTradition("Hermetic", new List<Skill.Attributes>() { Skill.Attributes.LOG, Skill.Attributes.WIL }));
            TraditionList.Add(new MagicTradition("Shamanic", new List<Skill.Attributes>() { Skill.Attributes.CHA, Skill.Attributes.WIL }));
            TraditionList.Add(null);
        }

        public static TraditionsList Instance { get; } = new TraditionsList();

    }
}
