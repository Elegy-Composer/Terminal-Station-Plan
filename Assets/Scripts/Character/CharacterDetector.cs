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

    private void BeforeMove(Action abortMovement, Vector2 direction)
    {
        Vector3 distination = gameObject.transform.position + direction.ExtendToVector3();
        Vector3Int distCell = tilemap.WorldToCell(distination);
        Vector3Int otherCharacterCell = tilemap.WorldToCell(otherCharacter.transform.position);

        if (distCell == otherCharacterCell)
        {
            abortMovement();
        }
    }
}
