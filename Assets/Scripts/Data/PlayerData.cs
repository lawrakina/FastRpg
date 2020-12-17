using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField] public GameObject StoragePlayerPrefab;
        [SerializeField] public int _numberActiveCharacter;
        [SerializeField] public Characters _characters;

        public CharacterSettings SelectedCharacter
        {
            get
            {
                if (_numberActiveCharacter >= 0 && _numberActiveCharacter <= _characters.ListCharacters.Count)
                    return _characters.ListCharacters[_numberActiveCharacter];
                return new CharacterSettings();
            }
        }
    }
}