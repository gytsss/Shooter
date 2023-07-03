using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    public static Player CreatePlayer()
    {
        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject != null)
        {
            Player playerComponent = playerGameObject.GetComponent<Player>();
            if (playerComponent != null)
            {
                return playerComponent;
            }
        }

        GameObject newPlayerObject = new GameObject("Player");
        Player newPlayerComponent = newPlayerObject.AddComponent<Player>();

        return newPlayerComponent;
    }
}
