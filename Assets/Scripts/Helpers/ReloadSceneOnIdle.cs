using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnIdle : MonoBehaviour
{
    public Transform targetTransform; // The transform to track
    public float idleTimeThreshold = 5f; // The idle time threshold in seconds

    private Vector3 lastPosition;
    private float idleTimer;

    private void Start()
    {
        lastPosition = targetTransform.position;
    }

    private void Update()
    {
        if (targetTransform.position == lastPosition)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeThreshold)
            {
                ReloadScene();
            }
        }
        else
        {
            idleTimer = 0f;
            lastPosition = targetTransform.position;
        }
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}