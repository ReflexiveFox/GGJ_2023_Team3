using System.Collections;
using UnityEngine;

namespace RootBoy
{
    public class MeleeDamageDealer : MonoBehaviour
    {
        private PlayerChecker playerChecker;
        [SerializeField] private int damage = 10;
        [SerializeField] private float timeToWait;
        [SerializeField] private float distanceFromPlayer;
        private bool canDealDamage;
        private Transform target;


        private void Awake()
        {
            playerChecker = GetComponentInChildren<PlayerChecker>();
        }

        private void Start()
        {
            canDealDamage = true;
            playerChecker.OnPlayerEntered += CheckAttack;
            playerChecker.OnPlayerLeft += DisableAttack;
        }

        private void Update()
        {
            if(target && canDealDamage)
            {
                if(Vector3.Distance(transform.position, target.position) <= distanceFromPlayer)
                {
                    target.GetComponentInChildren<PlayerHealth>().DealDamage(damage);
                    StartCoroutine(CountBeforeAttack());
                }
            }
        }

        private void OnDestroy()
        {
            playerChecker.OnPlayerEntered -= CheckAttack;
            playerChecker.OnPlayerLeft -= DisableAttack;
        }

        private void DisableAttack()
        {
            target = null;
        }

        private void CheckAttack(GameObject playerGO)
        {
            target = playerGO.transform;
        }

        private IEnumerator CountBeforeAttack()
        {
            canDealDamage = false;
            yield return new WaitForSeconds(timeToWait);
            canDealDamage = true;
        }
    }
}