using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnImpact : MonoBehaviour
{
    public UnityEvent onImpact;
    public float impactVelocity = 3.5f;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > impactVelocity)
        {
            Impact();
        }
    }

    void Impact()
    {
        onImpact.Invoke();
    }

    void DestroyComponents()
    {
        Destroy(GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>());
        Destroy(rigidbody);
    }

}
