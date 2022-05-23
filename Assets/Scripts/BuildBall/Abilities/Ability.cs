using BuildBall.Player;
using UnityEngine;

namespace BuildBall.Abilities
{

    public abstract class Ability : ScriptableObject
    {
        [Header("Basic Ability Settings")]
        [Tooltip("Minimum amount of time (in seconds) between ability usages")]
        public int UsageCooldown;

        [Tooltip("How long (in seconds) the ability is active for")]
        public float AbilityDuration;

        [Tooltip("How much stamina the ability consumes")]
        public int StaminaCost;

        public abstract void AbilityStart(PlayerStats stats);
        public abstract void AbilityUpdate(PlayerStats stats);
        public abstract void AbilityEnd(PlayerStats stats);
    }
}
