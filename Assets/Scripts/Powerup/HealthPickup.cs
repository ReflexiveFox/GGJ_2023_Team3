using UnityEngine;

namespace RootBoy
{
    public class HealthPickup : MonoBehaviour
    {
        public float rotationSpeed = 100.0f;
        [SerializeField] private int healAmount;
        public AudioClip PowerUp;
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
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