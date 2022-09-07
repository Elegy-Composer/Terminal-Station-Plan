using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightDetectorManager : MonoBehaviour
{
    public HeightChangeDetector[] heightDetectors;

    public void EnableDetectors(List<GameObject> detectTargets)
    {
        foreach (HeightChangeDetector heightDetector in heightDetectors)
        {
            heightDetector.EnableDetector(detectTargets);
        }
    }
    public void CloseDetectors()
    {
        foreach (HeightChangeDetector heightDetector in heightDetectors)
        {
            heightDetector.CloseDetector();
        }
    }
}
