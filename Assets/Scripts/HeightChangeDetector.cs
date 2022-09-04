using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HeightChangeDetector : MonoBehaviour
{
    private const float yDelta = 0.01f;// to overcome some mysterious position inaccuracy between platform and character
    public Transform PivotPosition;
    public string SortingLayerName; // [TODO] maybe change to dropdown menu will be a better choice

    private void OnTriggerEnter2D(Collider2D collision) // if the player pass certain height
    {
        //Debug.Log(collision.gameObject.name + " enter the detector, y = " + PivotPosition.position.y.ToString());
        if (collision.GetComponent<SpriteManager>() == null) return;
        int priorty = collision.GetComponent<SpriteManager>().SortingPriority;
        float offset = priorty * yDelta;
        collision.GetComponent<SpriteManager>()?.OnHeightChangeStart(PivotPosition.position.y-offset, SortingLayerName);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " leave the detector");
        collision.GetComponent<SpriteManager>()?.OnHeightChangeEnd();
    }

}
