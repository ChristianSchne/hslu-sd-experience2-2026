using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public TextMeshPro text;
    public bool destroyOnCollect = true;
    public UnityEvent onCollect;
    int count = 0;

    private void Start()
    {
        UpdateText();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collectable collectable = other.GetComponent<Collectable>();
        if(collectable != null)
        {
            count = count + 1;
            collectable.onCollect.Invoke();
            onCollect.Invoke();
            UpdateText();
            if (destroyOnCollect)
            {
                Destroy(other.gameObject);
            }
        }
    }

    void UpdateText()
    {
        if(text == null)
            return;
        text.text = count + "";
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
