using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Globals;

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
