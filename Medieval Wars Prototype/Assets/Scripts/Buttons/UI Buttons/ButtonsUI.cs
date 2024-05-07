using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{

    //! BON HNA F UI T3 LES BOUTTONS 
    //! BANETLI 3FSSA BACH NGERI KIFACH THEY GET DISPLAYED IN THE SCENE
    //! NESTORI ARRAY T3 LES POSITIONS position fel y t3houm differente
    //! lazem tkoun differente b 25 unites ( verifyit f la scene beli hadi hiya la valeur parfaite t9der tchouf ida rak 7ab )
    //! mb3d nespawnihoum b l order f la methode displayButtons()
    //! besah probleme c que kbar bzf w y9drou yghetou des elemtns mel map sema lazem nel9aw tari9a w7do5ra mb3d n5emoulha had l3fssa 
    //! ma 7abitch nebda fiha 7ata n5emou exact f positions where they get displayed bach ma y5srouch player experience

    //!! wch 9olt ishak n5loh 7etta ywjed l ui , ltemma y5yro les buttons kifach ndirohom wkifach ykono , moraha n9dro n'implimentiw had logique li rak thder 3liha  

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
    public float[] buttonsPositionsonY = { 186 , 120 , 54 , -12 , -78 , -144} ;  //position par rapport a CANVAS . 


    //!!! we should search for a way to automate that the buttons are displayed in the right palces ,
    //!!! when we remove one , the others should be repositioned

    public void DisplayButtons()
    {
        foreach (Button button in buttonsToDisplay)
        {
            button.gameObject.SetActive(true);
            button.transform.localPosition = new Vector3(button.transform.localPosition.x, buttonsPositionsonY[buttonsToDisplay.IndexOf(button)], button.transform.localPosition.z);
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
    public void UpdateActionButtonsToDisplayWhenAButtonIsClicked(Button buttonClicked)
    {
        HideButtons();
        buttonsToDisplay.Clear();
        
        Button cancelButton = ActionsHandler.Instance.actionButtons[5];

        if (buttonClicked != cancelButton)
        {
            buttonsToDisplay.Add(cancelButton);
            DisplayButtons();
        }

    }



}