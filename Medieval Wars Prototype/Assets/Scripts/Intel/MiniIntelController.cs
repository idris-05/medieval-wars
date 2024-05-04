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



    // Reference to the card object in the scene
    public GameObject card;
    public CanvasGroup canvasGroup;



    // Offset from the bottom-right corner for card placement
    public float offsetX;
    public float offsetY;



    // Card dimensions
    public float cardHeight;
    public float cardWidth;

    // Sub-card dimensions
    public float SubCardHeight;
    public float SubCardWidth;



    // x coordinate of the card position in both right and left sides
    private float XCardRightPosition;
    private float XCardLeftPosition;

    // y coordinate of the card position 
    public float YcardPosition;


    // initial x coordinate of the card position in both right and left sides , (where to start the animation)
    float initialXCardRightPosition;
    float initialXCardLeftPosition;



    // position of the card in both right and left sides
    Vector3 rightCardPosition;
    Vector3 leftCardPosition;

    // Initial position of the card in both right and left sides
    Vector3 InitialLeftPositionOfTheCard;
    Vector3 InitialRightPositionOfTheCard;

    // Vector3 rightCardPosition = new Vector3(637.5f, -330, 0);
    // Vector3 leftCardPosition  =new Vector3(-637.5f, -330, 0);

    public bool IsTheCardWillDisplayedInRightSide;
    public bool SideHasChanged;
    public bool IsAnimating;
    public bool TheMiniCardIsLocked = false;


    public float HideAnimationDuration; // Adjust as needed
    public float AppearAnimationDuration; // Adjust as needed


    public GameObject building;
    public GameObject terrain;
    public GameObject unit;
    public GameObject loadedUnit;

    public int numberOfMiniCardsActivated = 0;

    Vector3 buildingAndTerrainPositionInRightSide = new(175, 0, 0);
    Vector3 loadedUnitPositionInRightSide = new(-175, 0, 0);





    private RectTransform canvasRect;

    public bool IsTheCardActivated = false;



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




    void Start()
    {
        XCardRightPosition = Screen.width / 2 - cardWidth / 2 - offsetX;
        XCardLeftPosition = -XCardRightPosition;

        initialXCardRightPosition = Screen.width / 2 + cardWidth / 2 + offsetX;
        initialXCardLeftPosition = -initialXCardRightPosition;


        YcardPosition = -Screen.height / 2 + cardHeight / 2 + offsetY;


        rightCardPosition = new Vector3(XCardRightPosition, YcardPosition, 0);
        leftCardPosition = new Vector3(XCardLeftPosition, YcardPosition, 0);


        InitialRightPositionOfTheCard = new Vector3(initialXCardRightPosition, YcardPosition, 0);
        InitialLeftPositionOfTheCard = new Vector3(initialXCardLeftPosition, YcardPosition, 0);


        // Get the RectTransform component of the Canvas
        canvasRect = GetComponent<RectTransform>();
    }



    public void HandleMINIIntel(GridCell gridCell)
    {
        if (!TheMiniCardIsLocked && !IsTheCardActivated) ActivateCard();

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

        AnimateTheCardMouvement();

    }




    // Calculate the position of the card relative to the bottom-right corner of the Canvas
    public void AnimateTheCardMouvement()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to Canvas local coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePos, null, out _);

        // Determine the new side based on the mouse position
        bool newIsRightSide = mousePos.x < Screen.width / 2;

        // Check if the side has changed
        if (newIsRightSide != IsTheCardWillDisplayedInRightSide)
        {
            SideHasChanged = true;
            // Don't update card position if animation is ongoing
            if (!IsAnimating)
            {
                IsTheCardWillDisplayedInRightSide = newIsRightSide;
                StartCoroutine(AnimateCard());
            }
        }
        else
        {
            SideHasChanged = false;
            // Don't update card position if animation is ongoing
            if (!IsAnimating)
            {
                ReorderTheMiniCardsDisplay();
                card.transform.localPosition = newIsRightSide ? rightCardPosition : leftCardPosition;
            }
        }
    }


    public void ReorderTheMiniCardsDisplay()
    {
        //  les positions sont par rapport au la carte principale 

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


    private IEnumerator AnimateCard()
    {
        IsAnimating = true;

        Vector3 initialPosition = IsTheCardWillDisplayedInRightSide ? leftCardPosition : rightCardPosition;
        Vector3 targetPosition = IsTheCardWillDisplayedInRightSide ? InitialLeftPositionOfTheCard : InitialRightPositionOfTheCard;


        float startTime = Time.time;
        float endTime = startTime + HideAnimationDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / HideAnimationDuration;
            float easedT = t;
            card.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, easedT);
            yield return null;
        }

        // Ensure final position is accurate
        card.transform.localPosition = targetPosition;
        ReorderTheMiniCardsDisplay();

        card.transform.localPosition = IsTheCardWillDisplayedInRightSide ? InitialRightPositionOfTheCard : InitialLeftPositionOfTheCard;
        initialPosition = card.transform.localPosition;
        targetPosition = IsTheCardWillDisplayedInRightSide ? rightCardPosition : leftCardPosition;

        startTime = Time.time;
        endTime = startTime + AppearAnimationDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / AppearAnimationDuration;
            float easedT = 1f - (1f - t) * (1f - t) * (1f - t); // Cubic easing function
            card.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, easedT);
            yield return null;
        }

        IsAnimating = false;

    }


    public void ActivateCard()
    {
        IsTheCardActivated = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void DesActivateCard()
    {
        IsTheCardActivated = false;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }



    public void LockTheMiniCard()
    {
        TheMiniCardIsLocked = true;
    }

    public void UnLockTheMiniCard()
    {
        TheMiniCardIsLocked = false;
    }
}

