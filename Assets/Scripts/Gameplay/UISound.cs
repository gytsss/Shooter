using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages UI Sound
/// </summary>
public class UISound : MonoBehaviour
{
    /// <summary>
    /// Plays UI sound
    /// </summary>
    public void PlayUISound()
    {
        SoundManager.instance.PlayMenu();
    }
}
