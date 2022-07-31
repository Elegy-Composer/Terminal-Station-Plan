using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChangeTest : MonoBehaviour
{
    // Start is called before the first frame update
    public TileManager tileManager;
    public Tile tile;
    public GameObject prefab;
    void Start()
    {
        Vector3Int[] positions = new Vector3Int[12];
        for (int i = 0; i < 6; i++)
        {
            positions[i] = new Vector3Int(-i, -i);
            positions[i + 6] = new Vector3Int(-i, i - 5);
        }
        tileManager.SetTile(positions, tile);

        tileManager.SetTile(new Vector3Int(7, -10, 0), tile, prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
