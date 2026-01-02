using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ThirdPersonCharacter : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float turnSpeed = 180f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Transform cameraTransform;
    private Vector2 inputMove;
    private Vector3 velocity;
    private bool isGrounded;
    
    public InputActionReference moveAction; // Assign via Inspector
    public InputActionReference jumpAction; // Assign via Inspector

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Check if character is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep character grounded
        }

        // Read movement input from XR Controller Joystick
        inputMove = moveAction.action.ReadValue<Vector2>();

        if (inputMove.magnitude > 0.1f)
        {
            Vector3 direction = new Vector3(inputMove.x, 0, inputMove.y).normalized;
            Vector3 moveDirection = cameraTransform.forward * direction.z + cameraTransform.right * direction.x;
            moveDirection.y = 0; // Prevent unintended vertical movement

            // Move the character
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Rotate character towards movement direction
            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            }
        }

        // Handle jumping
        if (jumpAction.action.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
