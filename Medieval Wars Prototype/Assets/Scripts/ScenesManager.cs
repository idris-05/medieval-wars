using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{
    public static int mapToLoad; 
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
    }

    public void StartMap(int mapId)
    {
        mapToLoad = mapId;
        Debug.Log("StartMap map" + mapId);
        StartCoroutine(LoadMap(1));
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
