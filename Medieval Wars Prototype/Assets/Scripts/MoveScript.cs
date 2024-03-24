using UnityEngine;

public class MoveScript : MonoBehaviour
{

    public MovementSystem movementSystem;

    void Start()
    {
        movementSystem = FindObjectOfType<MovementSystem>();
    }


    public void OnMoveButtonDown()
    {
        Debug.Log("Move button pressed");

        ButtonsUI.Instance.HideButtons();
        ButtonsUI.Instance.buttonsToDisplay.Clear();
        ButtonsUI.Instance.buttonsToDisplay.Add(ActionsHandler.Instance.actionButtons[5]);
        ButtonsUI.Instance.DisplayButtons();


        movementSystem.GetWalkableTilesMethod(UnitController.Instance.selectedUnit);
        ManageInteractableObjects.Instance.MakeGridCellsInteractableWhileInMoveState(UnitController.Instance.selectedUnit);
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.MOVE;



    }
}