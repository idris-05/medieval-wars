using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameMaster : MonoBehaviour
{
   
    public MapGrid mapGrid; 
    public GridCell GridCellPrefab;
    public Unit UnitPrefab;
    public Unit EnemyUnitPrefab;

    public Unit selectedUnit;
    public int playerTurn = 1;
    


    void Start()  
    {
        mapGrid.initialiseMapGrid();

        Debug.Log(MapGrid.Columns);
        Debug.Log(MapGrid.Rows);

        for (int i = 0; i < MapGrid.Rows; i++)
        {
            for (int j = 0; j < MapGrid.Columns; j++)
            {
                mapGrid.grid[i,j] = Instantiate(GridCellPrefab, new Vector3( -MapGrid.Horizontal + j + 0.5f, MapGrid.Vertical - i - 0.5f), Quaternion.identity);
                mapGrid.grid[i,j].gameObject.AdjustSpriteSize();
                mapGrid.grid[i,j].name = "GridCell (" + i + ", " + j + ")";
                mapGrid.grid[i,j].row = i;
                mapGrid.grid[i,j].column = j;
            }
        }

        SpawnUnitPLayer1(5, 5);
        SpawnUnitPlayer2(8, 8);


        /*   mapGrid.initialiseMapGrid(); 

           for (int i = 0; i < MapGrid.Columns; i++)
           {
               for (int j = 0; j < MapGrid.Rows; j++)  
               {
                  mapGrid.grid[i,j] = Instantiate(GridCellPrefab, new Vector3(i - MapGrid.Horizontal + 0.5f, j - MapGrid.Vertical + 0.5f), Quaternion.identity);
                   mapGrid.grid[i,j].gameObject.AdjustSpriteSize();
                   mapGrid.grid[i,j].name = "GridCell (" + i + ", " + j + ")";
                   mapGrid.grid[i,j].row = i;
                   mapGrid.grid[i,j].colomun = j;
               }
           } */

        //   SpawnUnitPLayer1(5,5);
        // SpawnUnitPlayer2(8,8);

        // coordonnees unite

        /*  int x = 0;
          int y = 0;    

          Unit unit = Instantiate(UnitPrefab,new Vector3(x - MapGrid.Horizontal + 0.5f, y - MapGrid.Vertical + 0.5f), Quaternion.identity);
          unit.gameObject.AdjustSpriteSize();
          mapGrid.grid[x,y].occupantUnit = unit;
          unit.Move(5,5);
          mapGrid.grid[x, y].occupantUnit = null;
          mapGrid.grid[5, 5].occupantUnit = unit;
        */

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }

       /* if (selectedUnit != null)
        {
           // selectedUnitSquare.SetActive(true);
           // selectedUnitSquare.transform.position = selectedUnit.transform.position;
        }
        else
        {
            // selectedUnitSquare.SetActive(false);
        } */
    }

    void EndTurn()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
        }

        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetGridCells();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            
            unit.hasMoved = false;
           // unit.weaponIcon.SetActive(false);
           // unit.hasAttacked = false;
        }

    }

    public void SpawnUnitPLayer1(int row , int column)
    {
        Unit unit = Instantiate(UnitPrefab, new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);
        unit.gameObject.AdjustSpriteSize();
        mapGrid.grid[row, column].occupantUnit = unit;
        mapGrid.grid[row, column].occupantUnit.row = row;
        mapGrid.grid[row, column].occupantUnit.col = column;
        unit.playerNumber = 1;
        
    }

    public void SpawnUnitPlayer2(int row, int column)
    {
        Unit unit = Instantiate(EnemyUnitPrefab, new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);
        unit.gameObject.AdjustSpriteSize();
        mapGrid.grid[row, column].occupantUnit = unit;
        mapGrid.grid[row, column].occupantUnit.row = row;
        mapGrid.grid[row, column].occupantUnit.col = column;
        unit.playerNumber = 2;
    }

    public void ResetGridCells()
    {
        foreach ( GridCell gridCell in FindObjectsOfType<GridCell>())
        {
            gridCell.ResetGridCell();
        }
    }
}


   


          
    
    
