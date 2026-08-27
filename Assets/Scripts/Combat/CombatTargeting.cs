using UnityEngine;
using UnityEngine.InputSystem;

namespace NeoTokyo.HackSlash.Combat
{
    public sealed class CombatTargeting : MonoBehaviour
    {
        [SerializeField] private Camera viewCamera;
        [SerializeField] private Transform aimOrigin;
        [SerializeField, Min(1f)] private float radius = 14f;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private Transform lockMarker;

        public CombatActor CurrentTarget { get; private set; }

        private void Awake()
        {
            if (viewCamera == null) viewCamera = Camera.main;
            if (aimOrigin == null) aimOrigin = transform;
            SetMarker(false);
        }

        private void Update()
        {
            if (Keyboard.current?.tabKey.wasPressedThisFrame == true)
            {
                if (CurrentTarget == null) AcquireNearest();
                else ClearTarget();
            }

            if (CurrentTarget != null && !IsValid(CurrentTarget))
                ClearTarget();

            if (CurrentTarget != null)
            {
                var look = CurrentTarget.transform.position - transform.position;
                look.y = 0f;
                if (look.sqrMagnitude > 0.01f)
                    transform.forward = Vector3.Slerp(transform.forward, look.normalized, 12f * Time.deltaTime);
                if (lockMarker != null)
                    lockMarker.position = CurrentTarget.transform.position + Vector3.up * 1.8f;
            }
        }

        public void AcquireNearest()
        {
            var bestDistance = float.PositiveInfinity;
            CombatActor best = null;

            foreach (var candidate in FindObjectsByType<CombatActor>(FindObjectsSortMode.None))
            {
                if (!IsValid(candidate) || !IsInMask(candidate.gameObject.layer)) continue;

                var offset = candidate.transform.position - aimOrigin.position;
                if (offset.sqrMagnitude > radius * radius) continue;
                if (viewCamera != null && Vector3.Dot(viewCamera.transform.forward, offset.normalized) < 0.1f) continue;

                var distance = offset.sqrMagnitude;
                if (distance < bestDistance && HasLineOfSight(candidate))
                {
                    bestDistance = distance;
                    best = candidate;
                }
            }

            CurrentTarget = best;
            SetMarker(CurrentTarget != null);
        }

        public void ClearTarget()
        {
            CurrentTarget = null;
            SetMarker(false);
        }

        private bool IsValid(CombatActor candidate) =>
            candidate != null && !candidate.IsDefeated && candidate.transform != transform;

        private bool IsInMask(int layer) => (targetMask.value & (1 << layer)) != 0;

        private bool HasLineOfSight(CombatActor candidate)
        {
            var origin = aimOrigin.position + Vector3.up * 1.1f;
            var destination = candidate.transform.position + Vector3.up * 1.1f;
            return !Physics.Linecast(origin, destination, out var hit) ||
                   hit.transform == candidate.transform ||
                   hit.transform.IsChildOf(candidate.transform);
        }

        private void SetMarker(bool visible)
        {
            if (lockMarker != null) lockMarker.gameObject.SetActive(visible);
        }
    }
}
