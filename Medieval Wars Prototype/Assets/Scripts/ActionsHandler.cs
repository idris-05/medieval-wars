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
    




    public List<Button> buttons = new List<Button>();


    public void FillButtonsToDisplay()
    {
        // fill the list in buttonsUI with the buttons that should be displayed
        foreach (Button button in buttons)
        {
            ButtonsUI.Instance.buttonsToDisplay.Add(button);
        }
    }
}