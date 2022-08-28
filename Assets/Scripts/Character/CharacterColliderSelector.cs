using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridMovement;

[RequireComponent(typeof(GridMovement))]
public class CharacterColliderSelector : MonoBehaviour
{
    [SerializeField]
    private CharacterCollider characterCollider;

    void Start()
    {
        GridMovement movement = GetComponent<GridMovement>();
        movement.BeforeMoveEvent += (Action _, ref Vector2 _) =>
        {
            //enable the collider which is perpendicular to moving direction
            if (movingUpOrDown(movement))
            {
                characterCollider.UseLRCollider();
            }
            else
            {
                characterCollider.UseUDCollider();
            }
        };
        movement.MoveFinishedEvent += characterCollider.UseAllColliders;
    }

    private bool movingUpOrDown(GridMovement movement)
    {
        return movement.Facing == Direction.UP || movement.Facing == Direction.DOWN;
    }
}
