using Model.Player;
using UnityEngine;


namespace Tests
{
    public class MyGui : MonoBehaviour
    {
        #region fields

        [SerializeField] private PlayerModel _playerModel;

        #endregion

    
        #region UnityMethods

        private void OnGUI()
        {
            GUI.Label(new Rect(0,0, Screen.width /2.0f, Screen.height * 0.1f), 
                $"Player HP: {_playerModel.Hp}/{_playerModel.MaxHp}" );
        }

        #endregion
    }
}
