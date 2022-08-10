using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChangeDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) // if the player pass certain height
    {
        Debug.Log("something enter");
        collision.GetComponent<SpriteManager>()?.OnHeightChangeStart(transform.position.y);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("something leave");
        collision.GetComponent<SpriteManager>()?.OnHeightChangeEnd();
    }

}
