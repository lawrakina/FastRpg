using System;
using Enums;
using Extension;
using SharpNav.Crowds;
using UniRx;
using UniRx.Triggers;
using Unit.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    //есть стойкое подозрение на то что тут нужен MVVM))))
    [Serializable]
    public sealed class CharacterPanel : BasePanel
    {
        #region Fields

        [Header("Create character")] 
        [SerializeField] private GameObject _newCharPanel;

        [SerializeField] private Button _prevClassButton;
        [SerializeField] private Button _nextClassButton;
        [SerializeField] private Button _createCharacterButton;

        [Header("Classes Icons")] 
        [SerializeField] private GameObject _warriorIcon;
        [SerializeField] private GameObject _rogueIcon;
        [SerializeField] private GameObject _hunterIcon;
        [SerializeField] private GameObject _mageIcon;

        [Header("Gender toggle")] 
        [SerializeField] private Toggle _genderManToggle;
        [SerializeField] private Toggle _genderWomanToggle;

        [Header("Race toggle")] 
        [SerializeField] private Toggle _raceHumanToggle;
        [SerializeField] private Toggle _raceNightElfToggle;
        [SerializeField] private Toggle _raceBloodElfToggle;
        [SerializeField] private Toggle _raceOrcToggle;
        
        [Header("Navigation buttons")] 
        [SerializeField] private Button _gotoSettingChar;
        [SerializeField] private Button _backtoSelectCharClass;

        [Header("Setting character")] 
        [SerializeField] private GameObject _newSettingCharPanel;

        private IReactiveProperty<EnumCharacterWindow> _charWindow;
        private PrototypePlayerModel _prototypePlayer;

        private GameObjectLinkedList _listClasses;

        #endregion

        public void Ctor(IReactiveProperty<EnumCharacterWindow> charWindow, PrototypePlayerModel prototypePlayer)
        {
            base.Ctor();

            _charWindow = charWindow;
            _prototypePlayer = prototypePlayer;
            
            _listClasses = new GameObjectLinkedList(new[]
            {
                new LinkedListItem((int) CharacterClass.Warrior, _warriorIcon),
                new LinkedListItem((int) CharacterClass.Rogue, _rogueIcon),
                new LinkedListItem((int) CharacterClass.Hunter, _hunterIcon),
                new LinkedListItem((int) CharacterClass.Mage, _mageIcon)
            });
            
            _charWindow.Subscribe(_ =>
            {
                _newCharPanel.SetActive(_charWindow.Value == EnumCharacterWindow.NewSelectClass);
                _newSettingCharPanel.SetActive(_charWindow.Value == EnumCharacterWindow.NewSettingsCharacter);
            }).AddTo(_subscriptions);
            //prev class
            _prevClassButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                if (_listClasses.MovePrev())
                    _prototypePlayer.CharacterClass.Value = (CharacterClass) _listClasses.Current.Key;
            }).AddTo(_subscriptions);
            //next class
            _nextClassButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                if (_listClasses.MoveNext())
                    _prototypePlayer.CharacterClass.Value = (CharacterClass) _listClasses.Current.Key;
            }).AddTo(_subscriptions);
            //goto settings
            _gotoSettingChar.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _newCharPanel.SetActive(false);
                _newSettingCharPanel.SetActive(true);
            }).AddTo(_subscriptions);
            //goto select class
            _backtoSelectCharClass.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _newCharPanel.SetActive(true);
                _newSettingCharPanel.SetActive(false);
            }).AddTo(_subscriptions);

            //gender
            _genderManToggle.OnValueChangedAsObservable().Subscribe(_ =>
            {
                _prototypePlayer.CharacterGender.Value = CharacterGender.Male;
            }).AddTo(_subscriptions);
            _genderWomanToggle.OnValueChangedAsObservable().Subscribe(_ =>
            {
                _prototypePlayer.CharacterGender.Value = CharacterGender.Female;
            }).AddTo(_subscriptions);
            
            //race
            _raceHumanToggle.OnValueChangedAsObservable().Subscribe(_ =>
            {
                _prototypePlayer.CharacterRace.Value = CharacterRace.Human;
            }).AddTo(_subscriptions);
            _raceNightElfToggle.OnValueChangedAsObservable().Subscribe(_ =>
            {
                _prototypePlayer.CharacterRace.Value = CharacterRace.NightElf;
            }).AddTo(_subscriptions);
            _raceBloodElfToggle.OnValueChangedAsObservable().Subscribe(_ =>
            {
                _prototypePlayer.CharacterRace.Value = CharacterRace.BloodElf;
            }).AddTo(_subscriptions);
            _raceOrcToggle.OnValueChangedAsObservable().Subscribe(_ =>
            {
                _prototypePlayer.CharacterRace.Value = CharacterRace.Orc;
            }).AddTo(_subscriptions);
        }
    }
}