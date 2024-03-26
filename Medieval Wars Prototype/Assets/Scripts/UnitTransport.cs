using System.Collections.Generic;

public class UnitTransport : Unit
{

    public Unit loadedUnit;
    public List<GridCell> dropableCells = new List<GridCell>();  // cells where the transporter can drop the loaded unit .
    public List<Unit> suppliableUnits = new List<Unit>();  // unit that can get supplyRation from the transporter .


    public float AvailableRationToShare; //!!! ch7al rahi rafda ration , bach tmed ll units lo5rin 

    // lazemna valuere max t3 AvailableRationToShare

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



    // method to get teh dropable units
    public void GetdropableCells()
    {
        // logic to get the dropable cells
        // virify cells in the 4 directions for the dactual position of the transporter unit
    }

    // Method to get a list of suppliable units
    public void GetSuppliableUnits()
    {
        // Implement logic to retrieve suppliable units here
        // search in the foor direction for unit can be supplied
    }



    // 
    public void HighlightDropableCells()
    {
        foreach (GridCell cell in dropableCells)
        {
            cell.HighlightAsDropable();
        }
        // highlight the dropable cells
    }

    // 
    public void HighlightSuppliableUnits()
    {
        foreach (Unit unit in suppliableUnits)
        {
            unit.unitView.HighlightAsSuppliable();
        }
        // highlight the dropable cells
    }

    public void ResetSuppliableUnits()
    {
        foreach (Unit unit in suppliableUnits)
        {
            unit.unitView.ResetHighlightedUnit();
        }
        suppliableUnits.Clear();
    }


    public void ResetHighlightedDropableCells()
    {
        foreach (GridCell cell in dropableCells)
        {
            cell.ResetHighlitedCell();
        }
        // highlight the dropable cells
        dropableCells.Clear();
    }




}