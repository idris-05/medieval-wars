using UnityEngine;

public class GridCellController : MonoBehaviour
{
    private static GridCellController instance;
    public static GridCellController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<GridCellController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("GridCellController");
                    instance = obj.AddComponent<GridCellController>();
                }
            }
            return instance;
        }
    }


    public void OnCellSelection(GridCell cellThatGotClickedOn)
    {

        Debug.Log("GridCell Clicked");

        switch (UnitController.Instance.CurrentActionStateBasedOnClickedButton)
        {
            case UnitUtil.ActionToDoWhenButtonIsClicked.MOVE:
                Debug.Log("cell clicked on move state");
                ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(UnitController.Instance.selectedUnit.walkableGridCells);
                MovementSystem.Instance.Movement(UnitController.Instance.selectedUnit, cellThatGotClickedOn.row, cellThatGotClickedOn.column);
                CancelScript.Instance.Cancel();
                break;
                
            case UnitUtil.ActionToDoWhenButtonIsClicked.DROP:
                Debug.Log("cell clicked on drop state");
                UnitTransport unitTransport = UnitController.Instance.selectedUnit as UnitTransport;
                ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(unitTransport.dropableCells);
                unitTransport.ResetDropableCells();
                unitTransport.Drop(cellThatGotClickedOn);
                CancelScript.Instance.Cancel();
                break;

            default:
                break;
        }


    }

}