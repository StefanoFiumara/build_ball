using Player;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "build_ball/Ability/Dash")]
    public class Dash : Ability
    {
        [Header("Dash Ability Settings")]

        [SerializeField] public float MovementSpeedMultiplier;

        protected override void AbilityStart(PlayerStats stats)
        {
            stats.MovementVelocity *= MovementSpeedMultiplier;
        }

        protected override void AbilityUpdate(PlayerStats stats)
        {
            // Do nothing
        }

        protected override void AbilityEnd(PlayerStats stats)
        {
            stats.MovementVelocity /= MovementSpeedMultiplier;
        }
    }
}
