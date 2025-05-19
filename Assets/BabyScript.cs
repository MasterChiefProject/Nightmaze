using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BabyScript : MonoBehaviour
{
    [Tooltip("How fast the statue turns to look at the player.")]
    public float rotationSpeed = 5f;

    private Transform player;
    private Rigidbody rb;
    private float baseY;

    void Awake()
    {
        // Ensure we have a kinematic Rigidbody with constraints
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX
                       | RigidbodyConstraints.FreezePositionY
                       | RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationZ;
    }

    void Start()
    {
        // Cache the player's transform
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
        else
            Debug.LogWarning("No GameObject tagged 'Player' found in the scene.");

        // Remember starting Y position
        baseY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // Direction on the XZ plane only
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.001f)
            return;

        // Compute target and smooth rotation
        Quaternion target = Quaternion.LookRotation(dir);
        Quaternion smooth = Quaternion.Slerp(
            transform.rotation,
            target,
            rotationSpeed * Time.deltaTime
        );

        // Apply only Y rotation
        transform.rotation = Quaternion.Euler(0f, smooth.eulerAngles.y, 0f);

        // Force original Y position (in case anything drifted)
        Vector3 pos = transform.position;
        pos.y = baseY;
        transform.position = pos;
    }
}
