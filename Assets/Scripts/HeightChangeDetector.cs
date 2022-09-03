using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HeightChangeDetector : MonoBehaviour
{
    public Transform PivotPosition;
    public string SortingLayerName; // [TODO] maybe change to dropdown menu will be a better choice

    private void OnTriggerEnter2D(Collider2D collision) // if the player pass certain height
    {
        Debug.Log(collision.gameObject.name + " enter the detector");
        collision.GetComponent<SpriteManager>()?.OnHeightChangeStart(PivotPosition.position.y, SortingLayerName);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " leave the detector");
        collision.GetComponent<SpriteManager>()?.OnHeightChangeEnd();
    }

}
