using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;
using System.Text.RegularExpressions;



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



    // TerrainIndex | Terrain
    // ------------------
    //   0   | BARRACK  | 
    //   1   | DOCK     | 
    //   2   | STABLE   | 
    //   3   | CASTLE   | 
    //   4   | VILLAGE  | 
    //   5   | ROAD     | 
    //   6   | BRIDGE   | 
    //   7   | RIVER    | 
    //   8   | SEA      | 
    //   9   | SHOAL    | 
    //  10   | REEF     | 
    //  11   | PLAIN    | 
    //  12   | WOOD     | 
    //  13   | MOUNTAIN | 
    //
    public List<TileBase> BarrackTileSprites = new List<TileBase>();
    public List<TileBase> DockTileSprites = new List<TileBase>();
    public List<TileBase> StableTileSprites = new List<TileBase>();
    public List<TileBase> CastleTileSprites = new List<TileBase>();
    public List<TileBase> VillageTileSprites = new List<TileBase>();
    public List<TileBase> RoadTileSprites = new List<TileBase>();
    public List<TileBase> BridgeTileSprites = new List<TileBase>();
    public List<TileBase> RiverTileSprites = new List<TileBase>();
    public List<TileBase> SeaTileSprites = new List<TileBase>();
    public List<TileBase> ShoalTileSprites = new List<TileBase>();
    public List<TileBase> ReefTileSprites = new List<TileBase>();
    public List<TileBase> PlainTileSprites = new List<TileBase>();
    public List<TileBase> WoodTileSprites = new List<TileBase>();
    public List<TileBase> MountainTileSprites = new List<TileBase>();
    public List<TileBase> AccessoriesSprites = new List<TileBase>();


    // Array of lists to hold terrain sprites
    public List<TileBase>[] listOfTerrainSpritesLists;

    public GridCell GridCellPrefab;
    public Terrain[] terrainPrefabs = new Terrain[14];
    public Accessory AccessoriesPrefabs;




    void Awake()
    {
        InitializeListOfTerrainSpritesLists();
        LoadMapData(ScenesManager.mapToLoad);
    }

    // !!!!!
    // Define a class to represent a cell on the map
    [System.Serializable]
    public class CellData
    {
        public int row;
        public int column;
        public string groundType;
        public string terrainType;
        public string accessoryType;
        public int groundTypeIndex;
        public int terrainTypeIndex;
        public int accessoryTypeIndex;

        public CellData(Vector3Int vector3Int)
        {
            column = vector3Int.x + 16;
            row = -vector3Int.y + 8;
        }
    }


    // !!!!!
    // Define a class to represent the map
    [System.Serializable]
    public class MapData
    {
        public CellData[,] cells;
        // public List<CellData> cells;
        public MapData(int numberOfRowsInTheMap, int numberOfColumnsInTheMap)
        {
            cells = new CellData[numberOfRowsInTheMap, numberOfColumnsInTheMap];
        }
    }

    [System.Serializable]
    public class MapDataForReadOnly
    {
        // public CellData[,] cells;
        public List<CellData> cells;

    }
    // !!!!!

    // private int CameraHeightSize = 10;

    // private int CameraWidhtSize = 18;

    public int numberOfRowsInTheMap;
    public int numberOfColumnsInTheMap;
    public Tilemap groundsTileMap;
    public Tilemap terrainsAndBuilingsTileMap;
    public Tilemap accessoriesTileMap;

    public GameObject GridCellsHolder;
    public GameObject TerrainsHolder;
    public GameObject AccessoriesHolder;


    // Function to save map data to a JSON file
    public void SaveMapData()
    {
        InitializeListOfTerrainSpritesLists();

        MapData mapData = new MapData(numberOfRowsInTheMap, numberOfColumnsInTheMap);

        //!!!!!!!!!!  groundsTileMap
        foreach (Vector3Int cellPosition in groundsTileMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = groundsTileMap.GetTile(cellPosition);
            if (tile != null)
            {

                CellData cell = new CellData(cellPosition)
                {
                    groundType = tile.name, // You may want to customize this based on your tile setup
                    groundTypeIndex = terrainIndexOfTile(tile)
                };

                mapData.cells[cell.row, cell.column] = cell;
            }
        }

        //!!!!!!!!!!  terrainsAndBuilingsTileMap
        foreach (Vector3Int cellPosition in terrainsAndBuilingsTileMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = terrainsAndBuilingsTileMap.GetTile(cellPosition);
            if (tile != null)
            {
                // int columnTemp = cellPosition.x + MapManager.instance.CameraWidhtSize / 2;  //  - 0.5f
                // int rowTemp = cellPosition.y + MapManager.instance.CameraHeightSize / 2;

                int columnTemp = cellPosition.x + 16;
                int rowTemp = -cellPosition.y + 8;

                mapData.cells[rowTemp, columnTemp].terrainType = tile.name; // hadi hya cell , doka nzidlha les info t3 terrain&building
                mapData.cells[rowTemp, columnTemp].terrainTypeIndex = terrainIndexOfTile(tile);
            }
        }

        //!!!!!!!!!!  Accessories TielMap
        foreach (Vector3Int cellPosition in accessoriesTileMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = accessoriesTileMap.GetTile(cellPosition);
            if (tile != null)
            {
                // int columnTemp = cellPosition.x + MapManager.instance.CameraWidhtSize / 2;  //  - 0.5f
                // int rowTemp = cellPosition.y + MapManager.instance.CameraHeightSize / 2;

                int columnTemp = cellPosition.x + 16;
                int rowTemp = -cellPosition.y + 8;
                Debug.Log(columnTemp + " " + rowTemp + " " + tile.name + " " + terrainIndexOfTile(tile));
                // Debug.Log()
                mapData.cells[rowTemp, columnTemp].accessoryType = tile.name;
                mapData.cells[rowTemp, columnTemp].accessoryTypeIndex = terrainIndexOfTile(tile);
            }
        }

        string json = ConvertMatrixToJson(mapData.cells);
        File.WriteAllText("map_data.json", json);

        TransformJsonFromMatrixToListFormat("map_data.json");
    }


    public int terrainIndexOfTile(TileBase tileBase)
    {
        for (int i = 0; i < listOfTerrainSpritesLists.Length; i++)
        {
            if (listOfTerrainSpritesLists[i].Contains(tileBase))
            {
                return i;
            }
        }
        return -1;
    }


    private string ConvertMatrixToJson(CellData[,] matrix)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter writer = new StringWriter(sb);

        writer.Write("[");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (i > 0)
            {
                writer.Write(",");
            }
            writer.Write("[");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (j > 0)
                {
                    writer.Write(",");
                }
                writer.Write(JsonUtility.ToJson(matrix[i, j]));
            }
            writer.Write("]");
        }
        writer.Write("]");

        string json = sb.ToString();
        return json;
    }




    public void LoadMapData(int mapToLoad)
    {
        MapGrid.Instance.CreateMapGridCellsMatrix();

        listOfTerrainSpritesLists[0] = BarrackTileSprites;
        listOfTerrainSpritesLists[1] = DockTileSprites;
        listOfTerrainSpritesLists[2] = StableTileSprites;
        listOfTerrainSpritesLists[3] = CastleTileSprites;
        listOfTerrainSpritesLists[4] = VillageTileSprites;
        listOfTerrainSpritesLists[5] = RoadTileSprites;
        listOfTerrainSpritesLists[6] = BridgeTileSprites;
        listOfTerrainSpritesLists[7] = RiverTileSprites;
        listOfTerrainSpritesLists[8] = SeaTileSprites;
        listOfTerrainSpritesLists[9] = ShoalTileSprites;
        listOfTerrainSpritesLists[10] = ReefTileSprites;
        listOfTerrainSpritesLists[11] = PlainTileSprites;
        listOfTerrainSpritesLists[12] = WoodTileSprites;
        listOfTerrainSpritesLists[13] = MountainTileSprites;
        listOfTerrainSpritesLists[14] = AccessoriesSprites;

        string jsonFilePath = Path.Combine(Application.streamingAssetsPath , "map_data1.json"); // Path to the JSON file containing map data

        switch (mapToLoad)
        {
            case 1:
                jsonFilePath = Path.Combine(Application.streamingAssetsPath ,"map_data1.json");
                break;
            case 2:
                jsonFilePath = Path.Combine(Application.streamingAssetsPath ,"map_data2.json");
                break;
            case 3:
                jsonFilePath = Path.Combine(Application.streamingAssetsPath ,"map_data3.json");
                break;
            case 4:
                jsonFilePath = Path.Combine(Application.streamingAssetsPath ,"map_data4.json");
                break;
            default:
                Debug.Log("map not found");
                break;
        }


        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("The map JSON file does not exist: " + jsonFilePath);
            return;
        }
        else
        {
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON string into a MapData object
            MapDataForReadOnly mapData = JsonUtility.FromJson<MapDataForReadOnly>(json);

            // Access the map data
            List<CellData> cells = mapData.cells;

            foreach (CellData cell in cells)
            {

                // !!!! GROUND TILES.
                // Get the tile for the ground type

                TileBase tileBaseGround = listOfTerrainSpritesLists[cell.groundTypeIndex].FirstOrDefault(tile => tile.name == cell.groundType);



                // error handler ( if u missed a ground tile )
                if (tileBaseGround == null) { Debug.Log("The tile base for the ground type does not exist ground: " + cell.groundType); return; }

                GridCell gridCell = Instantiate(GridCellPrefab, new Vector3(-16 + cell.column + 0.5f, 9 - cell.row - 0.5f, 0), Quaternion.identity);
                gridCell.transform.SetParent(GridCellsHolder.transform);

                // Get the sprite of the tile
                Sprite tileSprite = ((Tile)tileBaseGround).sprite;

                // Set the SpriteRenderer's sprite to the sprite of the tile
                gridCell.gridCellView.rend.sprite = tileSprite;

                gridCell.name = $"gridcell ({cell.row}, {cell.column})";

                gridCell.row = cell.row;
                gridCell.column = cell.column;




                //!!! TERRAIN TILES.
                // Get the tile base for the ground type

                TileBase tileBaseTerrain = listOfTerrainSpritesLists[cell.terrainTypeIndex].FirstOrDefault(tile => tile.name == cell.terrainType);

                // error handler ( if u missed a terrain tile )
                if (tileBaseTerrain == null) { Debug.Log("The tile base for the ground type does not exist terrain: " + cell.terrainType); return; }

                Debug.Log("hada houwa l print li rani n7ws 3lih " + cell.terrainTypeIndex);
                Debug.Log(terrainPrefabs[cell.terrainTypeIndex]);


                Terrain terrain = Instantiate(terrainPrefabs[cell.terrainTypeIndex], new Vector3(-16 + cell.column + 0.5f, 9 - cell.row - 0.5f, -0.5f), Quaternion.identity);

                terrain.transform.SetParent(TerrainsHolder.transform);

                // Get the sprite of the tile
                tileSprite = ((Tile)tileBaseTerrain).sprite;

                // Set the SpriteRenderer's sprite to the sprite of the tile
                terrain.spriteRenderer.sprite = tileSprite;

                terrain.name = $"terrain ({cell.row}, {cell.column})";

                terrain.row = cell.row;
                terrain.col = cell.column;


                gridCell.occupantTerrain = terrain;


                MapGrid.Instance.grid[cell.row, cell.column] = gridCell;





                // !!!! ACCESSOROY TILES.
                // Get the tile for the ground type
                if (cell.accessoryTypeIndex == 14)
                {
                    TileBase tileBaseAccessory = listOfTerrainSpritesLists[cell.accessoryTypeIndex].FirstOrDefault(tile => tile.name == cell.accessoryType);


                    // error handler ( if u missed a ground tile )
                    if (tileBaseAccessory == null) { Debug.Log("The tile base for the ground type does not exist ground: " + cell.accessoryType); return; }

                    Accessory accessory = Instantiate(AccessoriesPrefabs, new Vector3(-16 + cell.column + 0.5f, 9 - cell.row - 0.5f, -0.75f), Quaternion.identity, AccessoriesHolder.transform);

                    // Get the sprite of the tile
                    tileSprite = ((Tile)tileBaseAccessory).sprite;

                    // Set the SpriteRenderer's sprite to the sprite of the tile
                    accessory.spriteRenderer.sprite = tileSprite;

                    accessory.name = $"accesspry ({cell.row}, {cell.column})";
                }
            }
        }

    }

    public void TransformJsonFromMatrixToListFormat(string jsonFilePath)
    {
        string json = ReadJsonFile(jsonFilePath);
        json = ModifyJsonString(json);
        File.WriteAllText(jsonFilePath, json);
    }
    public string ReadJsonFile(string filePath)
    {
        string jsonString = "";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all text from the JSON file
            jsonString = File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogError("The JSON file does not exist: " + filePath);
        }

        return jsonString;
    }
    public string ModifyJsonString(string jsonString)
    {
        // Remove all whitespace characters (including spaces, tabs, and newlines)
        jsonString = Regex.Replace(jsonString, @"\s+", "");

        // Replace "],[" with ","
        jsonString = jsonString.Replace("],[", ",");

        // Replace first "[" with "{"
        int firstBracketIndex = jsonString.IndexOf("[");
        if (firstBracketIndex >= 0)
        {
            jsonString = "{" + jsonString.Substring(firstBracketIndex + 1);
        }

        // Replace last "]" with "}"
        int lastBracketIndex = jsonString.LastIndexOf("]");
        if (lastBracketIndex >= 0)
        {
            jsonString = jsonString.Substring(0, lastBracketIndex) + "}";
        }

        // Add "cells" after the first "{"
        int firstCurlyBraceIndex = jsonString.IndexOf("{");
        if (firstCurlyBraceIndex >= 0)
        {
            jsonString = jsonString.Insert(firstCurlyBraceIndex + 1, "\"cells\":");
        }

        return jsonString;
    }

    public void InitializeListOfTerrainSpritesLists()
    {
        listOfTerrainSpritesLists = new List<TileBase>[15] {
        BarrackTileSprites,
        DockTileSprites,
        StableTileSprites,
        CastleTileSprites,
        VillageTileSprites,
        RoadTileSprites,
        BridgeTileSprites,
        RiverTileSprites,
        SeaTileSprites,
        ShoalTileSprites,
        ReefTileSprites,
        PlainTileSprites,
        WoodTileSprites,
        MountainTileSprites,
        AccessoriesSprites
        };
    }

}