using UnityEngine;

public class MapGrid : MonoBehaviour
{

    private static MapGrid instance;
    public static MapGrid Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<MapGrid>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("MapGrid");
                    instance = obj.AddComponent<MapGrid>();
                }
            }
            return instance;
        }
    }



    public GridCell[,] grid;

    [SerializeField] public int Columns;
    [SerializeField] public int Rows;

    public void CreateMapGridCellsMatrix()
    {
        Debug.Log(Columns + " " + Rows);
        grid = new GridCell[Rows ,Columns]; 
    }

}
