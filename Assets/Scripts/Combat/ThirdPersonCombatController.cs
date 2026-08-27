using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeoTokyo.HackSlash.Combat
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CombatActor))]
    public sealed class ThirdPersonCombatController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Animator animator;
        [SerializeField] private AbilityDefinition[] equippedAbilities;
        [SerializeField, Min(0.1f)] private float moveSpeed = 4.8f;
        [SerializeField, Min(0.1f)] private float attackRadius = 2.4f;
        [SerializeField] private LayerMask enemyMask;
        private readonly Dictionary<string, float> cooldowns = new();
        private CharacterController controller;

        private void Awake() => controller = GetComponent<CharacterController>();

        private void Update()
        {
            var keyboard = Keyboard.current;
            var input = keyboard == null ? Vector2.zero : new Vector2((keyboard.dKey.isPressed ? 1 : 0) - (keyboard.aKey.isPressed ? 1 : 0), (keyboard.wKey.isPressed ? 1 : 0) - (keyboard.sKey.isPressed ? 1 : 0));
            Move(input);
            var keys = new[] { keyboard?.jKey, keyboard?.kKey, keyboard?.lKey };
            for (var i = 0; i < keys.Length && i < equippedAbilities.Length; i++) if (keys[i]?.wasPressedThisFrame == true) UseAbility(equippedAbilities[i]);
            foreach (var key in new List<string>(cooldowns.Keys)) cooldowns[key] = Mathf.Max(0f, cooldowns[key] - Time.deltaTime);
        }

        private void Move(Vector2 input)
        {
            if (cameraTransform == null) return;
            var forward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            var right = cameraTransform.right; right.y = 0f; right.Normalize();
            var direction = (forward * input.y + right * input.x);
            if (direction.sqrMagnitude > 1f) direction.Normalize();
            controller.SimpleMove(direction * moveSpeed);
            if (direction.sqrMagnitude > .01f) transform.forward = Vector3.Slerp(transform.forward, direction, 14f * Time.deltaTime);
            animator?.SetFloat("MoveSpeed", direction.magnitude);
        }

        private void UseAbility(AbilityDefinition ability)
        {
            if (ability == null || cooldowns.GetValueOrDefault(ability.AbilityId) > 0f) return;
            cooldowns[ability.AbilityId] = ability.CooldownSeconds;
            animator?.CrossFade(ability.AnimationClip != null ? ability.AnimationClip.name : "Attack", .06f);
            if (ability.VfxPrefab != null) Instantiate(ability.VfxPrefab, transform.position + transform.forward, transform.rotation);
            foreach (var hit in Physics.OverlapSphere(transform.position + transform.forward * ability.Range * .5f, attackRadius, enemyMask)) hit.GetComponentInParent<CombatActor>()?.ApplyDamage(ability.Damage);
        }
    }
}
