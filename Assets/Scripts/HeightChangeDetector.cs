using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChangeDetector : MonoBehaviour
{
    public Transform PivotPosition;

    private void OnTriggerEnter2D(Collider2D collision) // if the player pass certain height
    {
        Debug.Log("something enter");
        collision.GetComponent<SpriteManager>()?.OnHeightChangeStart(PivotPosition.position.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("something leave");
        collision.GetComponent<SpriteManager>()?.OnHeightChangeEnd();
    }

}
