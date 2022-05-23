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

        [Networked] private TickTimer StandardAbilityDurationTimer { get; set; }
        [Networked] private TickTimer StandardAbilityUsageTimer { get; set; }

        [Networked] private TickTimer UltimateAbilityDurationTimer { get; set; }
        [Networked] private TickTimer UltimateAbilityUsageTimer { get; set; }

        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        public override void FixedUpdateNetwork()
        {
            if (IsStandardDurationCooldownActive)
            {
                StandardAbility.AbilityUpdate(_stats);
            }

            if (StandardAbilityDurationTimer.Expired(Runner))
            {
                StandardAbility.AbilityEnd(_stats);
                StandardAbilityDurationTimer = default;
            }

            if (IsUltimateDurationCooldownActive)
            {
                UltimateAbility.AbilityUpdate(_stats);
            }

            if (UltimateAbilityDurationTimer.Expired(Runner))
            {
                UltimateAbility.AbilityEnd(_stats);
                UltimateAbilityDurationTimer = default;
            }
        }

        public void ActivateStandardAbility()
        {
            if (_stats.CanActivateAbility(StandardAbility.StaminaCost) && StandardAbilityUsageTimer.ExpiredOrNotRunning(Runner))
            {
                StandardAbilityUsageTimer = TickTimer.CreateFromSeconds(Runner, StandardAbility.UsageCooldown);
                StandardAbilityDurationTimer = TickTimer.CreateFromSeconds(Runner, StandardAbility.AbilityDuration);

                _stats.StaminaPointLoss(StandardAbility.StaminaCost);
                StandardAbility.AbilityStart(_stats);
            }
        }

        public void ActivateUltimateAbility()
        {
            if (_stats.CanActivateAbility(UltimateAbility.StaminaCost) && UltimateAbilityUsageTimer.ExpiredOrNotRunning(Runner))
            {
                UltimateAbilityUsageTimer = TickTimer.CreateFromSeconds(Runner, UltimateAbility.UsageCooldown);
                UltimateAbilityDurationTimer = TickTimer.CreateFromSeconds(Runner, UltimateAbility.AbilityDuration);

                _stats.StaminaPointLoss(UltimateAbility.StaminaCost);
                UltimateAbility.AbilityStart(_stats);
            }
        }

        public float UltimateUsageCooldownPercent => GetUsageCooldownPercent(UltimateAbility);
        public float UltimateUsageCooldown => GetUsageCooldown(UltimateAbility);

        public float StandardUsageCooldownPercent => GetUsageCooldownPercent(StandardAbility);
        public float StandardUsageCooldown => GetUsageCooldown(StandardAbility);

        public bool IsStandardUsageCooldownActive => !StandardAbilityUsageTimer.ExpiredOrNotRunning(Runner);
        public bool IsUltimateUsageCooldownActive => !UltimateAbilityUsageTimer.ExpiredOrNotRunning(Runner);

        public bool IsStandardDurationCooldownActive => !StandardAbilityDurationTimer.ExpiredOrNotRunning(Runner);
        public bool IsUltimateDurationCooldownActive => !UltimateAbilityDurationTimer.ExpiredOrNotRunning(Runner);

        private float GetUsageCooldownPercent(Ability ability)
        {
            var timer = ability == StandardAbility ? StandardAbilityUsageTimer : UltimateAbilityUsageTimer;

            var remaining = timer.RemainingTime(Runner) ?? 0f;
            return remaining / ability.UsageCooldown;
        }

        private float GetUsageCooldown(Ability ability)
        {
            var timer = ability == StandardAbility ? StandardAbilityUsageTimer : UltimateAbilityUsageTimer;
            return timer.RemainingTime(Runner) ?? 0f;
        }
    }
}
