using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Unity.IO;
using System.Linq;
using System;
using Unity.Collections;
using System.Linq.Expressions;

public class ArrowSystem : MonoBehaviour
{
    MapGrid mapGrid;
    [SerializeField] public GameObject[] Arrowprefabs = new GameObject[15];


    enum ArrowDirection
    {
        NONE,
        Vertical,
        Horizontal,
        Start_right,
        Start_left,
        Start_up,
        Start_down,
        Up,
        Down,
        Left,
        Right,
        Right_Down,
        Right_Up,
        Left_Down,
        Left_Up,

    }
    void Start()
    {
        mapGrid = FindObjectOfType<MapGrid>();
    }

    public static GameObject Spawn_arrow(int row, int col, GameObject arrowprefab)
    {

        GameObject arrow = Instantiate(arrowprefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);
        arrow.gameObject.AdjustSpriteSize();
        return arrow;

    }
    public static void DestroyArrow(GameObject arrow)
    {
        Destroy(arrow);
    }
    public int DrawautoPath(List<GridCell> Path, GameObject[] arrowprefabs, List<GridCell> cellsPath, List<GameObject> arrows, Unit unit)
    {
        int moveleft = unit.moveRange;
        foreach (GameObject item in arrows)
        {
            Destroy(item);
        }
        arrows.Clear();
        cellsPath.Clear();
        foreach (GridCell cell in Path)
        {
            if (cell == Path[0])
            {
                if (Path[1].column == cell.column)
                {
                    if (Path[1].row > cell.row)
                    {
                        arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[6]));
                    }
                    else
                    {
                        arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[5]));
                    }
                }
                else
                {
                    if (Path[1].column > cell.column)
                    {
                        arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[3]));
                    }
                    else
                    {
                        arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[4]));
                    }
                }
            }
            else
            {
                if (Path.IndexOf(cell) < Path.Count - 1)
                {
                    if (Path[Path.IndexOf(cell) - 1].column == Path[Path.IndexOf(cell) + 1].column)
                    {

                        arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[1]));

                    }
                    else
                    {
                        if (Path[Path.IndexOf(cell) - 1].row == Path[Path.IndexOf(cell) + 1].row)
                        {

                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[2]));

                        }
                        else
                        {
                            if (Path[Path.IndexOf(cell) - 1].column > cell.column)
                            {
                                if (Path[Path.IndexOf(cell) + 1].row > cell.row)
                                {
                                    arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[13]));

                                }
                                else
                                {
                                    arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[14]));

                                }
                            }
                            else
                            {
                                if (Path[Path.IndexOf(cell) - 1].column < cell.column)
                                {
                                    if (Path[Path.IndexOf(cell) + 1].row > cell.row)
                                    {
                                        arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[11]));

                                    }
                                    else
                                    {
                                        arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[12]));

                                    }

                                }
                                else
                                {
                                    if (Path[Path.IndexOf(cell) - 1].row > cell.row)
                                    {
                                        if (Path[Path.IndexOf(cell) + 1].column > cell.column)
                                        {
                                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[13]));
                                        }
                                        else
                                        {
                                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[11]));
                                        }

                                    }
                                    else
                                    {
                                        if (Path[Path.IndexOf(cell) + 1].column > cell.column)
                                        {
                                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[14]));
                                        }
                                        else
                                        {
                                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[12]));
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Path[Path.Count - 2].column == cell.column)
                    {

                        if (Path[Path.Count - 2].row > cell.row)
                        {

                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[7]));
                        }
                        else
                        {
                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[8]));
                        }
                    }
                    else
                    {
                        if (Path[Path.Count - 2].column > cell.column)
                        {
                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[9]));
                        }
                        else
                        {
                            arrows.Add(Spawn_arrow(cell.row, cell.column, arrowprefabs[10]));
                        }
                    }
                }
            }
        }
        foreach (GridCell item in Path)
        {
            cellsPath.Add(item);
            if (item != Path[0])
            {
                moveleft = moveleft - TerrainsUtils.MoveCost[item.occupantTerrain.TerrainIndex, unit.unitIndex];
            }
        }
        return moveleft;
    }
    public struct Point
    {
        public int x;
        public int y;

        public int moveleft;
    }

    public Point DrawArrow(GameObject[] arrowprefabs, int a, int b, List<GridCell> cellPath, List<GameObject> rrow, int movelft)
    {
        Unit unit = UnitController.Instance.selectedUnit;
        Point point = new Point();
        point.x = a;
        point.y = b;
        point.moveleft = movelft;
        int moveleft = movelft;
        int x = a;
        int y = b;
        if (!(unit == null))
        {
            List<GridCell> Borders = unit.walkableGridCells;
            List<GridCell> cellsPath = cellPath;
            List<GameObject> Arrows = rrow;

            if (Arrows.Count == 0)
            {
                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[0]));
                cellsPath.Add(mapGrid.grid[y, x]);
            }
            // x and y   need to be updated (done)
            // move left need to be updated(done)
            // auto drawing path (done)
            // map borders(in process)
            if(  y - 1 < 50 && y -1 >= 0){
                bool robin;
                try{
                   robin = mapGrid.grid[y - 1, x].occupantUnit.playerOwner == unit.playerOwner;
                }catch(Exception e){
                    robin= false;
                }
            if (Input.GetKeyDown(KeyCode.W) && (Borders.Contains(mapGrid.grid[y - 1, x]) || robin) && moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y - 1, x].occupantTerrain.TerrainIndex, unit.unitIndex] >= 0 && !cellsPath.Contains(mapGrid.grid[y - 1, x]))
            {
                if (cellsPath.Count == 1)
                {
                    Debug.Log("got in");
                    Destroy(Arrows[0]);
                    Arrows.Remove(Arrows[0]);
                    Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[5]));
                    Arrows.Add(Spawn_arrow(y - 1, x, Arrowprefabs[7]));
                    cellsPath.Add(mapGrid.grid[y - 1, x]);
                    moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y - 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];

                    y = y - 1;
                    Debug.Log("moveleft :" + moveleft);
                }
                else
                {
                    if (Arrows.Count >= 2)
                    {
                        Debug.Log(" got in 2nd division");
                        GameObject A = Arrows[Arrows.Count - 1];
                        GridCell Gprevious = cellsPath[cellsPath.Count - 2];
                        GridCell Gcurrent = cellsPath[cellsPath.Count - 1];
                        Destroy(A);
                        Arrows.Remove(Arrows[Arrows.Count - 1]);

                        if (Gprevious.column == Gcurrent.column)
                        {
                            Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[1]));
                        }
                        else
                        {
                            if (Gprevious.column > Gcurrent.column)
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[14]));
                            }
                            else
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[12]));
                            }
                        }
                        Arrows.Add(Spawn_arrow(y - 1, x, Arrowprefabs[7]));
                        cellsPath.Add(mapGrid.grid[y - 1, x]);
                        moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y - 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];
                        Debug.Log("moveleft :" + moveleft);

                    }
                    y = y - 1;
                }
            }
            else if (cellsPath.Contains(mapGrid.grid[y - 1, x]) && Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("got in 3rd divsion");
                GridCell B = mapGrid.grid[y - 1, x];
                if (B.column == unit.col && B.row == unit.row)
                {
                    foreach (GameObject arrow in Arrows)
                    {
                        Destroy(arrow);
                    }
                    cellsPath.Clear();
                    Arrows.Clear();
                    Arrows.Add(Spawn_arrow(unit.row, unit.col, Arrowprefabs[0]));
                    cellsPath.Add(mapGrid.grid[unit.row, unit.col]);
                    moveleft = unit.moveRange;
                }
                else
                {   int max = cellsPath.Count;
                    

                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        cellsPath.RemoveAt(cellsPath.Count - 1);
                    }
                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        GameObject Tmp = Arrows[Arrows.Count -1];
                        Destroy(Tmp);
                        Arrows.RemoveAt(Arrows.Count -1);
                        
                    }
                    moveleft = unit.moveRange;
                    foreach (GridCell item in cellsPath)
                    {
                        if (item != cellsPath[0])
                        {
                            moveleft = moveleft - TerrainsUtils.MoveCost[item.occupantTerrain.TerrainIndex, unit.unitIndex];
                        }
                    }
                    Destroy(Arrows[Arrows.Count - 1]);
                    Arrows.RemoveAt(Arrows.Count - 1);
                 //   Destroy(Arrows[cellsPath.IndexOf(B)]);
                  //  Arrows.Remove(Arrows[cellsPath.IndexOf(B)]);

                    if (cellsPath[cellsPath.IndexOf(B) - 1].column == B.column)
                    {
                        Arrows.Add(Spawn_arrow(y - 1, x, Arrowprefabs[8]));
                    }
                    else if (cellsPath[cellsPath.IndexOf(B) - 1].column > B.column)
                    {
                        Arrows.Add(Spawn_arrow(y - 1, x, Arrowprefabs[9]));
                    }
                    else
                    {
                        Arrows.Add(Spawn_arrow(y - 1, x, Arrowprefabs[10]));
                    }
                }
                y = y - 1;


            }
            else
            {
                if (moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y - 1, x].occupantTerrain.TerrainIndex, unit.unitIndex] < 0 && Borders.Contains(mapGrid.grid[y - 1, x]) && Input.GetKeyDown(KeyCode.W))
                {
                    Debug.Log("4th divsion");

                    moveleft = DrawautoPath(mapGrid.grid[y - 1, x].Pathlist, arrowprefabs, cellsPath, Arrows, unit);
                    y = y - 1;
                }

            }
            }
            if(x-1 < 50 && x-1 >= 0){
                bool robin;
                try{
                   robin = mapGrid.grid[y , x-1].occupantUnit.playerOwner == unit.playerOwner;
                }catch(Exception e){
                    robin= false;
                }
                

            if (Input.GetKeyDown(KeyCode.A) && (Borders.Contains(mapGrid.grid[y, x - 1])||robin) && moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x - 1].occupantTerrain.TerrainIndex, unit.unitIndex] >= 0 && !cellsPath.Contains(mapGrid.grid[y, x - 1]))
            {
                Debug.Log("got in left");
                if (cellsPath.Count == 1)
                {
                    Destroy(Arrows[0]);
                    Arrows.Remove(Arrows[0]);
                    Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[4]));
                    Arrows.Add(Spawn_arrow(y, x - 1, Arrowprefabs[9]));
                    cellsPath.Add(mapGrid.grid[y, x - 1]);
                    moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x - 1].occupantTerrain.TerrainIndex, unit.unitIndex];
                    x = x - 1;
                }
                else
                {
                    if (Arrows.Count >= 2)
                    {
                        GameObject A = Arrows[Arrows.Count - 1];
                        GridCell Gprevious = cellsPath[cellsPath.Count - 2];
                        GridCell Gcurrent = cellsPath[cellsPath.Count - 1];
                        Destroy(A);
                        Arrows.Remove(Arrows[Arrows.Count - 1]);

                        if (Gprevious.row == Gcurrent.row)
                        {
                            Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[2]));
                        }
                        else
                        {
                            if (Gprevious.row > Gcurrent.row)
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[11]));
                            }
                            else
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[12]));
                            }
                        }
                        Arrows.Add(Spawn_arrow(y, x - 1, Arrowprefabs[9]));
                        cellsPath.Add(mapGrid.grid[y, x - 1]);
                        moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x - 1].occupantTerrain.TerrainIndex, unit.unitIndex];

                    }
                    x = x - 1;
                }
            }
            else if (cellsPath.Contains(mapGrid.grid[y, x - 1]) && Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("got in left");
                GridCell B = mapGrid.grid[y, x - 1];
                if (B.column == unit.col && B.row == unit.row)
                {
                    foreach (GameObject arrow in Arrows)
                    {
                        Destroy(arrow);
                    }
                    cellsPath.Clear();
                    Arrows.Clear();
                    Arrows.Add(Spawn_arrow(unit.row, unit.col, Arrowprefabs[0]));
                    cellsPath.Add(mapGrid.grid[unit.row, unit.col]);
                    moveleft = unit.moveRange;
                }
                else
                {

                    int max = cellsPath.Count;
                    

                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        cellsPath.RemoveAt(cellsPath.Count - 1);
                    }
                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        GameObject Tmp = Arrows[Arrows.Count -1];
                        Destroy(Tmp);
                        Arrows.RemoveAt(Arrows.Count -1);
                        
                    }
                    moveleft = unit.moveRange;
                    foreach (GridCell item in cellsPath)
                    {
                        if (item != cellsPath[0])
                        {
                            moveleft = moveleft - TerrainsUtils.MoveCost[item.occupantTerrain.TerrainIndex, unit.unitIndex];
                        }
                    }
                    Destroy(Arrows[Arrows.Count - 1]);
                    Arrows.RemoveAt(Arrows.Count - 1);
                 //   Destroy(Arrows[cellsPath.IndexOf(B)]);
                  //  Arrows.Remove(Arrows[cellsPath.IndexOf(B)]);
                    if (cellsPath[cellsPath.IndexOf(B) - 1].row == B.row)
                    {
                        Arrows.Add(Spawn_arrow(y, x - 1, Arrowprefabs[10]));
                    }
                    else if (cellsPath[cellsPath.IndexOf(B) - 1].row > B.row)
                    {
                        Arrows.Add(Spawn_arrow(y, x - 1, Arrowprefabs[7]));
                    }
                    else
                    {
                        Arrows.Add(Spawn_arrow(y, x - 1, Arrowprefabs[8]));
                    }
                }
                x = x - 1;


            }
            
            else
            {
                if (moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x - 1].occupantTerrain.TerrainIndex, unit.unitIndex] < 0 && Borders.Contains(mapGrid.grid[y, x - 1]) && Input.GetKeyDown(KeyCode.A))
                {
                    Debug.Log("got in left");
                    moveleft = DrawautoPath(mapGrid.grid[y, x - 1].Pathlist, arrowprefabs, cellsPath, Arrows, unit);
                    x = x - 1;
                }


            }
            }







            if(x+1 < 50 && x+1>=0){
                bool robin;
                try{
                   robin = mapGrid.grid[y , x+1].occupantUnit.playerOwner == unit.playerOwner;
                }catch(Exception e){
                    robin= false;
                }

            if (Input.GetKeyDown(KeyCode.D) && (Borders.Contains(mapGrid.grid[y, x + 1]) || robin) && moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x + 1].occupantTerrain.TerrainIndex, unit.unitIndex] >= 0 && !cellsPath.Contains(mapGrid.grid[y, x + 1]))
            {
                Debug.Log("got in right");
                if (cellsPath.Count == 1)
                {
                    Destroy(Arrows[0]);
                    Arrows.Remove(Arrows[0]);
                    Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[3]));
                    Arrows.Add(Spawn_arrow(y, x + 1, Arrowprefabs[10]));
                    cellsPath.Add(mapGrid.grid[y, x + 1]);
                    moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x + 1].occupantTerrain.TerrainIndex, unit.unitIndex];
                    x = x + 1;
                }
                else
                {
                    if (Arrows.Count >= 2)
                    {
                        GameObject A = Arrows[Arrows.Count - 1];
                        GridCell Gprevious = cellsPath[cellsPath.Count - 2];
                        GridCell Gcurrent = cellsPath[cellsPath.Count - 1];
                        Destroy(A);
                        Arrows.Remove(Arrows[Arrows.Count - 1]);

                        if (Gprevious.row == Gcurrent.row)
                        {
                            Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[2]));
                        }
                        else
                        {
                            if (Gprevious.row > Gcurrent.row)
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[13]));
                            }
                            else
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[14]));
                            }
                        }
                        Arrows.Add(Spawn_arrow(y, x + 1, Arrowprefabs[10]));
                        cellsPath.Add(mapGrid.grid[y, x + 1]);
                        moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x + 1].occupantTerrain.TerrainIndex, unit.unitIndex];

                    }
                    x = x + 1;
                }
            }
            else if (cellsPath.Contains(mapGrid.grid[y, x + 1]) && Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("got in right");
                GridCell B = mapGrid.grid[y, x + 1];
                if (B.column == unit.col && B.row == unit.row)
                {
                    foreach (GameObject arrow in Arrows)
                    {
                        Destroy(arrow);
                    }
                    cellsPath.Clear();
                    Arrows.Clear();
                    Arrows.Add(Spawn_arrow(unit.row, unit.col, Arrowprefabs[0]));
                    cellsPath.Add(mapGrid.grid[unit.row, unit.col]);
                    moveleft = unit.moveRange;
                }
                else
                {

                    int max = cellsPath.Count;
                    

                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        cellsPath.RemoveAt(cellsPath.Count - 1);
                    }
                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        GameObject Tmp = Arrows[Arrows.Count -1];
                        Destroy(Tmp);
                        Arrows.RemoveAt(Arrows.Count -1);
                        
                    }
                    moveleft = unit.moveRange;
                    foreach (GridCell item in cellsPath)
                    {
                        if (item != cellsPath[0])
                        {
                            moveleft = moveleft - TerrainsUtils.MoveCost[item.occupantTerrain.TerrainIndex, unit.unitIndex];
                        }
                    }
                    Destroy(Arrows[Arrows.Count - 1]);
                    Arrows.RemoveAt(Arrows.Count - 1);
                 //   Destroy(Arrows[cellsPath.IndexOf(B)]);
                  //  Arrows.Remove(Arrows[cellsPath.IndexOf(B)]);
                    if (cellsPath[cellsPath.IndexOf(B) - 1].row == B.row)
                    {
                        Arrows.Add(Spawn_arrow(y, x + 1, Arrowprefabs[9]));
                    }
                    else if (cellsPath[cellsPath.IndexOf(B) - 1].row > B.row)
                    {
                        Arrows.Add(Spawn_arrow(y, x + 1, Arrowprefabs[7]));
                    }
                    else
                    {
                        Arrows.Add(Spawn_arrow(y, x + 1, Arrowprefabs[8]));
                    }
                }
                x = x + 1;


            }
            else
            {
                if (moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y, x + 1].occupantTerrain.TerrainIndex, unit.unitIndex] < 0 && Borders.Contains(mapGrid.grid[y, x + 1]) && Input.GetKeyDown(KeyCode.D))
                {
                    Debug.Log("got in right");

                    moveleft = DrawautoPath(mapGrid.grid[y, x + 1].Pathlist, arrowprefabs, cellsPath, Arrows, unit);
                    x = x + 1;
                }


            }
            }



















          if(y+1 <50 && y+1>=0 ){
            bool robin;
                try{
                   robin = mapGrid.grid[y + 1, x].occupantUnit.playerOwner == unit.playerOwner;
                }catch(Exception e){
                    robin= false;
                }
            if (Input.GetKeyDown(KeyCode.S) && (Borders.Contains(mapGrid.grid[y + 1, x]) || robin) && moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y + 1, x].occupantTerrain.TerrainIndex, unit.unitIndex] >= 0 && !cellsPath.Contains(mapGrid.grid[y + 1, x]))
            {
                Debug.Log("got in down");
                if (cellsPath.Count == 1)
                {
                    Destroy(Arrows[0]);
                    Arrows.Remove(Arrows[0]);
                    Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[6]));
                    Arrows.Add(Spawn_arrow(y + 1, x, Arrowprefabs[8]));
                    cellsPath.Add(mapGrid.grid[y + 1, x]);
                    moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y + 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];
                    y = y + 1;
                }
                else
                {
                    if (Arrows.Count >= 2)
                    {
                        GameObject A = Arrows[Arrows.Count - 1];
                        GridCell Gprevious = cellsPath[cellsPath.Count - 2];
                        GridCell Gcurrent = cellsPath[cellsPath.Count - 1];
                        Destroy(A);
                        Arrows.Remove(Arrows[Arrows.Count - 1]);

                        if (Gprevious.column == Gcurrent.column)
                        {
                            Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[1]));
                        }
                        else
                        {
                            if (Gprevious.column > Gcurrent.column)
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[13]));
                            }
                            else
                            {
                                Arrows.Add(Spawn_arrow(y, x, Arrowprefabs[11]));
                            }
                        }
                        Arrows.Add(Spawn_arrow(y + 1, x, Arrowprefabs[8]));
                        cellsPath.Add(mapGrid.grid[y + 1, x]);
                        moveleft = moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y + 1, x].occupantTerrain.TerrainIndex, unit.unitIndex];

                    }
                    y = y + 1;
                }
            }
            else if (cellsPath.Contains(mapGrid.grid[y + 1, x]) && Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("got in down 3rd layer");
                GridCell B = mapGrid.grid[y + 1, x];
                if (B.column == unit.col && B.row == unit.row)
                {
                    foreach (GameObject arrow in Arrows)
                    {
                        Destroy(arrow);
                    }
                    cellsPath.Clear();
                    Arrows.Clear();
                    Arrows.Add(Spawn_arrow(unit.row, unit.col, Arrowprefabs[0]));
                    cellsPath.Add(mapGrid.grid[unit.row, unit.col]);
                    moveleft = unit.moveRange;
                }
                else
                {

                    int max = cellsPath.Count;
                    

                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        cellsPath.RemoveAt(cellsPath.Count - 1);
                    }
                    for (int i = cellsPath.IndexOf(B) + 1; i < max; i++)
                    {
                        GameObject Tmp = Arrows[Arrows.Count -1];
                        Destroy(Tmp);
                        Arrows.RemoveAt(Arrows.Count -1);
                        
                    }
                    moveleft = unit.moveRange;
                    foreach (GridCell item in cellsPath)
                    {
                        if (item != cellsPath[0])
                        {
                            moveleft = moveleft - TerrainsUtils.MoveCost[item.occupantTerrain.TerrainIndex, unit.unitIndex];
                        }
                    }
                    Destroy(Arrows[Arrows.Count - 1]);
                    Arrows.RemoveAt(Arrows.Count - 1);
                 //   Destroy(Arrows[cellsPath.IndexOf(B)]);
                  //  Arrows.Remove(Arrows[cellsPath.IndexOf(B)]);
                    if (cellsPath[cellsPath.IndexOf(B) - 1].column == B.column)
                    {
                        Arrows.Add(Spawn_arrow(y + 1, x, Arrowprefabs[7]));
                    }
                    else if (cellsPath[cellsPath.IndexOf(B) - 1].column > B.column)
                    {
                        Arrows.Add(Spawn_arrow(y + 1, x, Arrowprefabs[9]));
                    }
                    else
                    {
                        Arrows.Add(Spawn_arrow(y + 1, x, Arrowprefabs[10]));
                    }
                }
                y = y + 1;


            }
            else
            {
                if (moveleft - TerrainsUtils.MoveCost[mapGrid.grid[y + 1, x].occupantTerrain.TerrainIndex, unit.unitIndex] < 0 && Borders.Contains(mapGrid.grid[y + 1, x])  && Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("got in down");

                    moveleft = DrawautoPath(mapGrid.grid[y + 1, x].Pathlist, arrowprefabs, cellsPath, Arrows, unit);
                    y = y + 1;
                }


            }
          }


        }
        point.x = x;
        point.y = y;
        point.moveleft = moveleft; ;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(y + " and " + x + "and " + moveleft);
        }

        return point;


    }

}
