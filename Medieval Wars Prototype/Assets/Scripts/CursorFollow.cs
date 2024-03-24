// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CursorFollow : MonoBehaviour
// {
//     private void Start()
//     {
//         // Hide the cursor
//         Cursor.visible = false;
//     }

//     private void Update()
//     {
//         FollowCursor();
//     }

//     // Follows the cursor position on the screen
//     private void FollowCursor()
//     {
//         if (Camera.main != null)
//         {
//             Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             transform.position = cursorPos;
//         }
//         else
//         {
//             Debug.Log("No main camera found");
//         }
//     }
// }
