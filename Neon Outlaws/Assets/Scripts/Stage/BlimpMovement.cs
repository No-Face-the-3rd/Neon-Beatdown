using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken and changed from a Sebastian Lague 2D platformer tutorial
public class BlimpMovement : MonoBehaviour {
    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

    public float speed;
    //public float waitTime;
    float nextMoveTime;
    float percentBetweenWaypoints;
    int fromWaypointIndex;
    public bool cyclic;
    [Range(0, 2)] // Ease amount needs to be between 0 and 2
    public float easeAmount;

	// Use this for initialization
	void Start () {
        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
            globalWaypoints[i] = localWaypoints[i] + transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveAmount = blimpMovement();
        transform.Translate(moveAmount);
	}

    float ease(float x)
    {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    // Waypoint positions are relative to the blimp's position, not the world position
    // (so to get it to move 50 units on the Y axis, a waypoint's position needs to be set as (0, 50, 0))
    Vector3 blimpMovement()
    {
        if (Time.time < nextMoveTime)
            return Vector3.zero;

        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distance = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * speed / distance;
        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easePercent = ease(percentBetweenWaypoints);

        Vector3 newPosition = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easePercent);

        // If a new waypoint has been reached, move on to the next one
        if (percentBetweenWaypoints >= 1)
        {
            percentBetweenWaypoints = 0;
            fromWaypointIndex++;
            if (!cyclic)
                if (fromWaypointIndex >= globalWaypoints.Length - 1)
                {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            //nextMoveTime = Time.time + waitTime;
        }
        return newPosition - transform.position;
    }

    //void OnDrawGizmos()
    //{
    //    if (localWaypoints != null)
    //    {
    //        Gizmos.color = Color.red;
    //        float size = .3f;

    //        for (int i = 0; i < localWaypoints.Length; i++)
    //        {
    //            Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
    //            Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
    //            Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
    //        }
    //    }
    //}
}
