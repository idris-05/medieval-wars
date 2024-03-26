using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{

    private static ButtonsUI instance;
    public static ButtonsUI Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<ButtonsUI>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("ButtonsUI");
                    instance = obj.AddComponent<ButtonsUI>();
                }
            }
            return instance;
        }
    }


    public List<Button> buttonsToDisplay = new List<Button>();


    //!!! we should search for a way to automate that the buttons are displayed in the right palces ,
    //!!! when we remove one , the others should be repositioned

    public void DisplayButtons()
    {
        foreach (Button button in buttonsToDisplay)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void HideButtons()
    {
        foreach (Button button in buttonsToDisplay)
        {
            button.gameObject.SetActive(false);
        }
    }


    // omb3d nsggem asm ta3ha ...  7ta nchofo les cas lo5rin .
    public void UpdateButtonsDisplayWhenAButtonClicked(Button buttonClicked)
    {
        HideButtons();
        buttonsToDisplay.Clear();
        
        // ida makanatch hya cancel t3awed trje3 cancel ..... mazal capable nzido f had la logique
        Button cancelButton = ActionsHandler.Instance.actionButtons[5];

        if (buttonClicked != cancelButton)
        {
            buttonsToDisplay.Add(cancelButton);
            DisplayButtons();
        }

    }

}