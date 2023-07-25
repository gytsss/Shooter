using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    private void Start()
    {
        normalSpeed = player.GetComponent<PlayerMovement>().speed;
    }

    public void OnNextLevel()
    {
        nextLevelEnabled = !nextLevelEnabled;
        SceneManager.LoadScene(nextLevelName);
    }

    public void OnGodMode()
    {
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

    public void OnFlash()
    {
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

    public void OnNuke()
    {
        nukePressed = true;

        if (nukePressed)
        {
            enemies.remainingEnemies = 0;

            Debug.Log("Nuke Drop");
            nukePressed = false;
        }
    }

}
