using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float turnSpeed = 10f;
    public float upDownSpeed = 3f;
    public float easingAcceleration = 1f;
    public float easingDeceleration = 0.5f;

    [Header("Input Settings")]
    public InputActionReference moveHorizontalAction;
    public InputActionReference moveVerticalAction;
    public InputActionReference moveUpAction;
    public InputActionReference moveDownAction;

    private Vector2 inputHorizontal = Vector2.zero;
    private Vector2 inputVertical = Vector2.zero;

    private Vector3 velocity = Vector3.zero;
    private float rotationVelocity = 0f;
    private float verticalVelocity = 0f;

    private CharacterController controller;
    private Vector3 pendingMove = Vector3.zero;
    public bool enableGravity = true;
    public float gravity = 9.81f;
    private float gravityVelocity = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null) controller = gameObject.AddComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (moveHorizontalAction != null) moveHorizontalAction.action.Enable();
        if (moveVerticalAction != null) moveVerticalAction.action.Enable();
        if (moveUpAction != null) moveUpAction.action.Enable();
        if (moveDownAction != null) moveDownAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveHorizontalAction != null) moveHorizontalAction.action.Disable();
        if (moveVerticalAction != null) moveVerticalAction.action.Disable();
        if (moveUpAction != null) moveUpAction.action.Disable();
        if (moveDownAction != null) moveDownAction.action.Disable();
    }

    void Update()
    {
        pendingMove = Vector3.zero;
        HandleMovement();
        HandleRotation();
        HandleVerticalMovement();
        if (enableGravity)
        {
            if (controller.isGrounded)
            {
                if (gravityVelocity < 0f) gravityVelocity = -1f;
            }
            else
            {
                gravityVelocity -= gravity * Time.deltaTime;
            }
            if (Mathf.Abs(verticalVelocity) > 0.001f) gravityVelocity = 0f;
            pendingMove += Vector3.up * gravityVelocity;
        }
        if (pendingMove != Vector3.zero) controller.Move(pendingMove * Time.deltaTime);
    }

    void HandleMovement()
    {
        if (moveVerticalAction == null) return;
        inputVertical = moveVerticalAction.action.ReadValue<Vector2>();
        float verticalInput = inputVertical.y;
        if (Mathf.Abs(verticalInput) > 0.5f)
        {
            Vector3 desiredVelocity = transform.forward * verticalInput;
            desiredVelocity.y = 0f;
            velocity = Vector3.Lerp(velocity, desiredVelocity, Time.deltaTime * easingAcceleration);
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * easingDeceleration);
        }
        pendingMove += velocity * moveSpeed;
    }

    void HandleRotation()
    {
        if (moveHorizontalAction == null) return;
        inputHorizontal = moveHorizontalAction.action.ReadValue<Vector2>();
        float horizontalInput = inputHorizontal.x;
        if (Mathf.Abs(horizontalInput) > 0.5f)
        {
            float desiredRotationVelocity = horizontalInput;
            rotationVelocity = Mathf.Lerp(rotationVelocity, desiredRotationVelocity, Time.deltaTime * easingAcceleration);
        }
        else
        {
            rotationVelocity = Mathf.Lerp(rotationVelocity, 0f, Time.deltaTime * easingDeceleration);
        }
        transform.Rotate(Vector3.up, rotationVelocity * turnSpeed * Time.deltaTime, Space.World);
    }

    void HandleVerticalMovement()
    {
        float desiredVerticalInput = 0f;
        if (moveUpAction != null && moveUpAction.action.ReadValue<float>() > 0.5f) desiredVerticalInput += 1f;
        if (moveDownAction != null && moveDownAction.action.ReadValue<float>() > 0.5f) desiredVerticalInput -= 1f;
        if (Mathf.Abs(desiredVerticalInput) > 0.1f)
        {
            float desiredVerticalVelocity = desiredVerticalInput;
            verticalVelocity = Mathf.Lerp(verticalVelocity, desiredVerticalVelocity, Time.deltaTime * easingAcceleration);
        }
        else
        {
            verticalVelocity = Mathf.Lerp(verticalVelocity, 0f, Time.deltaTime * easingDeceleration);
        }
        pendingMove += Vector3.up * verticalVelocity * upDownSpeed;
    }
}
