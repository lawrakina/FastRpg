using UnityEngine;


public class SoundPlayer : MonoBehaviour
{
    #region Fields

    [SerializeField] public AudioClip _stepFoot;
    [SerializeField] public AudioClip _attackSword;
    [SerializeField] public AudioClip _attack2HandAxe;

    #endregion
    

    #region Methods

    public void PlayStepFoot()
    {
        AudioSource.PlayClipAtPoint(_stepFoot, transform.position);
    }

    public void PlaySwordAttack()
    {
        AudioSource.PlayClipAtPoint(_attackSword, transform.position);   
    }
    
    public void PlayAxeAttack()
    {
        AudioSource.PlayClipAtPoint(_attack2HandAxe, transform.position);
    }

    #endregion
}
