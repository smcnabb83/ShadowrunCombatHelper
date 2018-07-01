﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Interfaces
{
    public interface ICharacterBindable
    {
        void BindToCharacter(Character c);
        void CharacterPropertyChangedEventHandler(Object c, PropertyChangedEventArgs e);
    }

     
}