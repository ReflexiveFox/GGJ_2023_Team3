using System;
using System.Collections;
using UnityEngine;

namespace RootBoy
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        public enum ProjectileType { NotDefined, Player, Enemy }
        [SerializeField] private ProjectileType type;
        [SerializeField] private float shootForce = 100f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float timeToLive = 5f;

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
            StartCoroutine(StartLifetimeCountdown());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((type is ProjectileType.Player && collision.collider.CompareTag("Enemy"))
                || (type is ProjectileType.Enemy && collision.collider.CompareTag("Player")))
            {                
                if(collision.collider.TryGetComponent(out Health entityHealth))
                    entityHealth.DealDamage(damage);
            }
            gameObject.SetActive(false);
        }


        private IEnumerator StartLifetimeCountdown()
        {
            yield return new WaitForSeconds(timeToLive);
            gameObject.SetActive(false);
        }
    }
}