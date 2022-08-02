using UnityEngine;
using System;
using MapObject.Interactable;
using UnityEngine.Tilemaps;

public class Interactor : MonoBehaviour
{
    public Tilemap map;
    private IInteractable interactTargetObject;
    void Start()
    {
        GridMovement gridMovement = gameObject.GetComponent<GridMovement>();
        gridMovement.BeforeMoveEvent += preMovement;
        gridMovement.MoveFinishedEvent += afterMovement;
    }

    private bool isInteracting
    {
        get
        {
            if (interactTargetObject == null) return false;
            return !interactTargetObject.CheckInteractionEnd();
        }
    }

    private void preMovement(Action abortMovement, Vector2 vec)
    {
        if (isInteracting)
        {
            abortMovement();
        }
        else
        {
            interactTargetObject = null;
        }

        // offset the raycast origin a little bit, so we won't interact with the block below us again
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(vec.x * 0.5f, vec.y * 0.5f, 0), vec, 0.5f);

        // If it hits something...
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                Debug.Log("Raycast IInteractable");
                interactTargetObject = hit.collider.gameObject.GetComponent<IInteractable>();
            }
        }
    }

    private void afterMovement()
    {
        // Position After Movement
        Debug.Log(map.WorldToCell(gameObject.transform.position));
        interactTargetObject?.Interact();
    }
}
