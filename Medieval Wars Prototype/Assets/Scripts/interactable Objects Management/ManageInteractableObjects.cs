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


    //!!!!!!!!! dir les valeurs lmssgmin
    // BLOCK INTERACTABLES LAYER         inactive  default
    // UNITS  CELLS TERRAIN BIULDINGS       0      default

    // BLOCK INTERACTABLES LAYER           -10     interactable
    // UNITS CELLS BIULDINGS               -15     interactable

    // BUTTONS                             -15     interactable
    // BUTTONS                           inactive  default


    // MAIN CAMERA                         -50     FIX 

    // 0 - 10  -50 .. homa valeur t3  transform.position.z  pour chaque ojbect


    public GameObject BlockInteractablesLayer;

    public void ActivateBlockInteractionsLayer()
    {
        BlockInteractablesLayer.SetActive(true);
    }
    public void DesctivateBlockInteractionsLayer()
    {
        BlockInteractablesLayer.SetActive(false);
    }



    public void MakeOnlySpecificCellsInteractable(List<GridCell> cellsToMakeInteractable)
    {
        cellsToMakeInteractable.ForEach(cell => cell.MakeCellInteractable());
    }
    public void ResetSpecificCellsBackToTheirOriginalLayer(List<GridCell> cellsToResetToOriginalLayer)
    {
        cellsToResetToOriginalLayer.ForEach(cell => cell.ResetCellBackToTheirOriginalLayer());
    }



    public void MakeOnlySpecificUnitsInteractable(List<Unit> unitsToMakeInteractable)
    {
        unitsToMakeInteractable.ForEach(unit => unit.unitView.MakeUnitInteractable());
    }
    public void ResetSpecificUnitsBackToTheirOriginalLayer(List<Unit> unitsToResetToOriginalLayer)
    {
        unitsToResetToOriginalLayer.ForEach(unit => unit.unitView.ResetUnitBackToTheirOriginalLayer());
    }




}
