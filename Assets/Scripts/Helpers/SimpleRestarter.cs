using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class SimpleRestarter : MonoBehaviour
{
    float buttonStartTime;
    public float pressTimeInSeconds = 2;
    public InputActionReference resetAction;

    void Start()
    {
        if (resetAction == null)
            return;
        resetAction.action.started += StartResetAction;
        resetAction.action.canceled += CancelResetAction;
    }

    void StartResetAction(InputAction.CallbackContext obj)
    {
        buttonStartTime = Time.time;
        Debug.Log("Start reset");
    }

 

    void CancelResetAction(InputAction.CallbackContext obj)
    {
        Debug.Log("Complete reset");
        if (Time.time > buttonStartTime + pressTimeInSeconds)
        {
            Reset();
        }
    }

    public void Reset()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
}
