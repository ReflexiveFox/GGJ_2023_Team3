using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RootBoy
{
    public class MovementPattern : MonoBehaviour
    {
        private PlayerChecker playerChecker;
        private NavMeshAgent agent;
        public Transform[] waypoints;
        private int waypointIndex;

        private bool isChasingPlayer;
        private Vector3 target;
        private Transform _targetTransform;

        private Transform TargetTransform
        {
            get => _targetTransform;
            set
            {
                _targetTransform = value;
                isChasingPlayer = value is not null;
                if (!isChasingPlayer)
                    UpdateDestination(waypoints[waypointIndex].position);
            }
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            playerChecker = GetComponentInChildren<PlayerChecker>();
        }

        // Start is called before the first frame update
        void Start()
        {
            UpdateDestination(waypoints[waypointIndex].position);

            playerChecker.OnPlayerEntered += EngageTarget;
            playerChecker.OnPlayerLeft += DisengageTarget;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isChasingPlayer)
            {
                if (Vector3.Distance(transform.position, target) < 1)
                {
                    IterateWaypointIndex();
                    UpdateDestination(waypoints[waypointIndex].position);
                }
            }
            else if (TargetTransform != null)
            {
                UpdateDestination(TargetTransform.position);
            }
        }

        private void OnDestroy()
        {
            playerChecker.OnPlayerEntered -= EngageTarget;
            playerChecker.OnPlayerLeft -= DisengageTarget;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TargetTransform = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TargetTransform = null;
            }
        }

        void UpdateDestination(Vector3 targetPosition)
        {
            target = targetPosition;
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

        private void EngageTarget(GameObject playerGO)
        {
            TargetTransform = playerGO.transform;
        }

        private void DisengageTarget()
        {
            TargetTransform = null;
        }
    }
}