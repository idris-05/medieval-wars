

using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public Color color;
    public string name;
    public int playerId;
    public List<Unit> unitList = new List<Unit>();
    public int incomingFunds;
    public int availableFunds;



    public void AddUnits(Unit unit)
    {
        unitList.Add(unit);
    }


    public int GetIncomingFunds()
    {
        return incomingFunds;
    }



    public void SetAvailableFunds(int change)
    {
        availableFunds += change;
    }


    public void SetIncomingFunds(int change)

    {
        incomingFunds += change;
    }
    public void DisplayPlayerStatsInGame()
    {

    }
}

