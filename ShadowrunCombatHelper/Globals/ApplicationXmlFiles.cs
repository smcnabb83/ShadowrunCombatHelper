using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ShadowrunCombatHelper.Globals
{
    public static class ApplicationXmlFiles
    {
        public enum fileType { CHARACTERDATA, SKILLDATA }

        private static Dictionary<fileType, string> charFiles = new Dictionary<fileType, string>
        {
            {fileType.CHARACTERDATA, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "chardata.xml") },
            {fileType.SKILLDATA, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "skilldata.xml") }
        };

        public static string GetFilePath(fileType type)
        {
            return charFiles[type];
        }
    }
}