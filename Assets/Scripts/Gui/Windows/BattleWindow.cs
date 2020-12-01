using System;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using UnityEngine;


namespace Gui.Windows
{
    public sealed class BattleWindow: BaseWindow
    {
        #region Fields

        private bool _isFoundDungeonGenerator = false;
        private bool _isFoundDungeonConfig = false;
        private bool _isFoundDungeonBuilder = false;
        private Dungeon _dungeonGenerator;
        private GridFlowDungeonConfig _dungeonDungeonConfig;
        private GridFlowDungeonBuilder _dungeonBuilder;

        [SerializeField] private Camera _forTextureRenderCamera;

        #endregion


        #region Properties

        public Dungeon DungeonGenerator
        {
            get {
                if (_isFoundDungeonGenerator)
                    return _dungeonGenerator;
                _dungeonGenerator = FindObjectOfType<Dungeon>();
                if (_dungeonGenerator == null) 
                    throw new Exception($"{nameof(DungeonGenerator)} not found!");
                _isFoundDungeonGenerator = true;
                return _dungeonGenerator;
            }
        }

        public GridFlowDungeonConfig DungeonConfig
        {
            get
            {
                if (_isFoundDungeonConfig)
                    return _dungeonDungeonConfig;
                _dungeonDungeonConfig = FindObjectOfType<GridFlowDungeonConfig>();
                if (_dungeonDungeonConfig == null) 
                    throw new Exception($"{nameof(GridFlowDungeonConfig)} not found!");
                _isFoundDungeonConfig = true;
                return _dungeonDungeonConfig;
            }
            
        }

        public GridFlowDungeonBuilder DungeonBuilder
        {
            get
            {
                if (_isFoundDungeonBuilder)
                    return _dungeonBuilder;
                _dungeonBuilder = FindObjectOfType<GridFlowDungeonBuilder>();
                if (_dungeonBuilder == null) 
                    throw new Exception($"{nameof(GridFlowDungeonBuilder)} not found!");
                _isFoundDungeonBuilder = true;
                return _dungeonBuilder;
            }
        }

        #endregion

        public override void Show()
        {
            base.Show();
            _forTextureRenderCamera.enabled = true;
        }

        public override void Hide()
        {
            base.Hide();
            _forTextureRenderCamera.enabled = false;
        }
    }
}