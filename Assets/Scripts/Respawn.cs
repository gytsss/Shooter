using UnityEngine;

/// <summary>
/// handles respawning objects by triggering the respawn logic when a collider enters the trigger zone. 
/// </summary>
public class Respawn : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    /// <summary>
    /// Triggered when a collider enters the trigger zone. If the collider has the playerTag or enemyTag, respawns it at the respawn point.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.position = transform.position;
        }
    }
}
