using UnityEngine;
using System;
using MapObject.Interactable;

/*
 * TODO: Currently the way to distinguish different IInteractable hasn't been decided yet.
 * Remember to adjust preMovement after it's decided and make sure this only handles the specified type of IInteractable.
 * (Or even further adjustment, if we decided to handle all IInteractable in one place)
 */

public class StepOnExclusiveSingleLockInteractor : MonoBehaviour
{
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
            return;
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
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable?.CheckInteractionEnd() ?? false)
            {
                Debug.Log("Raycast available IInteractable");
                interactTargetObject = interactable;
            }
        }
    }

    private void afterMovement()
    {
        interactTargetObject?.Interact();
    }
}
