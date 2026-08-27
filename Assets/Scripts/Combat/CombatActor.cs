using System;
using UnityEngine;

namespace NeoTokyo.HackSlash.Combat
{
    public sealed class CombatActor : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float maxHealth = 100f;
        public float CurrentHealth { get; private set; }
        public bool IsDefeated => CurrentHealth <= 0f;
        public event Action<float> Damaged;
        public event Action Defeated;

        private void Awake() => CurrentHealth = maxHealth;

        public void ApplyDamage(float amount)
        {
            if (IsDefeated || amount <= 0f) return;
            CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);
            Damaged?.Invoke(amount);
            if (IsDefeated) Defeated?.Invoke();
        }
    }
}
