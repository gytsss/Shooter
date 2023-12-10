using UnityEngine;

/// <summary>
/// handles respawning objects by triggering the respawn logic when a collider enters the trigger zone. 
/// </summary>
public class Respawn : MonoBehaviour
{
    #region UNITY_CALLS

    /// <summary>
    /// Triggered when a collider enters the trigger zone. If the collider has the playerTag or enemyTag, respawns it at the respawn point.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagsManager.instance.playerTag))
        {
            other.transform.position = transform.position;
        }
    }

    #endregion
}