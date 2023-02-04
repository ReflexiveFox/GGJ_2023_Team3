using System;
using UnityEngine;

namespace RootBoy
{
    public class Health : MonoBehaviour
    {
        public event Action<int> OnHealthDamaged = delegate { };
        public event Action<int> OnHealthHealed = delegate { };
        public event Action<int, int> OnHealthChanged = delegate { };   //Event when health changes with currentHealth and maxHealth
        public event Action OnHealthDepleted = delegate { };    //When health reaches 0

        [Header("References")]
        [SerializeField] private SimpleHealthBar healthBar;

        [Header("Stats")]
        [SerializeField] private int maxHealth = 100;
        private int _currentHealth;
        [Tooltip("Used for damage reduction with DoubleArmor module")]
        protected float reductionFactor = 1f;

        public int CurrentHealth => _currentHealth;

        public int MaxHealth => maxHealth;

        public bool IsMaxHealth => _currentHealth >= maxHealth;

        public bool IsAlive => _currentHealth > 0f;

        protected virtual void Start()
        {
            ResetHealth();
            reductionFactor = 1f;
        }

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
            healthBar.UpdateBar(_currentHealth, maxHealth);
        }

        public void DealDamage(int damage)
        {
            if (_currentHealth <= 0f)
                return;

            int newDamage = (int) Mathf.Floor(damage * reductionFactor);
            _currentHealth -= newDamage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnHealthDepleted?.Invoke();
            }
            OnHealthDamaged?.Invoke(damage);

            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
            healthBar.UpdateBar(_currentHealth, maxHealth);
        }

        public void Heal(int healingAmount)
        {
            _currentHealth += healingAmount;  
            if(_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }
            OnHealthHealed?.Invoke(healingAmount);

            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
            healthBar.UpdateBar(_currentHealth, maxHealth);
        }

        public static Health FindComponentIn(Transform otherTransform)
        {
            Transform currentTransform = otherTransform;
            Health health = null;
            while (health is null)
            {
                health = currentTransform.GetComponent<Health>();
                if (health is null)
                {
                    currentTransform = currentTransform.parent;
                }
            }
            return health;
        }
    }
}