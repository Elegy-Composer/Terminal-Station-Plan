using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapObject.Interactable;

public class DummyNPC : MonoBehaviour, IInteractable
{
    public bool interacting = false;

    public bool CheckInteractionEnd()
    {
        // TODO: implement end condition
        return !interacting;
    }

    public void Interact()
    {
        interacting = true;
        Debug.Log("[NPC] RRRRRRRR");
        Notification.Instance.ShowMessage("RRRRRRRRR");
        Invoke("EndConversation", 5);
    }

    private void EndConversation()
    {
        interacting = false;
    }
}
