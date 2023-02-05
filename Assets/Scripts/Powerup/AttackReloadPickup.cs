using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootBoy
{
    public class AttackReloadPickup : MonoBehaviour
    {
        public float rotationSpeed = 100.0f;
        [SerializeField] private float newReloadTime;
        [SerializeField] private float boostAmount;
        public AudioClip PowerUp;

        void Update()
        {
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerShooting playerShooting))
            {

                if (playerShooting)
                {
                    AudioSource.PlayClipAtPoint(PowerUp, transform.position);
                    playerShooting.BoostReloadTime(newReloadTime, boostAmount);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}