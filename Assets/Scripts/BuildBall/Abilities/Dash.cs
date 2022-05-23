using BuildBall.Player;
using UnityEngine;

namespace BuildBall.Abilities
{
    [CreateAssetMenu(menuName = "build_ball/Ability/Dash")]
    public class Dash : Ability
    {
        [Header("Dash Ability Settings")]

        [SerializeField] public float MovementSpeedMultiplier;

        public override void AbilityStart(PlayerStats stats)
        {
            stats.CurrentMovementSpeed = stats.MovementSpeed * MovementSpeedMultiplier;
        }

        public override void AbilityUpdate(PlayerStats stats)
        {
            // Do nothing
        }

        public override void AbilityEnd(PlayerStats stats)
        {
            stats.CurrentMovementSpeed = stats.MovementSpeed;
        }
    }
}
