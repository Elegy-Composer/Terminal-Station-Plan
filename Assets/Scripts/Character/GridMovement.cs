using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

/// <summary>
/// It is recommended that you try to implement move-related behaviour with <see cref="BeforeMoveEvent"/> and <see cref="MoveFinishedEvent"/> first, and change <see cref="GridMovement"/> only when those two can't satisfy your need.
/// </summary>
public class GridMovement : MonoBehaviour
{
    public Grid grid;

    public float xUnit => grid.cellSize.x;
    public float yUnit => grid.cellSize.y;

    public float stepDuration = 0.5f;
    private float stepStopAccumulated = 0f;
    private bool accumulating = true;

    private Vector2 movement = Vector2.zero;

    /// <summary>
    /// The delegate type of the <see cref="BeforeMoveEvent"/>.
    /// During the event, you may abort the upcoming movement.
    /// </summary>
    /// 
    /// <param name="abortMovement">
    /// A callback that provides the ability to abort the upcoming movement
    /// 
    /// Unless you're pretty sure what you're doing, you should put a return after every <c>abortMovement()</c> call. That will be the correct behaviour (for most of the time).
    /// </param>
    /// 
    /// <param name="direction">
    /// The direction of the upcoming movement.
    /// 
    /// You may calculate the destination by extending it to Vector3 and adding the ruselt to current GameObject's position.
    /// See example in <see cref="CharacterDetector"/>.
    /// </param>
    public delegate void BeforeMove(System.Action abortMovement, ref Vector2 direction);

    /// <summary>
    /// The event that will invoke before moving.
    /// </summary>
    public event BeforeMove BeforeMoveEvent;

    public delegate void MoveFinished();
    /// <summary>
    /// The event that will invoke after moving to the destination
    /// </summary>
    public event MoveFinished MoveFinishedEvent;

    public Animator SpriteRotate;
    private PointFollower pointFollower;


    void Awake()
    {
        pointFollower = GetComponent<PointFollower>();
        pointFollower.onReachTargetEvent += () =>
        {
            MoveFinishedEvent?.Invoke();
            accumulating = true;
        };
    }

    private void FixedUpdate()
    {
        if (accumulating)
        {
            stepStopAccumulated += Time.fixedDeltaTime;
            if (stepStopAccumulated > stepDuration)
            {
                stepStopAccumulated = 0f;
                if (movement != Vector2.zero)
                {
                    bool canMove = true;
                    BeforeMoveEvent?.Invoke(() => canMove = false, ref movement);

                    Vector3 move = movement.ExtendToVector3();
                    if (canMove)
                    {
                        pointFollower.UpdateTarget(gameObject.transform.position + move);
                        accumulating = false;
                    }
                }
            }
        }
    }


    private Action currentAction = Action.NONE;
    private enum Action { NONE, UP, RIGHT, LEFT, DOWN };

    public enum Direction { UP, RIGHT, LEFT, DOWN };
    [SerializeField]
    private Direction _facing;
    public Direction Facing => _facing;

    private Direction fromAction(Action action)
    {
        switch (action)
        {
            case Action.UP:
                return Direction.UP;
            case Action.RIGHT:
                return Direction.RIGHT;
            case Action.LEFT:
                return Direction.LEFT;
            case Action.DOWN:
                return Direction.DOWN;
        }
        throw new ArgumentOutOfRangeException("can't transform Action.NONE to Direction");
    }

    private void OnEnable()
    {
        switch (currentAction)
        {
            case Action.UP:
            case Action.RIGHT:
            case Action.LEFT:
            case Action.DOWN:
                SpriteRotate.Play(currentAction.ToString());
                break;
        }
    }

    private bool _acceptingInput = true;
    public bool acceptingInput
    {
        get => _acceptingInput;
        set
        {
            if (!value)
            {
                //force stop
                currentAction = Action.NONE;
                movement = Vector2.zero;
            }
            _acceptingInput = value;
        }
    }

    public void OnUp(InputValue value)
    {
        if (!acceptingInput) return;
        Debug.Log("Up");
        changeMovement(Action.UP, value.isPressed, 0.5f, 0.5f);
    }

    public void OnRight(InputValue value)
    {
        if (!acceptingInput) return;
        Debug.Log("Right");
        changeMovement(Action.RIGHT, value.isPressed, 0.5f, -0.5f);
    }

    public void OnLeft(InputValue value)
    {
        if (!acceptingInput) return;
        Debug.Log("Left");
        changeMovement(Action.LEFT, value.isPressed, -0.5f, 0.5f);
    }

    public void OnDown(InputValue value)
    {
        if (!acceptingInput) return;
        Debug.Log("Down");
        changeMovement(Action.DOWN, value.isPressed, -0.5f, -0.5f);
    }

    private void changeMovement(Action action, bool isPressed, float x, float y)
    {
        if (isPressed)
        {
            if (acceptingInput)
            {
                SpriteRotate.Play(action.ToString());
                _facing = fromAction(action);
            }
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
