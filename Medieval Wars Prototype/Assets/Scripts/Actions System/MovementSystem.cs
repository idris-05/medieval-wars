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


    public void Movement(Unit unit, int row, int col)
    {

        if (MapGrid.Instance.grid[row, col].occupantUnit is UnitTransport unitTransport) // If I try to move to a cell where there is a tronsporter we  will load the unit on the transporter .
        {

            //!!!!!! We are Sure here that the transporter is empty because it gets verified in GetWalkableTiles :  && unitTransport.loadedUnit == null

            if (UnitUtil.CanLoadThatUnit[unitTransport.unitIndex, unit.unitIndex])
            {
                unit.unitView.ResetHighlitedWalkableCells();

                unit.PrepareUnitToGetLoadedInTransporter();
                unit.unitView.AnimateMovement(row, col, true);

                return;
            }
        }

        // case where u just move to a gridcell

        //! i found a problem here , after moving the unit that just moved has a z of 0 , this is due to being moved with a Vector2
        //! therefore i'll Make A method named ResetUnitPositionInLayersAfterMovement to reset it back to it's original position in layer
        //! please don't change my method names , they are as explicit as possible

        //! hna n7ssbo moveCost ( n7ssbo ch7al n9ssolha men ration ki mchat ) wnmdoh parametre lel UpdateAttributsAfterMoving .
        unit.UpdateAttributsAfterMoving(row, col);
        unit.unitView.AnimateMovement(row, col, false);
        unit.unitView.ResetHighlitedWalkableCells();

    }

    public void GetWalkableTiles(Unit unit)
    {

        //int turn = gm.playerTurn;
        Player currentPlayer = GameController.Instance.currentPlayerInControl;
        int y = unit.row;
        int x = unit.col;
        int Mrange;
        if (unit.ration < unit.moveRange)
        {
            Mrange = (int)unit.ration;
        }
        else
        {
            Mrange = unit.moveRange;
        }
        MIcell temp = new MIcell(Mrange, MapGrid.Instance.grid[y, x]);
        MIcell temp2 = new MIcell(Mrange, MapGrid.Instance.grid[y, x]);


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


            if (y - 1 >= 0 && y - 1 < MapGrid.Instance.Rows && x >= 0 && x < MapGrid.Instance.Columns && temp.moveleft > 0 && !MapGrid.Instance.grid[y - 1, x].isWalkable)
            {
                // int moveleft = temp.moveleft - 1;//TerrainsUtil.MoveCost[mapGrid.grid[y - 1,x].occupantTerrain.TerrainIndex, unit.unitIndex];
                int moveleft = temp.moveleft - TerrainsUtils.MoveCost[MapGrid.Instance.grid[y - 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];
                if (MapGrid.Instance.grid[y - 1, x].occupantUnit != null && MapGrid.Instance.grid[y - 1, x].occupantUnit.playerOwner != currentPlayer)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, MapGrid.Instance.grid[y - 1, x]);
                    temp2.you.Pathlist = temp.you.Pathlist.ToList();
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(MapGrid.Instance.grid[y - 1, x]);
                    MapGrid.Instance.grid[y - 1, x].isWalkable = true;
                    queue.Enqueue(temp2);
                }


            }
            if (y >= 0 && y < MapGrid.Instance.Rows && x + 1 >= 0 && x + 1 < MapGrid.Instance.Columns && temp.moveleft > 0 && !MapGrid.Instance.grid[y, x + 1].isWalkable)
            {
                // int moveleft = temp.moveleft - 1;// TerrainsUtil.MoveCost[MapGrid.Instance.grid[y , x + 1].occupantTerrain.TerrainIndex, unit.unitIndex];
                int moveleft = temp.moveleft - TerrainsUtils.MoveCost[MapGrid.Instance.grid[y, x + 1].occupantTerrain.TerrainIndex, unit.unitIndex];
                if (MapGrid.Instance.grid[y, x + 1].occupantUnit != null && MapGrid.Instance.grid[y, x + 1].occupantUnit.playerOwner != currentPlayer)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, MapGrid.Instance.grid[y, x + 1]);
                    temp2.you.Pathlist = temp.you.Pathlist.ToList();
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(MapGrid.Instance.grid[y, x + 1]);
                    MapGrid.Instance.grid[y, x + 1].isWalkable = true;
                    queue.Enqueue(temp2);
                }


            }
            if (y + 1 >= 0 && y + 1 < MapGrid.Instance.Rows && x >= 0 && x < MapGrid.Instance.Columns && temp.moveleft > 0 && !MapGrid.Instance.grid[y + 1, x].isWalkable)
            {
                // int moveleft = temp.moveleft - 1; //TerrainsUtil.MoveCost[MapGrid.Instance.grid[y + 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];
                int moveleft = temp.moveleft - TerrainsUtils.MoveCost[MapGrid.Instance.grid[y + 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];
                if (MapGrid.Instance.grid[y + 1, x].occupantUnit != null && MapGrid.Instance.grid[y + 1, x].occupantUnit.playerOwner != currentPlayer)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, MapGrid.Instance.grid[y + 1, x]);
                    temp2.you.Pathlist = temp.you.Pathlist.ToList();
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(MapGrid.Instance.grid[y + 1, x]);
                    MapGrid.Instance.grid[y + 1, x].isWalkable = true;
                    queue.Enqueue(temp2);
                }


            }
            if (y >= 0 && y < MapGrid.Instance.Rows && x - 1 >= 0 && x < MapGrid.Instance.Columns && temp.moveleft > 0 && !MapGrid.Instance.grid[y, x - 1].isWalkable)
            {
                // int moveleft = temp.moveleft - 1;//TerrainsUtil.MoveCost[MapGrid.Instance.grid[y , x - 1].occupantTerrain.TerrainIndex, unit.unitIndex];
                int moveleft = temp.moveleft - TerrainsUtils.MoveCost[MapGrid.Instance.grid[y, x - 1].occupantTerrain.TerrainIndex, unit.unitIndex];

                if (MapGrid.Instance.grid[y, x - 1].occupantUnit != null && MapGrid.Instance.grid[y, x - 1].occupantUnit.playerOwner != currentPlayer)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, MapGrid.Instance.grid[y, x - 1]);
                    temp2.you.Pathlist = temp.you.Pathlist.ToList();
                    temp2.you.Pathlist.Add(temp2.you);
                    unit.walkableGridCells.Add(MapGrid.Instance.grid[y, x - 1]);
                    MapGrid.Instance.grid[y, x - 1].isWalkable = true;
                    queue.Enqueue(temp2);
                }

            }

        }

    }


}