using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class SavingSystem 
{
    //hello
    
   public static string PATH1 = Path.Combine(Application.dataPath, "player1stats.json");
   public static string PATH2 = Path.Combine(Application.dataPath, "player2stats.json");
   public static string PATHN = Path.Combine(Application.dataPath, "neutrestats.json");
   
    const string PATHneutre = "" ; 
    public struct playerUnitsInfos {
        public int player;
        public int funds ;
        public List<Buildingdata> buildings;
        public List<UnitData> unitdatas ;
    }
    public class PlayerData{
        int placement ;
        float powerMeter;
        int incomingFundsAtTheEndOfDay;
        int availableFunds;

    }
    public class UnitData{
        public int row;
        public int col;
        public int hp;
        public float rations;
        public int ammo;
        public int type;
        public bool  hasMoved;
        public bool numbState;
    }
    public class Buildingdata {
        public int row;
        public int col;
        public int remainningPointsToCapture;
        public int Buildingtype;

    }
    
    public static void savePlayer(Player player , string Path, int playeer) {

            SavingSystem.playerUnitsInfos unitPlayerdatas = new SavingSystem.playerUnitsInfos();
            unitPlayerdatas.unitdatas= new List<SavingSystem.UnitData>();
            unitPlayerdatas.buildings = new List<SavingSystem.Buildingdata>();
            unitPlayerdatas.funds =  player.availableFunds;
            unitPlayerdatas.player= playeer;

            foreach ( Building item in player.buildingList ){
                unitPlayerdatas.buildings.Add(SavingSystem.convertBuildingToData(item));
            }
   
            foreach(Unit item in player.unitList ){ 
               unitPlayerdatas.unitdatas.Add(SavingSystem.convertUnitToData(item));
            
            } 
            SavingSystem.Infosave(unitPlayerdatas,Path);
            Debug.Log("saved");
         

    }
    public static void Infosave(playerUnitsInfos unitdatas , string Path){
        using FileStream stream = File.Create(Path);
        stream.Close();
        File.WriteAllText(Path,JsonConvert.SerializeObject(unitdatas));
    }
    public static playerUnitsInfos Infoload(string Path ){
         playerUnitsInfos unitDatas = new playerUnitsInfos() ;
         unitDatas = JsonConvert.DeserializeObject<playerUnitsInfos>(File.ReadAllText(Path));
         return unitDatas;
    }
   /*  public static void LoadbuildingsToMap(playerUnitsInfos playerInfos){
        Player player ;
        if(playerInfos.player == 1){
            player = player1;
        }else{
            player=player2;
        }
        player.buildingList.Clear();
        foreach( Buildingdata buildingdata in playerInfos.buildings){
            Building building ;
            building = SpawnBuilding(player , buildingdata.row , buildingdata.col , (Building) indexTerrainprefab[buildingdata.Buildingtype]);
            building.remainningPointsToCapture = buildingdata.remainningPointsToCapture;
        }
   
} */
public static Buildingdata convertBuildingToData( Building building){
        Buildingdata buildingdata = new Buildingdata{
            row = building.row,
            col = building.col,
            remainningPointsToCapture = building.remainningPointsToCapture,
            Buildingtype = building.TerrainIndex,
        }; 
        return buildingdata;
    }
    public static UnitData convertUnitToData( Unit unit ){
        UnitData unitData = new UnitData{
            row = unit.row,
            col = unit.col,
            hp = unit.healthPoints,
            rations = unit.ration,
            type = unit.unitIndex,
            hasMoved=unit.hasMoved,
            numbState = unit.numbState
        };
         try {
            unitData.ammo= ((UnitAttack)unit).durability;
        }
        catch(Exception e ){
            Debug.Log(e.ToString());
            unitData.ammo=-1;

        } 
        return unitData;

    } 
}
