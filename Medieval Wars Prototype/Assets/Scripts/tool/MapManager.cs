using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.IO;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<MapManager>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("MapManager");
                    instance = obj.AddComponent<MapManager>();
                }
            }
            return instance;
        }
    }



    // !!!!!
    // Define a class to represent a cell on the map
    [System.Serializable]
    public class CellData
    {
        public Vector3Int position;
        public string terrainType;
    }
    
    
    // !!!!!
    // Define a class to represent the map
    [System.Serializable]
    public class MapData
    {
        public List<CellData> cells = new List<CellData>();
    }
    // !!!!!
    
    
    
    public int MapHightSize;
    public int MapWidthSize;
    public Tilemap groundsTileMap;
    public Tilemap terrainsTileMap;
    public Tilemap buildingsTileMap;
    public Tilemap unitsTileMap;


    // Function to save map data to a JSON file
    public void SaveMapData()
    {
        MapData mapData = new MapData();

        foreach (Vector3Int cellPosition in groundsTileMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = groundsTileMap.GetTile(cellPosition);
            if (tile != null)
            {
                CellData cell = new CellData();
                cell.position = cellPosition;
                cell.terrainType = tile.name; // You may want to customize this based on your tile setup
                mapData.cells.Add(cell);
            }
        }

        string json = JsonUtility.ToJson(mapData);
        File.WriteAllText("map_data.json", json);
    }
}