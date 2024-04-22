using System.Collections.Generic;
using UnityEngine;




public class GridCell : MonoBehaviour
{

    public GridCellView gridCellView;

     public bool isWalkable;   //!!! ?????? wch rana ndiro bih hada ,, update : raho 9rib ytir .

    public int row;
    public int column;

    public Unit occupantUnit;
    public Terrain occupantTerrain;
    public List<GridCell> Pathlist = new List<GridCell>();   

    public int moveCost = 1; //!!!!!!!!!!!! for now

    void Start()
    {
        gridCellView = GetComponent<GridCellView>();
    }




    // Method to reset the GridCell to its original state  
    public void ResetCellAttributsInEndTurn()
    {
        // isWalkable = false; // hada yji ghir hna , makalh ndiroh true f highlight as walkable .
        gridCellView.ResetHighlitedCell();
    }



    public void MakeCellInteractable()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = -3;
        transform.position = newPosition;
    }

    public void ResetCellBackToTheirOriginalLayer()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = 0;
        transform.position = newPosition;
    }



}