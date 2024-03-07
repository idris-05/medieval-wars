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

    public void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        mapGrid = FindObjectOfType<MapGrid>();
    }


    private void OnMouseDown()
    {

        // ResetWeaponIcons();

        if (hasMoved){
            return;
        }

        if (selected == true)
        {
            selected = false;
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


                GetWalkableTiles(row,col);
            }
        }

    }

    void GetWalkableTiles(int startRow, int startCol)
    {

        Vector2Int currentPos = new Vector2Int(startRow, startCol);

            for( int row = -moveRange ; row <= moveRange ; row++)
            {
                for (int col = -moveRange; col <= moveRange; col++)
                {

                    int nextRow = currentPos.x + row;
                    int nextCol = currentPos.y + col;
                
                    if ( nextRow >= 0 && nextRow <MapGrid.Rows && nextCol >=0 && nextCol < MapGrid.Columns )
                    {
                        if (mapGrid.grid[nextRow, nextCol].isHighlighted == false && MathF.Abs(row) + MathF.Abs(col) <= moveRange )
                        {
                            mapGrid.grid[nextRow, nextCol].Highlight();
                        }
                    }
                }
        }
    }

   /*void GetWalkableTiles()
    {
        if (hasMoved == true)
        {
            return;
        }

        foreach (GridCell gridCell in FindObjectsOfType<GridCell>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            {

                // if ( tile.IsClear() == true)
                //{
                tile.Highlight();
                // }
            }
        }

    } */
    
    public void Move(int row , int column) 
    {
        // gm.ResetTiles();
        Debug.Log(row + "," + column);
        Vector2 position = new Vector2(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f);
        StartCoroutine(StartMovement(position));

    }
    
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