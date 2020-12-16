namespace Unit.Player
{
    public interface IPlayerView: IBaseUnitView
    {
        BaseCharacterClass CharacterClass { get; set; }
    }
}