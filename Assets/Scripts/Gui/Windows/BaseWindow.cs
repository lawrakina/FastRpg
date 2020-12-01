using Interface;
using UnityEngine;


namespace Gui.Windows
{
    public class BaseWindow : MonoBehaviour, IWindow, IInit, ICleanup
    {
        #region Fields

        [SerializeField] private Camera _camera;

        #endregion
        
        public void Init()
        {
        }

        public void Cleanup()
        {
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _camera.enabled = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _camera.enabled = false;
        }
    }
}