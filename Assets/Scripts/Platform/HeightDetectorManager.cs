using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightDetectorManager : MonoBehaviour
{
    public HeightChangeDetector[] heightDetectors;
    private MovePlatform movePlatform;
    private List<GameObject> detectTargets = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        movePlatform = GetComponent<MovePlatform>();
    }

    public void EnableDetectors()
    {
        detectTargets.Add(movePlatform.gameObject.GetComponentInChildren<SpriteManager>().gameObject);
        if (movePlatform.IsStepped)
        {
            detectTargets.Add(movePlatform.Character.GetComponentInChildren<SpriteManager>().gameObject);
        }
        foreach (HeightChangeDetector heightDetector in heightDetectors)
        {
            heightDetector.EnableDetector(detectTargets);
        }
    }
    public void CloseDetectors()
    {
        detectTargets.Clear();
        foreach (HeightChangeDetector heightDetector in heightDetectors)
        {
            heightDetector.CloseDetector();
        }
    }
}
