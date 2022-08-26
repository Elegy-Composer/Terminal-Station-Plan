using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridMovement;

[RequireComponent(typeof(GridMovement))]
public class CharacterColliderSelector : MonoBehaviour
{
    [SerializeField]
    [Header("The collider that expands in up-down direction")]
    private Collider2D UDCollider;
    [SerializeField]
    [Header("The collider that expands in left-right direction")]
    private Collider2D LRCollider;
    void Start()
    {
        GridMovement movement = GetComponent<GridMovement>();
        movement.BeforeMoveEvent += (Action _, ref Vector2 _) =>
        {
            //enable the collider which is perpendicular to moving direction
            UDCollider.enabled = !movingUpOrDown(movement);
            LRCollider.enabled = movingUpOrDown(movement);
        };
        movement.MoveFinishedEvent += () =>
        {
            UDCollider.enabled = true;
            LRCollider.enabled = true;
        };
    }

    private bool movingUpOrDown(GridMovement movement)
    {
        return movement.Facing == Direction.UP || movement.Facing == Direction.DOWN;
    }
}
