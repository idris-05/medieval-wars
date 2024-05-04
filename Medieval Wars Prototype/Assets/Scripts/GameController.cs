using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;



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


    //!     Order in Layer :

    //! GridCells : 0
    //! Units : -1
    //! BlockInteractionsLayer : -2
    //! GridCellsWhenMadeInteractable : -3
    //! UnitsWhenMade(Attackable/Suppliable) : -3
    //! ActionButtons : -4


    public MapGrid mapGrid; // linked from the editor 

    public Unit BanditArabPrefab;

    [SerializeField] public Unit CaravanArabPrefabForTesting;


    public Player currentPlayerInControl;
    public Player player1;
    public Player player2;
    public CO coForTest;
    public int CurrentDayCounter;

    public List<Player> playerList = new List<Player>();

    void Awake()
    {
        coForTest = new GameObject("COForTest").AddComponent<CO>();
        player1 = new GameObject("Player1").AddComponent<Player>();
        player2 = new GameObject("Player2").AddComponent<Player>();
        currentPlayerInControl = player1;
        playerList.Add(player1);
        playerList.Add(player2);
        player1.Co = coForTest;
        player2.Co = coForTest;
    }

    // This method is called when the object is first enabled in the scene.
    void Start()
    {
        SpawnUnit(player1, 5, 5, BanditArabPrefab); // test 
        SpawnUnit(player1, 6, 5, BanditArabPrefab); // test 

        SpawnUnit(player1, 3, 3, BanditArabPrefab);


        SpawnUnit(player2, 8, 8, BanditArabPrefab);

        SpawnUnit(player1, 10, 10, CaravanArabPrefabForTesting);

       //SpawnUnit(player1, 2, 5, Infantry1PrefabTransport); 

    }


    void Update()
    {
       CheckEndTurnInput(); 
    }


    // this function is used to spawn a unit on the map
    public void SpawnUnit(Player player, int row, int column, Unit unitPrefab)
    {
        // instantiate the unit at the specified position , the position is calculated based on the row and column of the grid cell 
        Unit unit = Instantiate(unitPrefab, new Vector3(-16 + column + 0.5f, 9 - row - 0.5f + 0.125f, -1), Quaternion.identity);

        unit.playerOwner = player;
        player.AddUnit(unit);

        // set the occupantUnit of the grid cell to the unit 
        mapGrid.grid[row, column].occupantUnit = unit;

        // set the occupiedCell of the unit to the grid cell
        unit.occupiedCell = mapGrid.grid[row, column];

        // set the (row,column) of the unit to the (row,column) of the grid cell
        unit.row = row;
        unit.col = column;

        // flip the unit in case it is a player2 unit
        if (player == player2) unit.unitView.spriteRenderer.flipX = true;

        unit.unitView.spriteRenderer.material.color = new Color(255,62,255, 0); // set the outline to Orange

        // create the unit's health indicator
        SpawnHealthIcon(unit);

    }

    public void SpawnHealthIcon(Unit unit)
    {
        //! WHAT FOLLOWS IS IN ORDER TO CREATE THE HEALTH INDICATOR ON THE UNITS
        GameObject UnitHealthIcon = Instantiate(UserInterfaceUtil.Instance.UnitHealthIconPrefab, new Vector3(0, 0, 0), Quaternion.identity, unit.transform);
        UnitHealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[10];
        UnitHealthIcon.transform.localPosition = new Vector3(-0.17f, 0.17f, 0);
        unit.unitView.HealthIcon = UnitHealthIcon;
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
        CurrentDayCounter++;

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
        Debug.Log("NED GAME : player " + playerWinner.ToString() + " wins");
    }



}