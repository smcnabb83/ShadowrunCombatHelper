using ShadowrunCombatHelper.Models;
using System.Collections.Generic;

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
            TraditionList.Add(new MagicTradition("NONE", new List<Skill.Attributes>()));
        }

        public static TraditionsList Instance { get; } = new TraditionsList();
    }
}