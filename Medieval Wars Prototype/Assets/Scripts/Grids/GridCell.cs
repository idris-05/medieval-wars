using UnityEngine;




public class GridCell : MonoBehaviour
{

    public GridCellView gridCellView;

    public bool isWalkable;   //!!! ?????? wch rana ndiro bih hada 

    public int row;
    public int column;

    public Unit occupantUnit;

    public Terrain occupantTerrain;

    public int moveCost = 1; // for now

    void Start()
    {
        gridCellView = GetComponent<GridCellView>();
    }




    void OnMouseDown()
    {
        GridCellController.Instance.OnCellSelection(this);
        //! GETWALKABLE TILES YOU NEED TO CHECK IF A LOADED TRANSPORTER IS THERE ____AHMED AND RAYANE !!!!!!!!!!
    }




    // Method to reset the GridCell to its original state  
    public void ResetCellAttributsInEndTurn()
    {
        isWalkable = false; // hada yji ghir hna , makalh ndiroh true f highlight as walkable .
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