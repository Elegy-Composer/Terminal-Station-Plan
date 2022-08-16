using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PointFollower : MonoBehaviour
{
    public Tilemap tilemap;
    public float movingSpeed;
    private Vector3 lastTarget;
    public Vector3Int lastTargetToCell
    {
        get
        {
            return tilemap.WorldToCell(lastTarget);
        }
    }

    public void UpdateTarget()
    {
        lastTarget = gameObject.transform.position;
    }

    public void UpdateTarget(Vector3 target)
    {
        lastTarget = target;
    }

    public void UpdateTargetBy(Vector3 offset)
    {
        lastTarget += offset;
    }

    void Awake()
    {
        lastTarget = gameObject.transform.position;
    }

    private bool moving = false;
    public delegate void onReachTarget();
    public event onReachTarget onReachTargetEvent;

    private void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, lastTarget) == 0f)
        {
            //on target
            if (moving)
            {
                onReachTargetEvent?.Invoke();
                moving = false;
            }
        }
        else
        {
            //moving
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lastTarget, movingSpeed);
            moving = true;
        }
    }
}
