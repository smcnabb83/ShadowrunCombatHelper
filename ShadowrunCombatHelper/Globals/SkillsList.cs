using System.Collections.Generic;
using ShadowrunCombatHelper.ExternalData;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class SkillsList
    {
        private readonly IDataReadWriter<Skill> readWriter = new XMLDataReadWriter<Skill>();

        static SkillsList()
        {
        }

        private SkillsList()
        {
            Skills = readWriter.ReadFileToList(ApplicationXmlFiles.FileType.SkillData);

            if (Skills.Count == 0)
            {
                Skills.Add(new Skill("Archery", 0, null, new List<Skill.Attributes> {Skill.Attributes.AGI},
                    Skill.Attributes.AGI, "Combat"));
                Skills.Add(new Skill("Automatics", 0, null, new List<Skill.Attributes> {Skill.Attributes.AGI},
                    Skill.Attributes.AGI, "Combat"));
                Skills.Add(new Skill("Diving", 0, null, new List<Skill.Attributes> {Skill.Attributes.BOD},
                    Skill.Attributes.BOD));
            }
        }

        public List<Skill> Skills { get; set; } = new List<Skill>();

        public static SkillsList Instance { get; } = new SkillsList();

        public void OverwriteSkills(List<Skill> newSkills)
        {
            Skills = newSkills;
            readWriter.WriteListToFile(ApplicationXmlFiles.FileType.SkillData, Skills);
            ;
        }
    }
}