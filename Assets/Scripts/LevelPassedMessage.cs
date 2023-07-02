using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPassedMessage : MonoBehaviour
{
    private Text messageText;

    private void Start()
    {
        messageText = GetComponent<Text>();
        messageText.enabled = false;
    }

    private void OnDestroy()
    {
        messageText.enabled = true;
        messageText.text = "Level 1 Completed!";
    }
}
