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

    [Header("Tilemaps")] 
    [SerializeField] Tilemap commonTilemap;
    [SerializeField] Tilemap invisibleTilemap;
    [SerializeField] Tilemap noCollideTilemap;
    [SerializeField] Tilemap damageTilemap;

    [SerializeField] GameObject environment;

    [Header("Other side")] 
    [SerializeField] GameObject debugTilemapPrefab;
    [SerializeField] Tile debugBaseTile;

    int HalfWidth => levelWidth / 2;
    int HalfHeight => levelHeight / 2;
    
    void OnEnable()
    {
        GameManager.EnableDebugModeEvent += EnableDebug;
        GameManager.DisableDebugModeEvent += DisableDebug;
    }
    
    void OnDisable()
    {
        GameManager.EnableDebugModeEvent -= EnableDebug;
        GameManager.DisableDebugModeEvent -= DisableDebug;
    }

    void Start()
    {
        HideInvisibleBlocks();
        NormalizeColor(noCollideTilemap);
        NormalizeColor(damageTilemap);
    }

    public void GenerateWalls()
    {
        for (int x=-HalfWidth-1; x <= HalfWidth; x++)
        {
            DrawWall(x, HalfHeight);
            DrawWall(x, -HalfHeight-1);
        }
        
        for (int y=-HalfHeight-1; y <= HalfHeight; y++)
        {
            DrawWall(HalfWidth, y);
            DrawWall(-HalfWidth-1, y);
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
        SetTilemapRender(invisibleTilemap, false);
    }

    void NormalizeColor(Tilemap tilemap)
    {
        tilemap.color = Color.white;
    }

    void SetTilemapRender(Tilemap tilemap, bool enable)
    {
        tilemap.GetComponent<TilemapRenderer>().enabled = enable;
    }

    void EnableDebug()
    {
        CreateDebugTilemap(new List<Tilemap> { commonTilemap, invisibleTilemap, wallsTilemap }, Color.green);
        CreateDebugTilemap(new List<Tilemap> { damageTilemap }, Color.red);
        SetTilemapRender(noCollideTilemap, false);
        environment.SetActive(false);
    }

    void DisableDebug()
    {
        HideDebugTiles();
        SetTilemapRender(noCollideTilemap, true);
        environment.SetActive(true);
    }

    public void CreateDebugTilemap(List<Tilemap> tilemaps, Color color)
    {
        GameObject obj = Instantiate(debugTilemapPrefab, Vector2.zero, Quaternion.identity);
        obj.transform.SetParent(transform);
        obj.GetComponent<TilemapRenderer>().material.SetColor("_Color", color);
        obj.tag = "Debug";
        var debugTilemap = obj.GetComponent<Tilemap>();
        
        foreach (Tilemap tilemap in tilemaps)
        {
            for (int x=-HalfWidth; x <= HalfWidth; x++)
            {
                for (int y = -HalfHeight; y <= HalfHeight; y++)
                {
                    if (tilemap.GetTile(new Vector3Int(x, y)) != null)
                    {
                        debugTilemap.SetTile(new Vector3Int(x, y), debugBaseTile);
                    }
                }
            }
        }
    }

    void HideDebugTiles()
    {
        foreach (var tilemap in GameObject.FindGameObjectsWithTag("Debug"))
        {
            Destroy(tilemap.gameObject);
        }
    }
}
