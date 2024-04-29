using System.Collections;
using System.Linq;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    /*
        1. Private Constructor: The UnitController class has a private constructor, meaning it cannot be instantiated
        directly from outside the class. This prevents other parts of the code from creating instances of UnitController
        using the new keyword.

        2. Static Instance Variable: Inside the UnitController class, there's a private static variable instance of type 
        UnitController. This variable holds the single instance of UnitController that will be shared across the application.

        3. Public Static Property - Instance: The Instance property is a public static property of the UnitController class. 
        It provides access to the singleton instance of UnitController. When you access UnitController.Instance from 
        any part of your code, it returns the same instance of UnitController throughout the application.

        4. Lazy Initialization: The Instance property uses lazy initialization. When you first access UnitController.Instance,
        it checks if the instance variable is null. If it is null, meaning no instance of UnitController has been created yet,
        it creates a new instance of UnitController using the private constructor. Subsequent calls to Instance return
        the existing instance without creating a new one.


    */
    // Singleton instanceprivate static UnitController instance;
    // Public property to access the singleton instance


    private static UnitController instance;
    public static UnitController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<UnitController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("UnitController");
                    instance = obj.AddComponent<UnitController>();
                }
            }
            return instance;
        }
    }


    // private void Awake()
    // {
    //     // If there is an instance, and it's not me, delete myself.

    //     if (Instance != null && Instance != this)
    //     {
    //         Destroy(this);
    //     }
    //     else
    //     {
    //         Instance = this;
    //     }
    // }

    // Define a delegate for the event



    public Unit selectedUnit;
    public UnitUtil.ActionToDoWhenButtonIsClicked CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;


    // normal dependi men UnitUtil , psq howa li 3ndna fih hadok les info wkolch ,


    // !!! lazem script hada yt7et 3end kch gameobject fla scene , bch yt7sseb enable w ttexecuta start w awake , sinon lazem 7ta tdir l'appele lkch fonction ta3o bch yweli  enable : true.

    //! MAYBE MA YENDAROUCH LES BIJECTIONS LAHNA YENDAROU F DES SCRIPTS WA7DO5RIN

    //! GRIDCELL TAN TROU7 LES SWITCH KIMA HADI , BALAK N9SMOUHA KIMA HADA HIYA TAN

    public void OnUnitSelection(Unit unitThatGotClickedOn)
    {
        switch (CurrentActionStateBasedOnClickedButton)
        {
            case UnitUtil.ActionToDoWhenButtonIsClicked.NONE:

                // hna lazem Unit (hadi unitThatGotClickedOn) lazem tkon ta3ek , sinon mlazemch t9dr tchof l'enemie wch rah 9ader ydir
                //!!!! had les details ( commantaire li rah fo9i ) lazem meet ljay n7ddohom kamel .
                if (unitThatGotClickedOn.playerOwner != GameController.Instance.currentPlayerInControl) return;



                selectedUnit = unitThatGotClickedOn;

                ManageInteractableObjects.Instance.ActivateBlockInteractionsLayer();
                ActionsHandler.Instance.FillButtonsToDisplay(unitThatGotClickedOn);

                if (ButtonsUI.Instance.buttonsToDisplay.Any() == true)
                {
                    ButtonsUI.Instance.DisplayButtons();
                    selectedUnit.unitView.HighlightAsSelected();
                }
                else
                {
                    CancelScript.Instance.Cancel();
                }

                // wait for the player to click any button from the displayed buttons .


                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK:
                // we are sure that the selected unit is of type UnitAttack and not a UnitTranspor
                UnitAttack attackingUnit = selectedUnit as UnitAttack;

                ManageInteractableObjects.Instance.ResetSpecificUnitsBackToTheirOriginalLayer(attackingUnit.enemiesInRange);

                StartCoroutine(AttackAndFollowUp(attackingUnit, unitThatGotClickedOn));

                break;
                ;

                

            default:
                break;
        }

    }


    //! i needed this method because i need cancel and transition to numb state to be executed only after the attack coroutine ( because of animations )
    IEnumerator AttackAndFollowUp(UnitAttack attackingUnit, Unit unitThatGotClickedOn)
    {
        // Start the attack coroutine
        yield return StartCoroutine(AttackSystem.Instance.Attack(attackingUnit, unitThatGotClickedOn));

        // This code will execute after the attack coroutine has completely finished
        // Here you can put any follow-up actions you want to perform

        // Add the logic for counterattacks here if needed

        // Cancel any ongoing actions
        CancelScript.Instance.Cancel();

        // Transition the attacker to the "numb" state if it's still alive
        if (attackingUnit != null)
        {
            attackingUnit.TransitionToNumbState();
        }

        // Any code placed here will execute after the attack coroutine has finished
    }

}