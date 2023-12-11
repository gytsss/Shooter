using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game cheats
/// </summary>
public class CheatsManager : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private string nextLevelName;
    [SerializeField] private Player player;
    [SerializeField] private float flashSpeed = 20f;
    [SerializeField] private GameTimer timer;
    [SerializeField] private GameObject[] enemies;

    #endregion

    #region PRIVATE_FIELDS

    private float normalSpeed;
    private bool nextLevelEnabled = false;
    private bool godModeEnabled = false;
    private bool flashEnabled = false;
    private bool nukePressed = false;
    private bool infiniteTimer = false;

    #endregion

    #region UNITY_CALLS

    /// <summary>
    /// Gets player speed
    /// </summary>
    private void Start()
    {
        normalSpeed = player.GetComponent<PlayerMovement>().speed;
    }

    #endregion

    #region PRIVATE_METHODS

    /// <summary>
    /// Plays switch sound
    /// </summary>
    private void PlaySwitchSound()
    {
        SoundManager.instance.PlaySwitch();
    }

    #endregion

    #region PUBLIC_METHODS

    /// <summary>
    /// Pass to the next level
    /// </summary>
    public void OnNextLevel()
    {
        PlaySwitchSound();
        nextLevelEnabled = !nextLevelEnabled;
        SceneManager.LoadScene(nextLevelName);
    }

    /// <summary>
    /// Any enemy can kill you
    /// </summary>
    public void OnGodMode()
    {
        PlaySwitchSound();
        godModeEnabled = !godModeEnabled;

        if (godModeEnabled)
        {
            player.healthComponent._health = float.PositiveInfinity;
            Debug.Log("God Mode On");
        }
        else
        {
            player.healthComponent._health = player.healthComponent._maxHealth;
            Debug.Log("God Mode Off");
        }
    }

    /// <summary>
    /// Run like flash
    /// </summary>
    public void OnFlash()
    {
        PlaySwitchSound();
        flashEnabled = !flashEnabled;

        if (flashEnabled)
        {
            player.GetComponent<PlayerMovement>().speed = flashSpeed;
            Debug.Log("Flash Mode On");
        }
        else
        {
            player.GetComponent<PlayerMovement>().speed = normalSpeed;
            Debug.Log("Flash Mode Off");
        }
    }

    /// <summary>
    /// All enemies die
    /// </summary>
    public void OnNuke()
    {
        PlaySwitchSound();
        nukePressed = true;

        if (nukePressed)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                HealthComponent enemyHealthComponent = enemies[i].GetComponent<HealthComponent>();
                
                enemyHealthComponent.DecreaseHealth(9999f);
            }

            Debug.Log("Nuke Drop");
            nukePressed = false;
        }
    }

    /// <summary>
    /// Timer never ends
    /// </summary>
    public void OnInfiniteTimer()
    {
        PlaySwitchSound();
        infiniteTimer = !infiniteTimer;

        if (infiniteTimer)
        {
            timer.SetRemainingTime(Single.MaxValue);

            Debug.Log("Infinite Timer On");
            nukePressed = false;
        }
        else
        {
            timer.ResetRemainingTime();
            Debug.Log("Infinite Timer Off");
        }
    }

    #endregion
}