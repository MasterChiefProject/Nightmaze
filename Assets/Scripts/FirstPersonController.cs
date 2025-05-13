using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    public Transform mainCamera;
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Look Settings")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 60f;

    [Header("Move Settings")]
    public float walkSpeed = 3f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private float xRotation;
    private bool jumpRequest;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ——— Mouse Look ———
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= my;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        mainCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(0f, mx, 0f);

        // ——— Jump Request ———
        if (Input.GetButtonDown("Jump"))
            jumpRequest = true;
    }

    void FixedUpdate()
    {
        // ——— Ground Check ———
        bool isGrounded = Physics.CheckSphere(
            groundCheck.position,
            0.2f,
            groundMask
        );

        // ——— Movement ———
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        Vector3 vel = rb.linearVelocity;
        vel.x = move.x * walkSpeed;
        vel.z = move.z * walkSpeed;
        rb.linearVelocity = vel;

        // ——— Jump ———
        if (jumpRequest && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
        jumpRequest = false;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
}
