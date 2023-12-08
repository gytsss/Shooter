using UnityEngine;
/// <summary>
/// Abstract class which allows for easier control of the weapons.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected PauseController pauseController;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected bool isPlayerWeapon = true;
    
    public abstract void OnFire();
    
    protected void PlayShootSound()
    {
        SoundManager.instance.PlayShoot();
    }
    
    protected void PlayReloadSound()
    {
        SoundManager.instance.PlayReload();
    }
}
