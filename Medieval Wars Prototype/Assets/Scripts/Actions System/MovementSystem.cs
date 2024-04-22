using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    struct MIcell
    {
        public int moveleft;
        public GridCell you;
        public MIcell(int move, GridCell u)
        {
            this.moveleft = move;
            this.you = u;
        }
        public void affval(int move, GridCell u)
        {
            this.moveleft = move;
            this.you = u;
        }
    }

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

        if (mapGrid.grid[row, col].occupantUnit is UnitTransport) // If I try to move to a cell where there is a tronsporter we  will load the unit on the transporter .
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
        //int turn = gm.playerTurn;
        int y = unit.row;
        int x = unit.col;
        int Mrange;
        if (unit.ration < unit.moveRange)
        {
            Mrange = ((int)unit.ration);
        }
        else
        {
            Mrange = unit.moveRange;
        }
        MIcell temp = new MIcell(Mrange, mapGrid.grid[y, x]);
        MIcell temp2 = new MIcell(Mrange, mapGrid.grid[y, x]);


        Queue<MIcell> queue = new Queue<MIcell>();
        queue.Enqueue(temp);
        unit.walkableGridCells.Add(temp.you);
        temp.you.isWalkable = true;
        temp.you.Pathlist = new List<GridCell> { temp.you };
        while (queue.Count > 0)
        {
            temp = queue.Dequeue();
            x = temp.you.column;
            y = temp.you.row;


            if ((y - 1 >= 0 && y - 1 < MapGrid.Rows && x >= 0 && x < MapGrid.Columns) && temp.moveleft > 0 && !(mapGrid.grid[y - 1, x].isWalkable))
            {

                int moveleft = temp.moveleft - 1;//TerrainsUtil.MoveCost[mapGrid.grid[y - 1,x].occupantTerrain.TerrainIndex, unit.unitIndex];
                if (mapGrid.grid[y - 1, x].occupantUnit != null && mapGrid.grid[y - 1, x].occupantUnit.playerNumber != GameController.Instance.playerTurn)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y - 1, x]);
                    temp2.you.Pathlist = temp.you.Pathlist.ToList();
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(mapGrid.grid[y - 1, x]);
                    mapGrid.grid[y - 1, x].isWalkable = true;
                    queue.Enqueue(temp2);
                }


            }
            if ((y >= 0 && y < MapGrid.Rows && x + 1 >= 0 && x + 1 < MapGrid.Columns) && temp.moveleft > 0 && !(mapGrid.grid[y, x + 1].isWalkable))
            {
                int moveleft = temp.moveleft - 1;// TerrainsUtil.MoveCost[mapGrid.grid[y , x + 1].occupantTerrain.TerrainIndex, unit.unitIndex];
                if (mapGrid.grid[y, x + 1].occupantUnit != null && mapGrid.grid[y, x + 1].occupantUnit.playerNumber != GameController.Instance.playerTurn)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y, x + 1]);

                    temp2.you.Pathlist = temp.you.Pathlist.ToList();
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(mapGrid.grid[y, x + 1]);
                    mapGrid.grid[y, x + 1].isWalkable = true;
                    queue.Enqueue(temp2);

                }


            }
            if ((y + 1 >= 0 && y + 1 < MapGrid.Rows && x >= 0 && x < MapGrid.Columns) && temp.moveleft > 0 && !(mapGrid.grid[y + 1, x].isWalkable))
            {
                int moveleft = temp.moveleft - 1; //TerrainsUtil.MoveCost[mapGrid.grid[y + 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];
                if (mapGrid.grid[y + 1, x].occupantUnit != null && mapGrid.grid[y + 1, x].occupantUnit.playerNumber != GameController.Instance.playerTurn)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y + 1, x]);
                    temp2.you.Pathlist = temp.you.Pathlist.ToList() ;
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(mapGrid.grid[y + 1, x]);
                    mapGrid.grid[y + 1, x].isWalkable = true;
                    queue.Enqueue(temp2);

                }


            }
            if ((y >= 0 && y < MapGrid.Rows && x - 1 >= 0 && x < MapGrid.Columns) && temp.moveleft > 0 && !(mapGrid.grid[y, x - 1].isWalkable))
            {
                int moveleft = temp.moveleft - 1;//TerrainsUtil.MoveCost[mapGrid.grid[y , x - 1].occupantTerrain.TerrainIndex, unit.unitIndex];
                if (mapGrid.grid[y, x - 1].occupantUnit != null && mapGrid.grid[y, x - 1].occupantUnit.playerNumber != GameController.Instance.playerTurn)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y, x - 1]);
                    temp2.you.Pathlist = temp.you.Pathlist.ToList();
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(mapGrid.grid[y, x - 1]);
                    mapGrid.grid[y, x - 1].isWalkable = true;
                    queue.Enqueue(temp2);

                }


            }



        }



    }







}