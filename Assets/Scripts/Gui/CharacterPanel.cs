using System;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    [Serializable]
    public sealed class CharacterPanel : BasePanel
    {
        #region Fields

        private IReactiveProperty<EnumCharacterWindow> _charWindow;

        #endregion

        public void Ctor(IReactiveProperty<EnumCharacterWindow> charWindow)
        {
            _charWindow = charWindow;
        }
    }
}