using Controller;
using Controller.TimeRemaining;

namespace Model.Weapons
{
    public sealed class StandardHit : BaseHit
    {
        protected override void DestroyAmmunition()
        {
            _timePutToPool.RemoveTimeRemainingExecute();
            ServiceLocator.Resolve<PoolController>().PutToPool(this);
        }
    }
}