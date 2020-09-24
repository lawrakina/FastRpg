﻿using Controller.TimeRemaining;
using Interface;
using Model;
using Model.Player;
using Model.Weapons;
using UnityEngine;

namespace Controller
{
    public sealed class PlayerController : BaseController, IInitialization, IExecute, IFixedExecute
    {
        #region Fields

        private PlayerModel _playerModel;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            base.On();
            _playerModel = Object.FindObjectOfType<PlayerModel>();
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (_playerModel.Hp <= 0)
            {
                Off();
            }

            _playerModel.Execute();

            // UiInterface.PlayerXpUiText.Text = _playerModel.Hp;
            // UiInterface.PlayerXpUiBar.Fill = _playerModel.PercentXp;
        }

        #endregion


        #region IFixedExecute

        public void FixedExecute()
        {
            if (!IsActive) return;
            
            _playerModel.FixedExecute();
        }

        #endregion
        
        
        public void Move(Vector3 moveVector)
        {
            if(!IsActive) return;
            
            _playerModel.Move(moveVector);
        }

        public void EquipWeapon(Weapon weapon)
        {
            _playerModel.EquipmentSystem.Equip(weapon);
        }

    }
}