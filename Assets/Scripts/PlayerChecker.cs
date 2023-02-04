using System;
using UnityEngine;

namespace RootBoy
{
    public class PlayerChecker : MonoBehaviour
    {
        public event Action<GameObject> OnPlayerEntered = delegate { };
        public event Action OnPlayerLeft = delegate { };

        [SerializeField] private SphereCollider checkPlayerArea;
        [SerializeField] private float triggerAreaRadius = 20f;

        private void Start()
        {
            checkPlayerArea.radius = triggerAreaRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEntered?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                OnPlayerLeft?.Invoke();
            }
        }
    }
}