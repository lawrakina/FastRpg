using Enums;
using UniRx;
using UniRx.Triggers;
using Unit.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Gui.Characters
{
    public sealed class ListCharacterPanel:BasePanel
    {
        #region Fields

        [SerializeField] private Button _prevCharButton;
        [SerializeField] private Button _nextCharButton;
        [SerializeField] private Button _createCharacterButton;
        [SerializeField] private Text _info;
        
        private IReactiveProperty<EnumCharacterWindow> _activeCharWindow;
        private ListCharactersManager _listCharactersManager;

        #endregion


        public void Ctor(IReactiveProperty<EnumCharacterWindow> activeCharWindow, ListCharactersManager listCharactersManager)
        {
            base.Ctor();
            _listCharactersManager = listCharactersManager;
            _activeCharWindow = activeCharWindow;

            _createCharacterButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _activeCharWindow.Value = EnumCharacterWindow.NewSelectClass;
                _listCharactersManager.PrototypePlayer.State.Value = StatePrototypePlayer.New;
            }).AddTo(_subscriptions);

            _prevCharButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _listCharactersManager.MovePrev();
            }).AddTo(_subscriptions);
            _nextCharButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _listCharactersManager.MoveNext();
            }).AddTo(_subscriptions);

            _listCharactersManager.CurrentChar.Subscribe(view =>
            {
                _info.text = view.Description.Value;
            });
        }
    }
}