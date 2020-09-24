using Enums;
using UnityEngine;


namespace Model
{
    public class EnemyRespawn : MonoBehaviour
    {
        #region Fields

        [SerializeField] private EnemyType _enemyType;

        #endregion

        #region Properties

        public EnemyType EnemyType => _enemyType;

        #endregion
    }
}