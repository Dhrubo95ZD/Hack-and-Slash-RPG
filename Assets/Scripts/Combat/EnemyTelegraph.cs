using System;
using UnityEngine;

namespace NeoTokyo.HackSlash.Combat
{
    public sealed class EnemyTelegraph : MonoBehaviour
    {
        [SerializeField] private CombatActor actor;
        [SerializeField] private Transform warningOrigin;
        [SerializeField] private MeshRenderer warningRenderer;
        [SerializeField] private Color idleColor = new(0.04f, 0.85f, 1f, 0.18f);
        [SerializeField] private Color dangerColor = new(1f, 0.18f, 0.16f, 0.62f);
        [SerializeField, Min(0.1f)] private float warningSeconds = 0.7f;
        [SerializeField, Min(0.1f)] private float recoverySeconds = 1.4f;

        public event Action AttackStarted;
        public event Action AttackWindowOpened;
        public bool IsWarning { get; private set; }
        private float nextAttackAt;

        private void Awake()
        {
            if (actor == null) actor = GetComponent<CombatActor>();
            SetWarning(false);
            nextAttackAt = Time.time + recoverySeconds;
        }

        private void Update()
        {
            if (actor != null && actor.IsDefeated) return;
            if (!IsWarning && Time.time >= nextAttackAt) BeginWarning();
        }

        public void BeginWarning()
        {
            if (IsWarning) return;
            IsWarning = true;
            SetWarning(true);
            AttackStarted?.Invoke();
            Invoke(nameof(OpenAttackWindow), warningSeconds);
        }

        public void CompleteAttackWindow()
        {
            CancelInvoke(nameof(OpenAttackWindow));
            IsWarning = false;
            SetWarning(false);
            nextAttackAt = Time.time + recoverySeconds;
        }

        private void OpenAttackWindow()
        {
            if (!IsWarning) return;
            AttackWindowOpened?.Invoke();
            CompleteAttackWindow();
        }

        private void SetWarning(bool warning)
        {
            if (warningRenderer == null) return;
            warningRenderer.material.color = warning ? dangerColor : idleColor;
            if (warningOrigin != null) warningOrigin.localScale = warning ? Vector3.one * 1.12f : Vector3.one;
        }

        private void OnDisable() => CancelInvoke();
    }
}
