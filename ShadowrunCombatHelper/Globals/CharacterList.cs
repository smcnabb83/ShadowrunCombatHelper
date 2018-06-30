using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ExternalData;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class CharacterList
    {
        private static readonly Lazy<CharacterList> lazy = new Lazy<CharacterList>(() => new CharacterList());

        public static CharacterList Instance { get { return lazy.Value; } }

        private List<Character> CharList = new List<Character>();

        public List<Character> GetCharacterList()
        {
            return CharList;
        }

        public void AddCharacter(Character c)
        {
            CharList.Add(c);
            AddSkillsToCharacter(c);
            CharacterDataReader.WriteCharacterListToXMLFile();
        }

        public void RemoveCharacter(Character c)
        {
            CharList.Remove(c);
            CharacterDataReader.WriteCharacterListToXMLFile();
        }
        
        public void OverwriteCharacterList(List<Character> clist)
        {
            CharList = clist;
            foreach(Character c in CharList)
            {
                AddSkillsToCharacter(c);
            }
            CharacterDataReader.WriteCharacterListToXMLFile();
        }

        public void RemoveCharacterAtIndex(int i)
        {
            if(i >= 0 && i < CharList.Count)
            {
                CharList.RemoveAt(i);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }            
        }

        public void ReadCharacterDataFromFile()
        {
            CharList = ExternalData.CharacterDataReader.ReadCharacterDataToCharacterList();
            foreach(Character c in CharList)
            {
                AddSkillsToCharacter(c);
            }
        }

        private void AddSkillsToCharacter(Character c)
        {
            SkillsList sl = SkillsList.Instance;
            foreach(Skill toAdd in sl.Skills)
            {
                if (!c.Skills.Contains(toAdd))
                {
                    toAdd.AssignedCharacter = c;
                    c.Skills.Add(toAdd);
                }
                else
                {
                    int index = c.Skills.IndexOf(toAdd);
                    c.Skills[index].AssignedCharacter = c;
                }
            }
        }

        private CharacterList()
        {
           
        }
    }
}
