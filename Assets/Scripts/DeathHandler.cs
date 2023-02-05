using UnityEngine;

namespace RootBoy
{
    public class DeathHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("DeathColliders"))
            {
                GetComponent<PlayerHealth>().DealDamage(999);
            };
        }
    }
}