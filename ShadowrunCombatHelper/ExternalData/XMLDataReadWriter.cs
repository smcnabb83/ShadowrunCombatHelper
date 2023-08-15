using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace ShadowrunCombatHelper.ExternalData
{
    public class XMLDataReadWriter<T> : IDataReadWriter<T>
    {
        public List<T> ReadFileToList(ApplicationXmlFiles.FileType fileType)
        {
            string filepath = ApplicationXmlFiles.GetFilePath(fileType);

            if (!Directory.Exists(ApplicationXmlFiles.UserFileDirectory))
            {
                     Directory.CreateDirectory(ApplicationXmlFiles.UserFileDirectory);
            }

            if(!File.Exists(filepath)) return new List<T>();

            using (var reader = new StreamReader(filepath))
            {
                try
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<T>));
                    List<T> readTo = (List<T>)deserializer.Deserialize(reader);
                    return readTo;
                }
                catch
                {
                    MessageBox.Show($"There was an error reading {filepath}. The file will be overwritten with a new file.", "Error reading file", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<T>();
                }
            }
        }

        public void WriteListToFile(ApplicationXmlFiles.FileType fileType, List<T> ListToWrite)
        {
            string filepath = ApplicationXmlFiles.GetFilePath(fileType);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

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