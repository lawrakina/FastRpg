using System;
using Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    [Serializable]
    public sealed class BattlePanel : MonoBehaviour, IInit, ICleanup
    {
        public Button IntoBattleButton;
        public Button RandomSeedButton;
        public InputField SeedInputField;
        public Button GenerateMapButton;

        public Action OnIntoBattle;
        public Action OnRandomEdit;
        public Action OnGenerateMap;

        public void Init()
        {
            IntoBattleButton.onClick.AddListener(delegate
            {
                OnIntoBattle?.Invoke();
            });
            RandomSeedButton.onClick.AddListener(delegate
            {
                OnRandomEdit?.Invoke();
            });
            GenerateMapButton.onClick.AddListener(delegate
            {
                OnGenerateMap?.Invoke();
            });
        }

        public void Cleanup()
        {
            IntoBattleButton.onClick.RemoveAllListeners();
            RandomSeedButton.onClick.RemoveAllListeners();
            GenerateMapButton.onClick.RemoveAllListeners();
        }
    }
}