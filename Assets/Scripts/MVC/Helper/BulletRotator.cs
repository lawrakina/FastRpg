using UnityEngine;

namespace Helper
{
    public class BulletRotator : MonoBehaviour
    {
        #region Fields

        private Rigidbody _rigidbody;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 velocity = _rigidbody.velocity;      // Направление силы Rigidbody2D    

            float angleZ = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;   // Возвращаем угол в радианах тангенса y/x (направления) и переводим их в градусы
            float angleY = Mathf.Atan2(velocity.y, velocity.z) * Mathf.Rad2Deg;   // Возвращаем угол в радианах тангенса y/x (направления) и переводим их в градусы
            float angleX = Mathf.Atan2(velocity.z, velocity.x) * Mathf.Rad2Deg;   // Возвращаем угол в радианах тангенса y/x (направления) и переводим их в градусы

            
            transform.rotation = Quaternion.Euler(new Vector3(angleX, angleY, angleZ));		
        }

        #endregion
    }
}