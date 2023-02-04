using System.Collections;
using UnityEngine;

namespace RootBoy
{
    public class EnemyShooting : MonoBehaviour
    {
        private PlayerChecker playerChecker;
        [SerializeField] private Transform startProjectilePosition;
        [SerializeField, Min(0.01f)] private float reloadTime;
        [SerializeField, Min(1f)] private float shootingRange;
        private bool canShoot;
        private Transform targetTransform;

        private void Awake()
        {
            playerChecker = GetComponentInChildren<PlayerChecker>();
        }

        private void Start()
        {
            canShoot = true;
            playerChecker.OnPlayerEntered += SetTarget;
            playerChecker.OnPlayerLeft += DisengageTarget;
            PlayerHealth.OnPlayerDead += DisableComponent;
        }

        private void DisableComponent()
        {
            enabled = false;
        }

        private void OnDestroy()
        {
            playerChecker.OnPlayerEntered -= SetTarget;
            playerChecker.OnPlayerLeft -= DisengageTarget;
            PlayerHealth.OnPlayerDead -= DisableComponent;
        }

        private void DisengageTarget()
        {
            targetTransform = null;
        }

        private void SetTarget(GameObject playerGO)
        {
            targetTransform = playerGO.transform;
        }

        void Update()
        {
            if (targetTransform != null)
            {
                Vector3 aimToPlayer = targetTransform.position - transform.position;
                aimToPlayer.y = 0;
                transform.forward = aimToPlayer;
                if (canShoot && Vector3.Distance(transform.position, targetTransform.position) <= shootingRange)
                {
                    GameObject temp = ObjectPooler.SharedInstance.GetPooledObject("ProjectileEnemy");
                    temp.transform.position = startProjectilePosition.position;
                    temp.transform.rotation = startProjectilePosition.rotation;
                    temp.SetActive(true);
                    StartCoroutine(StartReloading());
                }
            }
        }

        private IEnumerator StartReloading()
        {
            canShoot = false;
            yield return new WaitForSeconds(reloadTime);
            canShoot = true;
        }
    }
}