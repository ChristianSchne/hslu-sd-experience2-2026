using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject original;


    public void InstantiateObject()
    {
        GameObject instance = Instantiate(original, transform.position, transform.rotation) as GameObject;
        instance.SetActive(true);
    }
}
