using Enums;
using UniRx;

namespace Unit.Player
{
    public sealed class PrototypePlayerModel
    {
        public IReactiveProperty<CharacterClass> CharacterClass { get; private set; }
        public IReactiveProperty<CharacterGender> CharacterGender { get; private set; }
        public IReactiveProperty<CharacterRace> CharacterRace { get; private set; }
        public IReactiveProperty<StatePrototypePlayer> State { get; private set; }

        public PrototypePlayerModel()
        {
            CharacterClass = new ReactiveProperty<CharacterClass>();
            CharacterGender = new ReactiveProperty<CharacterGender>();
            CharacterRace = new ReactiveProperty<CharacterRace>();
            State = new ReactiveProperty<StatePrototypePlayer>();
        }
    }
}