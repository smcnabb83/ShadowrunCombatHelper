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
    public static class CharacterDataReader
    {

        private static string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "chardata.xml");
        public static List<Character> ReadCharacterDataToCharacterList()
        {
            CharacterList clist = CharacterList.Instance;
            if (File.Exists(filepath))
            {

                List<Character> readTo = new List<Character>();

                using (var reader = new StreamReader(filepath))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Character>));
                    readTo = (List<Character>)deserializer.Deserialize(reader);
                    return readTo;
                }
            }
            else
            {
                return new List<Character>();
            }
        }

        public static void WriteCharacterListToXMLFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Character>));
            CharacterList clist = CharacterList.Instance;

            using (var writer = new StreamWriter(filepath))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    serializer.Serialize(xmlWriter, clist.GetCharacterList());                    
                }
                writer.Close();
            }
        }
    }


}
