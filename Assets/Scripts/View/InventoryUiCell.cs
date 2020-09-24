using Controller;
using Model;
using UnityEngine;


namespace View
{
    public class InventoryUiCell : MonoBehaviour
    {
        public void EquipItem(int id)
        {
            ServiceLocator.Resolve<InputController>().EquipWeapon(id);
        }
    }
}
