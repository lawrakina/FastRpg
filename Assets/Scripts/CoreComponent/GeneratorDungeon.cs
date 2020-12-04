using System;
using System.Reflection;
using Controller;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace CoreComponent
{
    public sealed class GeneratorDungeon
    {
        private bool isEnableDungeon = false;
        private DungeonGeneratorData _dungeonGeneratorData;
        private Transform _parent;
        private Dungeon _generator;
        private GridFlowDungeonConfig _config;
        private GridFlowDungeonBuilder _builder;
        private PooledDungeonSceneProvider _pooledSceneProvider;
        private GameObject _dungeon;
        private Type _typeSpawnPlayer;

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
            _pooledSceneProvider.itemParent = dungeon;

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
    }
}