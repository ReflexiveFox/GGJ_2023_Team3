using System;

namespace RootBoy
{
    public class EnemyHealth : Health
    {
        public static event Action<EnemyHealth> OnEnemyDead = delegate { };
        public static event Action<EnemyHealth, int> OnEnemyDamaged = delegate { };
        
        protected override void Start()
        {
            base.Start();
            OnHealthDepleted += InvokeDeathEvent;
            OnHealthDamaged += InvokeOnEnemyDamagedEvent;
        }

        private void InvokeOnEnemyDamagedEvent(int damage)
        {
            OnEnemyDamaged?.Invoke(this, damage);
        }

        private void OnDestroy()
        {
            OnHealthDepleted -= InvokeDeathEvent;
            OnHealthDamaged -= InvokeOnEnemyDamagedEvent;
        }

        //used for Rage module
        private void InvokeDeathEvent()
        {
            OnEnemyDead?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}