using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform[] idleSpots;

    int currentPos = 0;
    float waitTime = 1f; // in seconds
    float waitCounter = 0f;
    bool waiting = false;

    void Update()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
                return;
            waiting = false;
        }

        Transform corners = idleSpots[currentPos];
        if (Vector2.Distance(transform.position, corners.position) < 0.1f)
        {
            transform.position = corners.position;
            waitCounter = 0f;
            waiting = true;

            currentPos = (currentPos + 1) % idleSpots.Length;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, corners.position, speed * Time.deltaTime);
        }
    }
}
