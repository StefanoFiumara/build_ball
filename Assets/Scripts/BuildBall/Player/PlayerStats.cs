using System.Collections;
using BuildBall.Models;
using UnityEngine;

namespace BuildBall.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Health")] [SerializeField] public float HealthPoints;
        [SerializeField] public int MaxHealthPoints;

        [Header("Invulnerability Seconds After Hit")]
        public float InvulnerabilitySecondsAfterHit;

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

        [Header("Team Affiliation")]
        public TeamAffiliationEnum TeamAffiliation;

        [Header("Is Dummy")]
        public bool IsDummy;

        public bool IsInvulnerable => _isInvulnerable;

        private float _staminaTickIntervalTimer;

        private bool _isInvulnerable;

        public void Start()
        {
            _staminaTickIntervalTimer = StaminaTickInterval;

            if (TeamAffiliation == TeamAffiliationEnum.None || TeamAffiliation == TeamAffiliationEnum.Unknown)
            {
                TeamAffiliation = TeamAffiliationEnum.TeamA; // TODO: Assign actual team based on spawning
            }
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
            if (_isInvulnerable) return;

            HealthPoints--;

            StartCoroutine(BecomeTemporarilyInvulnerable());
        }

        public void HealthPointGain()
        {
            HealthPoints++;
        }

        public bool IsDead()
        {
            return HealthPoints <= 0;
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

        private IEnumerator BecomeTemporarilyInvulnerable()
        {
            _isInvulnerable = true;

            const float invulnerabilityDeltaTime = 0.15f;

            for (float i = 0; i < InvulnerabilitySecondsAfterHit; i += invulnerabilityDeltaTime)
            {
                // Alternate between 0 and 1 scale to simulate flashing
                if (gameObject.transform.localScale == Vector3.one)
                {
                    ScaleModelTo(gameObject, Vector3.zero);
                }
                else
                {
                    ScaleModelTo(gameObject, Vector3.one);
                }

                yield return new WaitForSeconds(invulnerabilityDeltaTime);
            }

            ScaleModelTo(gameObject, Vector3.one);
            _isInvulnerable = false;
        }

        private void ScaleModelTo(GameObject theGameObject, Vector3 scale)
        {
            theGameObject.transform.localScale = scale;
        }
    }
}
