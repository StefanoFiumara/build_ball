using UnityEngine;

namespace Player
{
    public class CharacterStats : MonoBehaviour
    {
        public int healthPoints;
        public int maxHealthPoints;

        public int staminaPoints;
        public int maxStaminaPoints;

        public void HealthPointLoss()
        {
            healthPoints--;
        }

        public void HealthPointGain()
        {
            healthPoints++;
        }
        
        public void StaminaPointLoss()
        {
            healthPoints--;
        }

        public void StaminaPointGain()
        {
            healthPoints++;
        }


        public void ResetHealth()
        {
            healthPoints = maxHealthPoints;
        }
        
        public void ResetStamina()
        {
            staminaPoints = maxStaminaPoints;
        }
        
    }
}
