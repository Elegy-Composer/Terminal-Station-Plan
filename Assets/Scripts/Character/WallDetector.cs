using UnityEngine;
using System;

public class WallDetector : MonoBehaviour
{
    private const int obstacleLayer = 6;

    void Start()
    {
        gameObject.GetComponent<GridMovement>().BeforeMoveEvent += preMovement;
    }

    private void preMovement(Action abortMovement, ref Vector2 vec)
    {
        // offset the raycast origin a little bit, so we won't interact with the block below us again
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(vec.x * 0.5f, vec.y * 0.5f, 0), vec, 0.5f, 1 << obstacleLayer);

        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log("Raycast Obstacle!");
            abortMovement();
        }
    }
}
