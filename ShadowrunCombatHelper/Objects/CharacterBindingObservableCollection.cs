using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ShadowrunCombatHelper.Interfaces;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.Objects
{
    public class CharacterBindingObservableCollection<T> : ItemChangeObservableCollection<T>
        where T : ICharacterBindable, INotifyPropertyChanged
    {
        private readonly Character _boundCharacter;

        public CharacterBindingObservableCollection(Character c)
        {
            _boundCharacter = c;
        }

        public CharacterBindingObservableCollection(Character c, IEnumerable<T> inputList)
        {
            _boundCharacter = c;
            foreach (T item in inputList)
            {
                _boundCharacter.PropertyChanged += item.CharacterPropertyChangedEventHandler;
                Add(item);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            Debug.Assert(item != null, nameof(item) + " != null");
            item.BindToCharacter(_boundCharacter);
            _boundCharacter.PropertyChanged += item.CharacterPropertyChangedEventHandler;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            Debug.Assert(item != null, nameof(item) + " != null");
            item.BindToCharacter(_boundCharacter);
            _boundCharacter.PropertyChanged += item.CharacterPropertyChangedEventHandler;
            base.SetItem(index, item);
        }
    }
}