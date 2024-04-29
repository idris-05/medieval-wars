using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIcon : MonoBehaviour
{
    public float lifeTime;

    public void Start()
    {
        Invoke("Destruction",lifeTime);
    }

    public void Setup(int damage)
    {
        // texte t3 sprite ndirou 1 wela 2 wela .....
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
