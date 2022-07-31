using UnityEngine;
using MapObject.Interactable;

public class TriggerTile : MonoBehaviour, IInteractable
{
    private bool interacting = false;

    public bool CheckInteractionEnd()
    {
        return !interacting;
    }

    public void Interact()
    {
        if (interacting) return;

        interacting = true;
        Debug.Log("TriggerTile Interact!");

        Invoke("InteractEnd", 2);
    }

    // Demo Interaction
    private void InteractEnd()
    {
        interacting = false;
        Debug.Log("TriggerTile Interact End!");
    }
}
