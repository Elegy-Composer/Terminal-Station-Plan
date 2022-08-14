using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapObject.Interactable;


public class GetItemTile : MonoBehaviour, IInteractable
{
    [SerializeField]
    private List<int> ItemsIDList; //may change this to dictionary or struct to handle multiple same item
    private GameObject characterStepOn;
    public SpriteRenderer ItemSprite;


    public void Interact()
    {
        TempBackpack bp;
        bp = characterStepOn?.GetComponent<TempBackpack>();
        foreach (int itemID in ItemsIDList)
        {
            bp.AddItem(itemID);
        }
        ItemSprite.enabled = false;
        Destroy(this);
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

    public bool CheckInteractionEnd()
    {
        return true;
    }
}
