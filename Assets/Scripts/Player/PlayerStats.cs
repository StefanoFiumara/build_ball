using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] public int HealthPoints;
        [SerializeField] public int MaxHealthPoints;
        [SerializeField] public int StaminaPoints;
        [SerializeField] public int MaxStaminaPoints;

        [SerializeField] public float Velocity;

        [Tooltip("How long (in seconds) it takes for Stamina to replenish")]
        [SerializeField] private float StaminaGainInterval;
        private float _staminaGainTimer;

        public void Start()
        {
            _staminaGainTimer = 0f;
        }

        private void Update()
        {
            // Refresh Cooldown Timer
            _staminaGainTimer += Time.deltaTime;
            if (_staminaGainTimer >= StaminaGainInterval)
            {
                StaminaPointGain();
                _staminaGainTimer = 0f;
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
