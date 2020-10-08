using System;
using Helper;
using Model.Player;
using UnityEngine;


namespace Model
{
    public class BaseUnitAnimationEvents : MonoBehaviour
    {
        #region Fields

        private PlayerModel _unitModel;
        private SoundPlayer _soundPlayer;

        #endregion
        private void Start()
        {
            _unitModel = GetComponent<PlayerModel>();
            _soundPlayer = GetComponentInChildren<SoundPlayer>();
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
            _soundPlayer.PlayStepFoot();
        }

        public void FootL()
        {
            Dbg.Log($"BaseUnitAnimationEvents.FootL");
            _soundPlayer.PlayStepFoot();
        }
    }
}