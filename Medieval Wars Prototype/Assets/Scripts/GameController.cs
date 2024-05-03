using System;
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



    //!     Order in Layer :

    //! GridCells : 0
    //! Units : -1
    //! BlockInteractionsLayer : -2
    //! GridCellsWhenMadeInteractable : -3
    //! UnitsWhenMade(Attackable/Suppliable) : -3
    //! ActionButtons : -4




    //need to be changed 

    [SerializeField] public GameObject[] Arrowprefabs = new GameObject[15];
    [SerializeField] public Unit[] indexUnitprefab = new Unit[13];
    [SerializeField] public Terrain[] indexTerrainprefab = new Terrain[14];

    // List<GameObject> arrows = new List<GameObject>();
    //this too need to be dynamic
    public ArrowSystem arrowSystem;

    public List<GridCell> cellsPath = new List<GridCell>();

    public List<GameObject> arrow = new List<GameObject>();

    ArrowSystem.Point point;

    public bool hasmoved;




    public GameObject Menu;
    public MapGrid mapGrid; // linked from the editor 

    public Unit BanditArabPrefab;

    public Unit Infantry1Prefab; // test , //: hada , n7to list fiha t3 player 1 , w list pour player 2

    public Unit Infantry1PrefabTransport; // test , //: hada , n7to list fiha t3 player 1 , w list pour player 2

    public Unit Infantry2Prefab; // test

    public Player currentPlayerInControl;
    public Player player1;
    public Player player2;
    public Player playerNeutre;

    public int CurrentDayCounter;

    public List<Player> playerList = new List<Player>();




    void Awake()
    {
        MapManager.Instance.InitializeListOfTerrainSpritesLists();
        // mapGrid.CreateMapGridCellsMatrix();
        MapManager.Instance.LoadMapData();
        player1 = new GameObject("Player1").AddComponent<Player>();
        player2 = new GameObject("Player2").AddComponent<Player>();
        playerNeutre = new GameObject("PlayerNeutre").AddComponent<Player>();
        currentPlayerInControl = player1;
        playerList.Add(player1);
        playerList.Add(player2);
    }

    // This method is called when the object is first enabled in the scene.
    void Start()
    {

        arrowSystem = FindAnyObjectByType<ArrowSystem>();
        SpawnUnit(player1, 5, 5, BanditArabPrefab); // test 
        SpawnUnit(player1, 6, 5, BanditArabPrefab); // test 

        SpawnUnit(player1, 3, 3, BanditArabPrefab);

        SpawnUnit(player1, 15, 27, BanditArabPrefab);
        SpawnUnit(player1, 7, 20, BanditArabPrefab);


        SpawnUnit(player2, 8, 8, BanditArabPrefab);

        SpawnUnit(player1, 2, 5, Infantry1PrefabTransport);

        // SpawnBuilding(player1, 8, 7, (Building)indexTerrainprefab[3]);

    }



    public void LoadbuildingsToMap(SavingSystem.playerUnitsInfos playerInfos)
    {
        Player player;
        if (playerInfos.player == 1)
        {
            player = player1;
        }
        else
        {
            if (playerInfos.player == 2)
            {
                player = player2;
            }
            else
            {
                player = playerNeutre;

            }
        }
        player.buildingList.Clear();
        foreach (SavingSystem.Buildingdata buildingdata in playerInfos.buildings)
        {
            Building building;
            building = SpawnBuilding(player, buildingdata.row, buildingdata.col, (Building)indexTerrainprefab[buildingdata.Buildingtype]);
            building.remainningPointsToCapture = buildingdata.remainningPointsToCapture;
        }


    }


    public void LaodUnitsToMap(SavingSystem.playerUnitsInfos playerinfos)
    {
        Player player = playerinfos.player == 1 ? player1 : player2;
        player.unitList.Clear();

        foreach (SavingSystem.UnitData unitData in playerinfos.unitdatas)
        {
            if (unitData.ammo == -1)
            {
                UnitTransport unit;
                unit = (UnitTransport)SpawnUnit(player, unitData.row, unitData.col, indexUnitprefab[unitData.type]);
                unit.healthPoints = unitData.hp;
                unit.hasMoved = unitData.hasMoved;
                unit.numbState = unitData.numbState;
                unit.ration = unitData.rations;
            }
            else
            {
                UnitAttack unit;
                unit = (UnitAttack)SpawnUnit(player, unitData.row, unitData.col, indexUnitprefab[unitData.type]);
                unit.durability = unitData.ammo;
                unit.healthPoints = unitData.hp;
                unit.hasMoved = unitData.hasMoved;
                unit.numbState = unitData.numbState;
                unit.ration = unitData.rations;
            }
        }

    }



    public void loadplayer(string path)
    {
        SavingSystem.playerUnitsInfos unitPlayerdatas = new SavingSystem.playerUnitsInfos();
        unitPlayerdatas.unitdatas = new List<SavingSystem.UnitData>();
        unitPlayerdatas = SavingSystem.Infoload(path);
        Debug.Log("loaded");
        if (unitPlayerdatas.player != 0)
        {
            LaodUnitsToMap(unitPlayerdatas);
        }
        LoadbuildingsToMap(unitPlayerdatas);

    }



    public void save()
    {
        SavingSystem.SavePlayer(player1, SavingSystem.PATH1, 1);
        SavingSystem.SavePlayer(player2, SavingSystem.PATH2, 2);
        SavingSystem.SavePlayer(playerNeutre, SavingSystem.PATHN, 0);

        Debug.Log("saved");
    }



    public void load()
    {
        loadplayer(SavingSystem.PATH1);
        loadplayer(SavingSystem.PATH2);
        loadplayer(SavingSystem.PATHN);

        Debug.Log("loaded");
    }



    /*  public void newGame(){

     } */


    public Building SpawnBuilding(Player player, int row, int col, Building buildingprefab)
    {

        Building building = Instantiate(buildingprefab, new Vector3(-16 + col + 0.5f, 9 - row - 0.5f, -1), Quaternion.identity);


        building.playerOwner = player;

        player.AddBuilding(building);

        building.gameObject.AdjustSpriteSize();

        mapGrid.grid[row, col].occupantTerrain = building;

        building.row = row;

        building.col = col;

        return building;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            save();

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            load();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (cellsPath.Count > 0)
            {
                GridCellController.Instance.OnCellSelection(cellsPath[cellsPath.Count - 1]);
            }
        }

        if (UnitController.Instance.selectedUnit == null)
        {
            hasmoved = false;
            if (arrow.Count != 0)
            {
                try
                {
                    foreach (GameObject item in arrow)
                    {
                        Destroy(item);
                    }

                    arrow.Clear();
                    cellsPath.Clear();
                }
                catch (Exception e)
                {
                    Debug.Log("hi");
                    Debug.Log(e);

                }

            }
        }
        else
        {
            if (!hasmoved)
            {
                if (Arrowprefabs == null) Debug.Log("Arrowprefabs is null");
                if (UnitController.Instance.selectedUnit == null) Debug.Log("UnitController.Instance.selectedUnit is null");

                point = arrowSystem.DrawArrow(Arrowprefabs, UnitController.Instance.selectedUnit.col, UnitController.Instance.selectedUnit.row, cellsPath, arrow, UnitController.Instance.selectedUnit.moveRange);
                hasmoved = true;
            }
            else
            {

                point = arrowSystem.DrawArrow(Arrowprefabs, point.x, point.y, cellsPath, arrow, point.moveleft);
                if (Input.GetKeyDown(KeyCode.K))
                {
                    Debug.Log(point.y + " and " + point.x);
                }
            }
        }
        /*  if (Input.GetKeyDown(KeyCode.Q)){
             arrowSystem.DrawautoPath(mapGrid.grid[5,5].Pathlist, Arrowprefabs , cellsPath , arrow , UnitController.Instance.selectedUnit );
         } */


        CheckEndTurnInput();
        //! we must add the ResetAllCellsAttributsInEndTurn and ResetAllUnitsAttributsInEndTurn inside EndTurn .
    }

    //player owner don't forget it !!!


    // this function is used to spawn a unit on the map
    public Unit SpawnUnit(Player player, int row, int column, Unit unitPrefab)
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

        if (unit.unitView == null) Debug.Log("perfect cell");

        if (player == player2) unit.unitView.spriteRenderer.flipX = true;

        return unit;
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