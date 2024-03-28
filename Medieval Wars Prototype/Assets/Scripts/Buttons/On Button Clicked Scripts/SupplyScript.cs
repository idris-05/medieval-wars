using UnityEngine;
using UnityEngine.UI;

public class SupplyScript : MonoBehaviour
{

    public Button supplyButton;

    public void OnMouseDown()
    {
        Debug.Log("Supply button pressed");

        ButtonsUI.Instance.UpdateActionButtonsToDisplayWhenAButtonIsClicked(supplyButton) ;

        UnitTransport supplyingUnit = UnitController.Instance.selectedUnit as UnitTransport;

        supplyingUnit.GetSuppliableUnits();

        supplyingUnit.HighlightSuppliableUnits();

        ManageInteractableObjects.Instance.MakeOnlySpecificUnitsInteractable(supplyingUnit.suppliableUnits);

        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.SUPPLY;

    }



}
