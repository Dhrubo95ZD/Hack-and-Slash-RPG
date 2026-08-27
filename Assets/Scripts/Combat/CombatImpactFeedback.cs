using System.Collections;
using UnityEngine;

namespace NeoTokyo.HackSlash.Combat
{
    public sealed class CombatImpactFeedback : MonoBehaviour
    {
        [SerializeField, Range(0f, 0.2f)] private float defaultDuration = 0.045f;
        [SerializeField, Range(0f, 1f)] private float timeScaleDuringImpact = 0.08f;
        private Coroutine activeRoutine;

        public void Play(float duration = -1f)
        {
            if (activeRoutine != null) StopCoroutine(activeRoutine);
            activeRoutine = StartCoroutine(ImpactRoutine(duration > 0f ? duration : defaultDuration));
        }

        private IEnumerator ImpactRoutine(float duration)
        {
            var previousScale = Time.timeScale;
            Time.timeScale = timeScaleDuringImpact;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = previousScale;
            activeRoutine = null;
        }

        private void OnDisable()
        {
            if (activeRoutine != null) StopCoroutine(activeRoutine);
            Time.timeScale = 1f;
            activeRoutine = null;
        }
    }
}
