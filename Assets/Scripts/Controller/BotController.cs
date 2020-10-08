using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Helper;
using Interface;
using Model;
using Model.Ai;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Controller
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private int _countBot;
        private readonly List<BaseUnitModel> _botList = new List<BaseUnitModel>();
        private List<EnemyRespawn> _respawns = new List<EnemyRespawn>();
        private List<Enemy> _enemies = new List<Enemy>();

        #endregion

        
        #region IInitialization

        public void Initialization()
        {
            _respawns = Object.FindObjectsOfType<EnemyRespawn>().ToList();
            foreach (var respawn in _respawns)
            {
                switch (respawn.EnemyType)
                {
                    case EnemyType.One:
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMele, respawn.transform);
                        break;
                    case EnemyType.OneMage:
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMage, respawn.transform);
                        break;
                    case EnemyType.Rogue:
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyRoque, respawn.transform);
                        break;
                    case EnemyType.MiniGroup:
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMele, respawn.transform);
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMele, respawn.transform);
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyRange, respawn.transform);
                        break;
                    case EnemyType.NormalGroup:
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMele, respawn.transform);
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMele, respawn.transform);
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMele, respawn.transform);
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyRange, respawn.transform);
                        CreateEnemy(ServiceLocatorMonoBehaviour.GetService<Reference>().EnemyMage, respawn.transform);
                        break;
                    default:
                        break;
                }
            }
            
            
            
            // _countBot = ServiceLocatorMonoBehaviour.GetService<Reference>().BotsCount();
            // for (var index = 0; index < _countBot; index++)
            // {
            //     var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
            //         Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
            //         Quaternion.identity);
            //
            //     tempBot.Agent.avoidancePriority = index;
            //     tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform; 
            //     //todo разных противников
            //     AddBotToList(tempBot);
            // }
        }

        #endregion


        #region Methods

        private void CreateEnemy(Enemy enemy, Transform point)
        {
            var tempEnemy = Object.Instantiate(enemy,
                // Patrol.GenericPoint(point),
                point.position,
                Quaternion.identity);

            tempEnemy.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform; 
            AddEnemyToList(tempEnemy);
        }

        private void AddEnemyToList(Enemy enemy)
        {
            if (_enemies.Contains(enemy)) return;
            _enemies.Add(enemy);
            enemy.OnDieChange += RemoveEnemyToList;
        }
        
        private void RemoveEnemyToList(Enemy enemy)
        {
            if (!_enemies.Contains(enemy))
            {
                return;
            }

            enemy.OnDieChange -= RemoveEnemyToList;
            _enemies.Remove(enemy);
        }

        private void AddBotToList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                _botList.Add(bot);
                bot.OnDieChange += RemoveBotToList;
            }
        }

        private void RemoveBotToList(BaseUnitModel bot)
        {
            if (!_botList.Contains(bot))
            {
                return;
            }

            bot.OnDieChange -= RemoveBotToList;
            _botList.Remove(bot);
        }

        #endregion

        
        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            for (var i = 0; i < _botList.Count; i++)
            {
                _botList[i].Execute();
            }

            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].Execute();
            }
        }

        #endregion
    }
}