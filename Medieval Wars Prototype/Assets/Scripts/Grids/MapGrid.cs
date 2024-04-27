using UnityEngine;

public class MapGrid : MonoBehaviour
{
    public GridCell[,] grid;

 


    public static int Columns;
    public static int Rows;

    public void CreateMapGridCellsMatrix()
    {
        Rows = MapManager.Instance.numberOfRowsInTheMap;
        Columns = MapManager.Instance.numberOfColumnsInTheMap;
        grid = new GridCell[Rows ,Columns]; 
    }

}
