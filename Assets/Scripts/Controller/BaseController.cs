using Interface;
using UniRx;

namespace Controller
{
    public abstract class BaseController: IInitialization, ICleanup
    {
        #region Fields

        protected CompositeDisposable _subscriptions;
        protected bool IsEnable;

        #endregion

        public BaseController()
        {
            _subscriptions = new CompositeDisposable();
        }
        
        public virtual void On()
        {
            IsEnable = true;
        }

        public virtual void Off()
        {
            IsEnable = false;
        }

        public virtual void Initialization()
        {
            
        }

        public virtual void Cleanup()
        {
            _subscriptions?.Dispose();
        }
    }
}