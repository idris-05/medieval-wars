using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour
{
    public bool selected;
    GameMaster gm;
    public int row;
    public int col;
    public int moveRange;
    public MapGrid mapGrid;
    public float moveSpeed;
    public int playerNumber;
    public bool hasMoved;
    public GridCell occupiedCell;

    public void Start()
    {
        // Get the GameMaster component from the scene
        gm = FindObjectOfType<GameMaster>();
        // Get the MapGrid component from the scene
        mapGrid = FindObjectOfType<MapGrid>();
    }


    // Method to highlight the GridCell when the mouse hovers over it 
    private void OnMouseDown()
    {
        // If the unit has moved, return
        if (hasMoved  == true) return;

        // If the unit is selected, deselect it
        if (selected == true)
        {
            // Set the selected property of the unit to false
            selected = false;
            // Set the selectedUnit property of the GameMaster to null 
            gm.selectedUnit = null;
            // gm.ResetTiles();
        }
        else
        {
            if (playerNumber == gm.playerTurn)
            {
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }
                selected = true;
                gm.selectedUnit = this;

                //gm.ResetTiles();
                //GetEnemies();

                // get te actual position (i,j) where the clicked unit was .
                //int x = gm.selectedUnit.transform.position.x;
                //int y = gm.selectedUnit.transform.position.y;

                GetWalkableTiles(row, col);
            }
        }
    }

    // Method to get the walkable tiles for the selected unit 
    void GetWalkableTiles(int startRow, int startCol)
    {
        // Get the current position of the selected unit
        Vector2Int currentPos = new Vector2Int(startRow, startCol);

        for (int row = -moveRange; row <= moveRange; row++)
        {
            for (int col = -moveRange; col <= moveRange; col++)
            {

                // where the unit want go
                int nextRow = currentPos.x + row;
                int nextCol = currentPos.y + col;

                if (nextRow >= 0 && nextRow < MapGrid.Rows && nextCol >= 0 && nextCol < MapGrid.Columns)
                {
                    // If the distance between the current position and the next position is less than or equal to the moveRange of the unit 
                    // and the next position is not highlighted, highlight it .
                    if (MathF.Abs(row) + MathF.Abs(col) <= moveRange)
                    {
                        mapGrid.grid[nextRow, nextCol].Highlight();
                    }
                }
            }
        }
    }



    public void Move(int row, int column)
    {
        // gm.ResetTiles();
        Debug.Log($"Position: ({row}, {column})");
        Vector2 position = new Vector2(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f);
        StartCoroutine(StartMovement(position));

    }

    // Method to move the unit to the specified position
    // IEnumerator is used to make the movement smooth // and to wait for the movement to finish before executing the next line of code // IEnumerator is used to make the movement smooth // and to wait for the movement to finish before executing the next line of code
    IEnumerator StartMovement(Vector2 position)
    {
        while (transform.position.x != position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(position.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.y != position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        // hasMoved = true;
        // ResetWeaponIcons();
        // GetEnemies();
    }

}
