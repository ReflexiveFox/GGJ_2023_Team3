using UnityEngine;

namespace RootBoy
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private int healAmount;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerHealth playerHealth))
            {
                if (!playerHealth.IsMaxHealth)
                {
                    playerHealth.Heal(healAmount);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}