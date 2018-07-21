using ShadowrunCombatHelper.Models;
using System;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Interfaces
{
    public interface ICharacterBindable
    {
        void BindToCharacter(Character c);

        void CharacterPropertyChangedEventHandler(Object c, PropertyChangedEventArgs e);
    }
}