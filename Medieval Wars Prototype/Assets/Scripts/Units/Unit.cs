using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour       // this class will not be instantiated , maybe abstract ?
{


    public Player playerOwner;
    public MapGrid mapGrid;
    public UnitView unitView;

    public int unitIndex;
    public UnitUtil.UnitName unitName;

    public GridCell occupiedCell;
    public int row;
    public int col;

    public int healthPoints;

    public int moveRange;
    public float ration;

    public float rationPerDay;
    public int lineOfSight;



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
        unitView.ResetHighlitedWalkableCells();
    }


    //! n9dro nzido paramter movecost 
    //! YCONSOMI 3LA 7SAB LI MCHA 3LIHOUM ( sema lazem best path )
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
        this.healthPoints -= inflictedDamage; // hna events
    }

    public void Kill()
    {
        // w occupant Unit t3 cell li kan fiha ? wla w7dha tweli null , l3fayes li kima hadi wchnohom kamel
        playerOwner.unitList.Remove(this);
        Destroy(this.gameObject);
    }



    public void Heal()
    {
        healthPoints += 20;  //!! valeur berk , omb3d nsgmohom 
        if (this.healthPoints > 100)
        {
            this.healthPoints = 100;
        }
    }

    public void RecieveRationSupply()
    {
        ration = UnitUtil.maxRations[unitIndex];
        // Debug.Log("I just Got Supplied bro");
    }

    public void ConsumeDailyRation()
    {
        ration -= rationPerDay;
        if (ration < 0) ration = 0;
    }



    public void TransitionToNumbState()
    {
        numbState = true;
        hasMoved = true; //  normalement tt7ana //! WE NEED TO LOOK AT THIS , tweli ki tselectionniw : numb State => return  
        unitView.spriteRenderer.color = Color.gray; // ttsegem .
    }



    public void ResetUnitAttributsInEndTurn()
    {
        numbState = false;
        hasMoved = false;
        ConsumeDailyRation();
    }


    public void PrepareUnitToGetLoadedInTransporter()
    {
        occupiedCell.occupantUnit = null;
    }



    public void TryToCapture(Building building)
    {
        // Debug.Log(unit.healthPoints);
        building.remainningPointsToCapture -= healthPoints;
        // Debug.Log("unit syit n capturiiiibuilding w  5litlo  " + remainningPointsToCapture);
        if (building.remainningPointsToCapture <= 0) building.GetCaptured(this);
    }


}