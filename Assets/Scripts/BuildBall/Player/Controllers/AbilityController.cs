using BuildBall.Abilities;
using Extensions;
using Fusion;
using UnityEngine;

namespace BuildBall.Player.Controllers
{
    [RequireComponent(typeof(PlayerStats))]
    public class AbilityController : NetworkBehaviour
    {
        [Expandable] public Ability StandardAbility;
        [Expandable] public Ability UltimateAbility;

        [Networked] private TickTimer AbilityDurationTimer { get; set; }
        [Networked] private TickTimer AbilityUsageTimer { get; set; }

        private PlayerStats _stats;
        private Ability _activeAbility;

        public float UltimateUsageCooldownPercent => GetUsageCooldownPercent(UltimateAbility);
        public float UltimateUsageCooldown => GetUsageCooldown(UltimateAbility);

        public float StandardUsageCooldownPercent => GetUsageCooldownPercent(StandardAbility);
        public float StandardUsageCooldown => GetUsageCooldown(StandardAbility);

        public bool IsStandardUsageCooldownActive => IsUsageCooldownActive(StandardAbility);

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
            _activeAbility = null;
        }

        public override void FixedUpdateNetwork()
        {
            // TODO SF: This only allows for one active ability at a time
            //          Also, the long usage timer on ultimates prevents standard
            //          abilities from activating until it expires,
            //          need to have separate usage timers for each ability, at least.
            if (_activeAbility != null)
            {
                _activeAbility.AbilityUpdate(_stats);

                if (AbilityDurationTimer.Expired(Runner))
                {
                    Debug.Log("Ability Ended");
                    _activeAbility.AbilityEnd(_stats);
                    _activeAbility = null;
                }
            }
        }

        public void ActivateStandardAbility() => ActivateAbility(StandardAbility);
        public void ActivateUltimateAbility() => ActivateAbility(UltimateAbility);


        private bool IsUsageCooldownActive(Ability ability)
        {
            // TODO: Split for Ultimate
            return !AbilityUsageTimer.ExpiredOrNotRunning(Runner);
        }

        private void ActivateAbility(Ability ability)
        {
            if (_stats.CanActivateAbility(ability.StaminaCost) && AbilityUsageTimer.ExpiredOrNotRunning(Runner))
            {
                AbilityUsageTimer = TickTimer.CreateFromSeconds(Runner, ability.UsageCooldown);
                AbilityDurationTimer = TickTimer.CreateFromSeconds(Runner, ability.AbilityDuration);

                Debug.Log("Ability Started");
                ability.AbilityStart(_stats);

                _activeAbility = ability;
            }
        }

        private float GetUsageCooldownPercent(Ability ability)
        {
            // TODO: Query correct timer depending on ability parameter
            var remaining = AbilityUsageTimer.RemainingTime(Runner) ?? 0f;
             return remaining / ability.UsageCooldown;
        }

        private float GetUsageCooldown(Ability ability)
        {
            // TODO: Query correct timer depending on ability parameter
            return AbilityUsageTimer.RemainingTime(Runner) ?? 0f;
        }
    }
}
