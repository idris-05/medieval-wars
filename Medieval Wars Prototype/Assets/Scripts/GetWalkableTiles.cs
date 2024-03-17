using System;
using UnityEngine;

public class GetWalkableTiles : MonoBehaviour
{
    MapGrid mapGrid;

    void Start()
    {
        // HandelPlayerInput t9der ttne7a mena wndiroha parametre ll getWalkableTilesMethod .
        mapGrid = FindObjectOfType<MapGrid>();
    }

    // // Method to get the walkable tiles for the selected unit 
    // //!!!! we should check if the cell we want to doesn't already contain another unit , in our case , we can put two units on the same cell




    // public void getWalkableTilesMethod( Unit unit )
    // {
    //     unit.walkableGridCells.Clear();

    //     int startRow = unit.row;
    //     int startCol = unit.col;
    //     int moveRange = unit.moveRange;

    //     //! we should make sure that there is only one instance of the MapGrid in the scene .
    //     //! we can also pass the MapGrid as a parameter to the getWalkableTiles method 


    //     // Get the current position of the selected unit
    //     Vector2Int currentPos = new Vector2Int(startRow, startCol);

    //     for (int row = -moveRange; row <= moveRange; row++)
    //     {
    //         for (int col = -moveRange; col <= moveRange; col++)
    //         {

    //             // where the unit want go
    //             int nextRow = currentPos.x + row;
    //             int nextCol = currentPos.y + col;

    //             if (nextRow >= 0 && nextRow < MapGrid.Rows && nextCol >= 0 && nextCol < MapGrid.Columns)
    //             {
    //                 // If the distance between the current position and the next position is less than or equal to the moveRange of the unit 
    //                 // and the next position is not highlighted, highlight it .
    //                 if (MathF.Abs(row) + MathF.Abs(col) <= moveRange)
    //                 {
    //                     mapGrid.grid[nextRow, nextCol].Highlight();
    //                     unit.walkableGridCells.Add(mapGrid.grid[nextRow, nextCol]);
    //                 }
    //             }
    //         }
    //     }
    // }



    //!!! mazal 5ess tverier beli cell mafihach unit deja , w tverifier terrain hadik est ce que t9der tmchi fiha aslan (ship fl ground )
    public void getWalkableTilesMethod(Unit unit)
    {
        int startRow = unit.row;
        int startCol = unit.col;
        int moveRange = unit.moveRange;
        int moveleft = unit.moveRange;

        //! we should make sure that there is only one instance of the MapGrid in the scene .
        //! we can also pass the MapGrid as a parameter to the getWalkableTiles method 


        // Get the current position of the selected unit
        Vector2Int currentPos = new Vector2Int(startRow, startCol);

        for (int row = -moveRange; row <= moveRange; row++)
        {
            for (int col = -moveRange; col <= moveRange; col++)
            {

                // // where the unit want go
                // int nextRow = currentPos.x + row;
                // int nextCol = currentPos.y + col;

                // if (nextRow >= 0 && nextRow < MapGrid.Rows && nextCol >= 0 && nextCol < MapGrid.Columns)
                // {
                //     // If the distance between the current position and the next position is less than or equal to the moveRange of the unit 
                //     // and the next position is not highlighted, highlight it .
                //     if (MathF.Abs(row) + MathF.Abs(col) <= moveRange)
                //     {
                //         mapGrid.grid[nextRow, nextCol].Highlight();
                //     }
                // }
                if (!(MathF.Abs(row) + MathF.Abs(col) <= moveRange && currentPos.x + row >= 0 && currentPos.x + row < MapGrid.Rows && currentPos.y + col >= 0 && currentPos.y + col < MapGrid.Columns))
                {
                    continue;
                }

                else
                {
                    moveleft = unit.moveRange;
                    int row2 = 0;
                    int col2 = 0;


                    while (moveleft > 0 && row2 != row)
                    {
                        int nextCol = currentPos.y;
                        int nextRow = currentPos.x + row2;

                        moveleft = moveleft - mapGrid.grid[nextRow, nextCol].movecoast;

                        if (moveleft >= 0)
                        {
                            if (row < 0)
                            {
                                row2--;
                            }
                            else
                            {
                                row2++;
                            }
                        }
                    }


                    if (row2 == row)
                    {
                        while (moveleft > 0 && col2 != col)
                        {
                            int nextRow = currentPos.x + row2;
                            int nextCol = currentPos.y + col2;

                            moveleft = moveleft - mapGrid.grid[nextRow, nextCol].movecoast;

                            if (moveleft >= 0)
                            {
                                if (col < 0)
                                {
                                    col2--;
                                }
                                else
                                {
                                    col2++;
                                }
                            }
                        }
                    }

                    // Debug.Log("row in :" + row2 + "collum in : " + col2 + "row obj" + row + "collum obj" + col);
                    if (row == row2 && col == col2)
                    {
                        int nextRow = currentPos.x + row2;
                        int nextCol = currentPos.y + col2;
                        // Debug.Log(nextRow + nextCol);
                        mapGrid.grid[nextRow, nextCol].Highlight();
                        unit.walkableGridCells.Add(mapGrid.grid[nextRow, nextCol]);
                    }
                    else
                    {
                        moveleft = unit.moveRange;
                        row2 = 0;
                        col2 = 0;
                        while (moveleft > 0 && col2 != col)
                        {
                            int nextRow = currentPos.x + row2;
                            int nextCol = currentPos.y + col2;

                            moveleft = moveleft - mapGrid.grid[nextRow, nextCol].movecoast;

                            if (moveleft >= 0)
                            {
                                if (col < 0)
                                {
                                    col2--;
                                }
                                else
                                {
                                    col2++;
                                }
                            }
                        }

                        if (col2 == col)
                        {
                            while (moveleft > 0 && row2 != row)
                            {
                                int nextCol = currentPos.y + col2;
                                int nextRow = currentPos.x + row2;
                                moveleft = moveleft - mapGrid.grid[nextRow, nextCol].movecoast;

                                if (moveleft >= 0)
                                {
                                    if (row < 0)
                                    {
                                        row2--;
                                    }
                                    else
                                    {
                                        row2++;
                                    }
                                }
                            }
                        }
                        
                        if (row == row2 && col == col2)
                        {
                            int nextRow = currentPos.x + row2;
                            int nextCol = currentPos.y + col2;
                            mapGrid.grid[nextRow, nextCol].Highlight();
                            unit.walkableGridCells.Add(mapGrid.grid[nextRow, nextCol]);
                        }
                    }
                }
            }
        }

    }



    // n9dro nzido hna get enemy in all possible move cases . tssema hadik get enemt tdirha men ckamel lplayess possible li t9der temchilhom l unit ta3k
    //  (meme t3 l'enemy bch tchof win y9der y'attacker howa ) .



    public void highlightAttackableCells(Unit unit)
    {
        int startRow = unit.row;
        int startCol = unit.col;
        int attackRange = unit.attackRange;

        //! we should make sure that there is only one instance of the MapGrid in the scene .
        //! we can also pass the MapGrid as a parameter to the getWalkableTiles method 


        // Get the current position of the selected unit
        Vector2Int currentPos = new Vector2Int(startRow, startCol);

        for (int row = -attackRange; row <= attackRange; row++)
        {
            for (int col = -attackRange; col <= attackRange; col++)
            {

                // where the unit want go
                int nextRow = currentPos.x + row;
                int nextCol = currentPos.y + col;

                if (nextRow >= 0 && nextRow < MapGrid.Rows && nextCol >= 0 && nextCol < MapGrid.Columns)
                {
                    // If the distance between the current position and the next position is less than or equal to the moveRange of the unit 
                    // and the next position is not highlighted, highlight it .
                    if (MathF.Abs(row) + MathF.Abs(col) <= attackRange)
                    {
                        mapGrid.grid[nextRow, nextCol].rend.color = Color.red;
                    }
                }
            }
        }
    }


    public void unHighlightAttackableCells(Unit unit)
    {
        int startRow = unit.row;
        int startCol = unit.col;
        int attackRange = unit.attackRange;

        //! we should make sure that there is only one instance of the MapGrid in the scene .
        //! we can also pass the MapGrid as a parameter to the getWalkableTiles method 


        // Get the current position of the selected unit
        Vector2Int currentPos = new Vector2Int(startRow, startCol);

        for (int row = -attackRange; row <= attackRange; row++)
        {
            for (int col = -attackRange; col <= attackRange; col++)
            {

                // where the unit want go
                int nextRow = currentPos.x + row;
                int nextCol = currentPos.y + col;

                if (nextRow >= 0 && nextRow < MapGrid.Rows && nextCol >= 0 && nextCol < MapGrid.Columns)
                {
                    // If the distance between the current position and the next position is less than or equal to the moveRange of the unit 
                    // and the next position is not highlighted, highlight it .
                    if (MathF.Abs(row) + MathF.Abs(col) <= attackRange)
                    {
                        mapGrid.grid[nextRow, nextCol].rend.color = Color.white;
                    }
                }
            }
        }
    }





}