using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnUnitsAndBuildings : MonoBehaviour
{

    private static SpawnUnitsAndBuildings instance;
    public static SpawnUnitsAndBuildings Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<SpawnUnitsAndBuildings>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("SpawnUnitsAndBuildings");
                    instance = obj.AddComponent<SpawnUnitsAndBuildings>();
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



    public int[] ListOfBarrackNumberForNeutreNation = { 6, 7, 8 };
    public int[] ListOfBarrackNumberForEnglishNation = { 0, 1, 2 };
    public int[] ListOfBarrackNumberForFrenchNation = { 3, 4, 5 };


    public int[] ListOfDockNumberForNeutreNation = { 4, 5, 10, 11 };
    public int[] ListOfDockNumberForEnglishNation = { 0, 1, 6, 7 };
    public int[] ListOfDockNumberForFrenchNation = { 2, 3, 8, 9 };



    public int[] ListOfStableNumberForNeutreNation = { 4, 5, 8, 11 };
    public int[] ListOfStableNumberForEnglishNation = { 0, 1, 6, 9 };
    public int[] ListOfStableNumberForFrenchNation = { 2, 3, 7, 10 };



    public int[] ListOfCastleNumberForNeutreNation = { 5 };
    public int[] ListOfCastleNumberForEnglishNation = { 0 };
    public int[] ListOfCastleNumberForFrenchNation = { 3 };


    public int[] ListOfVillageNumberForNeutreNation = { 4, 5, 10, 16, 17 };
    public int[] ListOfVillageNumberForEnglishNation = { 0, 1, 6, 12, 13 };
    public int[] ListOfVillageNumberForFrenchNation = { 2, 3, 8, 14, 15 };




    public int[][] ListOfBarrackNumberOfSameStyle = {
        new int[] { 6, 0, 3 },
        new int[] { 7, 1, 4 },
        new int[] { 8, 2, 5 }
        };


    public int[][] ListOfDockNumberOfSameStyle = {
        new int[] { 4, 0, 2 },
        new int[] { 5, 1, 3 },
        new int[] { 10, 6, 8 },
        new int[] { 11, 7, 9 }
        };


    public int[][] ListOfStableNumberOfSameStyle = {
        new int[] { 4, 0, 2 },
        new int[] { 5, 1, 3 },
        new int[] { 8, 6, 7 },
        new int[] { 11, 9, 10 }
        };


    public int[][] ListOfCastleNumberOfSameStyle = {
        new int[] { 5 , 0 , 3 }
        };


    public int[][] ListOfVillageNumberOfSameStyle = {
        new int[] { 4 , 0 , 2  },
        new int[] { 5 , 1 , 3  },
        new int[] { 10, 6 , 8  },
        new int[] { 16, 12, 14 },
        new int[] { 17, 13, 15 }
        };


    //   les indices des nations sont les suivants:
    //  neutre : 0
    //  anglais : 1
    //  français : 2

    public void SpawnUnitsForMAP1()
    {
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 6, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 2, 4, GameController.Instance.EnglishUnitPrefabsList[1]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 15, 10, GameController.Instance.EnglishUnitPrefabsList[2]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 12, 12, GameController.Instance.EnglishUnitPrefabsList[3]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 21, GameController.Instance.EnglishUnitPrefabsList[4]); 
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 6, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 5, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 14, 9, GameController.Instance.EnglishUnitPrefabsList[5]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 2, GameController.Instance.EnglishUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 1, GameController.Instance.EnglishUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 9, GameController.Instance.EnglishUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 1, 10, GameController.Instance.EnglishUnitPrefabsList[7]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 10, 7, GameController.Instance.EnglishUnitPrefabsList[10]);






        GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 25, GameController.Instance.FrenchUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 29, GameController.Instance.FrenchUnitPrefabsList[1]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 15, 23, GameController.Instance.FrenchUnitPrefabsList[2]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 13 ,4, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 6, 26, GameController.Instance.FrenchUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 24, GameController.Instance.FrenchUnitPrefabsList[4]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player2, 7, 1, GameController.Instance.FrenchUnitPrefabsList[4]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 14, 4, GameController.Instance.FrenchUnitPrefabsList[5]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 5, 23, GameController.Instance.FrenchUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 4, GameController.Instance.FrenchUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 19, GameController.Instance.FrenchUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 1, GameController.Instance.FrenchUnitPrefabsList[10]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 13, 24, GameController.Instance.FrenchUnitPrefabsList[5]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 15, 26, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 11, 27, GameController.Instance.FrenchUnitPrefabsList[10]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 13, 30, GameController.Instance.FrenchUnitPrefabsList[7]);




    }


    public void SpawnUnitsForMAP2()
    {
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 6, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 2, 4, GameController.Instance.EnglishUnitPrefabsList[1]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 15, 10, GameController.Instance.EnglishUnitPrefabsList[2]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 12, 12, GameController.Instance.EnglishUnitPrefabsList[3]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 21, GameController.Instance.EnglishUnitPrefabsList[4]); 
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 6, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 5, GameController.Instance.EnglishUnitPrefabsList[4]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 14, 9, GameController.Instance.EnglishUnitPrefabsList[5]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 2, GameController.Instance.EnglishUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 1, GameController.Instance.EnglishUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 9, GameController.Instance.EnglishUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 1, 10, GameController.Instance.EnglishUnitPrefabsList[7]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 10, 7, GameController.Instance.EnglishUnitPrefabsList[10]);






        GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 25, GameController.Instance.FrenchUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 29, GameController.Instance.FrenchUnitPrefabsList[1]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player2, 15, 23, GameController.Instance.FrenchUnitPrefabsList[2]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 13 ,4, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 6, 26, GameController.Instance.FrenchUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 24, GameController.Instance.FrenchUnitPrefabsList[4]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player2, 7, 1, GameController.Instance.FrenchUnitPrefabsList[4]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 14, 4, GameController.Instance.FrenchUnitPrefabsList[5]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 5, 23, GameController.Instance.FrenchUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 4, GameController.Instance.FrenchUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 19, GameController.Instance.FrenchUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 1, GameController.Instance.FrenchUnitPrefabsList[10]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 13, 24, GameController.Instance.FrenchUnitPrefabsList[5]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 15, 26, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 11, 27, GameController.Instance.FrenchUnitPrefabsList[10]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 13, 30, GameController.Instance.FrenchUnitPrefabsList[7]);
    }


    public void SpawnUnitsForMAP3()
    {
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 6, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 2, 4, GameController.Instance.EnglishUnitPrefabsList[1]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 15, 10, GameController.Instance.EnglishUnitPrefabsList[2]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 12, 12, GameController.Instance.EnglishUnitPrefabsList[3]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 21, GameController.Instance.EnglishUnitPrefabsList[4]); 
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 5, 6, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 5, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 14, 9, GameController.Instance.EnglishUnitPrefabsList[5]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 5, 2, GameController.Instance.EnglishUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 1, GameController.Instance.EnglishUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 4, GameController.Instance.EnglishUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 1, 8, GameController.Instance.EnglishUnitPrefabsList[7]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 10, 7, GameController.Instance.EnglishUnitPrefabsList[10]);






        GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 25, GameController.Instance.FrenchUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 2, 29, GameController.Instance.FrenchUnitPrefabsList[1]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 10, 24, GameController.Instance.FrenchUnitPrefabsList[2]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 13 ,4, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 22, GameController.Instance.FrenchUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 5, 23, GameController.Instance.FrenchUnitPrefabsList[4]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player2, 7, 1, GameController.Instance.FrenchUnitPrefabsList[4]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 14, 4, GameController.Instance.FrenchUnitPrefabsList[5]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 5, 23, GameController.Instance.FrenchUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 4, GameController.Instance.FrenchUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 15, GameController.Instance.FrenchUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 1, GameController.Instance.FrenchUnitPrefabsList[10]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 10, 22, GameController.Instance.FrenchUnitPrefabsList[5]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 10, 23, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 15, 30, GameController.Instance.FrenchUnitPrefabsList[10]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 13, 30, GameController.Instance.FrenchUnitPrefabsList[7]);


    }


    public void SpawnUnitsForMAP4()
    {
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 6, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 2, 4, GameController.Instance.EnglishUnitPrefabsList[1]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 15, 10, GameController.Instance.EnglishUnitPrefabsList[2]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 12, 12, GameController.Instance.EnglishUnitPrefabsList[3]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 21, GameController.Instance.EnglishUnitPrefabsList[4]); 
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 5, 6, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 5, GameController.Instance.EnglishUnitPrefabsList[4]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 14, 9, GameController.Instance.EnglishUnitPrefabsList[5]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 5, 2, GameController.Instance.EnglishUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 4, 1, GameController.Instance.EnglishUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 4, GameController.Instance.EnglishUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 1, 8, GameController.Instance.EnglishUnitPrefabsList[7]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 10, 7, GameController.Instance.EnglishUnitPrefabsList[10]);

        GameController.Instance.SpawnUnit(GameController.Instance.player1, 15, 2, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 14, 3, GameController.Instance.EnglishUnitPrefabsList[7]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 12, 3, GameController.Instance.EnglishUnitPrefabsList[6]);

        GameController.Instance.SpawnUnit(GameController.Instance.player1, 10, 1, GameController.Instance.EnglishUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 5, GameController.Instance.EnglishUnitPrefabsList[1]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 10, GameController.Instance.EnglishUnitPrefabsList[10]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 14, 16, GameController.Instance.EnglishUnitPrefabsList[10]);








        GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 25, GameController.Instance.FrenchUnitPrefabsList[0]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 2, 29, GameController.Instance.FrenchUnitPrefabsList[1]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 10, 24, GameController.Instance.FrenchUnitPrefabsList[2]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 13 ,4, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 22, GameController.Instance.FrenchUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 5, 23, GameController.Instance.FrenchUnitPrefabsList[4]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player2, 7, 1, GameController.Instance.FrenchUnitPrefabsList[4]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 14, 4, GameController.Instance.FrenchUnitPrefabsList[5]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 5, 23, GameController.Instance.FrenchUnitPrefabsList[6]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 7, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 4, 4, GameController.Instance.FrenchUnitPrefabsList[8]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 8, 15, GameController.Instance.FrenchUnitPrefabsList[9]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player1, 6, 1, GameController.Instance.FrenchUnitPrefabsList[10]);
        // GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 0, GameController.Instance.FrenchUnitPrefabsList[0]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 10, 22, GameController.Instance.FrenchUnitPrefabsList[5]);
        //GameController.Instance.SpawnUnit(GameController.Instance.player2, 10, 23, GameController.Instance.FrenchUnitPrefabsList[3]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 15, 30, GameController.Instance.FrenchUnitPrefabsList[10]);
        GameController.Instance.SpawnUnit(GameController.Instance.player2, 13, 30, GameController.Instance.FrenchUnitPrefabsList[7]);



        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 24, GameController.Instance.EnglishUnitPrefabsList[4]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 3, 25, GameController.Instance.EnglishUnitPrefabsList[7]);
        GameController.Instance.SpawnUnit(GameController.Instance.player1, 11, 26, GameController.Instance.EnglishUnitPrefabsList[6]);

        GameController.Instance.SpawnUnit(GameController.Instance.player1, 15, 24, GameController.Instance.EnglishUnitPrefabsList[0]);
    }

    public void CorrectBuildingsPlayerOwner()
    {
        foreach (Building building in FindObjectsOfType<Building>())
        {
            int nationIndex = GetTheNationIndexFromTheSpriteName(building);
            switch (nationIndex)
            {
                case 0:  // neutre
                    building.playerOwner = null;
                    break;
                case 1: // anglais
                    building.playerOwner = GameController.Instance.player1;
                    GameController.Instance.player1.buildingList.Add(building);
                    break;
                case 2: // français
                    building.playerOwner = GameController.Instance.player2;
                    GameController.Instance.player2.buildingList.Add(building);
                    break;
                default:
                    Debug.Log("The nation index is not recognized: " + nationIndex);
                    break;
            }
        }
    }



    private int GetTheNationIndexFromTheSpriteName(Building building)
    {
        string spriteName = building.spriteRenderer.sprite.name;

        // Split the string by underscore
        string[] parts = spriteName.Split('_');

        // Extract the building type
        string buildingType = parts[0];
        // Extract the number part and convert it to an integer
        int numberIndicatesTheNation = int.Parse(parts[1]);

        switch (buildingType)
        {
            case "Barrack":
                if (Array.Exists(ListOfBarrackNumberForNeutreNation, element => element == numberIndicatesTheNation))
                    return 0; // Neutre Nation
                else if (Array.Exists(ListOfBarrackNumberForEnglishNation, element => element == numberIndicatesTheNation))
                    return 1; // English Nation
                else if (Array.Exists(ListOfBarrackNumberForFrenchNation, element => element == numberIndicatesTheNation))
                    return 2; // French Nation
                else
                    return -1; // Not recognized

            case "Dock":
                if (Array.Exists(ListOfDockNumberForNeutreNation, element => element == numberIndicatesTheNation))
                    return 0; // Neutre Nation
                else if (Array.Exists(ListOfDockNumberForEnglishNation, element => element == numberIndicatesTheNation))
                    return 1; // English Nation
                else if (Array.Exists(ListOfDockNumberForFrenchNation, element => element == numberIndicatesTheNation))
                    return 2; // French Nation
                else
                    return -1; // Not recognized

            case "Stable":
                if (Array.Exists(ListOfStableNumberForNeutreNation, element => element == numberIndicatesTheNation))
                    return 0; // Neutre Nation
                else if (Array.Exists(ListOfStableNumberForEnglishNation, element => element == numberIndicatesTheNation))
                    return 1; // English Nation
                else if (Array.Exists(ListOfStableNumberForFrenchNation, element => element == numberIndicatesTheNation))
                    return 2; // French Nation
                else
                    return -1; // Not recognized

            case "Castle":
                if (Array.Exists(ListOfCastleNumberForNeutreNation, element => element == numberIndicatesTheNation))
                    return 0; // Neutre Nation
                else if (Array.Exists(ListOfCastleNumberForEnglishNation, element => element == numberIndicatesTheNation))
                    return 1; // English Nation
                else if (Array.Exists(ListOfCastleNumberForFrenchNation, element => element == numberIndicatesTheNation))
                    return 2; // French Nation
                else
                    return -1; // Not recognized

            case "Village":
                if (Array.Exists(ListOfVillageNumberForNeutreNation, element => element == numberIndicatesTheNation))
                    return 0; // Neutre Nation
                else if (Array.Exists(ListOfVillageNumberForEnglishNation, element => element == numberIndicatesTheNation))
                    return 1; // English Nation
                else if (Array.Exists(ListOfVillageNumberForFrenchNation, element => element == numberIndicatesTheNation))
                    return 2; // French Nation
                else
                    return -1; // Not recognized

            default:
                Debug.Log("The building type is not recognized: " + building.spriteRenderer.sprite.name);
                return -1;
        }


    }


    private int[][] GetTheListOfBuildingNumberOfSameTerrainType(Building building)
    {
        switch (building.spriteRenderer.sprite.name.Split('_')[0])
        {
            case "Barrack":
                return ListOfBarrackNumberOfSameStyle;
            case "Dock":
                return ListOfDockNumberOfSameStyle;
            case "Stable":
                return ListOfStableNumberOfSameStyle;
            case "Castle":
                return ListOfCastleNumberOfSameStyle;
            case "Village":
                return ListOfVillageNumberOfSameStyle;
            default:
                Debug.Log("The building type is not recognized: " + building.spriteRenderer.sprite.name);
                return null;
        }
    }


    private int SwitchNationAndMaintainTheSameStyle(Player newPlayerOwner, Building building)
    {
        int newNationIndex;
        //  neutre : 0
        //  anglais : 1
        //  français : 2

        if (newPlayerOwner == null) newNationIndex = 0;
        else if (newPlayerOwner == GameController.Instance.player1) newNationIndex = 1;
        else if (newPlayerOwner == GameController.Instance.player2) newNationIndex = 2;
        else
        {
            Debug.Log("The player owner is not recognized: " + newPlayerOwner);
            return -1;
        }

        // get the old sprite number : sprites are saved in this format : TerrainName_Number .
        int oldSpriteNumber = int.Parse(building.spriteRenderer.sprite.name.Split('_')[1]);

        int[][] listOfBuildingNumberOfTerrainType = GetTheListOfBuildingNumberOfSameTerrainType(building);

        foreach (int[] buildingNumberArrayWithTaheSameStyle in listOfBuildingNumberOfTerrainType)
        {
            if (buildingNumberArrayWithTaheSameStyle.Contains(oldSpriteNumber))
            {
                return buildingNumberArrayWithTaheSameStyle[newNationIndex];
            }
        }

        Debug.Log("i return -1 i doesn't found the new nation index for the building: " + building.spriteRenderer.sprite.name);
        return -1;
    }


    public Sprite GetNewBuildingSprite(Player newPlayerOwner, Building building)
    {
        int newSpriteNumber = SwitchNationAndMaintainTheSameStyle(newPlayerOwner, building);

        string newSpriteName = building.spriteRenderer.sprite.name.Split('_')[0] + "_" + newSpriteNumber;

        TileBase tileBase = MapManager.Instance.listOfTerrainSpritesLists[building.TerrainIndex].FirstOrDefault(tile => tile.name == newSpriteName);

        Sprite tileSprite = ((Tile)tileBase).sprite;

        return tileSprite;
    }




}