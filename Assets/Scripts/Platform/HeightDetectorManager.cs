using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightDetectorManager : MonoBehaviour
{
    public BoxCollider2D heightDetector;
    private MovePlatform movePlatform;

    // Start is called before the first frame update
    void Start()
    {
        movePlatform = GetComponent<MovePlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movePlatform.IsStepped)
        {
            heightDetector.enabled = true;
        }
        else
        {
            heightDetector.enabled = false;
        }
    }
}
