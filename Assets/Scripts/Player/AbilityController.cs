using System;
using Abilities;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStats))]
    public class AbilityController : MonoBehaviour
    {
        [SerializeField] private Ability StandardAbility;
        [SerializeField] private Ability UltimateAbility;

        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if(StandardAbility != null) StandardAbility.Update();
            if(UltimateAbility != null) UltimateAbility.Update();
        }

        public void ActivateStandardAbility() => ActivateAbility(StandardAbility);
        public void ActivateUltimateAbility() => ActivateAbility(UltimateAbility);

        private void ActivateAbility(Ability ability)
        {
            if (ability == null) return;

            if (_stats.CanActivateAbility(ability.StaminaCost))
            {
                ability.Trigger(_stats);
            }
        }


    }
}
