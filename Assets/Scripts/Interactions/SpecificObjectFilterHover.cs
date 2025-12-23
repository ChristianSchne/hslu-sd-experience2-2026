using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SpecificObjectFilter : MonoBehaviour, IXRHoverFilter
{
    public bool canProcess => isActiveAndEnabled;

    public bool Process(UnityEngine.XR.Interaction.Toolkit.Interactors.IXRHoverInteractor interactor, UnityEngine.XR.Interaction.Toolkit.Interactables.IXRHoverInteractable interactable)
    {
        var target = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.IXRHoverInteractable>();
        if (target == null) return false;
        return target == interactable;
    }
}