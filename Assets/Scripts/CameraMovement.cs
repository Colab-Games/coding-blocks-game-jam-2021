using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectToFollow;
    public float smoothTime = 0.3f;

    Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
