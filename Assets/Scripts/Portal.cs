using UnityEngine;

public class Portal : MonoBehaviour
{
    [Tooltip("Drag next position to teleport to")]
    public Transform teleportToLocation;
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Globals.playerTag)) return;

        var cc = other.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            other.transform.position = teleportToLocation.position;
            other.transform.rotation = teleportToLocation.rotation;
            cc.enabled = true;
            return;
        }
        else
        {
            var rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                rb.MovePosition(teleportToLocation.position);
                rb.MoveRotation(teleportToLocation.rotation);
            }
            else
            {
                other.transform.position = teleportToLocation.position;
                other.transform.rotation = teleportToLocation.rotation;
            }
        }
    }
}
