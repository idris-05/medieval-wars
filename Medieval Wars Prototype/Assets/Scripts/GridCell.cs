using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;




public class GridCell : MonoBehaviour
{

    public SpriteRenderer rend;

    public bool isWalkable;

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

            // lazem had l'orodre f les appeles sinon r7 tne7i lis ta3 walkable grid cells

            ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(UnitController.Instance.selectedUnit.walkableGridCells);
            MovementSystem.Instance.Movement(UnitController.Instance.selectedUnit, row, column);

            CancelScript.Instance.OnCancelButtonClicked();
        }

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.DROP)
        {
            Debug.Log("cell clicked on drop state");

            // lazem had l'orodre f les appeles sinon r7 tne7i lis ta3 walkable grid cells

            UnitTransport unitTransport = UnitController.Instance.selectedUnit as UnitTransport;
            unitTransport.Drop(this);
            ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(unitTransport.dropableCells);

            CancelScript.Instance.OnCancelButtonClicked();
        }





    }




    // Method to highlight the GridCell when the mouse hovers over it
    //!- if Any One Can Do This : Plaese Find A Better Way For Highlighting Things
    public void HighlightAsWalkable()
    {
        // Change the color of the GridCell to the highlighted color and the properties of the GridCell
        // rend.color = highlightedColor;
        rend.color = Color.green;
        // isHighlighted = true;
        isWalkable = true;
    }

    public void HighlightAsDropable()
    {
        rend.color = Color.blue;
    }

    // Method to reset the GridCell to its original state 
    public void ResetGridCell()
    {
        // reset the color ( the color by default ) and the properties of the GridCell
        rend.color = Color.white;
        isWalkable = false;
        // isHighlighted = false;
    }

    public void MakeCellInteractable()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = -15;
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