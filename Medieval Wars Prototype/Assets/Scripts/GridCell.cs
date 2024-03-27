using UnityEngine;




public class GridCell : MonoBehaviour
{

    public SpriteRenderer rend;

    public bool isWalkable;   //!!! ?????? wch rana ndiro bih hada 

    public int row;
    public int column;

    public Unit occupantUnit;

    public Terrain occupantTerrain;

    public int moveCost = 1; // for now

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }


    void OnMouseDown()
    {

        //! GETWALKABLE TILES YOU NEED TO CHECK IF A LOADED TRANSPORTER IS THERE AHMED AND RAYANE !!!!!!!!!!!

        Debug.Log("GridCell Clicked");

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE)
        {
            Debug.Log("cell clicked on move state");

            // lazem had l'ordre f les appels sinon r7 tne7i liste ta3 walkable grid cells

            ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(UnitController.Instance.selectedUnit.walkableGridCells);
            MovementSystem.Instance.Movement(UnitController.Instance.selectedUnit, row, column);

            CancelScript.Instance.Cancel();
        }

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.DROP)
        {
            Debug.Log("cell clicked on drop state");


            UnitTransport unitTransport = UnitController.Instance.selectedUnit as UnitTransport;
            unitTransport.Drop(this);
            ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(unitTransport.dropableCells);

            CancelScript.Instance.Cancel();
        }





    }




    //!- if Any One Can Do This : Plaese Find A Better Way For Highlighting Things
    public void HighlightAsWalkable()
    {
        rend.color = Color.green;
        isWalkable = true;
    }

    public void HighlightAsDropable()
    {
        rend.color = Color.blue;
    }


    public void HighlightAsAttackable()
    {
        rend.color = Color.red;
    }


    public void ResetHighlitedCell()
    {
        rend.color = Color.white;
    }
   
    
    
    // Method to reset the GridCell to its original state  
    public void ResetCellAttributsInEndTurn()
    {
        rend.color = Color.white;
        isWalkable = false;
        ResetHighlitedCell();
        // isHighlighted = false;
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