using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ExternalData;
using ShadowrunCombatHelper.Interfaces;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class SkillsList
    {
        private List<Skill> _skills = new List<Skill>();

        private IDataReadWriter<Skill> readWriter = new XMLDataReadWriter<Skill>();

        public List<Skill> Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }


        static SkillsList()
        {

        }

        private SkillsList()
        {
            Skills = readWriter.ReadFileToList(ApplicationXmlFiles.fileType.SKILLDATA);

            if (Skills.Count == 0)
            {
                Skills.Add(new Skill("Archery", 0, null, new List<Skill.Attributes>() { Skill.Attributes.AGI }, Skill.Attributes.AGI, "Combat"));
                Skills.Add(new Skill("Automatics", 0, null, new List<Skill.Attributes>() { Skill.Attributes.AGI }, Skill.Attributes.AGI, "Combat"));
                Skills.Add(new Skill("Diving", 0, null, new List<Skill.Attributes>() { Skill.Attributes.BOD }, Skill.Attributes.BOD));
            }
        }

        public void OverwriteSkills(List<Skill> newSkills)
        {
            Skills = newSkills;
            readWriter.WriteListToFile(ApplicationXmlFiles.fileType.SKILLDATA, Skills); ;
        }

        public static SkillsList Instance { get; } = new SkillsList();
    }
}
