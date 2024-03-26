using UnityEngine;

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
        if (UnitController.Instance.selectedUnit != null)
        {
            // capable nzido 3fayess w7do5rin hna , 3la 7ssab ida n5loh y9der yclicker 3la cancel f wsst l5dma ta3o wla non .
            // meme tani 3la 7ssab transorter wla attack unit , resiti l3fayess li yssraw ki tclicker 3la button move , attack ... , bch ida 7eb ydir cancel yrje3 kolch kima kan 9bel ma yclicker 3la lbutton ( attack wla move button z3ma) .
            // cancel hadi fiha chwya 5dma ... ida 7bina n5loh ydir cancel .( r nmodifyiw bzaf 3feyess hna )
            UnitController.Instance.selectedUnit.unitView.ResetHighlightedUnit();
            UnitController.Instance.selectedUnit.ResetWalkableGridCells();

            if (UnitController.Instance.selectedUnit is UnitAttack unitAttack)
            {
                unitAttack.ResetHighlightedEnemyInRange();
            }

            if (UnitController.Instance.selectedUnit is UnitTransport unitTransport)
            {
                unitTransport.ResetSuppliableUnits();
            }

        }

        UnitController.Instance.selectedUnit = null;
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;

        ButtonsUI.Instance.HideButtons();
        ButtonsUI.Instance.buttonsToDisplay.Clear();
        ManageInteractableObjects.Instance.DesctivateBlockInteractionsLayer();
    }
}
