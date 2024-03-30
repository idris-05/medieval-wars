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

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE)
        {
            Debug.Log("cell clicked on move state");

            // lazem had l'ordre f les appels sinon r7 tne7i liste ta3 walkable grid cells

            ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(UnitController.Instance.selectedUnit.walkableGridCells);
            MovementSystem.Instance.Movement(UnitController.Instance.selectedUnit, cellThatGotClickedOn.row, cellThatGotClickedOn.column);

            CancelScript.Instance.Cancel();
        }

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.DROP)
        {
            Debug.Log("cell clicked on drop state");

            // lazem drop mdirolha kima move 3ndna method movement f move system w kima attack system , psq fiha chwya 5dma drop .
            UnitTransport unitTransport = UnitController.Instance.selectedUnit as UnitTransport;

            ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(unitTransport.dropableCells);

            unitTransport.ResetDropableCells();
            unitTransport.Drop(cellThatGotClickedOn);

            CancelScript.Instance.Cancel();
        }


    }

}