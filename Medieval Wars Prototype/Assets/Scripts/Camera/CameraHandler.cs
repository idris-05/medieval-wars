using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        HandleManualMovement();
    }

    private void HandleManualMovement()
    {
        float moveAmount = 10f;

        // Calculate new position based on input
        Vector3 newPosition = cameraPosition.position;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += Vector3.up * moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += Vector3.down * moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += Vector3.left * moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += Vector3.right * moveAmount * Time.deltaTime;
        }

        // Apply the new position to the cameraPosition
        cameraPosition.position = newPosition;
    }
}
