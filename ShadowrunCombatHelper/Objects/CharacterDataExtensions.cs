using ShadowrunCombatHelper.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunCombatHelper.Objects
{
    public static class CharacterDataExtensions
    {
        public static List<string> GetSkillTypes(this Character cha)
        {
            return cha.Skills.Select(x => x.SkillType).Distinct().ToList();
        }
    }
}