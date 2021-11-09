using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelController : MonoBehaviour
{
    [Header("Walls")] 
    [SerializeField] int levelWidth;
    [SerializeField] int levelHeight;
    [SerializeField] Tilemap wallsTilemap;
    [SerializeField] Tile wallTile;
    
    [Header("Buggy tilemaps")]
    [SerializeField] TilemapRenderer invisibleTilemap;
    [SerializeField] Tilemap noCollideTilemap;
    [SerializeField] Tilemap damageTilemap;
    
    void Start()
    {
        HideInvisibleBlocks();
        NormalizeColor(noCollideTilemap);
        NormalizeColor(damageTilemap);
    }

    public void GenerateWalls()
    {
        int w2 = levelWidth / 2;
        int h2 = levelHeight / 2;
        
        for (int x=-w2; x < w2; x++)
        {
            DrawWall(x, h2);
            DrawWall(x, -h2);
        }
        
        for (int y=-h2; y < h2; y++)
        {
            DrawWall(w2, y);
            DrawWall(-w2, y);
        }   
    }

    public void ClearWalls()
    {
        wallsTilemap.ClearAllTiles();
    }

    void DrawWall(int x, int y)
    {
        wallsTilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
    }

    void HideInvisibleBlocks()
    {
        invisibleTilemap.GetComponent<TilemapRenderer>().enabled = false;
    }

    void NormalizeColor(Tilemap tilemap)
    {
        tilemap.color = Color.white;
    }
}
