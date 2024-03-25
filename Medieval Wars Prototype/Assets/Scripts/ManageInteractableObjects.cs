using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageInteractableObjects : MonoBehaviour
{

    private static ManageInteractableObjects instance;
    public static ManageInteractableObjects Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<ManageInteractableObjects>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("ManageIntereactableObjects");
                    instance = obj.AddComponent<ManageInteractableObjects>();
                }
            }
            return instance;
        }
    }


    public GameObject BlockInteractablesLayer;

    public void ActivateBlockInteractionsLayer()
    {
        BlockInteractablesLayer.SetActive(true);
    }

    public void DesctivateBlockInteractionsLayer()
    {
        BlockInteractablesLayer.SetActive(false);
    }

    public void MakeGridCellsInteractableWhileInMoveState(Unit unitInMoveState)
    {
        foreach (GridCell gridcell in unitInMoveState.walkableGridCells)
        {
            gridcell.MakeGridCellInteractableWhileInMoveState();
        }
    }

    public void ResetGridCellsBackToTheirOriginalLayerAfterMoveState(Unit unitInMoveState)
    {
        foreach (GridCell gridcell in unitInMoveState.walkableGridCells)
        {
            gridcell.ResetGridCellBackToTheirOriginalLayerAfterMoveState();
        }
    }

}
