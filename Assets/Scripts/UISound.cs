using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage UI Sound
/// </summary>
public class UISound : MonoBehaviour
{
    /// <summary>
    /// Play UI sound
    /// </summary>
    public void PlayUISound()
    {
        FindObjectOfType<SoundManager>().Play("UI");
    }
}
