using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    public GameMaster gm ;

void Start()
{
    gm = FindObjectOfType<GameMaster>();
}

    public void FillButtonsToDisplay(Unit unitThatGotClickedOn)
    {

        // MOVE BUTTON ?
        if (unitThatGotClickedOn.hasMoved == false /* and there are tiles you can walk on */ )
        {
            ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[0]);
        }



        // ATTACK BUTTON ?
        if (unitThatGotClickedOn.playerNumber == gm.playerTurn)
        {
            if (unitThatGotClickedOn is UnitAttack unitAttack)
            {
                if (unitAttack.hasAttacked == false)
                {
                    unitAttack.GetEnemies();
                    // zid virefier beli Vulnerability > 0 // sla7 ( rssas) ta3ek mazal m5lasch .
                    if (unitAttack.enemiesInRange.Any() == true)
                    {

                        ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[1]);
                        unitAttack.enemiesInRange.Clear();  // jmi3 3fssa dertha na7iha fi w9tha mt5lihach t9ol omb3d nss79ha
                    }
                }
            }
        }


        // DROP BUTTON ?
        if (unitThatGotClickedOn.playerNumber == gm.playerTurn)
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


        ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[5]);

    }

}