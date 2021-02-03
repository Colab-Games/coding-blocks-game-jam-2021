using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Activatable))]
public class MovingPlatform : MonoBehaviour
{
    public float speed = 0.06f;
    public float waitDuration = 1f;
    public float lerpValue = 0.1f;
    public Vector2[] points;
    public float tolerance = 0.01f;

    uint destinationIndex;
    Vector2 offset;
    float waitTime;

    Vector3 targetPosition;

    Activatable activable;

    void OnDrawGizmos()
    {
        if (points != null)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawLine(points[i], points[(i+1) % points.Length]);
            }
        }
    }

    void Start()
    {
        activable = GetComponent<Activatable>();

        destinationIndex = 0;
        offset = new Vector2(transform.position.x, transform.position.y) - points[0];
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpValue);
        if (!GameManager.IsMechanicOperational(BreakableMechanic.MovablePlatform) || !activable.isActive || waitTime > Time.time)
            return;

        Vector2 destination = points[destinationIndex] + offset;
        Vector2 heading = destination - new Vector2(targetPosition.x, targetPosition.y);
        Vector2 movement = heading.normalized * speed;
        if (heading.sqrMagnitude <= movement.sqrMagnitude + tolerance) // sqrMagnitude is faster than magnitude and the comparison stills the same
        {
            targetPosition = new Vector3(destination.x, destination.y, targetPosition.z);
            destinationIndex += 1;
            if (destinationIndex >= points.Length)
                destinationIndex = 0;

            waitTime = Time.time + waitDuration;
        }
        else
            targetPosition += new Vector3(movement.x, movement.y, 0);
    }
}
