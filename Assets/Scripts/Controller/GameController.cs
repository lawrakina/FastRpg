using System;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{
    public sealed class GameController : MonoBehaviour
    {

        #region Fields

        private Controllers _controllers;

        #endregion

        
        #region UnityMethods

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < _controllers.FixedLenght; i++)
            {
                _controllers.FixedExecute[i].FixedExecute();
            }
        }

        #endregion
    }
    



    public sealed class MainController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private EnemyData _enemyData;
        private List<IUpdateble> _iIdpatables = new List<IUpdateble>();

        private void Start()
        {
            new InitializeController(this);
        }
    }

    internal class InitializeController
    {
        public InitializeController(MainController mainController)
        {
            
        }
    }

    internal interface IUpdateble
    {
    }

    internal class EnemyData
    {
    }

    internal class PlayerData
    {
    }
}