using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Interfaces;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Objects
{
    public class CharacterBindingObservableCollection<T> :  ItemChangeObservableCollection<T> where T : ICharacterBindable, INotifyPropertyChanged
    {
        private Character BoundCharacter;

        public CharacterBindingObservableCollection(Character c)
        {
            BoundCharacter = c;
        }

        public CharacterBindingObservableCollection(Character c, List<T> inputList)
        {
            BoundCharacter = c;
            foreach(var item in inputList)
            {
                Add(item);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            item.BindToCharacter(BoundCharacter);
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            item.BindToCharacter(BoundCharacter);
            base.SetItem(index, item);
        }
    }
}
