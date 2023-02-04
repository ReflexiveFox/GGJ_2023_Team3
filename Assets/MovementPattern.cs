using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RootBoy
{
    public class MovementPattern : MonoBehaviour
    {
        NavMeshAgent agent;
        public Transform[] waypoints;
        int waypointIndex;
        Vector3 target;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Start is called before the first frame update
        void OnEnable()
        {
            UpdateDestination();
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }

        void UpdateDestination()
        {
            target = waypoints[waypointIndex].position;
            agent.SetDestination(target);
        }

        void IterateWaypointIndex()
        {
            waypointIndex++;
            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
}