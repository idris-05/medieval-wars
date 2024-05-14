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

    // public Unit[] indexUnitprefab = new Unit[13];
    // public Terrain[] indexTerrainprefab = new Terrain[14];

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

        CurrentDayCounter = 1;
    }

    // This method is called when the object is first enabled in the scene.
    void Start()
    {

        mapGrid = MapGrid.Instance;
        arrowSystem = FindAnyObjectByType<ArrowSystem>();
        SpawnUnitsAndBuildings.Instance.SpawnUnitsForMAP1();
        SpawnUnitsAndBuildings.Instance.CorrectBuildingsPlayerOwner();

        // EndDayController.Instance.AnimateTheEndDayPanel();

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

        if (player == player1) unit.unitView.spriteRenderer.material.color = Color.red; // set the outline to Blue
        if (player == player2) unit.unitView.spriteRenderer.material.color = Color.blue; // set the outline to Red
        unit.unitView.spriteRenderer.material.SetFloat(Shader.PropertyToID("_Thickness"), 0.001f);

        if (unit.unitIndex == 4 && unit.playerOwner == player1) unit.unitView.spriteRenderer.material.SetFloat(Shader.PropertyToID("_Thickness"), 0.0005f); // because english infantry looked really bizzare

        // create the unit's health indicator
        SpawnHealthIcon(unit);

        return unit;
    }

    public void SpawnHealthIcon(Unit unit)
    {
        // WHAT FOLLOWS IS IN ORDER TO CREATE THE HEALTH INDICATOR ON THE UNITS

        UnitHealthIcon unitHealthIcon = Instantiate(UserInterfaceUtil.Instance.UnitHealthIconPrefab, new Vector3(-16 + unit.col + 0.5f, 9 - unit.row + 0.125f, 0), Quaternion.identity, unitHealthIconHolder.transform);

        unitHealthIcon.unit = unit;

        // unitHealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[10];
        unitHealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[GameUtil.GetHPToDisplayFromRealHP(unit.healthPoints)];
        unitHealthIcon.GetComponent<SpriteRenderer>().material.SetFloat(Shader.PropertyToID("_Thickness"), 0.008f);
        unitHealthIcon.GetComponent<SpriteRenderer>().material.color = Color.black;
        unit.unitView.HealthIcon = unitHealthIcon;
    }







    public void save()
    {
        SavingSystem.ClearAllJSONFiles(1);
        SavingSystem.SavePlayer(player1, SavingSystem.PATH1, 1);
        SavingSystem.SavePlayer(player2, SavingSystem.PATH2, 2);
        SavingSystem.SaveGame();

        Debug.Log("saved");
    }


    public void load()
    {
        DestroyAllUnitsForLoad();
        SavingSystem.loadplayer(SavingSystem.PATH1);
        SavingSystem.loadplayer(SavingSystem.PATH2);
        SavingSystem.LoadGameToGame();

        Debug.Log("loaded");
    }

    public void DestroyAllUnitsForLoad()
    {
        List<Unit> unitsToDestroy = new List<Unit>();
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unitsToDestroy.Add(unit);


            if ((unit as UnitTransport) != null)
            {
                if ((unit as UnitTransport).loadedUnit != null)
                {
                    unitsToDestroy.Add((unit as UnitTransport).loadedUnit);
                }
            }
        }

        foreach (Unit unit in unitsToDestroy)
        {
            if ((unit as UnitTransport) != null)
            {
                if ((unit as UnitTransport).loadedUnit != null)
                {
                    Unit unit1 = (unit as UnitTransport).loadedUnit;
                    Destroy(unit1.unitView.SupplyLackApple != null ? unit1.unitView.SupplyLackApple.gameObject : null);
                    Destroy(unit1.unitView.HealthIcon.gameObject);
                    unit1.playerOwner.unitList.Remove(unit1);
                    unit1.occupiedCell.occupantUnit = null;
                    Destroy(unit1.gameObject);
                }
            }
            Destroy(unit.unitView.SupplyLackApple != null ? unit.unitView.SupplyLackApple.gameObject : null);
            Destroy(unit.unitView.HealthIcon.gameObject);
            unit.playerOwner.unitList.Remove(unit);
            unit.occupiedCell.occupantUnit = null;
            Destroy(unit.gameObject);
        }
    }

    public void changeBuilding(Player player, int row, int col, SavingSystem.Buildingdata buildingdata)
    {

        // public String spritename;


        // public int incomingFunds;  // incomingFunds from the terrain: 0 , and buildings 1000 ; 

        // public GameObject captureFlag;


        Building building = MapGrid.Instance.grid[row, col].occupantTerrain as Building;

        building.remainningPointsToCapture = buildingdata.remainningPointsToCapture;


        building.playerOwner = player;
        // player.AddBuilding(building); rahi tessra lte7t f affect buildings to player .

        building.AffetcBuildingToPlayer(player);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuController.Instance.ActivateMenu();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            // currentPlayerInControl.Co.ActivateSuperPower();
            SavingSystem.ClearAllJSONFiles(1);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            save();

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            load();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cellsPath.Count > 0)
            {
                // this verification to avoid the case where the player can drop a unit clicking E (the position where it's droped is totally wrong) .
                if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE)
                {
                    // if there is a transporter unit that already has loaded another unit .
                    if (cellsPath.Count == 1)
                    {
                        GridCellController.Instance.OnCellSelection(cellsPath[0]);
                        return;
                    }
                    if (cellsPath[cellsPath.Count - 1].occupantUnit is UnitTransport unitTransport && unitTransport.loadedUnit != null) return;
                    if (cellsPath[cellsPath.Count - 1].occupantUnit is UnitTransport unitTransport1 && unitTransport1.loadedUnit == null && !UnitUtil.CanLoadThatUnit[unitTransport1.unitIndex, UnitController.Instance.selectedUnit.unitIndex]) return;
                    if (cellsPath[cellsPath.Count - 1].occupantUnit is UnitAttack unitAttack && unitAttack != null) return;

                    GridCellController.Instance.OnCellSelection(cellsPath[cellsPath.Count - 1]);
                }
                // }
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
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton != UnitUtil.ActionToDoWhenButtonIsClicked.NONE) return;
        //! KI YEKLIKI 3lA UNIT NA7OULOU BOUTON T3 END TURN
        //! WE DO NOT HAVE TO DO MANY THINGS HERE BECAUSE WE WILL NOT LET THE PLAYER END HIS TURN UNLESS HE IS IN THE "NONE" STATE
        // currentPlayerInControl.UpdatePlayerStats(); // normalement tessra f end day mchi f turn ????? .


        CancelScript.Instance.OnCancelButtonClicked();

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
                if (unit is UnitTransport unitTransport)
                {
                    unitTransport.GetSuppliableUnits();
                    unitTransport.SupplyAllSuppliableUnits();
                    unitTransport.ResetSuppliableUnits();
                }
            }

            foreach (Building building in player.buildingList)
            {
                building.HealAndSupplyUnitIfPossible(mapGrid);
            }

            if (player.Co.isSuperPowerActivated) player.Co.DeactivateSuperPower();
        }

        EndDayController.Instance.AnimateTheEndDayPanel();

    }

    // this function is used to switch the turn of the players    
    public void SwitchPlayeTurn()
    {
        // playerTurn = (playerTurn == 1) ? 2 : 1;  // if playerTurn == 1, then playerTurn = 2, else playerTurn = 1
        if (currentPlayerInControl == player1)
        {
            currentPlayerInControl = player2;
            CoCardsController.Instance.CO1.SetActive(false);
            CoCardsController.Instance.CO2.SetActive(true);
        }
        else
        {
            currentPlayerInControl = player1;
            CoCardsController.Instance.CO2.SetActive(false);
            CoCardsController.Instance.CO1.SetActive(true);

        }
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