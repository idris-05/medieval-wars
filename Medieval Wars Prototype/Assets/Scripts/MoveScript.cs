using UnityEngine;
using UnityEngine.UI;

public class MoveScript : MonoBehaviour
{

    public MovementSystem movementSystem;
    public Button moveButton;

    void Start()
    {
        movementSystem = FindObjectOfType<MovementSystem>();
    }




    // EventSystem . 
    public void OnMoveButtonDown()
    {
        Debug.Log("Move button pressed");

        ButtonsUI.Instance.UpdateButtonsDisplayWhenAButtonClicked(moveButton);

        movementSystem.GetWalkableTilesMethod(UnitController.Instance.selectedUnit);

        UnitController.Instance.selectedUnit.unitView.HighlightWalkablesCells();

        ManageInteractableObjects.Instance.MakeOnlySpecificCellsInteractable(UnitController.Instance.selectedUnit.walkableGridCells);

        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.MOVE;

        // wait clicking one cell .

    }

}