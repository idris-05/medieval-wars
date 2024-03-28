using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public void OnAttackButtonClicked()
    {
        UnitAttack unitAttack = UnitController.Instance.selectedUnit as UnitAttack;

        ButtonsUI.Instance.UpdateActionButtonsToDisplayWhenAButtonIsClicked(ActionsHandler.Instance.actionButtons[1]); //! REMOVES ATTACK BUTTON

        unitAttack.GetEnemiesInRange();
        unitAttack.HighlightEnemyInRange();
        ManageInteractableObjects.Instance.MakeOnlySpecificUnitsInteractable(unitAttack.enemiesInRange);
        Debug.Log("only attackables are clickable now");

        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK;

        // wait for onUnitSelected with state = attack  in the unitcontroller
        //! I THINK WE SHOULD FIND A WAY TO STOP EVERY INTERACTION IN THE GAME UNTIL HE INTERACTS WITH A UNIT 
        //! I KNOW WE ARE ALREADY DOING THIS WITH THE LAYER BUT I WANT SOMETHIGN DONE WITH CODING
        //! AND I ALSO WANT THIS FUNCTION AND THE OnMoveButtonClicked TO WAIT FOR THE DESIRED OnMouseDOwn()
        //! THEN THE ACTIONS THAT MUST HAPPEN IN OnMouseDOwn WILL HAPPEN 
        //! BUT THEN I WANT THE REST OF THE WORK TO HAPPEN HERE NOT IN THE OTHER SCRIPTS I THINK WE SHOULD BE ABLE TO DO THAT 
        //! IT DOES NOT SEEM THAT COMPLICATED TO DO


    }



}