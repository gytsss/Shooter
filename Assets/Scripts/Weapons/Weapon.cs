using UnityEngine;

/// <summary>
/// Abstract class which allows for easier control of the weapons.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] protected PauseController pauseController;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected bool isPlayerWeapon = true;

    #endregion

    #region PROTECTED_METHODS

    /// <summary>
    /// Plays the shoot sound 
    /// </summary>
    protected void PlayShootSound()
    {
        SoundManager.instance.PlayShoot();
    }

    /// <summary>
    /// Plays the reload sound
    /// </summary>
    protected void PlayReloadSound()
    {
        SoundManager.instance.PlayReload();
    }

    #endregion

    #region PUBLIC_METHODS

    /// <summary>
    /// abstract method which is called when the weapon is fired, each weapon override this method 
    /// </summary>
    public abstract void OnFire();

    #endregion
}