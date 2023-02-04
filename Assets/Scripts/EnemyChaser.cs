using UnityEngine;
using UnityEngine.AI;

namespace RootBoy
{
    public class EnemyChaser : MonoBehaviour
    {
        private NavMeshAgent agent;
        private PlayerChecker playerChecker;
        private Vector3 startPos;
        private Transform _targetTransform;

        private Transform TargetTransform
        {
            get => _targetTransform;
            set
            {
                _targetTransform = value;
                agent.SetDestination(_targetTransform is null ? startPos : _targetTransform.position);
            }
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            playerChecker = GetComponentInChildren<PlayerChecker>();
        }

        private void Start()
        {
            startPos = transform.position;
            playerChecker.OnPlayerEntered += EngageTarget;
            playerChecker.OnPlayerLeft += DisengageTarget;
        }

        private void Update()
        {
            if (_targetTransform != null)
            {
                agent.SetDestination(_targetTransform.position);
            }
        }

        private void OnDestroy()
        {
            playerChecker.OnPlayerEntered -= EngageTarget;
            playerChecker.OnPlayerLeft -= DisengageTarget;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
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