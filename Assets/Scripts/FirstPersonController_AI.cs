using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController_AI : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Walking speed in units per second.")]
    public float walkSpeed = 5f;
    [Tooltip("Jump speed when pressing Jump button.")]
    public float jumpSpeed = 8f;
    [Tooltip("Gravity applied to the player.")]
    public float gravity = 20f;

    [Header("Look Settings")]
    [Tooltip("Mouse look sensitivity (higher is faster).")]
    public float mouseSensitivity = 2f;
    [Tooltip("Minimum vertical angle (look down limit). ")]
    public float minVerticalAngle = -90f;
    [Tooltip("Maximum vertical angle (look up limit). ")]
    public float maxVerticalAngle = 90f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity;

    private float rotationX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Lock cursor to the center and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    /// <summary>
    /// Handles first-person mouse look (pitch & yaw).
    /// </summary>
    private void HandleMouseLook()
    {
        // Horizontal rotation (yaw) applied to the player transform
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, mouseX, 0f);

        // Vertical rotation (pitch) applied to camera
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // Apply to camera child (assumes camera is a child of this object)
        Transform cam = Camera.main.transform;
        cam.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    /// <summary>
    /// Handles movement input, jumping, and gravity.
    /// </summary>
    private void HandleMovement()
    {
        if (controller.isGrounded)
        {
            // Read input
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            // Calculate desired move direction in world space
            Vector3 desiredMove = (forward * inputZ + right * inputX).normalized;

            // Multiply by speed
            moveDirection = desiredMove * walkSpeed;

            // Jump
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpSpeed;
            }
        }

        // Apply gravity always
        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
