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
using ShadowrunCombatHelper.Interfaces;
using System.Windows;

namespace ShadowrunCombatHelper.ExternalData
{
    public class XMLDataReadWriter<T> : IDataReadWriter<T>
    {
        public List<T> ReadFileToList(ApplicationXmlFiles.fileType fileType)
        {
            string filepath = ApplicationXmlFiles.GetFilePath(fileType);
            if (File.Exists(filepath))
            {

                List<T> readTo = new List<T>();

                using (var reader = new StreamReader(filepath))
                {
                    try
                    {
                        XmlSerializer deserializer = new XmlSerializer(typeof(List<T>));
                        readTo = (List<T>)deserializer.Deserialize(reader);
                        return readTo;
                    }
                    catch
                    {
                        MessageBox.Show($"There was an error reading {filepath}. The file will be overwritten with a new file.", "Error reading file", MessageBoxButton.OK, MessageBoxImage.Error);
                        return new List<T>();
                    }
                }
            }
            else
            {
                return new List<T>();
            }
        }


        public void WriteListToFile(ApplicationXmlFiles.fileType fileType, List<T> ListToWrite)
        {

            string filepath = ApplicationXmlFiles.GetFilePath(fileType);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            CharacterList clist = CharacterList.Instance;

            using (var writer = new StreamWriter(filepath))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    serializer.Serialize(xmlWriter, ListToWrite);
                }
                writer.Close();
            }
        }
    }


}
