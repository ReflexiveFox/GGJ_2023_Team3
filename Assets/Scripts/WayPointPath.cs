using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointPath : MonoBehaviour
{
    public Transform GetWaypoint (int waypointindex)
    {
        return transform.GetChild(waypointindex);
    }

    public int GetNextWaypointIndex (int currentWaypointIndex)
    {
        int nextWaypointindex = currentWaypointIndex + 1;

        if (nextWaypointindex == transform.childCount)
        {
            nextWaypointindex = 0;
        }

        return nextWaypointindex;
    }
}
