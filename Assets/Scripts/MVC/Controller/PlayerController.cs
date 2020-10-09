using Controller.TimeRemaining;
using Interface;
using Model.Player;
using Model.Weapons;
using UnityEngine;

namespace MVC.Controller
{
    public sealed class PlayerController : BaseController, IInitialization, IExecute, IFixedExecute
    {
        #region Fields

        private PlayerModel _playerModel;
        private FairyComponent _fairyComponent; 

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            base.On();
            _playerModel = Object.FindObjectOfType<PlayerModel>();
            _fairyComponent = Object.FindObjectOfType<FairyComponent>();
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
            if(_fairyComponent)
                _fairyComponent.Execute();

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

        public SoundPlayer GetSoundPlayer()
        {
            return _playerModel.SoundPlayer;
        }

    }
}