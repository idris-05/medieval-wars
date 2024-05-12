using System.Collections;
using UnityEngine;

public class CoCardsController : MonoBehaviour
{


    private static CoCardsController instance;
    public static CoCardsController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<CoCardsController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("CoCardsController");
                    instance = obj.AddComponent<CoCardsController>();
                }
            }
            return instance;
        }
    }

    public GameObject card;
    private RectTransform canvasRect;
    public CanvasGroup canvasGroup;
    public bool IsAnimating;
    public bool PreviousSideOfMouseIsTop;
    public bool TheCoCardIsLocked;
    public bool IsTheCardActivated;


    public float AppearAnimationDuration;
    public float HideAnimationDuration;



    public Vector3 CardPosition = new Vector3(-498, 365, 0);
    // Initial position of the card in both right and left sides
    public Vector3 HidenPositionOfTheCard = new Vector3(-498, 700, 0);

    Vector3 mousePos;


    [SerializeField] public GameObject CO1Fill;
    [SerializeField] public GameObject CO2Fill;


    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
    }

    void Update()
    {

        if (!TheCoCardIsLocked && !IsTheCardActivated) ActivateCard();
        // Get the mouse position in screen coordinates
        mousePos = Input.mousePosition;

        // Convert the mouse position to Canvas local coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePos, null, out _);

        // Determine the new side based on the mouse position
        bool CurrentSideOfMouseIsTop = mousePos.y > 2 * Screen.height / 3;


        // Check if the side has changed
        if (CurrentSideOfMouseIsTop != PreviousSideOfMouseIsTop)
        {
            // Don't update card position if animation is ongoing
            if (!IsAnimating)
            {
                if (CurrentSideOfMouseIsTop)
                {
                    StartCoroutine(AnimateCardWhenItHides());
                }
                else
                {
                    StartCoroutine(AnimateCardWhenItAppears());
                }
            }
        }
        else
        {
            if (CurrentSideOfMouseIsTop)
            {
                card.transform.localPosition = HidenPositionOfTheCard;
            }
            else
            {
                card.transform.localPosition = CardPosition;
            }
        }
        PreviousSideOfMouseIsTop = CurrentSideOfMouseIsTop;

    }



    private IEnumerator AnimateCardWhenItAppears()
    {
        IsAnimating = true;
        // MiniIntelController.Instance.LockTheMiniCard();
        // MiniIntelController.Instance.DesActivateCard();

        Vector3 initialPosition = HidenPositionOfTheCard;
        Vector3 targetPosition = CardPosition;

        float startTime = Time.time;
        float endTime = startTime + AppearAnimationDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / AppearAnimationDuration;
            float easedT = 1f - Mathf.Exp(-5f * t); // Ease-out function: 1 - e^(-5t)
            card.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, easedT);
            yield return null;
        }

        card.transform.localPosition = targetPosition;
        IsAnimating = false;

    }


    private IEnumerator AnimateCardWhenItHides()
    {
        IsAnimating = true;
        // MiniIntelController.Instance.LockTheMiniCard();
        // MiniIntelController.Instance.DesActivateCard();

        Vector3 initialPosition = CardPosition;
        Vector3 targetPosition = HidenPositionOfTheCard;

        float startTime = Time.time;
        float endTime = startTime + HideAnimationDuration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / HideAnimationDuration;
            float easedT = t;
            card.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, easedT);
            yield return null;
        }

        card.transform.localPosition = targetPosition;
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



    public void LockTheCOCard()
    {
        TheCoCardIsLocked = true;
    }

    public void UnLockTheCOCard()
    {
        TheCoCardIsLocked = false;
    }


}