using UnityEngine;



public class GameMaster : MonoBehaviour
{

    private static GameMaster instance;
    public static GameMaster Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<GameMaster>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameMaster");
                    instance = obj.AddComponent<GameMaster>();
                }
            }
            return instance;
        }
    }



    public MapGrid mapGrid;

    public Unit Infantry1Prefab; // test , //: hada , n7to list fiha t3 player 1 , w list pour player 2
    public Unit Infantry2Prefab; // test
    public Unit selectedUnit;   // pour le movement , pour l'instant /// hadi ybanli ytn7a , doka manach nss79oh 
    public Terrain TerrainGrassPrefab;
    public int playerTurn = 1; // hadi ttssegem .


    // This method is called when the object is first enabled in the scene.
    void Start()
    {
        mapGrid.CalculateMapGridSize();
        mapGrid.InitialiseMapGridCells();

        SpawnUnit(1, 5, 5, Infantry1Prefab); // test 

        SpawnUnit(2, 8, 8, Infantry2Prefab);
        // SpawnUnit(2, 7, 10, Infantry2Prefab);
        // SpawnUnit(2, 8, 20, Infantry2Prefab);
    }

    
    void Update()
    {
        CheckEndTurnInput();
    }


    // this function is used to spawn a unit on the map
    private void SpawnUnit(int playerNumber, int row, int column, Unit unitPrefab)
    {
        // instantiate the unit at the specified position , the position is calculated based on the row and column of the grid cell 
        Unit unit = Instantiate(unitPrefab, new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f, -1), Quaternion.identity);

        // adjust the size of the unit sprite to fit the grid cell size , this function is defined in GameUtil.cs    
        unit.gameObject.AdjustSpriteSize();

        // set the occupantUnit of the grid cell to the unit 
        mapGrid.grid[row, column].occupantUnit = unit;

        // set the occupiedCell of the unit to the grid cell
        unit.occupiedCell = mapGrid.grid[row, column];

        // set the (row,column) of the unit to the (row,column) of the grid cell
        unit.row = row;
        unit.col = column;

        unit.playerNumber = playerNumber;

    }


    // this function is used to check if the player has pressed the space key to end his turn
    private void CheckEndTurnInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    // this function is used to end the turn of the current player
    private void EndTurn()
    {
        selectedUnit = null; // ???? tbdlet la logique ya selectedUnit  , hada ytn7a . 

        SwitchPlayeTurn();

        ResetAllCellsAttributsInEndTurn();
        ResetAllUnitsAttributsInEndTurn();

        CancelScript.Instance.Cancel();  // hadi omb3d nchofo m3aha

    }

    // this function is used to switch the turn of the players    
    public void SwitchPlayeTurn()
    {
        playerTurn = (playerTurn == 1) ? 2 : 1;  // if playerTurn == 1, then playerTurn = 2, else playerTurn = 1
    }

    // this function is used to reset all the gridCells to their original state in the end of the turn
    public void ResetAllCellsAttributsInEndTurn()
    {
        foreach (GridCell gridCell in FindObjectsOfType<GridCell>())
        {
            gridCell.ResetCellAttributsInEndTurn();
        }
    }

    // this function is used to reset all the units to their original state in the end of the turn
    public void ResetAllUnitsAttributsInEndTurn()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>()) // FindObjectsOfType<Unit>() returns an array of all the units in the scene
        {
            unit.ResetUnitAttributsInEndTurn(); // others proprities
        }
    }





}