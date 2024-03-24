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

    // ??? n5loha hadi .
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



    // Enum to define the action to do when a button is clicked
    public enum ActionToDoWhenClikButton
    {
        NONE,
        ATTACK,
        MOVE,
        CANCEL,
        DROP,
        LOAD,
        SUPPLY,
        CAPTURE
    }


    public Unit selectedUnit;


    public ActionToDoWhenClikButton CurrentActionStateBasedOnClickedButton = ActionToDoWhenClikButton.NONE;





    public void OnUnitSelection(Unit unitThatGotClicked)
    {
        switch (CurrentActionStateBasedOnClickedButton)
        {
            case ActionToDoWhenClikButton.NONE:
            
                selectedUnit = unitThatGotClicked;
                selectedUnit.unitView.HighlightUnitOnSelection();
                ActionsHandler.Instance.FillButtonsToDisplay();
                ButtonsUI.Instance.DisplayButtons();
                //!!! make only those buttons interactable . ndiroha da5el displayButtons !
                // wait for the player to click on a button

                // FillExecutableActionsList ( buttons to display logic will be implemented here , fills the list )
                //  Invoke( DisplayButtons() )
                break;

            case ActionToDoWhenClikButton.ATTACK:
                break;

            case ActionToDoWhenClikButton.MOVE:
                break;

            case ActionToDoWhenClikButton.CANCEL:
                break;

            case ActionToDoWhenClikButton.DROP:
                break;

            case ActionToDoWhenClikButton.LOAD:
                break;

            case ActionToDoWhenClikButton.SUPPLY:
                break;

            case ActionToDoWhenClikButton.CAPTURE:
                break;

            default:
                break;
        }
    }




}