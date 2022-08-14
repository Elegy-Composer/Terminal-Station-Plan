using UnityEngine;
using UnityEngine.Tilemaps;

public class ActivatePlatform : MonoBehaviour
{
    public GameObject movePlatformObject;
    private MovePlatform platform;

    private void Start()
    {
        platform = movePlatformObject.transform.Find("Platform").gameObject.GetComponent<MovePlatform>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter ActivatePlatform: " + collision.gameObject.name);
        platform.ActivatePlatform();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Leave ActivatePlatform: " + collision.gameObject.name);
        platform.DeactivatePlatform();
    }
}
