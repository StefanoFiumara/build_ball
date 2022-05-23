using BuildBall.Player;
using UnityEngine;

namespace BuildBall.Abilities.Ultimates
{
    [CreateAssetMenu(menuName = "build_ball/Ability/Ultimates/VelocityAccelerator")]
    public class VelocityAccelerator : Ability
    {
        [Header("Dash Ability Settings")]
        [SerializeField] public float MovementSpeedMultiplier;

        [SerializeField] public float ThrowSpeedMultiplier;

        public override void AbilityStart(PlayerStats stats)
        {
            stats.CurrentMovementSpeed = stats.MovementSpeed * MovementSpeedMultiplier;
            stats.CurrentThrowSpeed = stats.ThrowSpeed * ThrowSpeedMultiplier;
        }

        public override void AbilityUpdate(PlayerStats stats)
        {
            // Do nothing
        }

        public override void AbilityEnd(PlayerStats stats)
        {
            stats.CurrentMovementSpeed = stats.MovementSpeed;
            stats.CurrentThrowSpeed = stats.ThrowSpeed;
        }
    }
}
