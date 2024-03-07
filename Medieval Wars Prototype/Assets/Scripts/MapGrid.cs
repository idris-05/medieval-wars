using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class MapGrid : MonoBehaviour
{

    public GridCell[,] grid;
    public static int Vertical, Horizontal, Columns, Rows;
    public void initialiseMapGrid()
    {
        Vertical = (int)Camera.main.orthographicSize;         //  unite de calcul : metres
        Horizontal = Vertical * Screen.width / Screen.height; //  unite de calcul : metres
        Columns = Horizontal * 2;
        Rows = Vertical * 2;

        grid = new GridCell[Rows, Columns];

    }


}
