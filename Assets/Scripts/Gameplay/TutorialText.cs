using TMPro;
using UnityEngine;

/// <summary>
/// Handles displaying tutorial text using the GUI system in Unity. It includes variables for enabling/disabling the GUI, the tutorial text to display, the size of the text box, and a custom skin for styling.
/// </summary>
public class TutorialText : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private TextMeshProUGUI text;

    #endregion

    #region UNITY_CALLS

    /// <summary>
    ///Triggered when the object enters a collider trigger zone. Turns on the text.
    /// </summary>
    void OnTriggerEnter()
    {
        text.gameObject.SetActive(true);
    }

    /// <summary>
    ///Triggered when the object exits a collider trigger zone. Turns off the text.
    /// </summary>
    void OnTriggerExit()
    {
        text.gameObject.SetActive(false);
    }
    
    #endregion
}