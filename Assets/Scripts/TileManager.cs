using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap map;
    public void SetTile(Vector3Int[] positions, TileBase[] tiles)
    {
        map.SetTiles(positions, tiles);
    }

    public void SetTile(Vector3Int[] positions, TileBase tile)
    {
        TileBase[] tiles = new TileBase[positions.Length];
        
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = tile;
        }
        map.SetTiles(positions, tiles);
    }

    public void SetTile(Vector3Int position, TileBase tile)
    {
        map.SetTile(position, tile);
    }

    public void SetTile(Vector3Int position, TileBase tile, GameObject gameObject)
    {
        map.SetTile(position, tile);
        Instantiate(gameObject, map.CellToWorld(position), Quaternion.identity);
    }
}
