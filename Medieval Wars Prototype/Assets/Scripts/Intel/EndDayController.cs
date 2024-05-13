using System.Collections;
using UnityEngine;

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
    public GameObject EndDayPanelDark;


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
        EndDayPanel.SetActive(true);
        EndDayPanelBackground.SetActive(true);
        EndDayPanelText.SetActive(true);
        EndDayPanelDark.SetActive(true);
    }

    public void DeactivateEndDayPanel()
    {
        EndDayPanel.SetActive(false);
        EndDayPanelBackground.SetActive(false);
        EndDayPanelText.SetActive(false);
        EndDayPanelDark.SetActive(false);
    }
}