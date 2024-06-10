using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SavingSystem : MonoBehaviour
{

    // public static string PATH1 = Path.Combine(Application.streamingAssetsPath, "player1stats.json");
    // public static string PATH2 = Path.Combine(Application.streamingAssetsPath, "player2stats.json");
    // public static string PATHN = Path.Combine(Application.streamingAssetsPath, "GamesInfos.json");
    public static string PATH1;
    public static string PATH2;
    public static string PATHN;
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


    public static void GetThePathForLoad(int mapToLpoad)
    {
        switch (mapToLpoad)
        {
            case 1:
                PATH1 = Path.Combine(Application.streamingAssetsPath, "player1stats.json");
                PATH2 = Path.Combine(Application.streamingAssetsPath, "player2stats.json");
                PATHN = Path.Combine(Application.streamingAssetsPath, "GamesInfos.json");
                break;
            case 2:
                PATH1 = Path.Combine(Application.streamingAssetsPath, "player1stats2.json");
                PATH2 = Path.Combine(Application.streamingAssetsPath, "player2stats2.json");
                PATHN = Path.Combine(Application.streamingAssetsPath, "GamesInfos2.json");
                break;
            case 3:
                PATH1 = Path.Combine(Application.streamingAssetsPath, "player1stats3.json");
                PATH2 = Path.Combine(Application.streamingAssetsPath, "player2stats3.json");
                PATHN = Path.Combine(Application.streamingAssetsPath, "GamesInfos3.json");
                break;
            case 4:
                PATH1 = Path.Combine(Application.streamingAssetsPath, "player1stats4.json");
                PATH2 = Path.Combine(Application.streamingAssetsPath, "player2stats4.json");
                PATHN = Path.Combine(Application.streamingAssetsPath, "GamesInfos4.json");
                break;
            default:
                Debug.Log("map number is not valid");
                break;
        }
    }
    public struct Coinfo
    {
        public COUtil.COName coName;
        public bool isSuperPowerActivated;
        public float BarLevel;
        public float BarLevelMustHaveToActivateCoPower;
        public int numberOfTimeThatTheSuperPowerHasBeenUsed;

        public bool CanActivateSuperPower;

    }

    public struct playerUnitsInfos
    {
        public Coinfo Co;
        public int player;
        public int funds;
        public List<Buildingdata> buildings;
        public List<UnitData> unitdatas;
    }

    public class UnitData
    {
        public int row;
        public int col;
        public int hp;
        public float rations;
        public int durability;
        public int type;
        public bool hasMoved;
        public bool numbState;
        public bool hasAttacked;

        public bool hasSupply;

        public UnitData loadedUnit;

        // !!!!!!!!!!!!!!!!!!!!! hasAttacked
        // !!!!!!!!!!!!!!!!!!! info ta3 TransportUnit
    }
    public class Buildingdata
    {
        public String spritename;
        public int row;
        public int col;
        public int remainningPointsToCapture;
        public int Buildingtype;

    }
    public class GamesInofs
    {
        public int turn;

        public int currentDay;
        public List<Buildingdata> buildingsneutral;

    }
    public static void LoadbuildingsToMap(SavingSystem.playerUnitsInfos playerInfos)
    {
        Player player = playerInfos.player == 1 ? GameController.Instance.player1 : GameController.Instance.player2;
        player.buildingList.Clear();

        foreach (SavingSystem.Buildingdata buildingdata in playerInfos.buildings)
        {
            GameController.Instance.changeBuilding(player, buildingdata.row, buildingdata.col, buildingdata);
        }


    }

    public static void LaodUnitsToMap(SavingSystem.playerUnitsInfos playerinfos)
    {
        Player player;
        List<Unit> unitprefabs;
        if (playerinfos.player == 1)
        {
            player = GameController.Instance.player1;
            unitprefabs = GameController.Instance.EnglishUnitPrefabsList;

        }
        else
        {

            player = GameController.Instance.player2;
            unitprefabs = GameController.Instance.FrenchUnitPrefabsList;
        }
        player.unitList.Clear();
        if (player.unitList.Count == 0)
        {
            Debug.Log("unit list is empty");
        }



        foreach (SavingSystem.UnitData unitData in playerinfos.unitdatas)
        {
            if (unitData.durability == -1)
            {
                UnitTransport unit;
                unit = (UnitTransport)GameController.Instance.SpawnUnit(player, unitData.row, unitData.col, unitprefabs[unitData.type]);
                unit.healthPoints = unitData.hp;
                unit.unitView.HealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[GameUtil.GetHPToDisplayFromRealHP(unit.healthPoints)];
                unit.hasMoved = unitData.hasMoved;
                unit.numbState = unitData.numbState;
                if (unit.numbState) unit.unitView.HighlightAsInNumbState();
                unit.ration = unitData.rations;
                unit.hasSupply = unitData.hasSupply;

                /// DEJA VU i instentiated it deja !!!!???????
                if (unitData.loadedUnit != null)
                {
                    unit.loadedUnit = (UnitAttack)GameController.Instance.SpawnUnit(player, unitData.row, unitData.col, unitprefabs[unitData.loadedUnit.type]);
                    MapGrid.Instance.grid[unitData.row, unitData.col].occupantUnit = unit;
                    unit.Load(unit.loadedUnit);
                    unit.loadedUnit.healthPoints = unitData.loadedUnit.hp;
                    unit.unitView.HealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[GameUtil.GetHPToDisplayFromRealHP(unit.healthPoints)];
                    unit.loadedUnit.hasMoved = unitData.loadedUnit.hasMoved;
                    unit.loadedUnit.numbState = unitData.loadedUnit.numbState;
                    unit.loadedUnit.ration = unitData.loadedUnit.rations;
                    ((UnitAttack)unit.loadedUnit).durability = unitData.loadedUnit.durability;
                    ((UnitAttack)unit.loadedUnit).hasAttacked = unitData.loadedUnit.hasAttacked;
                    // player.unitList.Add(unit.loadedUnit);
                }
                // player.unitList.Add(unit);
            }
            else
            {
                UnitAttack unit;
                unit = (UnitAttack)GameController.Instance.SpawnUnit(player, unitData.row, unitData.col, unitprefabs[unitData.type]);
                unit.durability = unitData.durability;
                unit.healthPoints = unitData.hp;
                unit.unitView.HealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[GameUtil.GetHPToDisplayFromRealHP(unit.healthPoints)];
                unit.hasMoved = unitData.hasMoved;
                unit.numbState = unitData.numbState;
                if (unit.numbState) unit.unitView.HighlightAsInNumbState();
                unit.ration = unitData.rations;
                unit.hasAttacked = unitData.hasAttacked;
                // player.unitList.Add(unit);
            }


        }
    }


    public static bool IsEmpty(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return string.IsNullOrEmpty(json.Trim());
        }
        else
        {
            Debug.Log("File not found at path: " + filePath);
            return true; // Treat as empty if file doesn't exist
        }
    }
    
    public static void loadplayer(string path)
    {
        SavingSystem.playerUnitsInfos unitPlayerdatas = new SavingSystem.playerUnitsInfos();
        unitPlayerdatas.unitdatas = new List<SavingSystem.UnitData>();
        unitPlayerdatas = SavingSystem.Infoload(path);
        if (path == PATH1)
        {
            GameController.Instance.player1.availableFunds = unitPlayerdatas.funds;
            ConvertDataToCo(1, unitPlayerdatas.Co);
        }
        else
        {
            GameController.Instance.player2.availableFunds = unitPlayerdatas.funds;
            ConvertDataToCo(2, unitPlayerdatas.Co);

        }
        LaodUnitsToMap(unitPlayerdatas);
        LoadbuildingsToMap(unitPlayerdatas);

    }



    public static void SavePlayer(Player player, string Path, int playeer)
    {

        SavingSystem.playerUnitsInfos unitPlayerdatas = new SavingSystem.playerUnitsInfos();
        unitPlayerdatas.unitdatas = new List<SavingSystem.UnitData>();
        unitPlayerdatas.buildings = new List<SavingSystem.Buildingdata>();
        unitPlayerdatas.funds = player.availableFunds;
        unitPlayerdatas.player = playeer;
        unitPlayerdatas.Co = ConvertCoData(player.Co);
        List<Unit> units = new List<Unit>();
        foreach (Unit unit in player.unitList)
        {
            if (unit is UnitTransport unitTransport)
            {
                if (unitTransport.loadedUnit != null)
                {
                    units.Add(unitTransport.loadedUnit);

                }
            }

        }

        foreach (Building item in player.buildingList)
        {
            unitPlayerdatas.buildings.Add(SavingSystem.ConvertBuildingToData(item));
        }

        foreach (Unit item in player.unitList)
        {
            if (!units.Contains(item))
            {
                unitPlayerdatas.unitdatas.Add(SavingSystem.ConvertUnitToData(item));
            }

        }
        SavingSystem.Infosave(unitPlayerdatas, Path);

    }
    public static void SaveGame()
    {
        GamesInofs gamesInofs = new GamesInofs();
        gamesInofs.currentDay = GameController.Instance.CurrentDayCounter;
        gamesInofs.buildingsneutral = new List<Buildingdata>();
        if (GameController.Instance.currentPlayerInControl == GameController.Instance.player1)
        {
            gamesInofs.turn = 1;

        }
        else
        {
            gamesInofs.turn = 2;

        };
        foreach (GridCell item in GameController.Instance.mapGrid.grid)
        {
            Debug.Log(item.row + " " + item.column + " " + item.occupantTerrain + " " + item.occupantUnit);
            if (item.occupantTerrain is Building building)
            {
                if (building.playerOwner == null)
                {
                    gamesInofs.buildingsneutral.Add(ConvertBuildingToData(building));

                }

            }
        }
        using FileStream stream = File.Create(PATHN);
        stream.Close();
        File.WriteAllText(PATHN, JsonConvert.SerializeObject(gamesInofs));

    }
    public static void LoadGameToGame()
    {
        GamesInofs gamesInofs = loadGame();

        GameController.Instance.currentPlayerInControl = gamesInofs.turn == 1 ? GameController.Instance.player1 : GameController.Instance.player2;

        GameController.Instance.CurrentDayCounter = gamesInofs.currentDay;

        foreach (SavingSystem.Buildingdata buildingdata in gamesInofs.buildingsneutral)
        {
            (MapGrid.Instance.grid[buildingdata.row, buildingdata.col].occupantTerrain as Building).remainningPointsToCapture = buildingdata.remainningPointsToCapture;
        }


    }


    public static void Infosave(playerUnitsInfos unitdatas, string Path)
    {
        using FileStream stream = File.Create(Path);
        stream.Close();
        File.WriteAllText(Path, JsonConvert.SerializeObject(unitdatas));
    }


    public static playerUnitsInfos Infoload(string Path)
    {
        playerUnitsInfos unitDatas = new playerUnitsInfos();
        unitDatas = JsonConvert.DeserializeObject<playerUnitsInfos>(File.ReadAllText(Path));
        return unitDatas;
    }

    public static GamesInofs loadGame()
    {
        GamesInofs gamesInofs = new GamesInofs();
        gamesInofs = JsonConvert.DeserializeObject<GamesInofs>(File.ReadAllText(PATHN));
        return gamesInofs;
    }


    public static Buildingdata ConvertBuildingToData(Building building)
    {
        Buildingdata buildingdata = new Buildingdata
        {
            spritename = building.spriteRenderer.sprite.name,
            row = building.row,
            col = building.col,
            remainningPointsToCapture = building.remainningPointsToCapture,
            Buildingtype = building.TerrainIndex,
        };
        return buildingdata;
    }
    public static Coinfo ConvertCoData(CO co)
    {
        Coinfo coinfo = new Coinfo();
        coinfo.BarLevel = co.BarLevel;
        coinfo.BarLevelMustHaveToActivateCoPower = co.BarLevelMustHaveToActivateCoPower;
        coinfo.CanActivateSuperPower = co.CanActivateSuperPower;
        coinfo.coName = co.coName;
        coinfo.numberOfTimeThatTheSuperPowerHasBeenUsed = co.numberOfTimeThatTheSuperPowerHasBeenUsed;
        coinfo.isSuperPowerActivated = co.isSuperPowerActivated;

        return coinfo;

    }

    public static UnitData ConvertUnitToData(Unit unit)
    {
        UnitData unitData = new UnitData
        {
            row = unit.row,
            col = unit.col,
            hp = unit.healthPoints,
            rations = unit.ration,
            type = unit.unitIndex,
            hasMoved = unit.hasMoved,
            numbState = unit.numbState
        };

        if (unit is UnitAttack unitAttack)
        {
            unitData.durability = unitAttack.durability;
            unitData.hasAttacked = unitAttack.hasAttacked;
        }
        else
        {
            unitData.durability = -1;
            if (unit is UnitTransport unitTransport)
            {
                if (unitTransport.loadedUnit != null) unitData.loadedUnit = ConvertUnitToData(unitTransport.loadedUnit);
                unitData.hasSupply = unitTransport.hasSupply;
            }

        }
        return unitData;

    }


    public static CO ConvertDataToCo(int playerNumber, Coinfo coinfo)
    {
        CO co = playerNumber == 1 ? GameController.Instance.player1.Co : GameController.Instance.player2.Co;

        co.BarLevel = coinfo.BarLevel;
        co.BarLevelMustHaveToActivateCoPower = coinfo.BarLevelMustHaveToActivateCoPower;
        co.CanActivateSuperPower = coinfo.CanActivateSuperPower;
        co.coName = coinfo.coName;
        co.numberOfTimeThatTheSuperPowerHasBeenUsed = coinfo.numberOfTimeThatTheSuperPowerHasBeenUsed;
        co.isSuperPowerActivated = coinfo.isSuperPowerActivated;

        return co;
    }




    // public static void ClearAllJSONFiles(int mapNumber)
    // {
    //     File.WriteAllText(PATH1, string.Empty);
    //     File.WriteAllText(PATH2, string.Empty);
    //     File.WriteAllText(PATHN, string.Empty);
    // }

}