using System.Collections.Generic;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class TraditionsList
    {
        static TraditionsList()
        {
        }

        private TraditionsList()
        {
            TraditionList.Add(new MagicTradition("Hermetic",
                new List<Skill.Attributes> {Skill.Attributes.LOG, Skill.Attributes.WIL}));
            TraditionList.Add(new MagicTradition("Shamanic",
                new List<Skill.Attributes> {Skill.Attributes.CHA, Skill.Attributes.WIL}));
            TraditionList.Add(new MagicTradition("NONE", new List<Skill.Attributes>()));
        }

        public List<MagicTradition> TraditionList { get; set; } = new List<MagicTradition>();

        public static TraditionsList Instance { get; } = new TraditionsList();
    }
}