using UnityEngine;

public class MainMenuController : MonoBehaviour
{


    private static MainMenuController instance;
    public static MainMenuController Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<MainMenuController>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("MainMenuController");
                    instance = obj.AddComponent<MainMenuController>();
                }
            }
            return instance;
        }
    }



    public GameObject mainMenu;

    public void ActivateMenu()
    {
        CancelScript.Instance.OnCancelButtonClicked();
        mainMenu.SetActive(true);
        ManageInteractableObjects.Instance.ActivateBlockInteractionsLayer();
    }


    public void DeactivateMenu()
    {
        mainMenu.SetActive(false);
        ManageInteractableObjects.Instance.DesctivateBlockInteractionsLayer();
    }


}