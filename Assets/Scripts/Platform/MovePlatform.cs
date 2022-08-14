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
    private Vector3 originWorld, targetWorld, movePosition;
    private bool moving = false;
    private int activationCounter = 0;

    public float VerticalOffset => 0.0555f;
    public float HorizontalOffset => 0f;
    private bool Raised
    {
        get
        {
            return !moving && (movePosition == targetWorld);
        }
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (!moving)
        {
            character = collision.gameObject;
            if (!Raised)
            {
                //character.GetComponent<GridMovement>().enabled = false;
                //character.transform.position += ;
                character.GetComponent<GridMovement>().UpdateTargetBy(new Vector3(HorizontalOffset, VerticalOffset));
                //character.GetComponent<GridMovement>().enabled = true;
                Debug.Log("lift character up");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!moving)
        {
            if (!Raised)
            {
                //character.GetComponent<GridMovement>().enabled = false;
                //character.transform.position -= new Vector3(HorizontalOffset, VerticalOffset);
                character.GetComponent<GridMovement>().UpdateTargetBy(new Vector3(-HorizontalOffset, -VerticalOffset));
                //character.GetComponent<GridMovement>().enabled = true;
                Debug.Log("put character down");
            }
            character = null;
        }
        Debug.Log("leave");
    }

    /*IEnumerator MoveToTarget()
    {
        //yield return new WaitForSeconds(5f);
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
    }*/

    IEnumerator TestMoving()
    {
        yield return new WaitForSeconds(5f);
        ActivatePlatform();
        yield return new WaitForSeconds(5f);
        DeactivatePlatform();
    }

    void Start()
    {
        originWorld = gameObject.transform.parent.Find("Origin").gameObject.transform.position;
        targetWorld = gameObject.transform.parent.Find("Target").gameObject.transform.position;
        movePosition = originWorld;
        //StartCoroutine(TestMoving());
    }

    internal void ActivatePlatform()
    {
        activationCounter++;
        movePosition = targetWorld;
    }

    internal void DeactivatePlatform()
    {
        activationCounter--;
        if (activationCounter == 0) movePosition = originWorld;
    }

    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, movePosition) == 0f)
        {
            if (moving)
            {
                moving = false;
                if (character != null)
                {
                    //character.GetComponent<GridMovement>().UpdateTarget(character.transform.position);
                    character.GetComponent<GridMovement>().enabled = true;
                }
                //Debug.Log("MOVING STOP!!");
            }

        }
        else
        {
            moving = true;
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, movePosition, .01f) - gameObject.transform.position;
            gameObject.transform.position += movement;
            if (character != null)
            {
                character.GetComponent<GridMovement>().enabled = false;
                character.transform.position += movement;
                character.GetComponent<GridMovement>().UpdateTargetBy(movement);
            }
        }
    }

}
