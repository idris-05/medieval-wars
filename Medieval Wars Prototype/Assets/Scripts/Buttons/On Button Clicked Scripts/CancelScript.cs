using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CancelScript : MonoBehaviour
{
    private static CancelScript instance;
    public static CancelScript Instance
    {
        get
        {

            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<CancelScript>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("CancelScript");
                    instance = obj.AddComponent<CancelScript>();
                }
            }
            return instance;
        }
    }

    public void OnCancelButtonClicked()
    {
        switch (UnitController.Instance.CurrentActionStateBasedOnClickedButton)
        {
            case UnitUtil.ActionToDoWhenButtonIsClicked.NONE:

                CancelWhenNoButtonHasBeenPressedYet();

                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.MOVE:

                CancelAfterMoveButtonPressed();

                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK:

                CancelAfterAttackButtonPressed();

                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.DROP:

                CancelAfterDropButtonPressed();

                break;


            default:
                break;

        }
    }




    public void Cancel()
    {

        UnitController.Instance.selectedUnit.unitView.ResetHighlightedUnit();

        UnitController.Instance.selectedUnit = null;

        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;

        ButtonsUI.Instance.HideButtons();
        ButtonsUI.Instance.buttonsToDisplay.Clear();
        ManageInteractableObjects.Instance.DesctivateBlockInteractionsLayer();
    }


    public void CancelWhenNoButtonHasBeenPressedYet()
    {
        Cancel();
    }

    public void CancelAfterMoveButtonPressed()
    {
        ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(UnitController.Instance.selectedUnit.walkableGridCells);
        UnitController.Instance.selectedUnit.unitView.ResetHighlitedWalkableCells();
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;
        Cancel();
    }

    public void CancelAfterAttackButtonPressed()
    {
        UnitAttack unitAttack = UnitController.Instance.selectedUnit as UnitAttack;
        ManageInteractableObjects.Instance.ResetSpecificUnitsBackToTheirOriginalLayer(unitAttack.enemiesInRange);
        unitAttack.ResetHighlightedEnemyInRange();
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;
        Cancel();
    }

    public void CancelAfterDropButtonPressed()
    {
        UnitTransport unitTransport = UnitController.Instance.selectedUnit as UnitTransport;
        ManageInteractableObjects.Instance.ResetSpecificCellsBackToTheirOriginalLayer(unitTransport.dropableCells);
        unitTransport.ResetDropableCells();
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;
        Cancel();
    }


}
