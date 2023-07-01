using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private bool GuiOn;

    [SerializeField] private string Text = "Press 'E' to pick up the gun";

    [SerializeField] private Rect BoxSize = new Rect(0, 0, 200, 100);

    [SerializeField] private GUISkin customSkin;

    void OnTriggerEnter()
    {
        GuiOn = true;
    }


    void OnTriggerExit()
    {
        GuiOn = false;
    }

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
