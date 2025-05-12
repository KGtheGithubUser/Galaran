using UnityEngine;

public class BreakOnImpact : MonoBehaviour
{
    public float breakForceThreshold = 10f;
    public GameObject brokenVersion; // Drag fractured model prefab here

    void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > breakForceThreshold)
        {
            Break();
        }
    }

    void Break()
    {
        if (brokenVersion != null)
        {
            Instantiate(brokenVersion, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}