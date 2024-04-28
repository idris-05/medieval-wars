using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.IO;
using System.IO;
using Newtonsoft.Json;
using UnityEditor.Playables;
using Unity.Services.Analytics;
using UnityEngine.TerrainUtils;
using System.Drawing;



public class GameController : MonoBehaviour
{
    //need to be changed 
   [SerializeField] public GameObject[] Arrowprefabs = new GameObject[15];
  [SerializeField] public  Unit[] indexUnitprefab = new Unit[5];
 [ SerializeField] public Terrain[] indexTerrainprefab = new Terrain[14]; 

 List<GameObject> arrows = new List<GameObject>();
   //this too need to be dynamic


    private static GameController instance;

    public ArrowSystem arrowSystem ;
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

    public Unit BanditArabPrefab;

    public Unit Infantry1Prefab; // test , //: hada , n7to list fiha t3 player 1 , w list pour player 2

    public Unit Infantry1PrefabTransport; // test , //: hada , n7to list fiha t3 player 1 , w list pour player 2

    public Unit Infantry2Prefab; // test
    public Player currentPlayerInControl;
    public Player player1;
    public Player player2;
    public List<GridCell> cellsPath =  new List<GridCell>();

    public List<GameObject> arrow = new List<GameObject>();

    public int CurrentDayCounter;

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
    ArrowSystem.Point point;
    bool hasmoved;
    void Start()
    {
        mapGrid.CalculateMapGridSize();
        mapGrid.InitialiseMapGridCells();

        arrowSystem = FindObjectOfType<ArrowSystem>();
        point.x=-1;
        point.y=-1;
        point.moveleft=-1;
        hasmoved=false;
        
        

        SpawnUnit(player1, 5, 5, Infantry1Prefab); // test 
        
        SpawnUnit(player1, 6, 5, Infantry1Prefab);
        


        SpawnUnit(player2, 8, 8, Infantry2Prefab);

        SpawnUnit(player1, 2, 5, Infantry1PrefabTransport);
        // SpawnUnit(2, 8, 20, Infantry2Prefab);

    }
      public void LoadbuildingsToMap(SavingSystem.playerUnitsInfos playerInfos){
        Player player ;
        if(playerInfos.player == 1){
            player = player1;
        }else{
            player=player2;
        }
        player.buildingList.Clear();
        foreach( SavingSystem.Buildingdata buildingdata in playerInfos.buildings){
            Building building ;
            building = SpawnBuilding(player , buildingdata.row , buildingdata.col , (Building) indexTerrainprefab[buildingdata.Buildingtype]);
            building.remainningPointsToCapture = buildingdata.remainningPointsToCapture;
        }


    }
    public void LaodUnitsToMap(SavingSystem.playerUnitsInfos playerinfos){
        Player player ;
        if(playerinfos.player == 1){
            player = player1;
        }else{
            player=player2;
        }
        player.unitList.Clear();
        foreach(SavingSystem.UnitData unitData in playerinfos.unitdatas){
            if (unitData.ammo == -1){
                UnitTransport unit ;
                unit = (UnitTransport) SpawnUnit(player,unitData.row,unitData.col,indexUnitprefab[unitData.type]);
                unit.healthPoints = unitData.hp;
                unit.hasMoved = unitData.hasMoved;
                unit.numbState = unitData.numbState;
                unit.ration = unitData.rations;
            }else{
                UnitAttack unit;
                unit = (UnitAttack)SpawnUnit(player,unitData.row,unitData.col,indexUnitprefab[unitData.type]);
                unit.durability= unitData.ammo;
                unit.healthPoints = unitData.hp;
                unit.hasMoved = unitData.hasMoved;
                unit.numbState = unitData.numbState;
                unit.ration = unitData.rations;
            }

        }

    }
    public void loadplayer(string path){
            SavingSystem.playerUnitsInfos unitPlayerdatas = new SavingSystem.playerUnitsInfos();
            unitPlayerdatas.unitdatas= new List<SavingSystem.UnitData>();
           unitPlayerdatas = SavingSystem.Infoload(path);
           Debug.Log("loaded");
           LaodUnitsToMap(unitPlayerdatas);
           LoadbuildingsToMap(unitPlayerdatas);
    }
    public void save(){
       SavingSystem.savePlayer(player1,SavingSystem.PATH1,1);
       SavingSystem.savePlayer(player2,SavingSystem.PATH2,2);
    }
    public void load(){
        loadplayer(SavingSystem.PATH1);
        loadplayer(SavingSystem.PATH2);
    }
   /*  public void newGame(){

    } */
    


      void Update()
    {
         if ( UnitController.Instance.selectedUnit == null ){
            hasmoved=false;
            if( arrow.Count != 0 ){
                try{
                foreach( GameObject item in arrow){
                    Destroy(item);
                }
                
                arrow.Clear();
                cellsPath.Clear();
                }catch(Exception e){
                    Debug.Log("hi");
                    Debug.Log(e);

                }

            }
        }else{
            if(!hasmoved){
               point = arrowSystem.DrawArrow(Arrowprefabs,UnitController.Instance.selectedUnit.col,UnitController.Instance.selectedUnit.row,cellsPath,arrow,UnitController.Instance.selectedUnit.moveRange);
               hasmoved = true;
            }else{
                
                point = arrowSystem.DrawArrow(Arrowprefabs,point.x,point.y,cellsPath,arrow,point.moveleft);
                if(Input.GetKeyDown(KeyCode.K)){
                Debug.Log(point.y+" and "+point.x);
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
    public Building SpawnBuilding(Player player, int row , int col , Building buildingprefab){

        Building building = Instantiate(buildingprefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);

        building.playerOwner=player;

        player.AddBuilding(building);

        building.gameObject.AdjustSpriteSize();

        mapGrid.grid[row, col].occupantTerrain = building;

        building.row = row;

        building.col = col;

        return building; 

    }
    public Unit SpawnUnit(Player player, int row, int column, Unit unitPrefab)
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