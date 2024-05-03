using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIcon : MonoBehaviour
{

    public Sprite[] damageSprites;


    public float lifeTime;

    public void Start()
    {
        Invoke("Destruction",lifeTime);
    }

    public void SetupDamageToDisplay(int damage)
    {
        GetComponent<SpriteRenderer>().sprite = damageSprites[GameUtil.GetDamageToDisplayFromRealDamage(damage)];
    }

    void Destruction()
    {
        Destroy(gameObject);
    }

   
}
