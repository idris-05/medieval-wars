// using System;
// using System.Diagnostics;
using System.Collections.Generic;
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


    // !!! lazem script hada yt7et 3end kch gameobject fla scene , bch yt7sseb enable w ttexecyta start w awake , sinon lazen 7ta tdir l'appale lkch finction ta3o bch yweli enable


    public void OnUnitSelection(Unit unitThatGotClickedOn)
    {
        switch (CurrentActionStateBasedOnClickedButton)
        {
            case UnitUtil.ActionToDoWhenButtonIsClicked.NONE:

                selectedUnit = unitThatGotClickedOn;
                // selectedUnit.unitView = selectedUnit.GetComponent<UnitView>();
                selectedUnit.unitView.HighlightUnitOnSelection();
 
                ManageInteractableObjects.Instance.ActivateBlockInteractionsLayer();
                ActionsHandler.Instance.FillButtonsToDisplay(unitThatGotClickedOn);
                ButtonsUI.Instance.DisplayButtons();
                // wait clicking one button .


                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK:

                List<Unit> enemiesInRange = (selectedUnit as UnitAttack).enemiesInRange;
                AttackSystem.Attack(selectedUnit as UnitAttack, unitThatGotClickedOn);
                ManageInteractableObjects.Instance.ResetSpecificUnitsBackToTheirOriginalLayer(enemiesInRange);
                enemiesInRange.Clear();
                CancelScript.Instance.OnCancelButtonClicked();

                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.MOVE:
                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.CANCEL:
                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.DROP:
                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.LOAD:
                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.SUPPLY:
                break;

            case UnitUtil.ActionToDoWhenButtonIsClicked.CAPTURE:
                break;

            default:
                break;
        }

    }


}