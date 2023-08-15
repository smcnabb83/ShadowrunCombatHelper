using System;
using System.Collections.Generic;
using ShadowrunCombatHelper.ExternalData;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class CharacterList
    {
        private static readonly Lazy<CharacterList> lazy = new Lazy<CharacterList>(() => new CharacterList());

        private List<Character> CharList = new List<Character>();

        private readonly IDataReadWriter<Character> readWriter = new XMLDataReadWriter<Character>();

        private CharacterList()
        {
        }

        public static CharacterList Instance => lazy.Value;

        public List<Character> GetCharacterList()
        {
            return CharList;
        }

        public void AddCharacter(Character c)
        {
            CharList.Add(c);
            readWriter.WriteListToFile(ApplicationXmlFiles.FileType.CharacterData, CharList);
        }

        public void RemoveCharacter(Character c)
        {
            CharList.Remove(c);
            readWriter.WriteListToFile(ApplicationXmlFiles.FileType.CharacterData, CharList);
        }

        public void OverwriteCharacterList(List<Character> clist)
        {
            CharList = clist;
            readWriter.WriteListToFile(ApplicationXmlFiles.FileType.CharacterData, CharList);
        }

        public void RemoveCharacterAtIndex(int i)
        {
            if (i >= 0 && i < CharList.Count)
            {
                CharList.RemoveAt(i);
            }
        }

        public void ReadCharacterDataFromFile()
        {
            CharList = readWriter.ReadFileToList(ApplicationXmlFiles.FileType.CharacterData);
        }

        public void ForceCharacterDataSave()
        {
            readWriter.WriteListToFile(ApplicationXmlFiles.FileType.CharacterData, CharList);
        }
    }
}