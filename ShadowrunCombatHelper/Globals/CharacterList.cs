using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ExternalData;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper.Globals
{
    public sealed class CharacterList
    {
        private static readonly Lazy<CharacterList> lazy = new Lazy<CharacterList>(() => new CharacterList());

        public static CharacterList Instance { get { return lazy.Value; } }

        private IDataReadWriter<Character> readWriter = new XMLDataReadWriter<Character>();

        private List<Character> CharList = new List<Character>();

        public List<Character> GetCharacterList()
        {
            return CharList;
        }

        public void AddCharacter(Character c)
        {
            CharList.Add(c);
            readWriter.WriteListToFile(ApplicationXmlFiles.fileType.CHARACTERDATA, CharList);
        }

        public void RemoveCharacter(Character c)
        {
            CharList.Remove(c);
            readWriter.WriteListToFile(ApplicationXmlFiles.fileType.CHARACTERDATA, CharList);
        }
        
        public void OverwriteCharacterList(List<Character> clist)
        {
            CharList = clist;
            readWriter.WriteListToFile(ApplicationXmlFiles.fileType.CHARACTERDATA, CharList);
        }

        public void UpdateCharacterPostCombat(Character c)
        {
            Character toUpdate = CharList.Where(x => x == c).First();
            if(toUpdate != null)
            {
                toUpdate.CurrentPhysicalDamage = c.CurrentPhysicalDamage;
                toUpdate.CurrentStunDamage = c.CurrentStunDamage;
            }
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
            CharList = readWriter.ReadFileToList(ApplicationXmlFiles.fileType.CHARACTERDATA);
        }

        private CharacterList()
        {
           
        }

       
    }
}
