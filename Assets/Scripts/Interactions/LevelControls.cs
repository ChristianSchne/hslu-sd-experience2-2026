using UnityEngine;

public class LevelControls : MonoBehaviour
{
    public HingeJoint hingeJoint; // Reference to hinge joint
    public Rigidbody movingObject; // The object whose speed we control
    public float maxSpeed = 10f; // Maximum speed
    public float minSpeed = 0f;  // Minimum speed

    void Update()
    {
        if (hingeJoint == null || movingObject == null) return;

        // Get the handle's rotation angle
        float angle = hingeJoint.angle;

        // Normalize angle to a range of 0 to 1 based on hinge limits
        float normalizedAngle = Mathf.InverseLerp(hingeJoint.limits.min, hingeJoint.limits.max, angle);

        // Map the normalized angle to speed range
        float targetSpeed = Mathf.Lerp(minSpeed, maxSpeed, normalizedAngle);

        // Apply velocity in a direction (example: forward in world space)
        movingObject.linearVelocity = transform.forward * targetSpeed;
    }
}
