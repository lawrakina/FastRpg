using System;
using Helper;
using Model.Player;
using UnityEngine;


namespace Model
{
    [RequireComponent(typeof(PlayerModel))]
    public class BaseUnitAnimationEvents : MonoBehaviour
    {
        #region Fields

        private PlayerModel _unitModel;

        #endregion
        private void Start()
        {
            _unitModel = GetComponent<PlayerModel>();
        }

        public void Hit()
        {
            // Dbg.Log($"BaseUnitAnimationEvents.Hit");
            // _unitModel.BattleSystem.CanHit();
        }

        public void Shoot()
        {
            // _unitModel.BattleSystem.CanShoot();
        }

        public void FootR()
        {
            Dbg.Log($"BaseUnitAnimationEvents.FootR");
        }

        public void FootL()
        {
            Dbg.Log($"BaseUnitAnimationEvents.FootL");
        }
    }
}