using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour       // this class will not be instantiated , maybe abstract ?
{



    public MapGrid mapGrid;
    public UnitView unitView;





    public int unitIndex;
    public UnitUtil.UnitName unitName;

    public GridCell occupiedCell;
    public int row;
    public int col;

    public int playerNumber;

    public int healthPoints;

    public int moveRange;
    public float ration;
    public float rationPerDay;
    public int lineOfSight;
    public int moveCost;


    public bool hasMoved;
    public bool numbState;


    public List<GridCell> walkableGridCells = new List<GridCell>(); // this list will contain the grid cells that the unit can move to
    //

    public int attackBoost;
    public int specialAttackBoost;
    public int defenseBoost = 0; //!!!!!!!!! pour l'instant 0
    public int specialDefenseBoost = 0; //!!!!!!!!! pour l'instant 0;




    void Start()
    {
        // Get the UnitView component from the scene
        unitView = GetComponent<UnitView>();
        // Get the MapGrid component from the scene
        mapGrid = FindObjectOfType<MapGrid>();   //!!! ttna7a

    }


    //!!!!! le nom t3 method hadi 3yan , fiha UpdateAttributsAfterMoving , bssah hadi UpdateAttributsAfterMoving marahich vraiment t'updai kolch wch lazem , psq WalkableGridCells mazalhom .
    public void Move(int row, int col)
    {
        UpdateAttributsAfterMoving(row, col);
        ResetWalkableGridCells();
    }

    public void UpdateAttributsAfterMoving(int row, int col)
    {
        occupiedCell.occupantUnit = null; // remove the unit from the old grid cell

        // hadi ntb3oha parametre w5las !? cell li tro7 liha , wla n5loha haka tssema , tjib reference ta3ha da5el Unit ? .
        occupiedCell = mapGrid.grid[row, col]; // set the occupiedCell of the unit to the grid cell
        //! ??? , here we are modify an atribut of the MapGrid, is it a good practice ? 
        mapGrid.grid[row, col].occupantUnit = this; // set the occupantUnit of the new grid cell to the unit    

        hasMoved = true;
        this.row = row;
        this.col = col;


        unitView.ResetHighlightedUnit();

    }



    public void RecieveDamage(int inflictedDamage)
    {
        this.healthPoints -= inflictedDamage;
    }

    public void Kill()
    {
        // if (this.healthPoints <= 0)
        //{
        //     this.occupiedCell.occupantUnit = null;   // remove the unit from the grid cell
        Destroy(this.gameObject);                // remove the unit from UNITY !!!!

        // unitView.DeathAnimation();
    }



    public void Heal()
    {
        healthPoints += 10;  //!! valeur berk , omb3d nsgmohom 
        if (this.healthPoints > 100)
        {
            this.healthPoints = 100;
        }
    }

    public void RecievRationSupply(float rationSupply)
    {
        ration += rationSupply;
        if (ration > UnitUtil.maxRations[unitIndex]) ration = UnitUtil.maxRations[unitIndex];
    }

    public void ConsumeDailyRation()
    {
        ration -= rationPerDay;
        if (ration < 0) ration = 0;
    }



    public void TransitionToNumbState()
    {
        numbState = true;
        // hasMoved = true; //  normalement tt7ana
        unitView.spriteRenderer.color = Color.gray; // ttsegem .
    }





    public void ResetUnitAttributsInEndTurn()
    {
        hasMoved = false;
        numbState = false;
        ConsumeDailyRation();
        // reset specialAttackBoost w specialDefenseBoost lel 0 wla l valeure par default ta3hom 
        // specialAttackBoost = 0;
        // specialDefenseBoost = 0;

        // reset 3fayes t3 unit Attack
        if (this is UnitAttack unitAttack)
        {
            unitAttack.hasAttacked = false;
            unitAttack.enemiesInRange.Clear();
            unitAttack.attackableGridCells.Clear();
        }

        // reset 3fayes t3 unit Transport
        if (this is UnitTransport unitTransport)
        {
            unitTransport.suppliableUnits.Clear();
            unitTransport.dropableCells.Clear();
        }


        //  unselect the unit
        unitView.spriteRenderer.color = Color.white;
        // ..... 
    }



    public void ResetWalkableGridCells()
    {
        foreach (GridCell cell in walkableGridCells)
        {
            cell.ResetCellAttributsInEndTurn();
        }
        walkableGridCells.Clear();
    }



}