using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private MapGrid mapGrid;

    private Vector3 cameraFollowPosition;

    private float zoom = 0f;

    private void Start()
    {
        cameraFollow.Setup(() => cameraFollowPosition, () => zoom);
    }

    private void Update()
    {
        HandleZoom();
        HandleManualMovement();
        // HandleEdgeScrolling(); 
    }

    private void HandleZoom()
    {
        float zoomChangeAmount = 100f;

        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime;
        }

        zoom = Mathf.Clamp(zoom, 2f, 250f);
    }

    private void HandleManualMovement()
    {

        float moveAmount = 10f;
        if (Input.GetKey(KeyCode.W))
        {
            cameraFollowPosition.y += moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraFollowPosition.y -= moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
    }

    private void HandleEdgeScrolling()
    {
        float moveAmount = 10f;
        float edgeSize = 10f;
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            //Edge Right
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.x < edgeSize)
        {
            //Edge Left
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            //Edge Up
            cameraFollowPosition.y += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y < edgeSize)
        {
            //Edge Down
            cameraFollowPosition.y -= moveAmount * Time.deltaTime;
        }
    }
}