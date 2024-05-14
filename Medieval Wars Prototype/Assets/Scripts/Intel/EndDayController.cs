using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndDayController : MonoBehaviour
{
    private static EndDayController instance;
    public static EndDayController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<EndDayController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("EndDayController");
                    instance = obj.AddComponent<EndDayController>();
                }
            }
            return instance;
        }
    }


    public GameObject EndDayPanel;
    public GameObject EndDayPanelBackground;
    public GameObject EndDayPanelText;

    public bool IsEndDayPanelActivated;


    public void AnimateTheEndDayPanel()
    {
        ActivateEndDayPanel();
        StartCoroutine(ActivateAndDesactivateEndDayPanel());
    }

    private IEnumerator ActivateAndDesactivateEndDayPanel()
    {
        // ActivateEndDayPanel();
        yield return new WaitForSeconds(2.5f);
        DeactivateEndDayPanel();
    }


    public void ActivateEndDayPanel()
    {
        ManageInteractableObjects.Instance.ActivateBlockInteractionsLayer();
        IsEndDayPanelActivated = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to prevent it from moving

        CoCardsController.Instance.canvasGroup.alpha = 0;
        InfoCardController.Instance.canvasGroup.alpha = 0;
        MiniIntelController.Instance.canvasGroup.alpha = 0;

        EndDayPanel.SetActive(true);
        EndDayPanelBackground.SetActive(true);
        EndDayPanelText.SetActive(true);
        EndDayPanelText.GetComponent<Text>().text = "Day " + GameController.Instance.CurrentDayCounter;
    }

    public void DeactivateEndDayPanel()
    {
        ManageInteractableObjects.Instance.DesctivateBlockInteractionsLayer();
        IsEndDayPanelActivated = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor

        CoCardsController.Instance.canvasGroup.alpha = 1;
        InfoCardController.Instance.canvasGroup.alpha = 1;
        MiniIntelController.Instance.canvasGroup.alpha = 1;

        EndDayPanel.SetActive(false);
        EndDayPanelBackground.SetActive(false);
        EndDayPanelText.SetActive(false);
    }
}