using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameMaster : MonoBehaviour
{

    public MapGrid mapGrid;
    public GridCell GridCellPrefab;
    public Unit Infantry1Prefab;
    public Unit Infantry2Prefab;
    public Unit selectedUnit;
    public int playerTurn = 1;



    // This method is called when the object is first enabled in the scene.
    void Start()
    {
        mapGrid.declareMapGrid();

        // Loop through each row and column of the map grid
        for (int row = 0; row < MapGrid.Rows; row++)
        {
            for (int col = 0; col < MapGrid.Columns; col++)
            {
                // Instantiate a GridCell prefab at the specified position
                GridCell gridCell = Instantiate(GridCellPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);

                // Adjust the sprite size of the instantiated GridCell
                gridCell.gameObject.AdjustSpriteSize();

                // Set the name of the GridCell
                gridCell.name = $"GridCell ({row}, {col})";

                // Set the row and column properties of the GridCell
                gridCell.row = row;
                gridCell.column = col;

                // Assign the GridCell to the corresponding position in the map grid
                mapGrid.grid[row, col] = gridCell;
            }
        }

        SpawnUnit(1,5,5,Infantry1Prefab); // test 
        SpawnUnit(2,8,8,Infantry2Prefab);
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

    // this function is used to end the turn of the current player and start the turn of the other player
    private void EndTurn()
    {
        

         playerTurn = (playerTurn == 1) ? 2 : 1;  // if playerTurn == 1, then playerTurn = 2, else playerTurn = 1

         if (selectedUnit != null)
         {
            selectedUnit.selected = false;      // unselect the unit
            selectedUnit = null;               // set the selected unit to null (no unit is selected)
         }

          ResetGridCells();  // reset the grid cells to their original state (white color) and isWalkable = false for all cells

          foreach (Unit unit in FindObjectsOfType<Unit>()) // FindObjectsOfType<Unit>() returns an array of all the units in the scene
          {
            unit.hasMoved = false;   // reset the hasMoved and hasAttacked variables to false  
            unit.hasAttacked = false;
            unit.spriteRenderer.color = Color.white;
            unit.hasAttacked = false;
        }

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

    // this function is used to reset the grid cells to their original state (white color) and isWalkable = false for all cells 
    public void ResetGridCells()
    {
        foreach (GridCell gridCell in FindObjectsOfType<GridCell>())
        {
            gridCell.ResetGridCell();
        }
    }
}