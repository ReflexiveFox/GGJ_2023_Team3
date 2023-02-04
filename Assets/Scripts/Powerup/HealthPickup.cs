using UnityEngine;

namespace RootBoy
{
    public class HealthPickup : MonoBehaviour
    {
        public float rotationSpeed = 100.0f;
        [SerializeField] private int healAmount;

        void update()
        {
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }

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