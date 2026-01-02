using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads a new scene based on the provided scene name.
    /// </summary>
    /// <param name="sceneName">The exact name of the scene to load.</param>
    public void LoadSceneByName(string sceneName)
    {
        // Check if the scene can be loaded
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            // Load the scene
            SceneManager.LoadScene(sceneName);
            Debug.Log($"[SceneLoader] Loading scene: {sceneName}");
        }
        else
        {
            Debug.LogError($"[SceneLoader] Cannot load scene '{sceneName}'. Ensure it is added to the Build Settings and the name is correct.");
        }
    }
}
