using System.Collections.Generic;
using UnityEngine;
using MapObject.Interactable;


//This script is just for temporary use(check objective item), we may introduce other way to check the objective in the future.
public class CheckItemTile : MonoBehaviour, IInteractable
{
    [SerializeField]
    private List<int> ItemsNeeded;
    private GameObject characterStepOn;



    public void Interact()
    {
        TempBackpack bp = characterStepOn?.GetComponent<TempBackpack>();
        foreach (int itemID in ItemsNeeded)
        {
            if (!bp.ItemsIDList.Contains(itemID))
            {
                return;
            }
        }
        Notification.Instance.ShowMessage("Congradulations!");
        Destroy(this);
    }

    public bool CheckInteractionEnd()
    {
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TempBackpack>() != null)
        {
            characterStepOn = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TempBackpack>() != null)
        {
            characterStepOn = null;
        }
    }
}
