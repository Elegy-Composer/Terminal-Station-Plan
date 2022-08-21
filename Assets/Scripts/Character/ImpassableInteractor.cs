using UnityEngine;
using UnityEngine.InputSystem;
using System;
using MapObject.Interactable;

/*
 * TODO: Currently the way to distinguish different IInteractable hasn't been decided yet.
 * Remember to adjust preMovement after it's decided and make sure this only handles the specified type of IInteractable.
 * (Or even further adjustment, if we decided to handle all IInteractable in one place)
 */

public class ImpassableInteractor : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer;

    public ImpassableInteractor otherInteractor;
    private IInteractable interactTargetObject;
    private Vector2 facingVectorCache = Vector2.zero;

    void Start()
    {
        GridMovement gridMovement = gameObject.GetComponent<GridMovement>();
        gridMovement.BeforeMoveEvent += preMovement;
    }

    // The interaction triggered by player key input.
    public void OnPlayerInteract(InputValue value)
    {
        if (!value.isPressed) return;

        // offset the raycast origin a little bit, so we won't interact with the block below us again
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + new Vector3(facingVectorCache.x * 0.5f, facingVectorCache.y * 0.5f, 0), facingVectorCache, 0.5f,
            interactableLayer
        );

        // If it hits something...
        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != otherInteractor.interactTargetObject || (interactable == otherInteractor.interactTargetObject && !otherInteractor.isInteracting))
            {
                interactTargetObject = interactable;
            } else
            {
                return;
            }
        }
        else
        {
            interactTargetObject = null;
            return;
        }

        if (!isInteracting)
        {
            Debug.Log(String.Format("[{0}] Key Interact", gameObject.name));
            interactTargetObject?.Interact();
        }        
    }

    public bool isInteracting
    {
        get
        {
            if (interactTargetObject == null) return false;
            return !interactTargetObject.CheckInteractionEnd();
        }
    }

    private void preMovement(Action abortMovement, ref Vector2 vec)
    {
        facingVectorCache = new Vector2(vec.x, vec.y);

        // offset the raycast origin a little bit, so we won't interact with the block below us again
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(vec.x * 0.5f, vec.y * 0.5f, 0), vec, 0.5f, interactableLayer);

        // If it hits something...
        if (hit.collider != null)
        {
            abortMovement();
        }
        else
        {
            interactTargetObject = null;
        }
    }
}
