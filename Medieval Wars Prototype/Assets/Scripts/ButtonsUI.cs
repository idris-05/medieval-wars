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
}