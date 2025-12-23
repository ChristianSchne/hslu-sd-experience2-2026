using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SpecificObjectFilterSelect : MonoBehaviour, IXRSelectFilter
{
    public bool canProcess => isActiveAndEnabled;

    public bool Process(UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor, UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable)
    {
        var targetInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.IXRHoverInteractable>();
        
        bool isValid = targetInteractable == interactable; 
Debug.Log(isValid);
        return isValid;
    }
}