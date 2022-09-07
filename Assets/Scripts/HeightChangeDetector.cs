using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HeightChangeDetector : MonoBehaviour 
{                                                 
    private const float yDelta = 0.02f;// to overcome some mysterious position inaccuracy between platform and character

    public Collider2D Collider;
    public Transform PivotPosition;
    public string SortingLayerName; // [TODO] maybe change to dropdown menu will be a better choice

    private List<GameObject> detectTargets = new List<GameObject>();
    private List<GameObject> gameObjectsInRange = new List<GameObject>();



    public void EnableDetector(List<GameObject> targets)
    {
        detectTargets = targets;
        Collider.enabled = true;
    }
    public void CloseDetector()
    {
        detectTargets.Clear();
        Collider.enabled = false;
        foreach (GameObject heightChangeObject in gameObjectsInRange)
        {
            heightChangeObject.GetComponent<SpriteManager>()?.OnHeightChangeEnd(gameObject);
        }
        gameObjectsInRange.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision) // if the player pass certain height
    {
        if (collision.GetComponent<SpriteManager>() == null) return;
        if (!detectTargets.Exists(x => x == collision.gameObject)) return; // since the "==" operator for GameObject will check for InstanceID, so it should be safe to use.

        Debug.Log(collision.gameObject.name + " enter the detector: "+ gameObject.name);
        gameObjectsInRange.Add(collision.gameObject);

        int priorty = collision.GetComponent<SpriteManager>().SortingPriority;
        float offset = priorty * yDelta;
        collision.GetComponent<SpriteManager>()?.OnHeightChangeStart(PivotPosition.position.y - offset, SortingLayerName, gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SpriteManager>() == null) return;
        if (!detectTargets.Exists(x => x == collision.gameObject)) return;

        Debug.Log(collision.gameObject.name + " leave the detector: " + gameObject.name);
        gameObjectsInRange.Remove(collision.gameObject);

        collision.GetComponent<SpriteManager>().OnHeightChangeEnd(gameObject);
    }
}
