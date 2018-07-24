using System;
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
            {fileType.CHARACTERDATA, Path.Combine(UserFileDirectory, "chardata.xml") },
            {fileType.SKILLDATA, Path.Combine(UserFileDirectory, "skilldata.xml") },
            {fileType.AFFILIATIONDATA, Path.Combine(UserFileDirectory, "affiliationdata.xml")  }
        };

        public static string UserFileDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ShadowRunCombatHelper");
            }
        }

        public static string GetFilePath(fileType type)
        {
            return charFiles[type];
        }
    }
}