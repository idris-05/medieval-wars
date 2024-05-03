using UnityEngine;

public class CardDisplayController : MonoBehaviour
{

    private static CardDisplayController instance;
    public static CardDisplayController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<CardDisplayController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("CardDisplayController");
                    instance = obj.AddComponent<CardDisplayController>();
                }
            }
            return instance;
        }
    }



    public GameObject card; // Reference to the card object in the scene
    public float offsetX; // Offset from the bottom-right corner for card placement
    public float offsetY;

    public float cardHeight;
    public float cardWidth;

    public float SubCardHeight;
    public float SubCardWidth;


    private float XCardRightPosition;
    private float XCardLeftPosition;

    public float YcardPosition;

    public bool IsTheCardWillDisplayedInRightSide;



    // Vector3 rightCardPosition = new Vector3(637.5f, -330, 0);
    // Vector3 leftCardPosition  =new Vector3(-637.5f, -330, 0);

    Vector3 rightCardPosition;
    Vector3 leftCardPosition;


    private RectTransform canvasRect;

    void Start()
    {
        XCardRightPosition = Screen.width / 2 - cardWidth / 2 - offsetX;
        XCardLeftPosition = -XCardRightPosition;

        YcardPosition = -Screen.height / 2 + cardHeight / 2 + offsetY;


        // Get the RectTransform component of the Canvas

        rightCardPosition = new Vector3(XCardRightPosition, YcardPosition, 0);

        leftCardPosition = new Vector3(XCardLeftPosition, YcardPosition, 0);

        canvasRect = GetComponent<RectTransform>();
    }


    // Calculate the position of the card relative to the bottom-right corner of the Canvas
    public void CalculateCardPosition()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to Canvas local coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePos, null, out _);

        // Compare the mouse position with the center of the screen
        if (mousePos.x < Screen.width / 2)
        {
            // If the mouse is on the left side of the screen, set the card position to the right
            IsTheCardWillDisplayedInRightSide = true;
            card.transform.localPosition = rightCardPosition;
        }
        else
        {
            // If the mouse is on the right side of the screen, set the card position to the left
            IsTheCardWillDisplayedInRightSide = false;
            card.transform.localPosition = leftCardPosition;
        }
    }



}
