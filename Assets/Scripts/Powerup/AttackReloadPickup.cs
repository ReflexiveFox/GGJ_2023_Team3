using UnityEngine;

namespace RootBoy
{
    public class AttackReloadPickup : MonoBehaviour
    {
        [SerializeField] private float newReloadTime;
        [SerializeField] private float boostAmount;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerShooting playerShooting))
            {
                if (playerShooting)
                {
                    playerShooting.BoostReloadTime(newReloadTime, boostAmount);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}