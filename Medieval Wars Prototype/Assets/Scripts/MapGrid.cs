using UnityEngine;

public class MapGrid : MonoBehaviour
{
    // map grid is a matrix of gridCell
    public GridCell[,] grid;

    public GridCell GridCellPrefab;

    public static int Vertical, Horizontal, Columns, Rows;


    public void CalculateMapGridSize()
    {
        Vertical = (int)Camera.main.orthographicSize;         //  unite de calcul : metres
        Horizontal = Vertical * Screen.width / Screen.height; //  unite de calcul : metres
        Columns = Horizontal * 2;
        Rows = Vertical * 2;

        grid = new GridCell[Rows, Columns];

    }

    public void InitialiseMapGridCells()
    {
        // Loop through each row and column of the map grid
        for (int row = 0; row < MapGrid.Rows; row++)
        {
            for (int col = 0; col < MapGrid.Columns; col++)
            {
                // Instantiate a GridCell prefab at the specified position
                GridCell gridCell = Instantiate(GridCellPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f, 0), Quaternion.identity);

                // Adjust the sprite size of the instantiated GridCell
                gridCell.gameObject.AdjustSpriteSize();

                // Set the name of the GridCell
                gridCell.name = $"GridCell ({row}, {col})";

                // Set the row and column properties of the GridCell
                gridCell.row = row;
                gridCell.column = col;

                //!!!!!!!!!!!!!!!! this affectation is temporary
                // gridCell.terrain = Instantiate(TerrainGrassPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);

                // Assign the GridCell to the corresponding position in the map grid
                grid[row, col] = gridCell;
            }
        }
    }



}
