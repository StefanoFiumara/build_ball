using UnityEngine;

namespace Abilities
{
    public class Ability : MonoBehaviour
    {
        protected bool _isAbilityActive  = false;
        protected bool _isInternalCoolDownActive = false;

        // Amount of time between Ability usages
        [SerializeField] protected int COOLDOWN_IN_SECONDS;
        
        // Minimum amount of time between Ability usages
        [SerializeField] protected int INTERNAL_COOLDOWN_IN_SECONDS;
        
        // Time length of Ability
        [SerializeField] protected float DURATION_IN_SECONDS;

        
        
    }
}