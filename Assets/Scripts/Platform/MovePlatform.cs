using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePlatform : MonoBehaviour, IMapOffset
{
    public float xUnit = 1f;
    public float yUnit = 0.3402062f;
    public Tilemap tilemap;
    private GameObject character;
    private Vector3 originWorld, targetWorld;
    private bool moving = false;

    public float VerticalOffset => 0.0555f;
    public float HorizontalOffset => 0f;

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!moving)
        {
            character = collision.gameObject;
            Debug.Log("enter");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!moving)
        {
            character = null;
            Debug.Log("leave");
        }
    }

    IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(5f);
        moving = true;
        while (Vector3.Distance(gameObject.transform.position, targetWorld) != 0f)
        {
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, targetWorld, .01f) - gameObject.transform.position;
            gameObject.transform.position += movement;
            if (character != null)
            {
                character.GetComponent<GridMovement>().enabled = false;
                character.transform.position += movement;
            }
            yield return null;
        }
        if(character != null)
        {
            character.GetComponent<GridMovement>().UpdateTarget();
            character.GetComponent<GridMovement>().enabled = true;
        }
        moving = false;
    }

    IEnumerator MoveToOrigin()
    {
        moving = true;
        while (Vector3.Distance(gameObject.transform.position, originWorld) != 0f)
        {
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, originWorld, .01f) - gameObject.transform.position;
            gameObject.transform.position += movement;
            if (character != null)
            {
                character.GetComponent<GridMovement>().enabled = false;
                character.transform.position += movement;
            }
            yield return null;
        }
        if (character != null)
        {
            character.GetComponent<GridMovement>().UpdateTarget();
            character.GetComponent<GridMovement>().enabled = true;
        }
        moving = false;
    }

    void Start()
    {
        originWorld = gameObject.transform.parent.Find("Origin").gameObject.transform.position;
        targetWorld = gameObject.transform.parent.Find("Target").gameObject.transform.position;
        StartCoroutine(MoveToTarget());
    }

}
