using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Globals;
namespace ShadowrunCombatHelper.ExternalData
{
    public static class SkillDataReader
    {
        private static string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "skilldata.xml");
        public static List<Skill> ReadSkillDataToSkillList()
        {
            CharacterList clist = CharacterList.Instance;
            if (File.Exists(filepath))
            {

                List<Skill> readTo = new List<Skill>();

                using (var reader = new StreamReader(filepath))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Skill>));
                    readTo = (List<Skill>)deserializer.Deserialize(reader);
                    return readTo;
                }
            }
            else
            {
                return new List<Skill>();
            }
        }

        public static void WriteSkillListToXMLFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Skill>));
            SkillsList slist = SkillsList.Instance;

            using (var writer = new StreamWriter(filepath))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    serializer.Serialize(xmlWriter, slist.Skills);
                }
                writer.Close();
            }
        }
    }
}
