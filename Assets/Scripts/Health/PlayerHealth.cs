using System;
using UnityEngine;

namespace RootBoy
{
    public class PlayerHealth : Health
    {
        public static event Action OnPlayerDead = delegate { };
        public static event Action<int> OnPlayerDamaged = delegate { };
        public static event Action<int> OnPlayerHealed = delegate { };
        public static event Action<int, int> OnHealthValueChanged = delegate { };

        protected override void Start()
        {
            base.Start();
            OnHealthDepleted += InvokePlayerDeathEvent;
            OnHealthHealed += InvokePlayerHealedEvent;
            OnHealthDamaged += InvokePlayerDamagedEvent;
            OnHealthChanged += InvokePlayerHealthChangedEvent;
        }

        private void OnDestroy()
        {
            OnHealthDepleted -= InvokePlayerDeathEvent;
            OnHealthHealed -= InvokePlayerHealedEvent;
            OnHealthDamaged -= InvokePlayerDamagedEvent;
            OnHealthChanged -= InvokePlayerHealthChangedEvent;
        }

        private void TakeHeatDamage(float curretHeatDamage)
        {
            DealDamage((int)curretHeatDamage);
        }

        private void ResetResistance()
        {
            reductionFactor = 1f;
        }

        private void IncreaseResistance(float resistance)
        {
            //increase protection by removing % resistance from current reduction factor
            reductionFactor *= (resistance / 100f);
        }

        private void InvokePlayerHealthChangedEvent(int curHealth, int maxHealth)
        {
            OnHealthValueChanged?.Invoke(curHealth, maxHealth);
        }

        private void InvokePlayerDeathEvent()
        {
            OnPlayerDead?.Invoke();
        }

        private void InvokePlayerDamagedEvent(int damage)
        {
            OnPlayerDamaged?.Invoke(damage);
        }

        private void InvokePlayerHealedEvent(int healAmount)
        {
            OnPlayerHealed?.Invoke(healAmount);
        }
    }
}