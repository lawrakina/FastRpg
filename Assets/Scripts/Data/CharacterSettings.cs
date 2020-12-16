using System;
using Enums;
using Unit.Player;

namespace Data
{
    [Serializable]
    public class CharacterSettings
    {
        public CharacterClass CharacterClass;
        public CharacterGender CharacterGender;
        public CharacterRace CharacterRace;
    }
}