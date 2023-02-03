using System.Collections;
using UnityEngine;

namespace RootBoy
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] private float shootForce = 100f;

        private Rigidbody projectileRb;
        private void Awake()
        {
            projectileRb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            projectileRb.velocity = Vector3.zero;
            projectileRb.angularVelocity = Vector3.zero;

            projectileRb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
        }
    }
}