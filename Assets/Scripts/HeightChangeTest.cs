using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChangeTest : MonoBehaviour
{

    public Transform target;
    public bool StartTest;
    public float velocity;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (StartTest)
        {
            float prev_y = target.position.y;
            target.position = Vector3.MoveTowards(target.position, target.position + Vector3.up, velocity);
            //target.Translate(Vector3.up * velocity);
            float pos_y = target.position.y;
            Debug.Log("Parent on character move " + (pos_y - prev_y).ToString() + " at one frame");
        }
       
    }
}
