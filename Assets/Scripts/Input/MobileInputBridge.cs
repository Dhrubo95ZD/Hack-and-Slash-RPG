using System;
using UnityEngine;

namespace NeoTokyo.HackSlash.Input
{
    public sealed class MobileInputBridge : MonoBehaviour
    {
        public static MobileInputBridge Active { get; private set; }
        public Vector2 Move { get; private set; }
        public event Action<int> AbilityPressed;
        public event Action LockOnPressed;

        private void Awake()
        {
            if (Active != null && Active != this)
            {
                Destroy(gameObject);
                return;
            }
            Active = this;
        }

        public void SetMove(Vector2 value) => Move = Vector2.ClampMagnitude(value, 1f);
        public void ClearMove() => Move = Vector2.zero;
        public void PressAbility(int slot) => AbilityPressed?.Invoke(slot);
        public void PressLockOn() => LockOnPressed?.Invoke();

        private void OnDestroy()
        {
            if (Active == this) Active = null;
        }
    }
}
