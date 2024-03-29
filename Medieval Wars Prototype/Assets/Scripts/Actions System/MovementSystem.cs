using System;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private static MovementSystem instance;
    public static MovementSystem Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<MovementSystem>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("MovementSystem");
                    instance = obj.AddComponent<MovementSystem>();
                }
            }
            return instance;
        }
    }

    // mapgrid hadi ttna7a
    public MapGrid mapGrid;  // lokan hadi tkon , lazem la class mtkonch static , llazem ya hadi ya hadi 



    //! hna r7 tkon l7akaya lMoveCost ... 

    void Start()
    {
        mapGrid = FindObjectOfType<MapGrid>();
    }


    public void Movement(Unit unit, int row, int col)
    {


        if (mapGrid.grid[row, col].occupantUnit is UnitTransport) // If I try to move to a cell where there is a tronsporter
        {

            // We are Sure here that the transporter is empty because it gets verified in GetWalkableTiles
            // this is just to be able to call PrepareUnitToGetLoaded

            UnitAttack unitThatWillGetLoaded = unit as UnitAttack;
            UnitTransport unitTransport = mapGrid.grid[row, col].occupantUnit as UnitTransport;

            unit.unitView.ResetHighlitedWalkableCells();

            unitThatWillGetLoaded.PrepareUnitToGetLoadedInTransporter();
            unit.unitView.AnimateMovement(row, col);
            unit.TransitionToNumbState();
            unitTransport.Load(unit);


            return;
        }


        // case where u just move to a gridcell

        //! i found a problem here , after moving the unit that just moved has a z of 0 , this is due to being moved with a Vector2
        //! therefore i'll Make A method named ResetUnitPositionInLayersAfterMovement to reset it back to it's original position in layer
        //! please don't change my method names , they are as explicit as possible

        //! hna n7ssbo moveCost ( n7ssbo ch7al n9ssolha men ration ki mchat ) wnmdoh parametre lel UpdateAttributsAfterMoving .
        unit.UpdateAttributsAfterMoving(row, col);
        unit.unitView.AnimateMovement(row, col);
        unit.unitView.ResetHighlitedWalkableCells();

    }



    public void GetWalkableTiles(Unit unit)
    {
        unit.walkableGridCells.Clear();

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

                        moveleft = moveleft - mapGrid.grid[nextRow, nextCol].moveCost;

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

                            moveleft = moveleft - mapGrid.grid[nextRow, nextCol].moveCost;

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
                        //!- if Any One Can Do This : Plaese Find A Better Way For Highlighting Things
                        // mapGrid.grid[nextRow, nextCol].HighlightAsWalkable();
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

                            moveleft = moveleft - mapGrid.grid[nextRow, nextCol].moveCost;

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
                                moveleft = moveleft - mapGrid.grid[nextRow, nextCol].moveCost;

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
                            //!- if Any One Can Do This : Plaese Find A Better Way For Highlighting Things
                            // mapGrid.grid[nextRow, nextCol].HighlightAsWalkable();
                            unit.walkableGridCells.Add(mapGrid.grid[nextRow, nextCol]);
                        }
                    }
                }
            }
        }

    }



}