using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HeightChangeDetector : MonoBehaviour // [TODO] adjust the collider shape to make two detector near each other
{
    private const float yDelta = 0.02f;// to overcome some mysterious position inaccuracy between platform and character

    public Collider2D Collider;
    public Transform PivotPosition;
    public string SortingLayerName; // [TODO] maybe change to dropdown menu will be a better choice
    private List<GameObject> detectTargets = new List<GameObject>();

    public void EnableDetector(List<GameObject> targets)
    {
        detectTargets = targets;
        Collider.enabled = true;
    }
    public void CloseDetector()
    {
        detectTargets.Clear();
        Collider.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision) // if the player pass certain height
    {
        //Debug.Log(collision.gameObject.name + " enter the detector, y = " + PivotPosition.position.y.ToString());
        if (collision.GetComponent<SpriteManager>() == null) return;
        if (!detectTargets.Exists(x => x == collision.gameObject)) return; // since the "==" operator for GameObject will check for InstanceID, so it should be safe to use.
        //if (!detectTargets.Contains(collision.gameObject)) return;
        int priorty = collision.GetComponent<SpriteManager>().SortingPriority;
        float offset = priorty * yDelta;
        collision.GetComponent<SpriteManager>()?.OnHeightChangeStart(PivotPosition.position.y-offset, SortingLayerName);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<SpriteManager>()?.OnHeightChangeEnd();
        if (!detectTargets.Exists(x => x == collision.gameObject)) return;
        //if (!detectTargets.Contains(collision.gameObject)) return;
        Debug.Log(collision.gameObject.name + " leave the detector");
    }
}
