using UnityEngine;

/// <summary>
/// Responsible for creating instances of the Player class.
/// </summary>
public class PlayerFactory : MonoBehaviour
{
    #region PUBLIC_METHODS

    /// <summary>
    /// Creates and returns an instance of the Player class. If a player object already exists, it returns the existing player component.
    /// If not, it creates a new player object and adds the Player component to it.
    /// </summary>
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

    #endregion
}