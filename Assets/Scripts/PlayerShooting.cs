using System.Collections;
using UnityEngine;

namespace RootBoy
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform startProjectilePosition;
        [SerializeField, Min(0.01f)] private float reloadTime;
        private bool canShoot;

        private void Start()
        {
            canShoot = true;
            PlayerHealth.OnPlayerDead += DisableComponent;
        }

        private void DisableComponent()
        {
            enabled = false;
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                GameObject temp = ObjectPooler.SharedInstance.GetPooledObject("ProjectilePlayer");
                temp.transform.position = startProjectilePosition.position;
                temp.transform.rotation = startProjectilePosition.rotation;
                temp.SetActive(true);
                StartCoroutine(StartReloading());
            }
        }

        private void OnDestroy()
        {
            PlayerHealth.OnPlayerDead -= DisableComponent;
        }

        private IEnumerator StartReloading()
        {
            canShoot = false;
            yield return new WaitForSeconds(reloadTime);
            canShoot = true;
        }
    }
}