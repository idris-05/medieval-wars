using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


//  here inside the gameController:
//  when th player select a unit , if it already selected , i unselect it ( unitsSelected = null)  , if not i set the unitsSelected to the selected unit ,
//  and highlight the grid cells that the unit can move to , 


//!!! when you move the unit , you cannot attack directly ( you need to select the unit again to attack ) should we fix this !?
public class GameMaster : MonoBehaviour
{
    public HandelPlayerInput handelPlayerInput;

    public MapGrid mapGrid;

    // public GridCell GridCellPrefab;
    public Unit Infantry1Prefab;
    public Unit Infantry2Prefab;
    public Unit selectedUnit;   // pour le movement , pour l'instant
    public Terrain TerrainGrassPrefab;
    public int playerTurn = 1;

    public Unit SelectedUnitFromAttacker;




    // This method is called when the object is first enabled in the scene.
    void Start()
    {
        handelPlayerInput = FindObjectOfType<HandelPlayerInput>();

        mapGrid.CalculateMapGridSize();
        mapGrid.InitialiseMapGridCells();

        SpawnUnit(1, 5, 5, Infantry1Prefab); // test 
        SpawnUnit(2, 8, 8, Infantry2Prefab);
    }


    // this function is used to spawn a unit on the map
    private void SpawnUnit(int playerNumber, int row, int column, Unit unitPrefab)
    {

        // instantiate the unit at the specified position , the position is calculated based on the row and column of the grid cell 
        Unit unit = Instantiate(unitPrefab, new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);

        // adjust the size of the unit sprite to fit the grid cell size , this function is defined in GameUtil.cs    
        unit.gameObject.AdjustSpriteSize();

        // set the occupantUnit of the grid cell to the unit 
        mapGrid.grid[row, column].occupantUnit = unit;

        // set the occupiedCell of the unit to the grid cell
        unit.occupiedCell = mapGrid.grid[row, column];

        // set the (row,column) of the unit to the (row,column) of the grid cell
        unit.row = row;
        unit.col = column;

        //the same as ::
        // mapGrid.grid[row, column].occupantUnit.row = row;
        // mapGrid.grid[row, column].occupantUnit.col = column;

        unit.playerNumber = playerNumber;

    }



    void Update()
    {
        CheckEndTurnInput();
    }


    // this function is used to check if the player has pressed the space key to end his turn
    private void CheckEndTurnInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    // this function is used to end the turn of the current player
    private void EndTurn()
    {

        SwitchPlayeTurn();

        // if (selectedUnit != null)
        // {
        //     selectedUnit.IsSelected = false;      // unselect the unit
        //     selectedUnit = null;               // set the selected unit to null (no unit is selected)
        //     ;
        //     SelectedUnitFromAttacker = null;
        // }

        ResetGridCells();  // reset the grid cells to their original state (white color) and isWalkable = false for all cells
        ResetUnitsPropritiesInEndTurn();

    }

    // this function is used to switch the turn of the players    
    public void SwitchPlayeTurn()
    {
        playerTurn = (playerTurn == 1) ? 2 : 1;  // if playerTurn == 1, then playerTurn = 2, else playerTurn = 1
    }


    // this function is used to reset the grid cells to their original state (white color) and isWalkable = false for all cells 
    public void ResetGridCells()
    {
        foreach (GridCell gridCell in FindObjectsOfType<GridCell>())
        {
            gridCell.ResetGridCell();
        }
    }

    // this function is used to reset the proprities of the units in the end of the turn
    public void ResetUnitsPropritiesInEndTurn()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>()) // FindObjectsOfType<Unit>() returns an array of all the units in the scene
        {
            unit.ResetUnitPropritiesInEndTurn();
        }
    }

    // in this function we handle the selection of a unit by the player
    public void OnUnitSelection(Unit unit)
    {
        HandelPlayerInput.UnitAction action = handelPlayerInput.DetermineUnitAction(unit, SelectedUnitFromAttacker, playerTurn);
        ExecuteUnitAction(unit, SelectedUnitFromAttacker, action);
    }

    void ExecuteUnitAction(Unit unit, Unit SelectedUnitFromAttacker, HandelPlayerInput.UnitAction action)
    {
        switch (action)
        {
            case HandelPlayerInput.UnitAction.Select:
                SelectUnit(unit);
                break;
            case HandelPlayerInput.UnitAction.Unselect:
                DeselectUnit(unit);
                break;
            case HandelPlayerInput.UnitAction.Attack:
                AttackUnit(SelectedUnitFromAttacker, unit);
                break;
            case HandelPlayerInput.UnitAction.Move:
                break;
            case HandelPlayerInput.UnitAction.None:
                break;
            default:
                break; 
        }
    }
    // select , unselect , attack  methods , are not conmplete yet ,
    // Method to select a unit and display its movement and attack range
    private void SelectUnit(Unit unit)
    {
        SelectedUnitFromAttacker = unit;
        selectedUnit = unit;
        unit.IsSelected = true;
        unit.GetWalkableTiles(unit.row, unit.col);
        unit.GetEnemies();
    }

    // Method to deselect a unit
    private void DeselectUnit(Unit unit)
    {
        unit.IsSelected = false;

        SelectedUnitFromAttacker = null;
        selectedUnit = null;
        ResetGridCells();
        unit.ResetRedEffectOnAttackbleEnemies();
    }



    // Method to attack a unit
    private void AttackUnit(Unit attacker, Unit defender)
    {
        attacker.Attack(attacker, defender);
        defender.DestroyIfPossible();
        DeselectUnit(attacker); // Deselect the attacker after the attack
        DeselectUnit(defender); // Deselect the defender after the attack
    }





    // Method to handle the selection of a GridCell


    public void OnCellSelection(GridCell cell)
    // i should contunue the same what i did for the unit selection
    {
        // If the GridCell is not walkable and a unit is selected , unselect that unit
        if (SelectedUnitFromAttacker != null && cell.isWalkable == false)
        {
            SelectedUnitFromAttacker.IsSelected = false;
            SelectedUnitFromAttacker.ResetRedEffectOnAttackbleEnemies();
            SelectedUnitFromAttacker = null;
            selectedUnit = null;
            ResetGridCells(); // Reset the grid cells to their original state
        }

        if (cell.isWalkable && SelectedUnitFromAttacker != null && SelectedUnitFromAttacker.hasMoved == false)
        {
            // Move the selected unit to the GridCell
            SelectedUnitFromAttacker.Move(cell.row, cell.column);
            SelectedUnitFromAttacker.row = cell.row;
            SelectedUnitFromAttacker.col = cell.column;
            selectedUnit.occupiedCell = cell;
            // Set the selected unit's hasMoved property to true to prevent it from moving again in the same turn 
            SelectedUnitFromAttacker.hasMoved = true;
            // Unselect the unit 
            SelectedUnitFromAttacker.IsSelected = false;
            SelectedUnitFromAttacker.ResetRedEffectOnAttackbleEnemies();
            SelectedUnitFromAttacker = null;
            selectedUnit = null;
            // Reset the grid cells to their original state 
            ResetGridCells();

        }
    }



}