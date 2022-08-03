﻿using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour
{
    public float xUnit = 1f;
    public float yUnit = 0.37f;

    public float stepDuration = 0.5f;
    private float stepStopAccumulated = 0f;

    private Vector2 movement = Vector2.zero;
    private Vector3 lastTarget;

    public delegate void BeforeMove(System.Action abortMovement, Vector2 direction);
    public event BeforeMove BeforeMoveEvent;

    public delegate void MoveFinished();
    public event MoveFinished MoveFinishedEvent;
    private bool moving = false;


    void Start()
    {
        lastTarget = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, lastTarget) == 0f)
        {
            //on target
            if (moving)
            {
                MoveFinishedEvent?.Invoke();
                moving = false;
            }
            stepStopAccumulated += Time.fixedDeltaTime;
            if (stepStopAccumulated > stepDuration)
            {
                stepStopAccumulated = 0f;
                if (movement != Vector2.zero)
                {
                    Vector3 move = movement.ExtendToVector3();
                    bool canMove = true;
                    BeforeMoveEvent?.Invoke(() => canMove = false, movement);

                    if (canMove)
                    {
                        lastTarget = gameObject.transform.position + move;
                    }
                }
            }
        }
        else
        {
            //moving
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lastTarget, .1f);
            moving = true;
        }
    }


    private Action currentAction = Action.NONE;
    private enum Action { NONE, UP, RIGHT, LEFT, DOWN };

    public void OnUp(InputValue value)
    {
        Debug.Log("Up");
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
            setMovement(action, x, y);
        }
        else
        {
            clearMovement(action);
        }
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
        }
    }
}
