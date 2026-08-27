using UnityEngine;

namespace NeoTokyo.HackSlash.Combat
{
    [CreateAssetMenu(menuName = "Neo-Tokyo/Combat/Ability Definition", fileName = "AbilityDefinition")]
    public sealed class AbilityDefinition : ScriptableObject
    {
        [SerializeField] private string abilityId = "ability.id";
        [SerializeField] private string displayName = "Unnamed Technique";
        [SerializeField, Min(0f)] private float cooldownSeconds = 4f;
        [SerializeField, Min(0f)] private float damage = 20f;
        [SerializeField, Min(0f)] private float range = 2.5f;
        [SerializeField] private AnimationClip animationClip;
        [SerializeField] private GameObject vfxPrefab;

        public string AbilityId => abilityId;
        public string DisplayName => displayName;
        public float CooldownSeconds => cooldownSeconds;
        public float Damage => damage;
        public float Range => range;
        public AnimationClip AnimationClip => animationClip;
        public GameObject VfxPrefab => vfxPrefab;
    }
}
