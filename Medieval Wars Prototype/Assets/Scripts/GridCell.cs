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

        Debug.Log("GridCellClicked");
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE)
        {
            // lazem had l'orodre f les appeles sinon r7 tne7i lis ta3 walkable grid cells
            ManageInteractableObjects.Instance.ResetGridCellsBackToTheirOriginalLayerAfterMoveState(UnitController.Instance.selectedUnit);
            MovementSystem.Instance.Movement(UnitController.Instance.selectedUnit, row, column);

            CancelScript.Instance.Cancel();
        }


    }




    // Method to highlight the GridCell when the mouse hovers over it
    //!- if Any One Can Do This : Plaese Find A Better Way For Highlighting Things
    public void Highlight()
    {
        // Change the color of the GridCell to the highlighted color and the properties of the GridCell
        // rend.color = highlightedColor;
        rend.color = Color.green;
        // isHighlighted = true;
        isWalkable = true;
    }

    // Method to reset the GridCell to its original state 
    public void ResetGridCell()
    {
        // reset the color ( the color by default ) and the properties of the GridCell
        rend.color = Color.white;
        isWalkable = false;
        // isHighlighted = false;
    }

    public void MakeGridCellInteractableWhileInMoveState()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = -3;
        transform.position = newPosition;
    }

    public void ResetGridCellBackToTheirOriginalLayerAfterMoveState()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = 0;
        transform.position = newPosition;
    }



}