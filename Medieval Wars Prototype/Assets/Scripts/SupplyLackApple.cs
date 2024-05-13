using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyLackApple : MonoBehaviour
{
    public Unit unit;
    public Transform supplyLackAppleTransform;


    private void Start()
    {
        StartCoroutine(PlaySupplyLackFlash());
    }


    private void Update()
    {
        supplyLackAppleTransform.position = unit.transform.position + new Vector3(0.4f,0.3f, 0);
    }

    public IEnumerator PlaySupplyLackFlash()
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
