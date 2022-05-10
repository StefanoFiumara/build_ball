using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

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

        [FormerlySerializedAs("TeamAffiliation")] [Header("Team Affiliation")] [SerializeField]
        public TeamAffiliationEnum teamAffiliation;

        private float _staminaTickIntervalTimer;

        public void Start()
        {
            _staminaTickIntervalTimer = StaminaTickInterval;

            teamAffiliation = TeamAffiliationEnum.TeamA; // TODO: Assign actual team based on spawning
        }

        private void Update()
        {
            StaminaRefreshCheck();
        }

        private void StaminaRefreshCheck()
        {
            // Refresh Cooldown Timer
            _staminaTickIntervalTimer -= Time.deltaTime;
            if (_staminaTickIntervalTimer <= 0 && StaminaPoints < MaxStaminaPoints) {
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
        public float GetPercentOfMaxStamina()
        {
            return StaminaPoints / (MaxStaminaPoints * 1.0f);
        }
    }
}
