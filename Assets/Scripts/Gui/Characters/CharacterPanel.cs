using System;
using Enums;
using UniRx;
using UniRx.Triggers;
using Unit.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Gui.Characters
{
    //есть стойкое подозрение на то что тут нужен MVVM))))
    [Serializable]
    public sealed class CharacterPanel : BasePanel
    {
        #region Fields

        [Header("Panels")] 
        [SerializeField] private CreateNewCharacterPanel _newCharPanel;
        [SerializeField] private CreateSettingCharacterPanel _settingCharPanel;
        [SerializeField] private ListCharacterPanel _listCharPanel;
        

        
        [Header("Navigation buttons")] 

        private IReactiveProperty<EnumCharacterWindow> _activeCharacterWindow;

        private ListCharactersManager _listCharactersManager;

        #endregion

        public void Ctor(IReactiveProperty<EnumCharacterWindow> activeCharacterWindow, ListCharactersManager listCharactersManager)
        {
            base.Ctor();
            _activeCharacterWindow = activeCharacterWindow;
            _listCharactersManager = listCharactersManager;

            _newCharPanel.Ctor(_activeCharacterWindow, _listCharactersManager);
            _settingCharPanel.Ctor(_activeCharacterWindow, _listCharactersManager);
            _listCharPanel.Ctor(_activeCharacterWindow, _listCharactersManager);
            
            //переключение между дочерними окнами
            _activeCharacterWindow.Subscribe(_ =>
            {
                if (_activeCharacterWindow.Value == EnumCharacterWindow.ListCharacters)
                    _listCharPanel.Show();else _listCharPanel.Hide();
                if(_activeCharacterWindow.Value == EnumCharacterWindow.NewSelectClass)
                    _newCharPanel.Show();else _newCharPanel.Hide();
                if(_activeCharacterWindow.Value == EnumCharacterWindow.NewSettingsCharacter)
                    _settingCharPanel.Show();else _settingCharPanel.Hide();
            }).AddTo(_subscriptions);
        }
    }
}