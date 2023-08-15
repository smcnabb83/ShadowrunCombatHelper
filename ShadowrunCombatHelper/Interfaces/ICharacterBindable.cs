using ShadowrunCombatHelper.Models;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Interfaces
{
    public interface ICharacterBindable
    {
        void BindToCharacter(Character c);

        void CharacterPropertyChangedEventHandler(object c, PropertyChangedEventArgs e);
    }
}