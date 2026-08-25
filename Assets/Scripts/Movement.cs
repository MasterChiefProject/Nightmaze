using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField, Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField] private bool cursorLock = true;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float Speed = 6.0f;
    [SerializeField, Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;

    public float jumpHeight = 6f;

    private float velocityY;
    private bool isGrounded;
    private float cameraCap;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;
    private CharacterController controller;
    private Vector2 currentDir;
    private Vector2 currentDirVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        UpdateMouse();
        UpdateMove();
    }

    private void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta,
            targetMouseDelta,
            ref currentMouseDeltaVelocity,
            mouseSmoothTime
        );

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    private void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 movement =
            (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed +
            Vector3.up * velocityY;

        controller.Move(movement * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }
}
