using UnityEngine;

namespace Player
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] public int HealthPoints;
        [SerializeField] public int MaxHealthPoints;
        [SerializeField] public int StaminaPoints;
        [SerializeField] public int MaxStaminaPoints;

        [SerializeField] public float DashDuration;
        [SerializeField] public float DashVelocity;

        public void HealthPointLoss()
        {
            HealthPoints--;
        }

        public void HealthPointGain()
        {
            HealthPoints++;
        }

        public void StaminaPointLoss()
        {
            StaminaPoints--;
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

        public bool CanDash()
        {
            return StaminaPoints > 0;
        }

        /**
         * Mostly applicable when modifying values in the editor for testing.
         * TODO: Consider removing or using "EDITOR" settings on method in the future 
         */
        public void LimitMaxHealthPoints()
        {
            if (HealthPoints > MaxHealthPoints)
            {
                HealthPoints = MaxHealthPoints;
            }
        }
        
        /**
         * Mostly applicable when modifying values in the editor for testing.
         * TODO: Consider removing or using "EDITOR" settings on method in the future 
         */
        public void LimitMaxStaminaPoints()
        {
            if (StaminaPoints > MaxStaminaPoints)
            {
                StaminaPoints = MaxStaminaPoints;
            }
        }
        
        
    }
}