using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsHandler : MonoBehaviour
{
    private static ActionsHandler instance;
    public static ActionsHandler Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<ActionsHandler>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("ActionsHandler");
                    instance = obj.AddComponent<ActionsHandler>();
                }
            }
            return instance;
        }
    }



    public Button[] actionButtons ;


    // button indexes 

    // move     0
    // attack   1
    // load     2
    // drop     3
    // supply   4
    // cancel   5



    public void FillButtonsToDisplay(Unit unitThatGotClickedOn)
    {
      
        // MOVE BUTTON ?
        if ( unitThatGotClickedOn.hasMoved == false /* and there are tiles you can walk on */ )
        {
            ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[0]);
        }

        ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[5]);

    }

}