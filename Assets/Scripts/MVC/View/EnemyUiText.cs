using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public class EnemyUiText : MonoBehaviour
    {
        #region Fields

        private Text _text;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        #endregion

        
        #region Properties

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }    

        #endregion
    }
}
