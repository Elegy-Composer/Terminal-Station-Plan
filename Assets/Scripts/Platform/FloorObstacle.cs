using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObstacle : MonoBehaviour
{
    private MovePlatform movePlatform;
    private PolygonCollider2D targetCollider, originCollider;
    // Start is called before the first frame update
    void Start()
    {
        movePlatform = GetComponent<MovePlatform>();
        targetCollider = transform.parent.Find("Target").GetComponent<PolygonCollider2D>();
        originCollider = transform.parent.Find("Origin").GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movePlatform.moving)
        {
            if (movePlatform.Raised)
            {
                targetCollider.enabled = false;
            }
            else
            {
                originCollider.enabled = false;
            }
        }
        else
        {
            targetCollider.enabled = true;
            originCollider.enabled = true;
        }
    }
}
