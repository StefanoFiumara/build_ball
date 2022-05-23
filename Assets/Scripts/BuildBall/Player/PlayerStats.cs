using System.Collections;
using BuildBall.Models;
using DG.Tweening;
using Fusion;
using UnityEngine;

namespace BuildBall.Player
{
    public class PlayerStats : NetworkBehaviour
    {
        [Networked] public float HealthPoints { get; set; }
        [Networked] public float StaminaPoints { get; set; }
        [Networked] public float CurrentMovementSpeed { get; set; }
        [Networked] public float CurrentThrowSpeed { get; set; }
        [Networked] public TeamAffiliationEnum TeamAffiliation { get; set; }
        [Networked] private TickTimer StaminaGainTimer { get; set; }
        [Networked] private TickTimer InvulnerabilityTimer { get; set; }

        [Networked(OnChanged = nameof(OnInvulnerableChanged))] public bool IsInvulnerable { get; private set; }


        [Header("Default/Starting Stats")]
        [SerializeField] public int MaxHealthPoints;
        [SerializeField] public int MaxStaminaPoints;
        [SerializeField] public float MovementSpeed;
        [SerializeField] public float ThrowSpeed;

        [SerializeField] private float InvulnerabilitySecondsAfterHit;

        [Tooltip("How long (in seconds) a Stamina Tick is")]
        [SerializeField] private float StaminaTickInterval;

        [Tooltip("How much Stamina is restored per Stamina Tick")]
        [SerializeField] private float StaminaTickGainAmount;

        public void Init(TeamAffiliationEnum team)
        {
            CurrentMovementSpeed = MovementSpeed;
            CurrentThrowSpeed = ThrowSpeed;

            HealthPoints = MaxHealthPoints;
            StaminaPoints = MaxStaminaPoints;

            StaminaGainTimer = TickTimer.CreateFromSeconds(Runner, StaminaTickInterval);

            TeamAffiliation = team;
        }

        public override void FixedUpdateNetwork()
        {
            if (StaminaGainTimer.Expired(Runner))
            {
                AddStamina(StaminaTickGainAmount);
                StaminaGainTimer = TickTimer.CreateFromSeconds(Runner, StaminaTickInterval);
            }

            if (InvulnerabilityTimer.Expired(Runner))
            {
                IsInvulnerable = false;
            }

        }

        public void HealthPointLoss()
        {
            if (IsInvulnerable) return;

            HealthPoints--;

            IsInvulnerable = true;
            InvulnerabilityTimer = TickTimer.CreateFromSeconds(Runner, InvulnerabilitySecondsAfterHit);
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
            if (StaminaPoints > MaxStaminaPoints)
            {
                StaminaPoints = MaxStaminaPoints;
            }
        }

        public bool CanActivateAbility(int abilityCost)
        {
            return StaminaPoints >= abilityCost;
        }
        public float GetPercentOfMaxStamina()
        {
            return StaminaPoints / (MaxStaminaPoints * 1.0f);
        }

        private static void OnInvulnerableChanged(Changed<PlayerStats> changed)
        {
            var renderer = changed.Behaviour.GetComponentInChildren<SpriteRenderer>();

            if (changed.Behaviour.IsInvulnerable)
            {
                renderer.DOKill();
                renderer.DOFade(0, 0.15f).SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                renderer.DOKill();
                renderer.DOFade(1, 0.05f);
            }
        }
    }
}
