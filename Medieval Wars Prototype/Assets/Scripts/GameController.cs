using System;
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




    //need to be changed 

    public Unit[] indexUnitprefab = new Unit[13];
    public Terrain[] indexTerrainprefab = new Terrain[14];

    public ArrowSystem arrowSystem;

    public List<GridCell> cellsPath = new List<GridCell>();

    public List<GameObject> arrow = new List<GameObject>();
    ArrowSystem.Point point;

    public bool hasmoved;


    public MapGrid mapGrid; // linked from the editor 

    public List<Unit> FrenchUnitPrefabsList;
    public List<Unit> EnglishUnitPrefabsList;




    public Player currentPlayerInControl;
    public Player player1;
    public Player player2;
    public Player playerNeutre;

    public CO coForTest1;
    public CO coForTest2;
    public int CurrentDayCounter;
    public List<Player> playerList = new List<Player>();
    public GameObject unitHealthIconHolder;



    void Awake()
    {
        coForTest1 = new GameObject("COForTest").AddComponent<AhmedPlayer>();
        coForTest1.coName = COUtil.COName.AHMEDPLAYER;
        coForTest1.BarLevelMustHaveToActivateCoPower = coForTest1.GetCoPowerBarLimit();

        coForTest2 = new GameObject("COForTest").AddComponent<AhmedPlayer>();
        coForTest2.coName = COUtil.COName.AHMEDPLAYER;
        coForTest2.BarLevelMustHaveToActivateCoPower = coForTest2.GetCoPowerBarLimit();

        player1 = new GameObject("Player1").AddComponent<Player>();
        player2 = new GameObject("Player2").AddComponent<Player>();
        playerNeutre = new GameObject("PlayerNeutre").AddComponent<Player>();
        currentPlayerInControl = player1;
        playerList.Add(player1);
        playerList.Add(player2);
        player1.Co = coForTest1;
        player2.Co = coForTest2;
        coForTest1.playerOwner = player1;
        coForTest2.playerOwner = player2;
    }

    // This method is called when the object is first enabled in the scene.
    void Start()
    {


        arrowSystem = FindAnyObjectByType<ArrowSystem>();
        SpawnUnit(player1, 3, 6, EnglishUnitPrefabsList[0]);
        SpawnUnit(player1, 2, 4, EnglishUnitPrefabsList[1]);
        SpawnUnit(player1, 15, 10, EnglishUnitPrefabsList[2]);
        SpawnUnit(player1, 12, 12, EnglishUnitPrefabsList[3]);
        // SpawnUnit(player1, 7, 21, EnglishUnitPrefabsList[4]);
        SpawnUnit(player1, 6, 6, EnglishUnitPrefabsList[4]);
        SpawnUnit(player1, 4, 5, EnglishUnitPrefabsList[4]);
        //SpawnUnit(player1, 14, 1, EnglishUnitPrefabsList[5]);
        SpawnUnit(player1, 6, 2, EnglishUnitPrefabsList[6]);
        // SpawnUnit(player1, 7, 0, EnglishUnitPrefabsList[0]);
        //  SpawnUnit(player1, 4, 1, EnglishUnitPrefabsList[8]);
        SpawnUnit(player1, 7, 9, EnglishUnitPrefabsList[9]);
        //SpawnUnit(player1, 11, 0, EnglishUnitPrefabsList[4]);



        SpawnUnit(player2, 4, 25, FrenchUnitPrefabsList[0]);
        SpawnUnit(player2, 8, 29, FrenchUnitPrefabsList[1]);
        SpawnUnit(player2, 15, 23, FrenchUnitPrefabsList[2]);
        //SpawnUnit(player2, 13 ,4, FrenchUnitPrefabsList[3]);
        SpawnUnit(player2, 6, 26, FrenchUnitPrefabsList[4]);
        SpawnUnit(player2, 8, 24, FrenchUnitPrefabsList[4]);
        // SpawnUnit(player2, 7, 1, FrenchUnitPrefabsList[4]);
        //SpawnUnit(player2, 14, 4, FrenchUnitPrefabsList[5]);
        SpawnUnit(player2, 5, 23, FrenchUnitPrefabsList[6]);
        // SpawnUnit(player1, 7, 0, FrenchUnitPrefabsList[0]);
        //SpawnUnit(player2, 4, 4, FrenchUnitPrefabsList[8]);
        SpawnUnit(player2, 8, 19, FrenchUnitPrefabsList[9]);
        //SpawnUnit(player1, 6, 1, FrenchUnitPrefabsList[10]);
        // SpawnUnit(player1, 11, 0, FrenchUnitPrefabsList[0]);

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


    // this function is used to spawn a unit on the map
    public Unit SpawnUnit(Player player, int row, int column, Unit unitPrefab)
    {
        // instantiate the unit at the specified position , the position is calculated based on the row and column of the grid cell 
        // Unit unit = Instantiate(unitPrefab, new Vector3(-16 + column + 0.5f, 9 - row - 0.5f + 0.125f, -1), Quaternion.identity);
        float yposition = player == player1 ? UnitUtil.AdditionInYPpositionForEnglishUnits[unitPrefab.unitIndex] : UnitUtil.AdditionInYPpositionForEnglishUnits[unitPrefab.unitIndex];
        Unit unit = Instantiate(unitPrefab, new Vector3(-16 + column + 0.5f, 9 - row - 0.5f + yposition - 0.5f, -1), Quaternion.identity);

        // -0.5f deux foix , parceque f tableau hadak t3 les positions rani zayed 0.5 , donc n3awed nn7iha . 

        unit.playerOwner = player;
        player.AddUnit(unit);
        unit.playerOwner.Co.ActivateDailyPower();

        // set the occupantUnit of the grid cell to the unit 
        mapGrid.grid[row, column].occupantUnit = unit;

        // set the occupiedCell of the unit to the grid cell
        unit.occupiedCell = mapGrid.grid[row, column];

        // set the (row,column) of the unit to the (row,column) of the grid cell
        unit.row = row;
        unit.col = column;

        // flip the unit in case it is a player2 unit
        if (player == player2) unit.unitView.spriteRenderer.flipX = true;

        Debug.Log("cell li rah yesra fiha le probleme  " + unit.occupiedCell.row + " " + unit.occupiedCell.column);

        if (player == player1) unit.unitView.spriteRenderer.material.color = Color.black; // set the outline to Blue
        if (player == player2) unit.unitView.spriteRenderer.material.color = new Color(255, 0, 0, 255); // set the outline to Red
        unit.unitView.spriteRenderer.material.SetFloat(Shader.PropertyToID("_Thickness"), 0.001f);
        // create the unit's health indicator
        SpawnHealthIcon(unit);

        return unit;
    }

    public void SpawnHealthIcon(Unit unit)
    {
        // WHAT FOLLOWS IS IN ORDER TO CREATE THE HEALTH INDICATOR ON THE UNITS

        UnitHealthIcon unitHealthIcon = Instantiate(UserInterfaceUtil.Instance.UnitHealthIconPrefab, new Vector3(-16 + unit.col + 0.5f, 9 - unit.row + 0.125f, 0), Quaternion.identity, unitHealthIconHolder.transform);

        unitHealthIcon.unit = unit;

        unitHealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[10];

        unitHealthIcon.GetComponent<SpriteRenderer>().material.SetFloat(Shader.PropertyToID("_Thickness"), 0.008f);
        unitHealthIcon.GetComponent<SpriteRenderer>().material.color = Color.black;
        unit.unitView.HealthIcon = unitHealthIcon;
    }




    public void LaodUnitsToMap(SavingSystem.playerUnitsInfos playerinfos)
    {
        Player player = playerinfos.player == 1 ? player1 : player2;
        player.unitList.Clear();
        List<SavingSystem.UnitData> loadedUnitsData = new List<SavingSystem.UnitData>();
        foreach (SavingSystem.UnitData unitData in playerinfos.unitdatas)
        {
            if (unitData.loadedUnit != null)
            {
                loadedUnitsData.Add(unitData.loadedUnit);
            }
        }

        foreach (SavingSystem.UnitData unitData in playerinfos.unitdatas)
        {
            if (!loadedUnitsData.Contains(unitData))
            {
                if (unitData.durability == -1)
                {
                    UnitTransport unit;
                    unit = (UnitTransport)SpawnUnit(player, unitData.row, unitData.col, indexUnitprefab[unitData.type]);
                    unit.healthPoints = unitData.hp;
                    unit.hasMoved = unitData.hasMoved;
                    unit.numbState = unitData.numbState;
                    unit.ration = unitData.rations;
                    unit.hasSupply = unitData.hasSupply;
                    /// DEJA VU i instentiated it deja !!!!???????
                    if (unitData.loadedUnit != null)
                    {
                        unit.loadedUnit = (UnitAttack)SpawnUnit(player, unitData.row, unitData.col, indexUnitprefab[unitData.loadedUnit.type]);
                        unit.Load(unit.loadedUnit);
                        unit.loadedUnit.healthPoints = unitData.loadedUnit.hp;
                        unit.loadedUnit.hasMoved = unitData.loadedUnit.hasMoved;
                        unit.loadedUnit.numbState = unitData.loadedUnit.numbState;
                        unit.loadedUnit.ration = unitData.loadedUnit.rations;
                        ((UnitAttack)unit.loadedUnit).durability = unitData.durability;
                        ((UnitAttack)unit.loadedUnit).hasAttacked = unitData.hasAttacked;
                    }
                }
                else
                {
                    UnitAttack unit;
                    unit = (UnitAttack)SpawnUnit(player, unitData.row, unitData.col, indexUnitprefab[unitData.type]);
                    unit.durability = unitData.durability;
                    unit.healthPoints = unitData.hp;
                    unit.hasMoved = unitData.hasMoved;
                    unit.numbState = unitData.numbState;
                    unit.ration = unitData.rations;
                    unit.hasAttacked = unitData.hasAttacked;
                }
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


    public Building SpawnBuilding(Player player, int row, int col, Building buildingprefab)
    {

        Building building = Instantiate(buildingprefab, new Vector3(-16 + col + 0.5f, 9 - row - 0.5f, -1), Quaternion.identity);


        building.playerOwner = player;

        player.AddBuilding(building);

        // building.gameObject.AdjustSpriteSize();

        mapGrid.grid[row, col].occupantTerrain = building;

        building.row = row;

        building.col = col;

        return building;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuController.Instance.ActivateMenu();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentPlayerInControl.Co.ActivateSuperPower();
        }

        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     save();

        // }
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     load();
        // }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cellsPath.Count > 0)
            {
                if (cellsPath[cellsPath.Count - 1].occupantUnit is UnitAttack) // added this to forbid moving to the same cell as ur ally attacking unit
                {
                    Debug.Log("Can't move on ally UnitAttack");
                }
                else
                {
                    GridCellController.Instance.OnCellSelection(cellsPath[cellsPath.Count - 1]);
                }
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
                    Debug.Log(e);

                }

            }
        }
        else
        {
            if (!hasmoved)
            {

                if (UnitController.Instance.selectedUnit == null) Debug.Log("UnitController.Instance.selectedUnit is null");

                point = arrowSystem.DrawArrow(UnitController.Instance.selectedUnit.col, UnitController.Instance.selectedUnit.row, cellsPath, arrow, UnitController.Instance.selectedUnit.moveRange);
                hasmoved = true;
            }
            else
            {

                point = arrowSystem.DrawArrow(point.x, point.y, cellsPath, arrow, point.moveleft);
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

            if (player.Co.isSuperPowerActivated) player.Co.DeactivateSuperPower();
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