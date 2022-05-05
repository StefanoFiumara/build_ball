using Events;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        // How long in takes for Stamina to replenish
        [SerializeField] private int RefreshCooldown;

        // Minimum amount of time between Ability usages
        [SerializeField] private int UsageCooldown;

        // Time length of Ability
        [SerializeField] private float AbilityDuration;

        // Internal variables to track above timers
        private bool _isAbilityActive;
        private bool _isUsageCooldownActive;

        private float _currentRefreshCooldown;
        private float _currentUsageCooldown;
        private float _currentDuration;

        public bool IsAbilityActive() => _isAbilityActive;
        public bool IsTriggerable() => !_isAbilityActive && !_isUsageCooldownActive;

        public void Start()
        {
            _isAbilityActive = false;
            _isUsageCooldownActive = false;
            _currentRefreshCooldown = 0f;
            _currentDuration = 0f;
            _currentUsageCooldown = 0f;
        }

        public void Update()
        {
            // Ability Duration Timer
            if (_isAbilityActive)
            {
                _currentDuration += Time.deltaTime;
                if (_currentDuration >= AbilityDuration)
                {
                    _isAbilityActive = false;
                    _currentDuration = 0f;
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

            // Refresh Cooldown Timer
            _currentRefreshCooldown += Time.deltaTime;
            if (_currentRefreshCooldown >= RefreshCooldown)
            {
                Actions.RefreshDashComplete?.Invoke();
                _currentRefreshCooldown = 0f;
            }
        }

        public void Trigger()
        {
            if (IsTriggerable())
            {
                _isAbilityActive = true;
                _isUsageCooldownActive = true;

                _currentRefreshCooldown = 0f;
                TriggerAbility();
            }
        }

        protected abstract void TriggerAbility();
    }
}
