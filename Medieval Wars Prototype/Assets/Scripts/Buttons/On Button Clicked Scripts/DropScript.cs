using UnityEngine;

public class DropScript : MonoBehaviour
{
    
    public void OnDropButtonClicked()
    {
        UnitTransport unitTransport = UnitController.Instance.selectedUnit as UnitTransport;

        ButtonsUI.Instance.UpdateActionButtonsToDisplayWhenAButtonIsClicked(ActionsHandler.Instance.actionButtons[3]);

        unitTransport.GetdropableCells();

        unitTransport.HighlightDropableCells();
        ManageInteractableObjects.Instance.MakeOnlySpecificCellsInteractable(unitTransport.dropableCells);

        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.DROP;

        // wait for the player to click on a dropable cell (onCellSelection with state = drop). 


    }
}