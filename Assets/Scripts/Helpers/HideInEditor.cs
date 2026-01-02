using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideInEditor : MonoBehaviour
{
    void Awake()
    {
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    void OnDestroy()
    {
        gameObject.hideFlags = HideFlags.None;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!EditorApplication.isPlayingOrWillChangePlaymode)
        {
            gameObject.hideFlags = HideFlags.HideInHierarchy;
        }
    }
#endif
}
