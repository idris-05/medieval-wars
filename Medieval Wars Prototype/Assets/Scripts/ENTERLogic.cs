using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ENTERLogic : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;

    void Update()
    {
        if (Input.GetKey("enter"))
        {
            StartCoroutine(LoadMainMenu());
        }

    }

    IEnumerator LoadMainMenu()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
        transitionAnim.SetTrigger("Start");

    }
}
