using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeBuildingsController : MonoBehaviour
{
    private static TradeBuildingsController instance;
    public static TradeBuildingsController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of TradeBuildingsController exists in the scene
                instance = FindObjectOfType<TradeBuildingsController>();

                // If not found, create a new GameObject with TradeBuildingsController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("TradeBuildingsController");
                    instance = obj.AddComponent<TradeBuildingsController>();
                }
            }
            return instance;
        }
    }

    // public GameObject tradeBuildingsMenu;

    public GameObject SHOP;
    public GameObject barrack;
    public GameObject stable;
    public GameObject dock;

    public Terrain currentTradeBuilding;


    // public void GetTheTradeBuildingsToDisplayMenu(TradeBuilding tradeBuilding)
    // {
    //     // Get the trade buildings to display in the menu
    //     switch (tradeBuilding.terrainName)
    //     {
    //         case TerrainsUtils.TerrainName.BARRACK:
    //             barrack.SetActive(true);
    //             break;

    //         case TerrainsUtils.TerrainName.STABLE:
    //             stable.SetActive(true);
    //             break;

    //         case TerrainsUtils.TerrainName.DOCK:
    //             dock.SetActive(true);
    //             break;

    //         default:
    //             break;
    //     }
    // }


    public void TradeUnit()
    {
        // // Get the text from the clicked button
        // string buttonText = this.GetComponentInChildren<Text>().text;

        string buttonText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        // Debug.Log("Button text: " + buttonText);

        switch (buttonText)
        {

            case "Infantry":
                TradeOneUnit(UnitUtil.UnitName.INFANTRY);
                break;

            case "Archer":
                TradeOneUnit(UnitUtil.UnitName.ARCHERS);
                break;

            case "Bandit":
                TradeOneUnit(UnitUtil.UnitName.BANDIT);
                break;

            case "SpearMan":
                TradeOneUnit(UnitUtil.UnitName.SPEARMAN);
                break;

            case "Catapult":
                TradeOneUnit(UnitUtil.UnitName.CATAPULT);
                break;





            case "Cavalry":
                TradeOneUnit(UnitUtil.UnitName.CAVALRY);
                break;

            case "RCavalry":
                TradeOneUnit(UnitUtil.UnitName.RCAVALRY);
                break;

            case "Caravan":
                TradeOneUnit(UnitUtil.UnitName.CARAVAN);
                break;




            case "Carrack":
                TradeOneUnit(UnitUtil.UnitName.CARRACK);
                break;

            case "Fire Ship":
                TradeOneUnit(UnitUtil.UnitName.FIRESHIP);
                break;

            case "TShip":
                TradeOneUnit(UnitUtil.UnitName.TSHIP);
                break;



            case "Cancel":
                DeactivateAllTradeBuildings();
                break;

            default:
                break;
        }

    }




    public void TradeOneUnit(UnitUtil.UnitName unitName)
    {
        Debug.Log("TradeOneUnit  : " + unitName.ToString());
        // tradebuilding.BuyUnit(GameController.Instance.currentPlayerInControl, GameController.Instance.indexUnitprefab[GetTheIndexOfUnitInEnum(unitName)]);
        // GameController.Instance.SpawnUnit(GameController.Instance.currentPlayerInControl, currentTradeBuilding.row, currentTradeBuilding.col, GameController.Instance.indexUnitprefab[GetTheIndexOfUnitInEnum(unitName)]);
        currentTradeBuilding = null;
        DeactivateAllTradeBuildings();


        // Debug.Log("Unit traded successfully" + unitName.ToString());
    }



    public int GetTheIndexOfUnitInEnum(UnitUtil.UnitName unitName)
    {
        // Convert the enum values to an array
        UnitUtil.UnitName[] enumValues = (UnitUtil.UnitName[])Enum.GetValues(typeof(UnitUtil.UnitName));

        // Find the index of the specified unit
        int index = Array.IndexOf(enumValues, unitName);
        return index;
    }


    public void ActivateOneTradeBuilding(Terrain tradebuilding)
    {
        CancelScript.Instance.OnCancelButtonClicked();

        switch (tradebuilding.terrainName)
        {
            case TerrainsUtils.TerrainName.BARRACK:
                barrack.SetActive(true);
                break;

            case TerrainsUtils.TerrainName.STABLE:
                stable.SetActive(true);
                break;

            case TerrainsUtils.TerrainName.DOCK:
                dock.SetActive(true);
                break;

            default:
                break;
        }

        currentTradeBuilding = tradebuilding;
        ManageInteractableObjects.Instance.ActivateBlockInteractionsLayer();

    }


    public void DeactivateAllTradeBuildings()
    {
        barrack.SetActive(false);
        stable.SetActive(false);
        dock.SetActive(false);
        // SHOP.SetActive(false);
        currentTradeBuilding = null;
        ManageInteractableObjects.Instance.DesctivateBlockInteractionsLayer();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            barrack.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            stable.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            dock.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            DeactivateAllTradeBuildings();
        }

    }


}