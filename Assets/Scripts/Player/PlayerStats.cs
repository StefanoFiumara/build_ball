using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Health")] [SerializeField] public float HealthPoints;
        [SerializeField] public int MaxHealthPoints;

        [Header("Stamina")] [SerializeField] public float StaminaPoints;
        [SerializeField] public int MaxStaminaPoints;

        [Tooltip("How long (in seconds) a Stamina Tick is")] [SerializeField]
        private float StaminaTickInterval;

        [Tooltip("How much Stamina is restored per Stamina Tick")] [SerializeField]
        private float StaminaTickGainAmount;

        [Header("Movement Speed")] [SerializeField]
        public float MovementVelocity;

        [Header("Throw Speed")] [SerializeField]
        public float ThrowVelocity;

        private float _staminaTickIntervalTimer;

        public void Start()
        {
            _staminaTickIntervalTimer = StaminaTickInterval;
        }

        private void Update()
        {
            StaminaRefreshCheck();
        }

        private void StaminaRefreshCheck()
        {
            // Refresh Cooldown Timer
            _staminaTickIntervalTimer -= Time.deltaTime;
            if (_staminaTickIntervalTimer <= 0) {
                //StaminaPointGain();
                AddStamina(StaminaTickGainAmount);
                _staminaTickIntervalTimer = StaminaTickInterval;
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

        public void AddStamina(float amount)
        {
            StaminaPoints += amount;
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

        public float GetPercentOfMaxStamina()
        {
            return StaminaPoints / (MaxStaminaPoints * 1.0f);
        }


    }
}
