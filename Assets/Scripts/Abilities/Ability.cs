using Player;
using UnityEngine;

namespace Abilities
{

    public abstract class Ability : ScriptableObject
    {
        [Header("Basic Ability Settings")] [Tooltip("Minimum amount of time (in seconds) between ability usages")] [SerializeField]
        public int UsageCooldown;

        [Tooltip("How long (in seconds) the ability is active for")] [SerializeField]
        private float AbilityDuration;

        [Tooltip("How much stamina the ability consumes")] [SerializeField]
        public int StaminaCost;

        // Internal variables to track above timers
        public bool IsAbilityActive { get; private set; }
        public float CurrentUsageCooldown { get; private set; }

        public bool IsUsageCooldownActive;
        private float _currentDuration;

        // This tracks the current player stats that the ability modifies
        // It feels like a bit of a hack, may need to revisit
        private PlayerStats _playerStats;

        private bool IsTriggerable() => !IsAbilityActive && !IsUsageCooldownActive;

        public void OnEnable()
        {
            _currentDuration = AbilityDuration;
            CurrentUsageCooldown = UsageCooldown;
        }

        public void Update()
        {
            // Ability Duration Timer
            AbilityDurationCheck();

            // Usage Cooldown Timer
            AbilityCooldownCheck();
        }

        private void AbilityDurationCheck()
        {
            if (IsAbilityActive) {
                AbilityUpdate(_playerStats);

                _currentDuration -= Time.deltaTime;
                if (_currentDuration <= 0) {
                    IsAbilityActive = false;
                    _currentDuration = AbilityDuration;
                    
                    AbilityEnd(_playerStats);
                    _playerStats = null;
                }
            }
        }

        private void AbilityCooldownCheck()
        {

            if (IsUsageCooldownActive) {
                CurrentUsageCooldown -= Time.deltaTime;
                if (CurrentUsageCooldown <= 0) {
                    CurrentUsageCooldown = UsageCooldown;
                    IsUsageCooldownActive = false;
                }
            }
        }

        public void Trigger(PlayerStats stats)
        {
            if (IsTriggerable()) {
                _playerStats = stats;

                IsAbilityActive = true;
                IsUsageCooldownActive = true;

                _playerStats.StaminaPointLoss(StaminaCost);

                AbilityStart(_playerStats);
            }
        }

        public float GetUltimateCooldownPercent()
        {
            if (!IsUsageCooldownActive) {
                return 1;
            }

            return (UsageCooldown - CurrentUsageCooldown) / UsageCooldown;
        }

        public float GetUsageCooldownPercent()
        {
            if (!IsUsageCooldownActive) {
                return 1;
            }

            return (UsageCooldown - CurrentUsageCooldown) / UsageCooldown; 
        }
        
        protected abstract void AbilityStart(PlayerStats stats);
        protected abstract void AbilityUpdate(PlayerStats stats);
        protected abstract void AbilityEnd(PlayerStats stats);
    }
}
