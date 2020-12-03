using UnityEngine;


namespace Windows
{
    public sealed class BattleWindow: BaseWindow
    {
        #region Fields
        
        [SerializeField] private Camera _forTextureRenderCamera;

        #endregion


        public override void Show()
        {
            base.Show();
            _forTextureRenderCamera.enabled = true;
        }

        public override void Hide()
        {
            base.Hide();
            _forTextureRenderCamera.enabled = false;
        }
    }
}