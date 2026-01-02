using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class RigidbodyCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Input Actions")]
    public InputActionReference moveActionReference;
    public InputActionReference jumpActionReference;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Vector2 moveInput;
    private bool jumpInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider component is missing from the GameObject.");
        }
    }

    private void OnEnable()
    {
        if (moveActionReference != null && moveActionReference.action != null)
        {
            moveActionReference.action.Enable();
            moveActionReference.action.performed += OnMove;
            moveActionReference.action.canceled += OnMove;
        }

        if (jumpActionReference != null && jumpActionReference.action != null)
        {
            jumpActionReference.action.Enable();
            jumpActionReference.action.performed += OnJump;
        }
    }

    private void OnDisable()
    {
        if (moveActionReference != null && moveActionReference.action != null)
        {
            moveActionReference.action.performed -= OnMove;
            moveActionReference.action.canceled -= OnMove;
            moveActionReference.action.Disable();
        }

        if (jumpActionReference != null && jumpActionReference.action != null)
        {
            jumpActionReference.action.performed -= OnJump;
            jumpActionReference.action.Disable();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            jumpInput = true;
        }
    }

    private void FixedUpdate()
    {
        // Calculate the bottom position of the CapsuleCollider
        float capsuleHeight = capsuleCollider.height;
        float capsuleRadius = capsuleCollider.radius;
        Vector3 capsuleBottom = transform.position + capsuleCollider.center - Vector3.up * (capsuleHeight / 2 - capsuleRadius);

        // Ground check using Raycast from the bottom of the CapsuleCollider
        float groundCheckDistance = 0.1f;
        isGrounded = Physics.Raycast(capsuleBottom, Vector3.down, groundCheckDistance + 0.1f, ~0, QueryTriggerInteraction.Ignore);

        // Visualize the ground check ray in the Scene view for debugging
        Debug.DrawRay(capsuleBottom, Vector3.down * (groundCheckDistance + 0.1f), isGrounded ? Color.green : Color.red);

        // Handle Horizontal Movement
        Vector3 horizontalVelocity = new Vector3(moveInput.x, rb.linearVelocity.y, moveInput.y) * moveSpeed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

        // Handle Jumping
        if (jumpInput)
        {
            // Reset vertical velocity before applying jump to ensure consistent jumps
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInput = false;
        }

        // Handle Rotation to Face Movement Direction
        if (moveInput != Vector2.zero)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, 720 * Time.fixedDeltaTime);
        }
    }
}
