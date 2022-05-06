using Player;
using UnityEngine;

namespace Abilities
{

    public abstract class Ability : ScriptableObject
    {
        [Header("Basic Ability Settings")]
        [Tooltip("Minimum amount of time (in seconds) between ability usages")]
        [SerializeField] private int UsageCooldown;

        [Tooltip("How long (in seconds) the ability is active for")]
        [SerializeField] private float AbilityDuration;

        [Tooltip("How much stamina the ability consumes")]
        [SerializeField] public int StaminaCost;

        // Internal variables to track above timers
        private bool _isAbilityActive;
        private bool _isUsageCooldownActive;

        private float _currentUsageCooldown;
        private float _currentDuration;

        // This tracks the current player stats that the ability modifies
        // It feels like a bit of a hack, may need to revisit
        private PlayerStats _playerStats;

        public bool IsTriggerable() => !_isAbilityActive && !_isUsageCooldownActive;

        public void Update()
        {
            // Ability Duration Timer
            if (_isAbilityActive)
            {
                AbilityUpdate(_playerStats);

                _currentDuration += Time.deltaTime;
                if (_currentDuration >= AbilityDuration)
                {
                    _isAbilityActive = false;
                    _currentDuration = 0f;
                    AbilityEnd(_playerStats);
                    _playerStats = null;
                }
            }

            // Usage Cooldown Timer
            if (_isUsageCooldownActive)
            {
                _currentUsageCooldown += Time.deltaTime;
                if (_currentUsageCooldown >= UsageCooldown)
                {
                    _currentUsageCooldown = 0f;
                    _isUsageCooldownActive = false;
                }
            }
        }

        public void Trigger(PlayerStats stats)
        {
            if (IsTriggerable())
            {
                _playerStats = stats;

                _isAbilityActive = true;
                _isUsageCooldownActive = true;
                _playerStats.StaminaPointLoss(StaminaCost);

                AbilityStart(_playerStats);
            }
        }

        protected abstract void AbilityStart(PlayerStats stats);
        protected abstract void AbilityUpdate(PlayerStats stats);
        protected abstract void AbilityEnd(PlayerStats stats);
    }
}
