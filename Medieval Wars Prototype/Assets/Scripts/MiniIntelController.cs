using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniIntelController : MonoBehaviour
{


    public GameObject building;
    public GameObject terrain;
    public GameObject unit;
    public GameObject loadedUnit;

    public void SetComponentAsActive(GameObject intelComponent)
    {
        intelComponent.SetActive(true);
    }

    public void SetComponentAsDesactive(GameObject intelComponent)
    {
        intelComponent.SetActive(false);
    }





    [SerializeField] private GameObject terrainTitle;
    [SerializeField] private GameObject terrainSprite;
    [SerializeField] private GameObject terrainDefenceNumber;
    public void UpdateterrainIntel(Terrain terrain)
    {
        terrainTitle.GetComponent<Text>().text = terrain.terrainName.ToString();
        terrainSprite.GetComponent<Image>().sprite = terrain.spriteRenderer.sprite;
        terrainDefenceNumber.GetComponent<Text>().text = TerrainsUtils.defenceStars[terrain.TerrainIndex].ToString();
    }





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



    [SerializeField] private GameObject loadedUnitSprite;
    public void UpdateLoadedUnitIntel(Unit unit)
    {
        loadedUnitSprite.GetComponent<Image>().sprite = unit.unitView.spriteRenderer.sprite;
    }













    public void HandleMINIIntel(GridCell gridCell)
    {
        SetComponentAsDesactive(building);
        SetComponentAsDesactive(terrain);
        SetComponentAsDesactive(unit);
        SetComponentAsDesactive(loadedUnit);

        // building intel
        if (gridCell.occupantTerrain is Building)
        {
            UpdateBuildingIntel(gridCell.occupantTerrain as Building);
            SetComponentAsActive(building);
        }

        // terrain intel
        else
        {
            UpdateterrainIntel(gridCell.occupantTerrain);
            SetComponentAsActive(terrain);
        }

        // unit intel
        if (gridCell.occupantUnit != null)
        {
            UpdateUnitIntel(gridCell.occupantUnit);
            SetComponentAsActive(unit);
        }

        // loaded unit intel
        if (gridCell.occupantUnit is UnitTransport unitTransport && unitTransport.loadedUnit != null)
        {
            UpdateLoadedUnitIntel(unitTransport.loadedUnit);
            SetComponentAsActive(loadedUnit);
        }
    }
}
