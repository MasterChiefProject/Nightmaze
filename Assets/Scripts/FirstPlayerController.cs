using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Look Settings")]
    public Transform main_camera;
    public float sensitivity = 10f;
    public float maxLookAngle = 60f;

    [Header("Move Settings")]
    public float speed = 3f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;               // assign an empty at your feet
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    Rigidbody rb;
    float xRotation = 0f;
    bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ��� Mouse Look ���
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        main_camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ��� Ground Check ���
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // ��� Movement & Jump ���
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Vector3 move = transform.TransformDirection(moveInput) * speed;

        // preserve current y-velocity
        move.y = rb.linearVelocity.y;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            move.y = jumpForce;
        }

        rb.linearVelocity = move;
    }

    // visualize the ground check sphere in editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}