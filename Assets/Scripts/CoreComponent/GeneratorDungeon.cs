using System;
using Controller;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace CoreComponent
{

    public sealed class GeneratorDungeon : IGeneratorDungeon
    {
        private bool isEnableDungeon = false;
        private DungeonGeneratorData _dungeonGeneratorData;
        private Transform _parent;
        private DungeonArchitect.Dungeon _generator;
        private GridFlowDungeonConfig _config;
        private GridFlowDungeonBuilder _builder;
        private PooledDungeonSceneProvider _pooledSceneProvider;
        private GameObject _dungeon;
        private Type _typeSpawnPlayer;

        public IReactiveProperty<uint> Seed { get; set; }

        public GeneratorDungeon(DungeonGeneratorData dungeonGeneratorData, Transform parent)
        {
            _dungeonGeneratorData = dungeonGeneratorData;
            _parent = parent;

            _dungeon = Object.Instantiate(new GameObject(), _parent);
            _dungeon.name = "Dungeon";

            var gO = Object.Instantiate(_dungeonGeneratorData.StorageGenerator, _parent);
            _generator = gO.GetComponent<DungeonArchitect.Dungeon>();
            _config = gO.GetComponent<GridFlowDungeonConfig>();
            _builder = gO.GetComponent<GridFlowDungeonBuilder>();
            _pooledSceneProvider = gO.GetComponent<PooledDungeonSceneProvider>();
            _pooledSceneProvider.itemParent = _dungeon;

            Seed = new ReactiveProperty<uint>(_config.Seed);
            Seed.Subscribe(x => { _config.Seed = x; });
        }

        public void BuildDungeon()
        {
            _generator.Build();
            isEnableDungeon = true;
        }

        public void DestroyDungeon()
        {
            _generator.DestroyDungeon();
            isEnableDungeon = false;
        }

        public Transform GetPlayerPosition()
        {
            if (!isEnableDungeon) return null;

            var result = _parent.GetComponentInChildren<SpawnMarkerCharacterInDungeon>();
            if (result != null)
                return result.transform;
            else
                return null;
        }

        public void SetRandomSeed()
        {
            DestroyDungeon();
            Seed.Value = (uint) Random.Range(0, int.MaxValue);
        }

        public GameObject Dungeon()
        {
            return _dungeon;
        }
    }
}