using UnityEngine;
using NeoTokyo.HackSlash.Combat;

namespace NeoTokyo.HackSlash.AI
{
    [RequireComponent(typeof(CombatActor))]
    public sealed class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField, Min(.1f)] private float chaseRange = 16f;
        [SerializeField, Min(.1f)] private float attackRange = 2.2f;
        [SerializeField, Min(.1f)] private float moveSpeed = 2.4f;
        [SerializeField] private Animator animator;
        private CombatActor actor;

        private void Awake() => actor = GetComponent<CombatActor>();
        private void Update()
        {
            if (actor.IsDefeated || target == null) return;
            var offset = target.position - transform.position; offset.y = 0f;
            if (offset.magnitude > chaseRange) return;
            if (offset.magnitude > attackRange) transform.position += offset.normalized * (moveSpeed * Time.deltaTime);
            transform.forward = Vector3.Slerp(transform.forward, offset.normalized, 8f * Time.deltaTime);
            animator?.SetFloat("MoveSpeed", offset.magnitude > attackRange ? 1f : 0f);
        }
    }
}
