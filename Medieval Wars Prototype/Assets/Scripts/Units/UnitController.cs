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

    public void OnUnitSelection(Unit unitThatGotClickedOn)
    {
        switch (CurrentActionStateBasedOnClickedButton)
        {
            case UnitUtil.ActionToDoWhenButtonIsClicked.NONE:

                // hna lazem Unit (hadi unitThatGotClickedOn) lazem tkon ta3ek , sinon mlazemch t9dr tchof l'enemie wch rah 9ader ydir
                //!!!! had les details ( commantaire li rah fo9i ) lazem meet ljay n7ddohom kamel .
                if (unitThatGotClickedOn.playerNumber != GameController.Instance.playerTurn) return;

                selectedUnit = unitThatGotClickedOn;
                selectedUnit.unitView.HighlightAsSelected();

                ManageInteractableObjects.Instance.ActivateBlockInteractionsLayer();
                ActionsHandler.Instance.FillButtonsToDisplay(unitThatGotClickedOn);
                ButtonsUI.Instance.DisplayButtons();
                // wait for the player to click any button from the displayed buttons .


                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK:
                // we are sure that the selected unit is of type UnitAttack and not a UnitTranspor
                UnitAttack attackingUnit = selectedUnit as UnitAttack;

                ManageInteractableObjects.Instance.ResetSpecificUnitsBackToTheirOriginalLayer(attackingUnit.enemiesInRange);

                AttackSystem.Attack(attackingUnit, unitThatGotClickedOn);

                //! COUNTERATTACK NEEDS TO BE ADDED HERE IN CASE THE ENEMY CAN ACTUALLY COUNTERATTACK
                //! I DON'T REALLY KNOW HOW WE CAN HANDLE THIS I REMEMBER ADEM TALKING ABOUT IT
                //! JUST ASK HIM ABOUT THAT , I THINK HE TALKED ABOUT A MATRIX IN WHICH THE COUNTERATTACK RELATIONS ARE STORED

                //! lconterAttack ra7 yessra da5el la methode attack fl attackSystem (is case you didn't see the comment there )
                //! ida cheft lcommentaire w 7bit tbdellha plassa, tbanli ltema 5ir . psq ki y'attacker yessra f nfss lwe9t counterattack . (tssema mklah n5roj men script wndirha fi plassa w7do5ra)

                CancelScript.Instance.Cancel();

                break;

            // case UnitUtil.ActionToDoWhenButtonIsClicked.MOVE:  .. this case is not possible .
            //     break;

            // case UnitUtil.ActionToDoWhenButtonIsClicked.CANCEL:  //!!!!!!!!!!!!!!!!!!!!!!!!!!11
            //     break;

            // case UnitUtil.ActionToDoWhenButtonIsClicked.DROP:
            //     break;

            // case UnitUtil.ActionToDoWhenButtonIsClicked.LOAD:   // hadi 9olna nforciwha tessra direct .
            //     break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.SUPPLY:
                UnitTransport supplyingUnit = selectedUnit as UnitTransport;

                ManageInteractableObjects.Instance.ResetSpecificUnitsBackToTheirOriginalLayer(supplyingUnit.suppliableUnits);

                supplyingUnit.Supply(unitThatGotClickedOn, 10); //! LAZEM NWELIW NMODIFYIW 10 HADI

                CancelScript.Instance.Cancel();

                break;

            // case UnitUtil.ActionToDoWhenButtonIsClicked.CAPTURE:
            //     break;

            default:
                break;
        }

    }


}