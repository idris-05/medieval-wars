using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform targetCameraTransform;
    void Update()
    {
        transform.position = targetCameraTransform.position;
    }
}
