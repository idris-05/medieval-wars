using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTransport : Unit
{

    //!!!!!!! ki t'attaquer Trasporter , tdreb ghir trasnporter , li lda5el mat9issoch , ida mat transporter ymot li m3ah tani (loaded unit)

    public Unit loadedUnit;
    public bool hasSupply;
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
    public IEnumerator Drop(GridCell cell)
    {
        //!!!!! remove the hide from the unit .

        // ... logic to drop the unit into the grid cell

        cell.occupantUnit = this.loadedUnit;
        this.loadedUnit.occupiedCell = cell;
        this.loadedUnit.row = cell.row;
        this.loadedUnit.col = cell.column;

        // set the new position of the unit .
        this.loadedUnit.unitView.SetUnitPosition(cell.row, cell.column);
        this.loadedUnit.unitView.ShowUnitAfterDrop();
        this.loadedUnit.unitView.ChangeAnimationState(UnitUtil.AnimationState.SHADOW_CLONE_JUTSU);
        yield return new WaitForSeconds(0.45f);
        this.loadedUnit.unitView.ChangeAnimationState(UnitUtil.AnimationState.IDLE);
        this.loadedUnit.TransitionToNumbState();

        loadedUnit = null;

        yield break;

    }

    // Method to supply a unit with something
    public void Supply(Unit unitToSupply)
    {
        // transporter howa selected unit fl Unitcontroller , omb3d UnitToSupply hya li tselectionniha omb3d (mor l7kaya t3 layer wg3) 
        hasSupply = true;
        unitToSupply.RecieveRationSupply();
        StartCoroutine(unitToSupply.unitView.PlaySRecieveRationSupplyAnimation());
    }



    // method to get teh dropable units
    public void GetDropableCells()
    {

        int currentRow = row;
        int currentCol = col;

        List<GridCell> dropableCellsCondidates = new List<GridCell>();

        if (currentRow - 1 >= 0) dropableCellsCondidates.Add(MapGrid.Instance.grid[currentRow - 1, currentCol]);   // top cell
        if (currentRow + 1 < MapGrid.Instance.grid.GetLength(0)) dropableCellsCondidates.Add(MapGrid.Instance.grid[currentRow + 1, currentCol]);   // bottom cell
        if (currentCol - 1 >= 0) dropableCellsCondidates.Add(MapGrid.Instance.grid[currentRow, currentCol - 1]);  // left cell
        if (currentCol + 1 < MapGrid.Instance.grid.GetLength(1)) dropableCellsCondidates.Add(MapGrid.Instance.grid[currentRow, currentCol + 1]);// right cell


        foreach (GridCell cell in dropableCellsCondidates)
        {
            //!!!!!! + lazem hadik unit li 7ab tdropiha t9der t3mchi 3la terrain li ayken f cell hadik . moveCost != -1 .
            if (cell.occupantUnit == null) this.dropableCells.Add(cell);
        }
    }

    // Method to get a list of suppliable units
    public void GetSuppliableUnits()
    {

        int currentRow = row;
        int currentCol = col;

        if (currentRow - 1 >= 0 && MapGrid.Instance.grid[currentRow - 1, currentCol].occupantUnit is Unit suppliableUnit1)
        {
            if (suppliableUnit1.playerOwner == this.playerOwner)
            {
                suppliableUnits.Add(suppliableUnit1);
            }
        }

        if (currentRow + 1 < MapGrid.Instance.grid.GetLength(0) && MapGrid.Instance.grid[currentRow + 1, currentCol].occupantUnit is Unit suppliableUnit2)
        {
            if (suppliableUnit2.playerOwner == this.playerOwner)
            {
                suppliableUnits.Add(suppliableUnit2);
            }
        }

        if (currentCol - 1 >= 0 && MapGrid.Instance.grid[currentRow, currentCol - 1].occupantUnit is Unit suppliableUnit3)
        {
            if (suppliableUnit3.playerOwner == this.playerOwner)
            {
                suppliableUnits.Add(suppliableUnit3);
            }
        }

        if (currentCol + 1 < MapGrid.Instance.grid.GetLength(1) && MapGrid.Instance.grid[currentRow, currentCol + 1].occupantUnit is Unit suppliableUnit4)
        {
            if (suppliableUnit4.playerOwner == this.playerOwner)
            {
                suppliableUnits.Add(suppliableUnit4);
            }
        }

    }

    public void SupplyAllSuppliableUnits()
    {
        // this line of code tparcouri la liste t3 suppliable units w dir appel la methode supply
        suppliableUnits.ForEach(unitToSupply => Supply(unitToSupply));
    }

    // 
    public void HighlightDropableCells()
    {
        // highlight the dropable cells
        UserInterfaceUtil.Instance.CellhighlightHolder.transform.position = this.transform.position;
        UserInterfaceUtil.Instance.CellhighlightLines.SetActive(true);
        UserInterfaceUtil.Instance.CellhighlightLines.GetComponent<SpriteRenderer>().color = Color.blue;

        dropableCells.ForEach(dropableCell => dropableCell.gridCellView.isHighlighted = true); // i need this for UI

        dropableCells.ForEach(dropableCell => dropableCell.gridCellView.HighlightAsDropable());
    }

    //
    public void ResetDropableCells()
    {
        UserInterfaceUtil.Instance.CellhighlightLines.SetActive(false);

        dropableCells.ForEach(dropableCell => dropableCell.gridCellView.isHighlighted = false); // i need this for UI
        dropableCells.ForEach(dropableCell => dropableCell.gridCellView.ResetHighlitedCell());

        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.ForEach(glowLine => Destroy(glowLine)); // DESTROY ALL THE GLOWLINES THAT HAVE ALREADY BEEN CREATED
        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.Clear();
        dropableCells.Clear();
    }

    //
    public void ResetSuppliableUnits()
    {
        suppliableUnits.Clear();
    }


}