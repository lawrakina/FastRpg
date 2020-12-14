using Interface;
using UniRx;

namespace Controller
{
    public abstract class BaseController: IInitialization, ICleanup
    {
        #region Fields

        protected CompositeDisposable _subscriptions;
        protected bool _isEnable;

        #endregion

        public BaseController()
        {
            _subscriptions = new CompositeDisposable();
        }
        
        public virtual void On()
        {
            _isEnable = true;
        }

        public virtual void Off()
        {
            _isEnable = false;
        }

        public virtual void Initialization()
        {
            _isEnable = true;
        }

        public virtual void Cleanup()
        {
            _subscriptions?.Dispose();
        }
    }
}