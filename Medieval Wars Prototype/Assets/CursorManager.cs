using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    
    }
}
