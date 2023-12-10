using UnityEngine;

public class ImpactEffectFactory : MonoBehaviour
{
    #region PUBLIC_METHODS

    /// <summary>
    /// Creates an impact effect at the specified position and rotation.
    /// </summary>
    public static GameObject CreateImpactEffect(GameObject impactEffectPrefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(impactEffectPrefab, position, rotation);
    }

    #endregion
}