using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public void OnAttackButtonClicked()
    {
        UnitAttack unitAttack = UnitController.Instance.selectedUnit as UnitAttack;

        ButtonsUI.Instance.UpdateButtonsDisplayWhenAButtonClicked(ActionsHandler.Instance.actionButtons[1]);

        unitAttack.GetEnemies();

        unitAttack.HighlightEnemyInRange();
        ManageInteractableObjects.Instance.MakeOnlySpecificUnitsInteractable(unitAttack.enemiesInRange);

        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK;

        // wait for onUnitSelected with state = attack  in the unitcontroller


    }



}