using System;
using System.Collections.Generic;
using System.IO;

namespace ShadowrunCombatHelper.Globals
{
    public static class ApplicationXmlFiles
    {
        public enum FileType
        {
            CharacterData,
            SkillData,
            AffiliationData
        }

        private static readonly Dictionary<FileType, string> charFiles = new Dictionary<FileType, string>
        {
            {FileType.CharacterData, Path.Combine(UserFileDirectory, "chardata.xml")},
            {FileType.SkillData, Path.Combine(UserFileDirectory, "skilldata.xml")},
            {FileType.AffiliationData, Path.Combine(UserFileDirectory, "affiliationdata.xml")}
        };

        public static string UserFileDirectory =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ShadowRunCombatHelper");

        public static string GetFilePath(FileType type)
        {
            return charFiles[type];
        }
    }
}