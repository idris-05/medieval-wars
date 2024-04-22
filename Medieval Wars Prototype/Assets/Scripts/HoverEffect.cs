// using System.Collections;
// using UnityEngine;

// public class HoverEffect : MonoBehaviour
// {
//     // The amount by which the object should scale when hovered
//     private readonly float hoverAmount = 0.5f; // Default hover amount
//     private readonly float hoverSpeed = 0.2f; // Default hover speed

//     private Vector3 originalScale; // The original scale of the object

//     private void Start()
//     {
//         // Store the original scale of the object
//         originalScale = transform.localScale;
//     }

//     // Method to scale the object when the mouse hovers over it 
//     private void OnMouseEnter()
//     {
//         // Apply hover effect if the object is not already scaled
//         if (!Mathf.Approximately(transform.localScale.magnitude, originalScale.magnitude))
//         {
//             return; // Exit if already scaled
//         }

//         // Scale the object smoothly with a coroutine
//         StartCoroutine(ScaleOverTime(transform.localScale + Vector3.one * hoverAmount, hoverSpeed));
//     }

//     // Method to scale the object when the mouse exits the object 
//     private void OnMouseExit()
//     {
//         // Scale the object smoothly with a coroutine
//         StartCoroutine(ScaleOverTime(originalScale, hoverSpeed));
//     }

//     // Coroutine to smoothly scale the object over time
//     private IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
//     {
//         float timer = 0f;
//         Vector3 initialScale = transform.localScale;

//         while (timer < duration)
//         {
//             transform.localScale = Vector3.Lerp(initialScale, targetScale, timer / duration);
//             timer += Time.deltaTime;
//             yield return null;
//         }

//         // Ensure the scale is exactly the target scale when the coroutine finishes
//         transform.localScale = targetScale;
//     }


// }








// // this was the script using before , and changed it now 



// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// // public class HoverEffect : MonoBehaviour
// // {

// //     // the hover effect is working well but sometimes it just gets crazy 
// //     // i dont know why yet , we'll be looking further to fix that later , its not a priority
// //     // i think the problem is in the way we handle the mouse events , and maybe the solution above works !
// //     // anyways , it doesn't matter for now , we'll be looking for it later , and we can remove it "no scaling for our units". 

// //     // The amount by which the object should scale when hovered
// //     public float hoverAmount;

// //     // Method to scale the object when the mouse hovers over it 
// //     private void OnMouseEnter()
// //     {
// //         transform.localScale += Vector3.one * hoverAmount;
// //     }

// //     // Method to scale the object when the mouse exits the object 
// //     private void OnMouseExit()
// //     {
// //         transform.localScale -= Vector3.one * hoverAmount;
// //     }

// // }
