using System;
using System.Text.RegularExpressions;
using Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    [Serializable]
    public sealed class BattlePanel : MonoBehaviour, IInit, ICleanup
    {
        [SerializeField] private Button IntoBattleButton;
        [SerializeField] private  Button RandomSeedButton;
        public  InputField SeedInputField;
        [SerializeField] private  Button GenerateMapButton;

        public Action OnIntoBattle;
        public Action OnRandomEdit;
        public Action OnGenerateMap;
        public Action<int> SeedChange;

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
            SeedInputField.onValueChanged.AddListener(delegate(string value)
            {
                SeedChange?.Invoke(int.Parse(value));
            });
        }

        public void Cleanup()
        {
            IntoBattleButton.onClick.RemoveAllListeners();
            RandomSeedButton.onClick.RemoveAllListeners();
            GenerateMapButton.onClick.RemoveAllListeners();
            SeedInputField.onValueChanged.RemoveAllListeners();
        }
    }
}