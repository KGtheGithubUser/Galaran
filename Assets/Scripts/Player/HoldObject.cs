using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public float throwForce = 100f;
    public float grabDistance = 3f;
    public float moveForce = 500f;
    private Rigidbody heldObject;
    private Vector3 holdPoint;

    void Update()
    {
        // Try to grab with left click
        if (Input.GetMouseButtonDown(1))
        {
            TryGrab();
        }

        if (Input.GetMouseButtonUp(0) && heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject.AddForce(transform.forward * throwForce);
            heldObject = null;
        }

        // Drop object
        if (Input.GetMouseButtonUp(1) && heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject = null;
        }
    }

    void FixedUpdate()
    {
        if (heldObject != null)
        {
            Vector3 targetPos = transform.position + transform.forward * grabDistance;
            Vector3 force = (targetPos - heldObject.position) * moveForce * Time.fixedDeltaTime;
            heldObject.linearVelocity = Vector3.zero; // for stability
            heldObject.AddForce(force);
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.rigidbody != null)
            {
                heldObject = hit.rigidbody;
                heldObject.useGravity = false;
            }
        }
    }
}