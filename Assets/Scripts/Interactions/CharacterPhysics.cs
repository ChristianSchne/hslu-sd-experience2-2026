using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterPhysics : MonoBehaviour
{
    public Transform vrCamera;
    public float movementSpeed = 5f;
    public float maxPushForce = 10f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(vrCamera.position.x, transform.position.y, vrCamera.position.z);
        Vector3 delta = targetPosition - transform.position;
        Vector3 movement = new Vector3(delta.x, 0, delta.z) * movementSpeed * Time.deltaTime;
        characterController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        float force = movementSpeed * 2f;
        force = Mathf.Min(force, maxPushForce);
        body.AddForce(pushDir * force, ForceMode.Impulse);
    }
}
