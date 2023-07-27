using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage game cheats
/// </summary>
public class CheatsManager : MonoBehaviour
{

    [SerializeField] private string nextLevelName;
    [SerializeField] private Player player;
    [SerializeField] private float flashSpeed = 20f;
    [SerializeField] private EnemiesManager enemies;
    private float normalSpeed;
    private bool nextLevelEnabled = false;
    private bool godModeEnabled = false;
    private bool flashEnabled = false;
    private bool nukePressed = false;


    /// <summary>
    /// Get player speed
    /// </summary>
    private void Start()
    {
        normalSpeed = player.GetComponent<PlayerMovement>().speed;
    }

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
            player.currentHealth = float.PositiveInfinity;
            Debug.Log("God Mode On");
        }
        else
        {
            player.currentHealth = player.maxHealth;
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
            enemies.remainingEnemies = 0;

            Debug.Log("Nuke Drop");
            nukePressed = false;
        }
    }

    /// <summary>
    /// Play switch sound
    /// </summary>
    private void PlaySwitchSound()
    {
        FindObjectOfType<SoundManager>().Play("Switch");
    }

}
