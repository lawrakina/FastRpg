using Controller;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UniRx;
using UnityEngine;


namespace CoreComponent
{
    public sealed class GeneratorDungeon
    {
        private DungeonGeneratorData _dungeonGeneratorData;
        private Transform _parent;
        private Dungeon _generator;
        private GridFlowDungeonConfig _config;
        private GridFlowDungeonBuilder _builder;
        private PooledDungeonSceneProvider _pooledSceneProvider;
        private GameObject _dungeon;
        
        public IReactiveProperty<uint> Seed;

        public GeneratorDungeon(DungeonGeneratorData dungeonGeneratorData, Transform parent)
        {
            _dungeonGeneratorData = dungeonGeneratorData;
            _parent = parent;
            
            var dungeon = Object.Instantiate(new GameObject(), _parent);
            dungeon.name = "Dungeon";

            var gO = Object.Instantiate(_dungeonGeneratorData.StorageGenerator, _parent);
            _generator = gO.GetComponent<Dungeon>();
            _config = gO.GetComponent<GridFlowDungeonConfig>();
            _builder = gO.GetComponent<GridFlowDungeonBuilder>();
            _pooledSceneProvider = gO.GetComponent<PooledDungeonSceneProvider>();
            
            Seed = new ReactiveProperty<uint>(_config.Seed);
            Seed.Subscribe(x => { _config.Seed = x; });
        }

        public void BuildDungeon()
        {
            _generator.Build();
        }

        public void DestroyDungeon()
        {
            _generator.DestroyDungeon();
        }
    }
}