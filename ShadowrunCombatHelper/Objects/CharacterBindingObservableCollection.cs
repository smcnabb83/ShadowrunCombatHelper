using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Objects
{
    public class CharacterBindingObservableCollection<T> : ItemChangeObservableCollection<T> where T : ICharacterBindable, INotifyPropertyChanged
    {
        private Character BoundCharacter;

        public CharacterBindingObservableCollection(Character c)
        {
            BoundCharacter = c;
        }

        public CharacterBindingObservableCollection(Character c, List<T> inputList)
        {
            BoundCharacter = c;
            foreach (var item in inputList)
            {
                BoundCharacter.PropertyChanged += new PropertyChangedEventHandler(item.CharacterPropertyChangedEventHandler);
                Add(item);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            item.BindToCharacter(BoundCharacter);
            BoundCharacter.PropertyChanged += new PropertyChangedEventHandler(item.CharacterPropertyChangedEventHandler);
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            item.BindToCharacter(BoundCharacter);
            BoundCharacter.PropertyChanged += new PropertyChangedEventHandler(item.CharacterPropertyChangedEventHandler);
            base.SetItem(index, item);
        }
    }
}