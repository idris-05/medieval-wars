using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform cameraPosition;

    public PolygonCollider2D confiner; // Reference to the confiner


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

        // Ensure the new position stays within the confiner bounds
        newPosition = ConfineToBounds(newPosition);

        // Apply the new position to the cameraPosition
        cameraPosition.position = newPosition;
    }

    // Function to confine the camera position within the bounds
    private Vector3 ConfineToBounds(Vector3 position)
    {
        // Get the camera's viewport position
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(position);

        // Clamp the viewport position to stay within (0, 1) range
        viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
        viewportPosition.y = Mathf.Clamp01(viewportPosition.y);

        // Convert the clamped viewport position back to world space
        position = Camera.main.ViewportToWorldPoint(viewportPosition);

        // Ensure the confined position stays within the confiner bounds
        position.x = Mathf.Clamp(position.x, confiner.bounds.min.x, confiner.bounds.max.x);
        position.y = Mathf.Clamp(position.y, confiner.bounds.min.y, confiner.bounds.max.y);

        return position;
    }


}
