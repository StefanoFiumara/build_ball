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

        protected override void AbilityStart(PlayerStats stats)
        {
            stats.MovementSpeed *= MovementSpeedMultiplier;
            stats.ThrowSpeed *= ThrowSpeedMultiplier;
        }

        protected override void AbilityUpdate(PlayerStats stats)
        {
            // Do nothing
        }

        protected override void AbilityEnd(PlayerStats stats)
        {
            stats.MovementSpeed /= MovementSpeedMultiplier;
            stats.ThrowSpeed /= ThrowSpeedMultiplier;
        }
    }
}
