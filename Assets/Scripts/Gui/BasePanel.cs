using Interface;
using UniRx;
using UnityEngine;

namespace Gui
{
    public class BasePanel: MonoBehaviour, IInit, ICleanup
    {
        #region Fields

        protected CompositeDisposable _subscriptions;

        #endregion
        
        public void Ctor()
        {
            _subscriptions = new CompositeDisposable();
        }
        public void Init()
        {
            
        }

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }


        public void Show()
        {
            Debug.Log($"ShowPanel:{name}");
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            Debug.Log($"HidePanel:{name}");
            gameObject.SetActive(false);
        }
    }
}