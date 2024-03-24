// using System.Dynamic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Events;
// using UnityEngine.EventSystems;
// using System.Linq;
// // using UnityEngine.UIElements;

// public class HandleUIButton : MonoBehaviour
// {


//     public GameMaster gm;
//     public Button attackButton;
//     public Button attendButton;
//     public Button cancelButton;
//     private bool buttonsActive;


//     //!!!! brikolage berk
//     public GridCell cell; //  i can't send the cell and the unit as parameter to the OnCancelButtonClick() method
//     public Unit unit;


//     void Start()
//     {
//         gm = FindObjectOfType<GameMaster>(); // singleton paradigm

//         // initially, buttons are not active
//         DeactivateButtons();
//     }

//     public void ActivateButtons(bool isThereIsAttackableUnit)
//     {
//         // attack button is activated only if ther is an attackable unit
//         if (isThereIsAttackableUnit == true)
//         {
//             attackButton.gameObject.SetActive(true);
//         }
//         attendButton.gameObject.SetActive(true);
//         // cancelButton.gameObject.SetActive(true);
//         buttonsActive = true;
//     }

//     public void DeactivateButtons()
//     {
//         // Deactivate the buttons and remove listeners
//         attackButton.gameObject.SetActive(false);
//         attendButton.gameObject.SetActive(false);
//         // cancelButton.gameObject.SetActive(false);
//         buttonsActive = false;
//     }


// // listener for click one of those 3 buttons .

//     public void OnAttackButtonClick()
//     {
//         Debug.Log("Attack Button Clicked");
//         DeactivateButtons();
//         // display attackable units and you can only click one from them .


//         //then  display another {.. menu?} to choose the target
//     }


//     public void BlocPlayerInputUnlessAttackableUnit()
//     {
//        // this method shouls handle all the player click , it block the interaction with other ojbects unless the attackable unit and untell the player click one from them .
//     }


//     //!!! hadi lazemha tessra ghir tselectionner unit . fl ACTION SelectUnit
//     public void OnAttendButtonClick()
//     {
//         Debug.Log("Attend Button Clicked");
//         DeactivateButtons();
//         // and Confirm the Move => so do nothing , 
//     }



//      // Define a delegate and event for BlocPlayerInputUnlessAttackableUnit
//     public delegate void BlocPlayerInputEventHandler();
//     public static event BlocPlayerInputEventHandler OnBlockPlayerInputUnlessAttackableUnit;


//     private bool isBlockingInput = false;

//     // Method to block player input unless an attackable unit is present
//     public void BlocPlayerInputUnlessAttackableUnit()
//     {
//         // Disable interaction with other objects
//         isBlockingInput = true;

//         // Enable interaction only when an attackable unit is present
//         EnableInteractionWithAttackableUnits();

//         // Trigger the event
//         OnBlockPlayerInputUnlessAttackableUnit?.Invoke();
//     }

//     private void EnableInteractionWithAttackableUnits()
//     {
//         int x = 9 ;
//         // Enable interaction with attackable units
//         // For example, you might iterate through a list of attackable units and enable their interaction
//     }

//     // Handle player clicks on attackable units
//     public void OnAttackableUnitClicked()
//     {
//         if (isBlockingInput)
//         {
//             // Revert interaction to the original state after the player has clicked on an attackable unit
//             isBlockingInput = false;

//             // Reset interaction with other objects
//             // For example, you might disable interaction with attackable units here
//         }
//     }

//     // Subscribe to the event
//     void OnEnable()
//     {
//         OnBlockPlayerInputUnlessAttackableUnit += BlocPlayerInputUnlessAttackableUnit;
//     }

//     // Unsubscribe from the event
//     void OnDisable()
//     {
//         OnBlockPlayerInputUnlessAttackableUnit -= BlocPlayerInputUnlessAttackableUnit;
//     }





//     // public void OnCancelButtonClick()
//     // {
//     //     Debug.Log("Cancel Button Clicked");

//     //     DeactivateButtons();
//     //     // return Backg to the initial cell
//     //     gm.goBackToCell(cell, unit);
//     //     cell = null;
//     //     unit = null;
//     // }



// }
