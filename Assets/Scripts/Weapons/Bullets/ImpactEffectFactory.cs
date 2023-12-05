using UnityEngine;

public class ImpactEffectFactory : MonoBehaviour
{
    public static GameObject CreateImpactEffect(GameObject impactEffectPrefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(impactEffectPrefab, position, rotation);
    }
}

