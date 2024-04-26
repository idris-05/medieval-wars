using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;

    public void StartGame()
    {
        StartCoroutine(LoadMap());
    }

    IEnumerator LoadMap()
    {
        transitionAnim.SetTrigger("End");
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(1);
        transitionAnim.SetTrigger("Start");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
