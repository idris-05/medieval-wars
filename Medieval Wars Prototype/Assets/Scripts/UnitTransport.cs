using System.Collections.Generic;
using UnityEngine;

public class UnitTransport : Unit
{

    public Unit loadedUnit;
    public List<GridCell> dropableCells = new List<GridCell>();  // cells where the transporter can drop the loaded unit .
    public List<Unit> suppliableUnits = new List<Unit>();  // unit that can get supplyRation from the transporter .


    public float AvailableRationToShare; //!!! ch7al rahi rafda ration , bach tmed ll units lo5rin 



    // Method to load a unit into the transporterUnit
    public void Load(Unit unit)
    {
        loadedUnit = unit;
        unit.unitView.HideUnitWhenLoaded();
    }

    // Method to drop a unit onto a grid cell
    public void Drop(GridCell gridCell)
    {
        //!!!!! remove the hide from the unit .
        // ... logic to drop the unit into the grid cell
        loadedUnit = null;

    }

    // Method to supply a unit with something
    public void Supply(Unit unitToSupply, float supplyAmount)
    {
        //!!!! supplier 3lach ??? kanet parametre doka n7itha .
        // transporter howa selected unit fl Unitcontroller , omb3d UnitToSupply hya li tselectionniha omb3d (mor l7kaya t3 layer wg3) 
        AvailableRationToShare -= supplyAmount;
        unitToSupply.RecievRationSupply(supplyAmount);
    }


    // Method to get a list of suppliable units
    public void GetSuppliableUnits()
    {
        // Implement logic to retrieve suppliable units here
        // search in the foor direction for unit can be supplied
    }

    public void ResetSuppliableUnits()
    {
        suppliableUnits.Clear();
    }

    public void Load(UnitAttack unitToLoad)
    {
        loadedUnit = unitToLoad;
    }

    public void GetdropableCells()
    {
        // logic to get the dropable cells
        // virify cells in the 4 directions for the dactual position of the transporter unit
    }

    public void HighlightDropableCells()
    {
        foreach (GridCell cell in dropableCells)
        {
            cell.HighlightAsDropable();
        }
        // highlight the dropable cells
    }

}