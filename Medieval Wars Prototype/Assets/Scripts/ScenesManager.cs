using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{
    private static ScenesManager instance;
    public static ScenesManager Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<ScenesManager>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("ScenesManager");
                    instance = obj.AddComponent<ScenesManager>();
                }
            }
            return instance;
        }
    }


    public static int mapToLoad;
    public static bool Load;
    [SerializeField] Animator transitionAnim;
    public void StartMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
        transitionAnim.SetTrigger("Start");
        Load = false ;
    }

    public void StartMap(int mapId)
    {
        mapToLoad = mapId;
        // ScenesManager.Load = Load;

        Debug.Log("StartMap map" + mapId);
        StartCoroutine(LoadMap(1));
    }

    public void LoadMapFromSave(int mapId)
    {
        SavingSystem.GetThePathForLoad(mapId);
        if (SavingSystem.IsEmpty(SavingSystem.PATH1) || SavingSystem.IsEmpty(SavingSystem.PATH2) || SavingSystem.IsEmpty(SavingSystem.PATHN))
        {
            StartMainMenu();
            Debug.Log("No save data found");
            return;
        }
        Load = true ;
        StartMap(mapId);
        // GameController.Instance.load();

        // ScenesManager.Load = Load;

        Debug.Log("LoadMap map" + mapId);
    }


    IEnumerator LoadMap(int mapId)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(mapId + 1);
        transitionAnim.SetTrigger("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
