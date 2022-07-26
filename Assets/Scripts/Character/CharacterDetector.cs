using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterDetector : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject otherCharacter;

    void Start()
    {
        GetComponent<GridMovement>().BeforeMoveEvent += BeforeMove;
    }

    private void BeforeMove(Action abortMovement, ref Vector2 direction)
    {
        Vector3 destination = gameObject.transform.position + direction.ExtendToVector3();
        Vector3Int destCell = tilemap.WorldToCell(destination);
        Vector3Int otherCharacterCell = otherCharacter.GetComponent<PointFollower>().lastTargetToCell;

        if (destCell == otherCharacterCell)
        {
            abortMovement();
        }
    }
}
