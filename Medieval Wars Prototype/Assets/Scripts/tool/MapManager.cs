using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;

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

    // Array of lists to hold terrain sprites
    public List<TileBase>[] listOfTerrainSpritesLists = new List<TileBase>[14];

    public MapGrid mapGrid;

    void Start()
    {

        // mapGrid.CalculateMapGridSize();
        // LoadMapData();
        // SliceJsonToLists("map_data.json");
    }

    // !!!!!
    // Define a class to represent a cell on the map
    [System.Serializable]
    public class CellData
    {
        public int row;
        public int column;
        public Vector3Int vector3Int;
        public string groundType;
        public string terrainType;
        public int groundTypeIndex;
        public int terrainTypeIndex;

        public CellData(Vector3Int vector3Int)
        {
            // new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f, -1)
            // column = vector3Int.x + MapManager.instance.CameraWidhtSize / 2;  //  - 0.5f
            // row = vector3Int.y + MapManager.instance.CameraHeightSize / 2;

            this.vector3Int = vector3Int;
            column = vector3Int.x + 9;

            row = -vector3Int.y + 4;

            // Debug.Log("matrice : " + row + " ; " + column);
        }
    }


    // !!!!!
    // Define a class to represent the map
    [System.Serializable]
    public class MapData
    {
        public CellData[,] cells;
        // public List<CellData> cells;
        public MapData(int CameraHeightSize, int CameraWidhtSize)
        {
            cells = new CellData[CameraHeightSize, CameraWidhtSize];
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

    public int MapHeightSize;
    public int MapWidthSize;
    public Tilemap groundsTileMap;
    public Tilemap terrainsAndBuilingsTileMap;


    // Function to save map data to a JSON file
    public void SaveMapData()
    {

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

        MapData mapData = new MapData(MapHeightSize, MapWidthSize);

        //!!!!!!!!!!  groundsTileMap
        foreach (Vector3Int cellPosition in groundsTileMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = groundsTileMap.GetTile(cellPosition);
            if (tile != null)
            {
                // Debug.Log(" position coordinates : " + cellPosition.x + " ; " + cellPosition.y);

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

                int columnTemp = cellPosition.x + 9;
                int rowTemp = -cellPosition.y + 4;

                mapData.cells[rowTemp, columnTemp].terrainType = tile.name; // hadi hya cell , doka nzidlha les info t3 terrain&building
                mapData.cells[rowTemp, columnTemp].terrainTypeIndex = terrainIndexOfTile(tile);
            }
        }

        string json = ConvertMatrixToJson(mapData.cells);
        File.WriteAllText("map_data.json", json);
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


    public List<string> SliceJsonToLists(string jsonString)
    {
        //!!! kayen PARSE w9il n9der ndir biha direct .

        // Trim the whitespace and remove leading/trailing square brackets
        jsonString = jsonString.Trim();
        jsonString = jsonString.Substring(1, jsonString.Length - 2);

        // Split the JSON string by '],[' to separate the lists
        string[] mainLists = jsonString.Split(new string[] { "],[" }, StringSplitOptions.None);

        // Convert each substring into a list of strings
        List<string> resultList = new List<string>();

        foreach (string list in mainLists)
        {
            // Split each list by ',' to separate the items
            string[] items = list.Split(new string[] { "," }, StringSplitOptions.None);

            // Add each item to the formatted list
            foreach (string item in items)
            {
                // Remove leading/trailing whitespace and add to the formatted list
                resultList.Add(item.Trim());
            }
        }

        // foreach (string item in resultList)
        // {
        //     Debug.Log(item);
        // }
        return resultList;
    }


    public void LoadMapData()
    {
        string jsonFilePath = "map_data.json"; // Path to the JSON file containing map data

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
                // Debug.Log("Row: " + cell.row + ", Column: " + cell.column + ", Ground Type: " + cell.groundType + ", Terrain Type: " + cell.terrainType);

                // !!!! GROUND TILES.
                // Get the tile base for the ground type
                TileBase tileBaseGround = groundsTileMap.GetTile(cell.vector3Int);

                if (tileBaseGround == null)
                { Debug.Log("The tile base for the ground type does not exist ground: " + cell.groundType); }
                else
                {
                    GameObject groundGameObject = new GameObject();
                    groundGameObject.transform.position = new Vector3(-MapGrid.Horizontal + cell.column + 0.5f, MapGrid.Vertical - cell.row - 0.5f, 0);
                    // groundGameObject.transform.SetParent(groundGameObjectsHolder.transform);

                    // Get the sprite of the tile
                    Sprite tileSprite = ((Tile)tileBaseGround).sprite;

                    // Set the SpriteRenderer's sprite to the sprite of the tile
                    groundGameObject.AddComponent<SpriteRenderer>();
                    groundGameObject.GetComponent<SpriteRenderer>().sprite = tileSprite;

                    groundGameObject.name = $"groundGameObject ({cell.row}, {cell.column})";

                }



                //!!! TERRAIN TILES.
                // Get the tile base for the ground type
                TileBase tileBaseTerrain = terrainsAndBuilingsTileMap.GetTile(cell.vector3Int);
                if (tileBaseTerrain == null)
                {
                    Debug.Log("The tile base for the ground type does not exist terrain: " + cell.terrainType);
                }
                else
                {

                    GameObject gridCell = new GameObject();
                    gridCell.transform.position = new Vector3(-MapGrid.Horizontal + cell.column + 0.5f, MapGrid.Vertical - cell.row - 0.5f, 0);
                    // gridCell.transform.SetParent(GridCellsHolder.transform);

                    // Get the sprite of the tile
                    Sprite tileSprite = ((Tile)tileBaseTerrain).sprite;

                    // Set the SpriteRenderer's sprite to the sprite of the tile
                    gridCell.AddComponent<SpriteRenderer>();
                    gridCell.GetComponent<SpriteRenderer>().sprite = tileSprite;
                    gridCell.name = $"gridCell ({cell.row}, {cell.column})";
                }
            }
        }

    }



    public void InitialiseMapGridCells()
    {



        // Loop through each row and column of the map grid
        for (int row = 0; row < 50; row++) /*row < MapGrid.Rows*/
        {
            for (int col = 0; col < 50; col++) /*col < MapGrid.Columns*/
            {



                // Instantiate a GridCell prefab at the specified position
                // GridCell gridCell = Instantiate(GridCellPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f, 0), Quaternion.identity);
                // gridCell.transform.SetParent(GridCellsHolder.transform);




                // if (row == 3 && col == 8)
                // {
                // Building building = Instantiate(CastleBuildingPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);
                // TerainSpriteRenderer = CastleBuilding.GetComponent<SpriteRenderer>(); // had la ligne brk bch n7et  Castle f posiiton hadik
                //     building.row = row;
                //     building.col = col;
                //     gridCell.occupantTerrain = building;
                // }
                // else
                // {

                // // Instantiate a Terrain prefab at the specified position
                // Terrain terrain = Instantiate(TerrainPlainPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);
                // terrain.row = row;
                // terrain.col = col;

                // set the terrain for the gridcell
                // gridCell.occupantTerrain = terrain;
                // }

                // gridCell.occupantTerrain.transform.SetParent(TerrainsHolder.transform);

                // ! hadi ttbdel 3la 7ssab kifach tessar generation t3 lmaps , en tous les cas , normal yeb9a hada howa lprincipe ta3ha , t'ajouti component terrain l cell 3la 7ssab wchmen terrain kayen (hadi tji mel maps)
                // ! whna yji probleme t3 lazem t5bi les terrains fkch plassa . (3zma f kach liste fl game controller f script map generator .)  
                // get the sprite renderer of the terrain

                // TerainSpriteRenderer = gridCell.occupantTerrain.GetComponent<SpriteRenderer>();
                // gridCell.GetComponent<SpriteRenderer>().sprite = TerainSpriteRenderer.sprite;



                // // Adjust the sprite size of the instantiated GridCell and the terrain
                // gridCell.gameObject.AdjustSpriteSize();
                // gridCell.occupantTerrain.gameObject.AdjustSpriteSize();

                // // Set the name of the GridCell
                // gridCell.name = $"GridCell ({row}, {col})";

                // // Set the row and column properties of the GridCell
                // gridCell.row = row;
                // gridCell.column = col;

                // //!!!!!!!!!!!!!!!! this affectation is temporary
                // // gridCell.terrain = Instantiate(TerrainGrassPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);

                // // Assign the GridCell to the corresponding position in the map grid
                // grid[row, col] = gridCell;
            }
        }


    }





}