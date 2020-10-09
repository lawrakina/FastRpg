using Model;
using Model.Weapons;


namespace MVC.Controller
{
    public sealed class WeaponController : BaseController
    {
        #region Fields

        private FpsWeapon _fpsWeapon;

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] weapon)
        {
            if (IsActive) return;
            if (weapon.Length > 0) _fpsWeapon = weapon[0] as FpsWeapon;
            if (_fpsWeapon == null) return;
            base.On(_fpsWeapon);
            _fpsWeapon.IsVisible = true;
            UiInterface.WeaponUiText.SetActive(true);
            UiInterface.WeaponUiText.ShowData(_fpsWeapon.Clip.CountAmmunition, _fpsWeapon.CountClip);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _fpsWeapon.IsVisible = false;
            _fpsWeapon = null;
            UiInterface.WeaponUiText.SetActive(false);
        }

        public void Fire()
        {
            _fpsWeapon.Fire();
            UiInterface.WeaponUiText.ShowData(_fpsWeapon.Clip.CountAmmunition, _fpsWeapon.CountClip);
        }

        public void ReloadClip()
        {
            if (_fpsWeapon == null) return;
            _fpsWeapon.ReloadClip();
            UiInterface.WeaponUiText.ShowData(_fpsWeapon.Clip.CountAmmunition, _fpsWeapon.CountClip);
        }

        #endregion
    }
}