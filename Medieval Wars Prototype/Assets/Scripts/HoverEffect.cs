using System.Collections;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    // The amount by which the object should scale when hovered
     private readonly float hoverAmount = 0.3f;  // Default hover amount
     private readonly float hoverSpeed = 0.1f;  // Default hover speed

     public SpriteRenderer spriteRenderer;

     private Vector3 originalScale;  // The original scale of the object

     private void Start()
     {
          // Store the original scale of the object
         spriteRenderer = GetComponent<SpriteRenderer>();
         originalScale = transform.localScale;
     }

     // Method to scale the object when the mouse hovers over it 
     private void OnMouseEnter()
     {
        spriteRenderer.material.color = Color.white;

        // Apply hover effect if the object is not already scaled
        if (!Mathf.Approximately(transform.localScale.magnitude, originalScale.magnitude))
         {
             return; // Exit if already scaled
         }

         // Scale the object smoothly with a coroutine
         StartCoroutine(ScaleOverTime(transform.localScale + Vector3.one * hoverAmount, hoverSpeed));
     }

     // Method to scale the object when the mouse exits the object 
     private void OnMouseExit()
     {

          // Scale the object smoothly with a coroutine
        StartCoroutine(ScaleOverTime(originalScale, hoverSpeed));
        spriteRenderer.material.color = new Color(0,0,0,0); // no outline at all

    }

    // Coroutine to smoothly scale the object over time
    private IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
     {
         float timer = 0f;
         Vector3 initialScale = transform.localScale;

         while (timer < duration)
         {
             transform.localScale = Vector3.Lerp(initialScale, targetScale, timer / duration);
             timer += Time.deltaTime;
             yield return null;
         }

          //Ensure the scale is exactly the target scale when the coroutine finishes
         transform.localScale = targetScale;
     }


}








 