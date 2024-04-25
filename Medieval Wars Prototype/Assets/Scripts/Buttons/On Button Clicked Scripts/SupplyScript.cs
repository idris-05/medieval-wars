using UnityEngine;
using UnityEngine.UI;

public class SupplyScript : MonoBehaviour
{

    public Button supplyButton;

    //! WE WILL NOT FORCE THE SUPPLY WHEN THE DAYS STARTS
    //! WHEN U PRESS SUPPLY IT AUTOMATICALY SUPPLIES THE UNITS THAT CAN BE SUPPLIED ( THIS IS BEACAUSE SUPPLY IS INFINITE AND IT DIRECTLY SUPPLIES TO THE MAX )
    //! WE NEED TO MAKE A UI ELEMENT THAT SHOWS THAT THE UNIT GOT SUPPLIED TO MAKE THE SUPPLY ACTION FEEL MORE ( insert adjective here (maybe real?) ) for the player

    public void OnMouseDown()
    {
        Debug.Log("Supply button pressed");

        ButtonsUI.Instance.UpdateActionButtonsToDisplayWhenAButtonIsClicked(supplyButton);

        UnitTransport supplyingUnit = UnitController.Instance.selectedUnit as UnitTransport;

        supplyingUnit.GetSuppliableUnits();
        supplyingUnit.SupplyAllSuppliableUnits();
        supplyingUnit.ResetSuppliableUnits();

        CancelScript.Instance.Cancel();
        
        supplyingUnit.TransitionToNumbState();
    }



}
