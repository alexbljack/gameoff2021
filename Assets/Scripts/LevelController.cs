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

    [Header("Other side")] 
    [SerializeField] GameObject debugTilemapPrefab;
    [SerializeField] Tile debugBaseTile;

    int HalfWidth => levelWidth / 2;
    int HalfHeight => levelHeight / 2;

    Tilemap _debugTilemap;

    void OnEnable()
    {
        GameManager.Instance.EnableDebugMode += EnableDebug;
        GameManager.Instance.DisableDebugMode += DisableDebug;
    }
    
    void OnDisable()
    {
        GameManager.Instance.EnableDebugMode -= EnableDebug;
        GameManager.Instance.DisableDebugMode -= DisableDebug;
    }

    void Start()
    {
        HideInvisibleBlocks();
        NormalizeColor(noCollideTilemap);
        NormalizeColor(damageTilemap);
    }

    public void GenerateWalls()
    {
        for (int x=-HalfWidth; x < HalfWidth; x++)
        {
            DrawWall(x, HalfHeight);
            DrawWall(x, -HalfHeight);
        }
        
        for (int y=-HalfHeight; y < HalfHeight; y++)
        {
            DrawWall(HalfWidth, y);
            DrawWall(-HalfWidth, y);
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
        CreateDebugTilemap();
        SetTilemapRender(noCollideTilemap, false);
    }

    void DisableDebug()
    {
        HideDebugTiles();
        SetTilemapRender(noCollideTilemap, true);
    }

    public void CreateDebugTilemap()
    {
        GameObject obj = Instantiate(debugTilemapPrefab, Vector2.zero, Quaternion.identity);
        obj.transform.SetParent(transform);
        _debugTilemap = obj.GetComponent<Tilemap>();
        
        foreach (Tilemap tilemap in new List<Tilemap>
        {
            commonTilemap, invisibleTilemap, damageTilemap, wallsTilemap
        })
        {
            for (int x=-HalfWidth; x <= HalfWidth; x++)
            {
                for (int y = -HalfHeight; y <= HalfHeight; y++)
                {
                    if (tilemap.GetTile(new Vector3Int(x, y)) != null)
                    {
                        _debugTilemap.SetTile(new Vector3Int(x, y), debugBaseTile);
                    }
                }
            }
        }
    }

    void HideDebugTiles()
    {
        Destroy(_debugTilemap.gameObject);
        _debugTilemap = null;
    }
}
