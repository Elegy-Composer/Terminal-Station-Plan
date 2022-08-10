using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChangeTest : MonoBehaviour
{
    [TextArea]
    public string description;
    public Transform target;

    [Tooltip("Check this to start test, uncheck this to stop the target")]
    public bool StartTest;

    public float velocity;



    // Update is called once per frame
    void FixedUpdate()
    {
        if (StartTest)
        {
            target.GetComponent<GridMovement>().enabled = false;
            target.position = Vector3.MoveTowards(target.position, target.position + Vector3.up, velocity);
        }
        else 
        {
            target.GetComponent<GridMovement>().enabled = true;
        } 
    }
}
