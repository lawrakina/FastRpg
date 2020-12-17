using Data;
using UnityEngine;

namespace Unit.Player
{
    public interface IPlayerFactory
    {
        IPlayerView CreatePlayer(GameObject playerDataStoragePlayerPrefab, CharacterSettings characterSettings);
    }
}