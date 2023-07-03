using UnityEngine;

/// <summary>
/// Handles displaying tutorial text using the GUI system in Unity. It includes variables for enabling/disabling the GUI, the tutorial text to display, the size of the text box, and a custom skin for styling.
/// </summary>
public class TutorialText : MonoBehaviour
{
    [SerializeField] private bool GuiOn;

    [SerializeField] private string Text = "Press 'E' to pick up the gun";

    [SerializeField] private Rect BoxSize = new Rect(0, 0, 200, 100);

    [SerializeField] private GUISkin customSkin;

    /// <summary>
    ///Triggered when the object enters a collider trigger zone. Turns on the GUI.
    /// </summary>
    void OnTriggerEnter()
    {
        GuiOn = true;
    }

    /// <summary>
    ///Triggered when the object exits a collider trigger zone. Turns off the GUI.
    /// </summary>
    void OnTriggerExit()
    {
        GuiOn = false;
    }

    /// <summary>
    /// Renders the GUI showing the tutorial text in the center of the screen.
    /// </summary>
    void OnGUI()
    {

        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn == true)
        {
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
            GUI.Label(BoxSize, Text);
            GUI.EndGroup();
        }
    }
}
