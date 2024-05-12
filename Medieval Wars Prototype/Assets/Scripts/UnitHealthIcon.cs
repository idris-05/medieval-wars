using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthIcon : MonoBehaviour
{

    public Unit unit;
    public Transform healthIconTransform;

    void Update()
    {

        if (unit.playerOwner == GameController.Instance.player1) {
            healthIconTransform.position = unit.transform.position + new Vector3(-0.3f,-0.4f,0);
        }
        if ( unit.playerOwner == GameController.Instance.player2)
        {
            healthIconTransform.position = unit.transform.position + new Vector3(0.3f,-0.4f,0);
        }
    }
}
