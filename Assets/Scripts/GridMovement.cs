using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using MapObject.Interactable;
using static UnityEngine.InputSystem.InputAction;

public class GridMovement : MonoBehaviour
{
    public Tilemap map;

    public float xUnit = 1f;
    public float yUnit = 0.37f;

    public float stepDuration = 0.5f;
    private float stepStopAccumulated = 0f;

    private Action currentAction = Action.NONE;
    private enum Action { NONE, UP, RIGHT, LEFT, DOWN };

    private Vector2 movement = Vector2.zero;
    private Vector3 lastTarget;

    private IInteractable interactTargetObject;

    // Start is called before the first frame update
    void Start()
    {
        lastTarget = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lastTarget, .1f);
        if (Vector3.Distance(gameObject.transform.position, lastTarget) == 0f)
        {
            stepStopAccumulated += Time.fixedDeltaTime;
            if (stepStopAccumulated > stepDuration)
            {
                stepStopAccumulated = 0f;
                Vector3 move = new Vector3(movement.x, movement.y, 0);
                lastTarget = gameObject.transform.position + move;
            }
        }
    }

    public void OnUp(InputValue value)
    {
        Debug.Log("Up");
        Debug.Log(value.isPressed);
        changeMovement(Action.UP, value.isPressed, 0.5f, 0.5f);
    }

    public void OnRight(InputValue value)
    {
        Debug.Log("Right");
        changeMovement(Action.RIGHT, value.isPressed, 0.5f, -0.5f);
    }

    public void OnLeft(InputValue value)
    {
        Debug.Log("Left");
        changeMovement(Action.LEFT, value.isPressed, -0.5f, 0.5f);
    }

    public void OnDown(InputValue value)
    {
        Debug.Log("Down");
        changeMovement(Action.DOWN, value.isPressed, -0.5f, -0.5f);
    }

    private void changeMovement(Action action, bool isPressed, float x, float y)
    {
        if (isPressed)
        {
            if (preMovementSetup(action, x, y)) setMovement(action, x, y);
        }
        else
        {
            clearMovement(action);
        }
    }

    private bool preMovementSetup(Action action, float x, float y)
    {
        // If interacting -> do not move
        if (interactTargetObject != null)
        {
            if (!interactTargetObject.CheckInteractionEnd()) return false;
            else interactTargetObject = null;
        }

        Vector2 vec = new Vector2(x * xUnit, y * yUnit);
        
        // offset the raycast origin a little bit, so we won't interact with the block below us again
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(vec.x * 0.5f, vec.y * 0.5f, 0), vec, 0.5f);

        // If it hits something...
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Raycast Obstacle!");
                return false;
            }

            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                Debug.Log("Raycast IInteractable");
                interactTargetObject = hit.collider.gameObject.GetComponent<IInteractable>();
            }
        }

        return true;
    }

    private void setMovement(Action action, float x, float y)
    {
        // new action take over
        stepStopAccumulated = stepDuration;
        currentAction = action;
        movement = new Vector2(x * xUnit, y * yUnit);
    }

    private void clearMovement(Action action)
    {
        if (currentAction == action)
        {
            currentAction = Action.NONE;
            movement = Vector2.zero;

            // Position After Movement
            Debug.Log(map.WorldToCell(gameObject.transform.position));

            if (interactTargetObject != null)
            {
                interactTargetObject.Interact();
            }
        }
    }
}
