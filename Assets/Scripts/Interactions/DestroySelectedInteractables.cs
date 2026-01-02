using UnityEngine;

using System.Collections.Generic;

public class DestroySelectedInteractables : MonoBehaviour
{
    /// <summary>
    /// Destroys all XRInteractable GameObjects currently selected by any XRSocketInteractor in the children of this GameObject.
    /// </summary>
    public void DestroySelectedInteractablesInSockets()
    {
        // Retrieve all XRSocketInteractor components in the children of this GameObject
        UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor[] socketInteractors = GetComponentsInChildren<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        // Use a HashSet to avoid destroying the same interactable multiple times
        HashSet<UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable> interactablesToDestroy = new HashSet<UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable>();

        foreach (UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket in socketInteractors)
        {
            // Iterate through all interactables currently selected by the socket interactor
            foreach (var interactable in socket.interactablesSelected)
            {
                if (interactable != null)
                {
                    interactablesToDestroy.Add(interactable);
                }
            }

            // **Optional:** If you need to target the oldest interactable (similar to the deprecated selectTarget)
            // Uncomment the following lines:

            /*
            var oldestInteractable = socket.GetOldestInteractableSelected();
            if (oldestInteractable != null)
            {
                interactablesToDestroy.Add(oldestInteractable);
            }
            */
        }

        // Iterate through the collected interactables and destroy their GameObjects
        foreach (var interactable in interactablesToDestroy)
        {
            // Ensure the interactable is a MonoBehaviour to access the GameObject
            if (interactable is MonoBehaviour interactableMono)
            {
                Destroy(interactableMono.gameObject);
                Debug.Log($"Destroyed interactable: {interactableMono.gameObject.name}");
            }
            else
            {
                Debug.LogWarning("Interactable is not a MonoBehaviour and cannot be destroyed.");
            }
        }
    }

    // Example usage: Call this method when needed, e.g., via a button press or event
    private void Update()
    {
        // For demonstration, press the "D" key to destroy selected interactables
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroySelectedInteractablesInSockets();
        }
    }
}
