using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // hada machi moul lcompte , hada li raho da5el lpartie yl3eb .

    // public Color color;
    public List<Unit> unitList = new List<Unit>();
    public List<Building> buildingList = new List<Building>();


    public int incomingFundsAtTheEndOfDay;
    public int availableFunds;



    // public Player()
    // {
    //     // this.playerNumber = playerNumber;
    // }
    // // public int playerNumber = 1;

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void AddBuilding(Building building)
    {
        buildingList.Add(building);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public void RemoveBuilding(Building building)
    {
        buildingList.Remove(building);
    }


    // capable nzido 7ajat w7do5rin  li ymodifyiw drahem .
    public void UpdateIncomingFundsAtTheEndOfDay()
    {
        foreach (Building building in buildingList)
        {
            incomingFundsAtTheEndOfDay += building.incomingFunds;
        }
    }

    public void UpdateAvailableFunds()
    {
        availableFunds += incomingFundsAtTheEndOfDay;
    }

    // nssgmo le nom t3 la method, nzido fih in End Turn wla day ...
    public void UpdatePlayerStats()
    {
        UpdateIncomingFundsAtTheEndOfDay();
        UpdateAvailableFunds();
        incomingFundsAtTheEndOfDay = 0;
    }






    public void DisplayPlayerStatsInGame()
    {

    }



}

