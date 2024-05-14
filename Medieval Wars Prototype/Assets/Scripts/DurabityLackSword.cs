using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabityLackSword : MonoBehaviour
{
    public Unit unit;
    public Transform durabilityLackAppleTransform;

    private void Start()
    {
        StartCoroutine(PlayDurabilityLackFlash());
    }


    private void Update()
    {
        durabilityLackAppleTransform.position = unit.transform.position + new Vector3(0.1f, 0.3f, 0);
    }

    public IEnumerator PlayDurabilityLackFlash()
    {
        while (true)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.4f);
            this.GetComponent<SpriteRenderer>().color = Color.black;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
