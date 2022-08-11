using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridMovement gridMovement = gameObject.GetComponent<GridMovement>();
        gridMovement.BeforeMoveEvent += beforeMove;
    }

    private void beforeMove(Action abortMovement, ref Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(direction.x * 0.5f, direction.y * 0.5f, 0), direction, 0.5f);
        IMapOffset offset = hit.collider?.gameObject.GetComponent<IMapOffset>();
        if (offset != null)
        {
            direction += new Vector2(offset.HorizontalOffset, offset.VerticalOffset);
        }
    }
}
