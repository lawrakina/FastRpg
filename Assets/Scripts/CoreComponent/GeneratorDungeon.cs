using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;


namespace CoreComponent
{
    public sealed class GeneratorDungeon
    {
        public Dungeon                Dungeon { get; set; }
        public GridFlowDungeonConfig  Config  { get; set; }
        public GridFlowDungeonBuilder Builder { get; set; }
    }
}