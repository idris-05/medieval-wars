using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{

    // the hover effect is working well but sometimes it just gets crazy 
    // i dont know why yet , we'll be looking further to fix that later , its not a priority

    // The amount by which the object should scale when hovered
    public float hoverAmount ;

    // Method to scale the object when the mouse hovers over it 
    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }

    // Method to scale the object when the mouse exits the object 
    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
    }

}
