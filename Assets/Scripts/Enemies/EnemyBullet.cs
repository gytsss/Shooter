using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the behavior of enemy bullets in the game. 
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    #region UNITY_CALLS

    /// <summary>
    /// Called when the bullet collides with another object. Destroys the bullet game object after a delay of 0.5 seconds.
    /// </summary>
    private void OnCollisionEnter()
    {
        Destroy(gameObject, .5f);
    }

    #endregion
}