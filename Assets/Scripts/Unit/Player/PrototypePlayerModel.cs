using Data;
using Enums;
using UniRx;

namespace Unit.Player
{
    public sealed class PrototypePlayerModel
    {
        #region Properties

        public IReactiveProperty<CharacterClass>       CharacterClass  { get; private set; }
        public IReactiveProperty<CharacterGender>      CharacterGender { get; private set; }
        public IReactiveProperty<CharacterRace>        CharacterRace   { get; private set; }

        public CharacterSettings GetCharacterSettings
        {
            get
            {
                var result = new CharacterSettings();
                result.CharacterClass = CharacterClass.Value;
                result.CharacterGender = CharacterGender.Value;
                result.CharacterRace = CharacterRace.Value;
                return result;
            }
        }

        #endregion


        #region ClassLiveCycles

        public PrototypePlayerModel()
        {
            CharacterClass = new ReactiveProperty<CharacterClass>(Enums.CharacterClass.Warrior);
            CharacterGender = new ReactiveProperty<CharacterGender>(Enums.CharacterGender.Male);
            CharacterRace = new ReactiveProperty<CharacterRace>(Enums.CharacterRace.Human);
        }

        #endregion
    }
}