using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        enterEvent.Invoke();
        Debug.Log("Trigger enter on " + gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        exitEvent.Invoke();
        Debug.Log("Trigger exit on " + gameObject.name);
    }
}
