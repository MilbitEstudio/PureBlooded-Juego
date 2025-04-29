using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    [Header("Input Settings")]
    public InputActionAsset inputActions; 

    private Rigidbody rb;
    private Vector2 movementInput;
    private Vector2 mouseInput;
    private bool isRunning;
    private bool isGrounded;
    private Vector3 velocity;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction runAction;
    private InputAction jumpAction;

    [Header("Ground Detection")]
    public LayerMask groundLayer; 
    public float groundCheckDistance = 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (inputActions == null)
        {
            Debug.LogError("InputActionAsset no asignado en el Inspector.");
            return;
        }

        var playerActionMap = inputActions.FindActionMap("Player");
        if (playerActionMap == null)
        {
            Debug.LogError("Action Map 'Player' no encontrado en el InputActionAsset.");
            return;
        }

        moveAction = playerActionMap.FindAction("Move");
        lookAction = playerActionMap.FindAction("Look");
        runAction = playerActionMap.FindAction("Run");
        jumpAction = playerActionMap.FindAction("Jump");

        moveAction.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => movementInput = Vector2.zero;

        lookAction.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => mouseInput = Vector2.zero;

        runAction.performed += ctx => isRunning = true;
        runAction.canceled += ctx => isRunning = false;

        jumpAction.performed += ctx => Jump();

        playerActionMap.Enable();
    }

    private void Update()
    {
        HandleCamera();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 move = (transform.right * movementInput.x + transform.forward * movementInput.y).normalized;
        
        // Aquí se usa MovePosition correctamente
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    private void HandleCamera()
    {
        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        float newXRotation = cameraTransform.localEulerAngles.x - mouseY;
        if (newXRotation > 180) newXRotation -= 360;
        newXRotation = Mathf.Clamp(newXRotation, -90f, 90f);

        cameraTransform.localEulerAngles = new Vector3(newXRotation, 0f, 0f);
    }

    private void ApplyGravity()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Ahora usamos AddForce en lugar de modificar directamente la velocidad
        rb.AddForce(Vector3.up * velocity.y, ForceMode.Acceleration);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
