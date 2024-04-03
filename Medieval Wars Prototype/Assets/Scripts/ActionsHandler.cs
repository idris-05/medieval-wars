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
    // capture  6

    // l'orodre mchi probleme , f la fin g3 nsgmohom



    public void FillButtonsToDisplay(Unit unitThatGotClickedOn)
    {
        if (unitThatGotClickedOn.playerOwner != GameController.Instance.currentPlayerInControl) return;

        ButtonsUI.Instance.buttonsToDisplay.Clear();


        // MOVE BUTTON 
        // hna nzido getWalkableCells() wnchofo ida kayen wla rahi fargha .
        if (unitThatGotClickedOn.hasMoved == false /* and there are tiles you can walk on */ )
        {
            ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[0]);
        }



        // ATTACK BUTTON 
        //!! had l verification t3 player owner srat lfo9 w ra7et b return. n9dro n7oha , mais n5loha 5ir .
        if (unitThatGotClickedOn.playerOwner == GameController.Instance.currentPlayerInControl)
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


        // DROP BUTTON 
        if (unitThatGotClickedOn.playerOwner == GameController.Instance.currentPlayerInControl)
        {
            if (unitThatGotClickedOn is UnitTransport transportUnitThatGotClickedOn)
            {
                if (transportUnitThatGotClickedOn.loadedUnit != null)
                {
                    transportUnitThatGotClickedOn.GetDropableCells();
                    // zid virefier beli Vulnerability > 0 // sla7 ( rssas) ta3ek mazal m5lasch .
                    if (transportUnitThatGotClickedOn.dropableCells.Any() == true)
                    {
                        ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[3]);
                        transportUnitThatGotClickedOn.dropableCells.Clear();  // jmi3 3fssa dertha na7iha fi w9tha mt5lihach t9ol omb3d nss79ha
                    }
                }
            }
        }

        // SUPPLY BUTTON
        if (unitThatGotClickedOn is UnitTransport transportUnitThatGotClickedOn_)
        {
            transportUnitThatGotClickedOn_.GetSuppliableUnits();
            if (transportUnitThatGotClickedOn_.suppliableUnits.Any() == true)
            {
                ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[4]);
                transportUnitThatGotClickedOn_.suppliableUnits.Clear();  // jmi3 3fssa dertha na7iha fi w9tha mt5lihach t9ol omb3d nss79ha
            }
        }


        // CAPTURE BUTTON
        if (unitThatGotClickedOn.occupiedCell.occupantTerrain is Building building)
        {
            if (building.playerOwner != unitThatGotClickedOn.playerOwner)
            {
                //!!!!! if (BuildingsUtil.BuildingCanHealAndSupplyThatUnit[building.TerrainIndex, unitThatGotClickedOn.unitIndex]) hadi t3 3fssa w7do5ra 
                // {
                ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[6]);
                // }
            }

        }






        // cancel button (always)
        // cancel button machi daymen , cancel button ki tkon 3ndek button w7do5ra dir m3aha camcel button .
        if (ButtonsUI.Instance.buttonsToDisplay.Any() == true)
        {
            ButtonsUI.Instance.buttonsToDisplay.Add(actionButtons[5]);
        }

    }

}