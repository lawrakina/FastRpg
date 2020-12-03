using Interface;
using UnityEngine;


namespace Windows
{
    public class BaseWindow : MonoBehaviour, IWindow, IInit, ICleanup
    {
        #region Fields

        [SerializeField] private Camera _camera;

        #endregion
        
        public virtual void Init()
        {
        }

        public void Cleanup()
        {
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            _camera.enabled = true;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            _camera.enabled = false;
        }
    }
}