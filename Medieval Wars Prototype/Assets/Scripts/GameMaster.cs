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

    public MapGrid mapGrid;

    // public GridCell GridCellPrefab;
    public Unit Infantry1Prefab;
    public Unit Infantry2Prefab;
    public Unit selectedUnit;   // pour le movement , pour l'instant
    public Terrain TerrainGrassPrefab;
    public int playerTurn = 1;

    public Unit SelectedUnitFromAttacker;

    //!!1 zyada ta3i ana 
    // public Unit unitsSelected;



    // This method is called when the object is first enabled in the scene.
    void Start()
    {
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

        if (selectedUnit != null)
        {
            selectedUnit.IsSelected = false;      // unselect the unit
            selectedUnit = null;               // set the selected unit to null (no unit is selected)
        }

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


    public void OnUnitSelection(Unit unit)
    {

        if (SelectedUnitFromAttacker == null)
        {
            if (unit.playerNumber != playerTurn)
            {

                // affichier les range de l'attaque t3 enneemi wla afficher win y9der yemchi ... 
                // (a descuter ...);

                return;
            }
            else   // unit.playerNumber == playerTurn  
            {

                if (unit.hasMoved == false)
                {
                    // select the clicked unit
                    SelectedUnitFromAttacker = unit;
                    selectedUnit = unit; // pour le movement
                    unit.IsSelected = true;
                    unit.GetWalkableTiles(unit.row, unit.col);
                    unit.GetEnemies();
                    return;
                }
                else
                {
                    // unit seleced has moved , there is no selectedunitFromAttacker before this selection , so , there is nothing to do 
                    return;
                }
            }
        }
        else // kayen deja unit mselectionniya men 3end li y'attacker
        {
            if (unit.playerNumber == playerTurn)
            {  // unselect it , is this case , you click on it two times
                unit.IsSelected = false;
                SelectedUnitFromAttacker = null;
                selectedUnit = null;
                ResetGridCells();
                unit.ResetRedEffectOnAttackbleEnemies();
                // we can consider that if you click on your unit (let's call it a) and then you cklick on another unit ( called b) {a and b are your units}
                // the unit a will be unselected and the unit b will be selected , or only a will be unselected ...  
                return;
            }
            else
            {    // capable tkon 9ader t'attacker 

                if (SelectedUnitFromAttacker.hasAttacked == false && SelectedUnitFromAttacker.enemiesInRange.Contains(unit))
                {
                    SelectedUnitFromAttacker.Attack(SelectedUnitFromAttacker, unit);

                    SelectedUnitFromAttacker.DestroyIfPossible(); // destroy the unit if it's health <= 0

                    // contre attaque ... 

                    // unselect the unit now , after it's attack
                    // SelectedUnitFromAttacker.IsSelected = false;
                    SelectedUnitFromAttacker = null;
                    selectedUnit = null;
                    ResetGridCells();
                    unit.ResetRedEffectOnAttackbleEnemies();
                    return;
                }
                else // if the unit is not in the range of the attacker  or you cannot attack it (already attacked)
                {
                    // unit.IsSelected = false;
                    SelectedUnitFromAttacker = null;
                    selectedUnit = null;
                    ResetGridCells();
                    unit.ResetRedEffectOnAttackbleEnemies();
                    return;
                }
            }
        }
    }



    public void OnCellSelection(GridCell cell)
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