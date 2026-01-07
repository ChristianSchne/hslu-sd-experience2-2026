using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public UnityEvent onCollect;
    public bool triggerOnlyOnce = false;

    public void Collect()
    {
        if (triggerOnlyOnce)
        {
            Destroy(this);
        }
        onCollect.Invoke();
    }

}
