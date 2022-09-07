using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class MovePlatform : MonoBehaviour
{
    public float xUnit = 1f;
    public float yUnit = 0.3402062f;
    public Tilemap tilemap;
    private GameObject character;
    private Vector3 originWorld, targetWorld, movePosition;
    //public bool moving = false;
    private int activationCounter = 0;
    private SortingGroup normalSortingGroup;
    private LightCircle lightCircle;
    private bool prevRaised = false;

    public float VerticalOffset;
    public float HorizontalOffset;
    public bool Raised
    {
        get
        {
            return !moving && (movePosition == targetWorld);
        }
    }

    public bool IsStepped
    {
        get
        {
            return character != null;
        }
    }

    private bool _moving = false;
    public bool moving
    {
        get => _moving;

        private set
        {
            if (value == _moving) return;
            _moving = value;
            if (value)
            {
                List<GameObject> detectTargets = new List<GameObject>();
                detectTargets.Add(GetComponentInChildren<SpriteManager>().gameObject);
                if (IsStepped)
                {
                    detectTargets.Add(character.GetComponentInChildren<SpriteManager>().gameObject);
                }
                gameObject.GetComponent<HeightDetectorManager>()?.EnableDetectors(detectTargets);
            }
            else
            {
                gameObject.GetComponent<HeightDetectorManager>()?.CloseDetectors();
            }
        }
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.GetComponent<GridMovement>() == null)
            return;
        if (!moving)
        {
            character = collision.gameObject;
            if (!Raised)
            {
                //character.GetComponent<GridMovement>().enabled = false;
                //character.transform.position += ;
                character.GetComponent<PointFollower>().UpdateTargetBy(new Vector3(HorizontalOffset, VerticalOffset));
                //character.GetComponent<GridMovement>().enabled = true;
                Debug.Log("lift character up");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GridMovement>() == null)
            return;
        if (!moving)
        {
            if (!Raised)
            {
                //character.GetComponent<GridMovement>().enabled = false;
                //character.transform.position -= new Vector3(HorizontalOffset, VerticalOffset);
                character.GetComponent<PointFollower>().UpdateTargetBy(new Vector3(-HorizontalOffset, -VerticalOffset));
                //character.GetComponent<GridMovement>().enabled = true;
                Debug.Log("put character down");
            }
            character = null;
        }
        Debug.Log("leave");
    }

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
        normalSortingGroup = gameObject.transform.Find("NormalPivot")?.GetComponent<SortingGroup>();
        movePosition = originWorld;
        lightCircle = transform.parent.GetComponentInChildren<LightCircle>();
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

    void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, movePosition) == 0f)
        {
            if (moving)
            {
                moving = false;
                lightCircle.EnableLightCircle();
                if (prevRaised != Raised)
                {
                    lightCircle.SwitchCollider();
                }
                prevRaised = Raised;

                if (character != null)
                {
                    //character.GetComponent<GridMovement>().UpdateTarget(character.transform.position);
                    character.GetComponent<GridMovement>().enabled = true;
                    character.GetComponent<PointFollower>().enabled = true;
                }
                //Debug.Log("MOVING STOP!!");
                if (IsStepped && Raised)
                {
                    //character.transform.Find("MovablePivot").GetComponent<SortingGroup>().sortingOrder = 0;
                    //character.transform.Find("NormalPivot").GetComponent<SortingGroup>().sortingLayerName = "Raised";
                    //character.GetComponentInChildren<SortingGroup>().sortingOrder = 0;
                }
            }
        }
        else
        {
            if (!moving) // start to move
            {
                lightCircle.DisableLightCircle();
            }
            moving = true;
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, movePosition, .01f) - gameObject.transform.position;
            gameObject.transform.position += movement;
            if (IsStepped)
            {
                character.GetComponent<PointFollower>().enabled = false;
                character.GetComponent<GridMovement>().enabled = false;
                character.transform.position += movement;
                character.GetComponent<PointFollower>().UpdateTargetBy(movement);
                //character.transform.Find("MovablePivot").GetComponent<SortingGroup>().sortingOrder = 1;
                //character.transform.Find("NormalPivot").GetComponent<SortingGroup>().sortingLayerName = "MapObject";
            }

            CheckSorting();
        }
    }

    private void CheckSorting()
    {
        if (activationCounter > 0)
        {
            if (Vector3.Distance(gameObject.transform.position, targetWorld) < 0.065f)
            {
                if (IsStepped)
                {
                    character.transform.Find("MovablePivot").GetComponent<SortingGroup>().sortingOrder = 1;
                    character.transform.Find("NormalPivot").GetComponent<SortingGroup>().sortingLayerName = "Raised";
                }
                normalSortingGroup.sortingLayerName = "Raised";
            }
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, targetWorld) > 0.065f)
            {
                if (IsStepped)
                {
                    character.transform.Find("MovablePivot").GetComponent<SortingGroup>().sortingOrder = 0;
                    character.transform.Find("NormalPivot").GetComponent<SortingGroup>().sortingLayerName = "MapObject";
                }
                normalSortingGroup.sortingLayerName = "MapObject";
            }
        }
    }
}
