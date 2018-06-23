using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;

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
        }

        public void RemoveCharacter(Character c)
        {
            CharList.Remove(c);
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

        private CharacterList()
        {

        }
    }
}
