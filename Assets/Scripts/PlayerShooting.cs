using System.Collections;
using UnityEngine;

namespace RootBoy
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform startProjectileTransform;
        [SerializeField, Min(0.01f)] private float reloadTime;
        private float _currentReload;
        private bool canShoot;

        private void Start()
        {
            _currentReload = reloadTime;
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
                temp.transform.parent = startProjectileTransform;
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localRotation = Quaternion.Euler(Vector3.zero);
                temp.transform.parent = null;
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
            yield return new WaitForSeconds(_currentReload);
            canShoot = true;
        }


        public void BoostReloadTime(float newReloadBoost, float boostTime)
        {
            StartCoroutine(BoostReloadTime_Coroutine(newReloadBoost, boostTime));
        }

        private IEnumerator BoostReloadTime_Coroutine(float reloadBoost, float boostTime)
        {
            _currentReload = reloadBoost;
            yield return new WaitForSeconds(boostTime);
            _currentReload = reloadTime;
        }
    }
}