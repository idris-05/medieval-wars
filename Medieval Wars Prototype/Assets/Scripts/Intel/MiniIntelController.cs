using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniIntelController : MonoBehaviour
{


    private static MiniIntelController instance;
    public static MiniIntelController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<MiniIntelController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("MiniIntelController");
                    instance = obj.AddComponent<MiniIntelController>();
                }
            }
            return instance;
        }
    }



    public GameObject building;
    public GameObject terrain;
    public GameObject unit;
    public GameObject loadedUnit;

    public int numberOfMiniCardsActivated = 0;

    Vector3 buildingAndTerrainPositionInRightSide = new(175, 0, 0);
    Vector3 loadedUnitPositionInRightSide = new(-175, 0, 0);

    public void SetComponentAsActive(GameObject intelComponent)
    {
        intelComponent.SetActive(true);
    }

    public void SetComponentAsDesactive(GameObject intelComponent)
    {
        intelComponent.SetActive(false);
    }




    #region Terrain
    [SerializeField] private GameObject terrainTitle;
    [SerializeField] private GameObject terrainSprite;
    [SerializeField] private GameObject terrainDefenceNumber;
    public void UpdateterrainIntel(Terrain terrain)
    {
        terrainTitle.GetComponent<Text>().text = terrain.terrainName.ToString();
        terrainSprite.GetComponent<Image>().sprite = terrain.spriteRenderer.sprite;
        terrainDefenceNumber.GetComponent<Text>().text = TerrainsUtils.defenceStars[terrain.TerrainIndex].ToString();
    }
    #endregion



    #region Building
    [SerializeField] private GameObject buildingTitle;
    [SerializeField] private GameObject buildingSprite;
    [SerializeField] private GameObject buildingDefenceNumber;
    [SerializeField] private GameObject buildingCaptureNumber;
    public void UpdateBuildingIntel(Building building)
    {
        buildingTitle.GetComponent<Text>().text = building.terrainName.ToString();
        buildingSprite.GetComponent<Image>().sprite = building.spriteRenderer.sprite;
        buildingDefenceNumber.GetComponent<Text>().text = TerrainsUtils.defenceStars[building.TerrainIndex].ToString();
        buildingCaptureNumber.GetComponent<Text>().text = building.remainningPointsToCapture.ToString();
    }
    #endregion



    #region Unit
    [SerializeField] private GameObject unitTitle;
    [SerializeField] private GameObject unitSprite;
    [SerializeField] private GameObject unitHPNumber;
    [SerializeField] private GameObject unitDurabilityNumber;
    [SerializeField] private GameObject unitRationNumber;
    public void UpdateUnitIntel(Unit unit)
    {
        unitTitle.GetComponent<Text>().text = unit.unitName.ToString();
        unitSprite.GetComponent<Image>().sprite = unit.unitView.spriteRenderer.sprite;
        unitHPNumber.GetComponent<Text>().text = unit.healthPoints.ToString();
        if (unit is UnitAttack) unitDurabilityNumber.GetComponent<Text>().text = (unit as UnitAttack).durability.ToString();
        else unitDurabilityNumber.GetComponent<Text>().text = "0"; //!!!!!!!!!!!1 wla 9999999 ??? 
        unitRationNumber.GetComponent<Text>().text = unit.ration.ToString();
    }
    #endregion



    #region LoadedUnit
    [SerializeField] private GameObject loadedUnitSprite;
    public void UpdateLoadedUnitIntel(Unit unit)
    {
        loadedUnitSprite.GetComponent<Image>().sprite = unit.unitView.spriteRenderer.sprite;
    }
    #endregion




    public void HandleMINIIntel(GridCell gridCell)
    {
        SetComponentAsDesactive(building);
        SetComponentAsDesactive(terrain);
        SetComponentAsDesactive(unit);
        SetComponentAsDesactive(loadedUnit);
        numberOfMiniCardsActivated = 0;

        // building intel
        if (gridCell.occupantTerrain is Building)
        {
            UpdateBuildingIntel(gridCell.occupantTerrain as Building);
            SetComponentAsActive(building);
            numberOfMiniCardsActivated++;
        }

        // terrain intel
        else
        {
            UpdateterrainIntel(gridCell.occupantTerrain);
            SetComponentAsActive(terrain);
            numberOfMiniCardsActivated++;

        }

        // unit intel
        if (gridCell.occupantUnit != null)
        {
            UpdateUnitIntel(gridCell.occupantUnit);
            SetComponentAsActive(unit);
            numberOfMiniCardsActivated++;

        }

        // loaded unit intel
        if (gridCell.occupantUnit is UnitTransport unitTransport && unitTransport.loadedUnit != null)
        {
            UpdateLoadedUnitIntel(unitTransport.loadedUnit);
            SetComponentAsActive(loadedUnit);
            numberOfMiniCardsActivated++;

        }

        CardDisplayController.Instance.CalculateCardPosition();
        ReorderTheMiniCardsDisplay(CardDisplayController.Instance.IsTheCardWillDisplayedInRightSide);


    }


    public void ReorderTheMiniCardsDisplay(bool IsTheCardWillDisplayedInRightSide)
    {

        if (IsTheCardWillDisplayedInRightSide)
        {
            //  correct the ordre of the cards
            building.transform.localPosition = buildingAndTerrainPositionInRightSide;
            terrain.transform.localPosition = buildingAndTerrainPositionInRightSide;

            loadedUnit.transform.localPosition = loadedUnitPositionInRightSide;

            return;
        }

        // swape the cards
        building.transform.localPosition = loadedUnitPositionInRightSide;
        terrain.transform.localPosition = loadedUnitPositionInRightSide;

        loadedUnit.transform.localPosition = buildingAndTerrainPositionInRightSide;


    }
}
