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

    public TradeBuilding currentTradeBuilding;


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
        // Debug.Log("TradeOneUnit  : " + unitName.ToString());

        Unit unitPrefab = GameController.Instance.currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[GetTheIndexOfUnitInEnum(unitName)] : GameController.Instance.FrenchUnitPrefabsList[GetTheIndexOfUnitInEnum(unitName)];

        currentTradeBuilding.BuyUnit(GameController.Instance.currentPlayerInControl, unitPrefab);
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


    public void ActivateOneTradeBuilding(TradeBuilding tradebuilding)
    {
        CancelScript.Instance.OnCancelButtonClicked();
        currentTradeBuilding = tradebuilding;

        switch (tradebuilding.terrainName)
        {
            case TerrainsUtils.TerrainName.BARRACK:
                barrack.SetActive(true);
                UpdateAllUnitsCostInTheDisplayForBarrack();
                break;

            case TerrainsUtils.TerrainName.STABLE:
                stable.SetActive(true);
                UpdateAllUnitsCostInTheDisplayForStable();
                break;

            case TerrainsUtils.TerrainName.DOCK:
                dock.SetActive(true);
                UpdateAllUnitsCostInTheDisplayForDock();
                break;

            default:
                break;
        }


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


    public void UpdateAllUnitsCostInTheDisplayForBarrack()
    {
        Player currentPlayerInControl = GameController.Instance.currentPlayerInControl;
        float playerFunds = currentPlayerInControl.availableFunds;

        Transform barrackTransform = barrack.transform;
        CO currentPlayerCO = currentPlayerInControl.Co;

        barrackTransform.Find("Infantry").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(4);
        barrackTransform.Find("Archer").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(1);
        barrackTransform.Find("Bandit").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(8);
        barrackTransform.Find("Spear Man").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(6);
        barrackTransform.Find("Catapult").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(9);

        barrackTransform.Find("Infantry").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(4).ToString();
        barrackTransform.Find("Archer").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(1).ToString();
        barrackTransform.Find("Bandit").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(8).ToString();
        barrackTransform.Find("Spear Man").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(6).ToString();
        barrackTransform.Find("Catapult").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(9).ToString();

        barrackTransform.Find("Infantry").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[4].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[4].unitView.spriteRenderer.sprite;
        barrackTransform.Find("Archer").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[1].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[1].unitView.spriteRenderer.sprite;
        barrackTransform.Find("Bandit").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[8].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[8].unitView.spriteRenderer.sprite;
        barrackTransform.Find("Spear Man").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[6].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[6].unitView.spriteRenderer.sprite;
        barrackTransform.Find("Catapult").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[9].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[9].unitView.spriteRenderer.sprite;

    }

    public void UpdateAllUnitsCostInTheDisplayForStable()
    {
        Player currentPlayerInControl = GameController.Instance.currentPlayerInControl;
        float playerFunds = currentPlayerInControl.availableFunds;

        Transform stableTransform = stable.transform;
        CO currentPlayerCO = currentPlayerInControl.Co;

        // stableTransform.Find("Cavalry").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(10);
        // stableTransform.Find("RCavalry").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(7);
        stableTransform.Find("Cavalry").Find("Button").GetComponent<Button>().interactable = false;
        stableTransform.Find("RCavalry").Find("Button").GetComponent<Button>().interactable = false;
        stableTransform.Find("Caravan").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(0);

        stableTransform.Find("Cavalry").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(10).ToString();
        stableTransform.Find("RCavalry").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(7).ToString();
        stableTransform.Find("Caravan").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(0).ToString();

        // stableTransform.Find("Cavalry").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[10].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[10].unitView.spriteRenderer.sprite;
        // stableTransform.Find("RCavalry").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[7].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[7].unitView.spriteRenderer.sprite;
        stableTransform.Find("Caravan").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[0].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[0].unitView.spriteRenderer.sprite;

    }

    public void UpdateAllUnitsCostInTheDisplayForDock()
    {
        Player currentPlayerInControl = GameController.Instance.currentPlayerInControl;
        float playerFunds = currentPlayerInControl.availableFunds;

        Transform dockTransform = dock.transform;
        CO currentPlayerCO = currentPlayerInControl.Co;

        dockTransform.Find("Carrack").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(2);
        dockTransform.Find("Fire Ship").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(3);
        dockTransform.Find("TShip").Find("Button").GetComponent<Button>().interactable = playerFunds >= currentPlayerCO.GetUnitCost(5);

        dockTransform.Find("Carrack").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(2).ToString();
        dockTransform.Find("Fire Ship").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(3).ToString();
        dockTransform.Find("TShip").Find("Price").GetComponent<Text>().text = currentPlayerCO.GetUnitCost(5).ToString();

        dockTransform.Find("Carrack").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[2].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[2].unitView.spriteRenderer.sprite;
        dockTransform.Find("Fire Ship").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[3].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[3].unitView.spriteRenderer.sprite;
        dockTransform.Find("TShip").Find("Image").GetComponent<Image>().sprite = currentPlayerInControl == GameController.Instance.player1 ? GameController.Instance.EnglishUnitPrefabsList[5].unitView.spriteRenderer.sprite : GameController.Instance.FrenchUnitPrefabsList[5].unitView.spriteRenderer.sprite;
    }


}