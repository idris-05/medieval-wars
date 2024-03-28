using System.Linq;
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



    public Button[] actionButtons;


    // button indexes 

    // move     0
    // attack   1
    // load     2
    // drop     3
    // supply   4
    // cancel   5



    public void FillButtonsToDisplay(Unit unitThatGotClickedOn)
    {
        if (unitThatGotClickedOn.playerNumber != GameController.Instance.playerTurn) return;


        // MOVE BUTTON ?
        //! WE DID THIS ONE TOGETHER THEREFORE I WILL NOT EVEN THINK ABOUT IT
        if (unitThatGotClickedOn.hasMoved == false /* and there are tiles you can walk on */ )
        {
            ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[0]);
        }



        // ATTACK BUTTON ?
        //! I REVIEWED THIS AND I THINK IT'S CORRECT
        if (unitThatGotClickedOn.playerNumber == GameController.Instance.playerTurn)
        {
            // Debug.Log("unitThatGotClickedOn.playerNumber == gm.playerTurn");
            if (unitThatGotClickedOn is UnitAttack unitAttack)
            {
                // Debug.Log("unitThatGotClickedOn is UnitAttack unitAttack");
                if (unitAttack.hasAttacked == false)
                {
                    // Debug.Log("unitAttack.hasAttacked == false");
                    unitAttack.GetEnemiesInRange();
                    // zid virefier beli Vulnerability > 0 // sla7 ( rssas) ta3ek mazal m5lasch .
                    if (unitAttack.enemiesInRange.Any() == true)
                    {
                        ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[1]);
                        unitAttack.enemiesInRange.Clear();
                        // jmi3 3fssa dertha na7iha fi w9tha mt5lihach t9ol omb3d nss79ha 
                        //! OUI 3ndk l7a9 , 5tak men optimisation , sah g3 f organisation
                    }
                }
            }
        }


        // DROP BUTTON ?
        //! I DONT THINK I WILL BE AT DROP LOGIC , I'LL JUST LEAVE IT TO LATER ( IT'S NOT A BIG DEAL )
        if (unitThatGotClickedOn.playerNumber == GameController.Instance.playerTurn)
        {
            if (unitThatGotClickedOn is UnitTransport unitTransport)
            {
                if (unitTransport.loadedUnit != null)
                {
                    unitTransport.GetdropableCells();
                    // zid virefier beli Vulnerability > 0 // sla7 ( rssas) ta3ek mazal m5lasch .
                    if (unitTransport.dropableCells.Any() == true)
                    {
                        ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[3]);
                        unitTransport.dropableCells.Clear();  // jmi3 3fssa dertha na7iha fi w9tha mt5lihach t9ol omb3d nss79ha
                    }
                }
            }
        }




        // cancel button (always)
        ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[5]);

    }

}