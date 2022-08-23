using UnityEngine;
using System.Collections.Generic;

public class ActivatePlatform : MonoBehaviour
{
    // TODO: remove old variable which only handle single move platform
    public GameObject[] movePlatformObjectList;
    private List<MovePlatform> platforms = new List<MovePlatform>();

    private void Start()
    {
        foreach (var platformObject in movePlatformObjectList)
        {
            platforms.Add(platformObject.transform.Find("Platform").gameObject.GetComponent<MovePlatform>());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter ActivatePlatform: " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<GridMovement>() != null)
        {
            platforms.ForEach((p) => p.ActivatePlatform());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Leave ActivatePlatform: " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<GridMovement>() != null)
        {
            platforms.ForEach((p) => p.DeactivatePlatform());
        }
    }
}
