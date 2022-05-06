using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] public int HealthPoints;
        [SerializeField] public int MaxHealthPoints;

        [Header("Stamina")]
        [SerializeField] public int StaminaPoints;
        [SerializeField] public int MaxStaminaPoints;

        [Tooltip("How long (in seconds) it takes for Stamina to replenish")]
        [SerializeField] private float StaminaGainInterval;

        [Header("Movement Speed")]
        [SerializeField] public float MovementVelocity;

        [Header("Throw Speed")]
        [SerializeField] public float ThrowVelocity;

        private float _staminaGainTimer;

        public void Start()
        {
            _staminaGainTimer = StaminaGainInterval;
        }

        private void Update()
        {
            StaminaRefreshCheck();
        }

        private void StaminaRefreshCheck()
        {
            // Refresh Cooldown Timer
            _staminaGainTimer -= Time.deltaTime;
            if (_staminaGainTimer <= 0) {
                StaminaPointGain();
                _staminaGainTimer = StaminaGainInterval;
            }
        }

        public void HealthPointLoss()
        {
            HealthPoints--;
        }

        public void HealthPointGain()
        {
            HealthPoints++;
        }

        public void StaminaPointLoss(int amount = 1)
        {
            StaminaPoints -= amount;
            if (StaminaPoints < 0) StaminaPoints = 0;
        }

        public void StaminaPointGain()
        {
            StaminaPoints++;
        }


        public void ResetHealth()
        {
            HealthPoints = MaxHealthPoints;
        }

        public void ResetStamina()
        {
            StaminaPoints = MaxStaminaPoints;
        }

        public bool CanActivateAbility(int abilityCost)
        {
            return StaminaPoints >= abilityCost;
        }

        /**
         * Mostly applicable when modifying values in the editor for testing.
         * TODO: Consider removing or using "EDITOR" settings on method in the future
         */
        public void LimitMaxHealthPoints()
        {
            if (HealthPoints > MaxHealthPoints) {
                HealthPoints = MaxHealthPoints;
            }
        }

        /**
         * Mostly applicable when modifying values in the editor for testing.
         * TODO: Consider removing or using "EDITOR" settings on method in the future
         */
        public void LimitMaxStaminaPoints()
        {
            if (StaminaPoints > MaxStaminaPoints) {
                StaminaPoints = MaxStaminaPoints;
            }
        }


    }
}
