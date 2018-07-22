using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ShadowrunCombatHelper.Globals
{
    public static class ApplicationXmlFiles
    {
        public enum fileType { CHARACTERDATA, SKILLDATA, AFFILIATIONDATA }

        private static Dictionary<fileType, string> charFiles = new Dictionary<fileType, string>
        {
            {fileType.CHARACTERDATA, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "chardata.xml") },
            {fileType.SKILLDATA, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "skilldata.xml") },
            {fileType.AFFILIATIONDATA, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "affiliationdata.xml")  }
        };

        public static string GetFilePath(fileType type)
        {
            return charFiles[type];
        }
    }
}