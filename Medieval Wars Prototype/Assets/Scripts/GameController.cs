using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour
{

    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<GameController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameController");
                    instance = obj.AddComponent<GameController>();
                }
            }
            return instance;
        }
    }


    public MapGrid mapGrid; // linked from the editor 

    public Unit Infantry1Prefab; // test , //: hada , n7to list fiha t3 player 1 , w list pour player 2

    public Unit Infantry1PrefabTransport; // test , //: hada , n7to list fiha t3 player 1 , w list pour player 2

    public Unit Infantry2Prefab; // test

    public Player currentPlayerInControl;
    public Player player1;
    public Player player2;

    public List<Player> playerList = new List<Player>();

    void Awake()
    {
        player1 = new GameObject("Player1").AddComponent<Player>();
        player2 = new GameObject("Player2").AddComponent<Player>();
        currentPlayerInControl = player1;
        playerList.Add(player1);
        playerList.Add(player2);
    }
    // This method is called when the object is first enabled in the scene.
    void Start()
    {
        mapGrid.CalculateMapGridSize();
        mapGrid.InitialiseMapGridCells();

        SpawnUnit(player1, 5, 5, Infantry1Prefab); // test 
        SpawnUnit(player1, 6, 5, Infantry1Prefab); // test 


        SpawnUnit(player2, 8, 8, Infantry2Prefab);

        SpawnUnit(player1, 2, 5, Infantry1PrefabTransport);
        // SpawnUnit(2, 8, 20, Infantry2Prefab);
       
    }


    void Update()
    {
        CheckEndTurnInput();
    }


    // this function is used to spawn a unit on the map
    public void SpawnUnit(Player player, int row, int column, Unit unitPrefab)
    {
        // instantiate the unit at the specified position , the position is calculated based on the row and column of the grid cell 
        Unit unit = Instantiate(unitPrefab, new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f, -1), Quaternion.identity);

        unit.playerOwner = player;

        player.AddUnit(unit);

        // adjust the size of the unit sprite to fit the grid cell size , this function is defined in GameUtil.cs    
        unit.gameObject.AdjustSpriteSize();

        // set the occupantUnit of the grid cell to the unit 
        mapGrid.grid[row, column].occupantUnit = unit;

        // set the occupiedCell of the unit to the grid cell
        unit.occupiedCell = mapGrid.grid[row, column];

        // set the (row,column) of the unit to the (row,column) of the grid cell
        unit.row = row;
        unit.col = column;


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
        //! KI YEKLIKI 3lA UNIT NA7OULOU BOUTON T3 END TURN
        //! WE DO NOT HAVE TO DO MANY THINGS HERE BECAUSE WE WILL NOT LET THE PLAYER END HIS TURN UNLESS HE IS IN THE "NONE" STATE
        // currentPlayerInControl.UpdatePlayerStats(); // normalement tessra f end day mchi f turn ????? .

        ResetAllCellsAttributsInEndTurn();
        ResetAllUnitsAttributsInEndTurn();

        SwitchPlayeTurn();
        if (currentPlayerInControl == player1) EndDay();

    }

    public void EndDay()
    {
        // 
        foreach (Player player in playerList)
        {
            player.UpdatePlayerStats();

            foreach (Unit unit in player.unitList)
            {
                unit.ConsumeDailyRation();
            }

            foreach (Building building in player.buildingList)
            {
                building.HealAndSupplyUnitIfPossible(mapGrid);
            }
        }

    }

    // this function is used to switch the turn of the players    
    public void SwitchPlayeTurn()
    {
        // playerTurn = (playerTurn == 1) ? 2 : 1;  // if playerTurn == 1, then playerTurn = 2, else playerTurn = 1
        currentPlayerInControl = (currentPlayerInControl == player1) ? player2 : player1;
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


    public void EndGame(Player playerWinner)
    {
        //
        Debug.Log("NED GAME : player " + playerWinner.ToString() + " wins") ;
    }



}