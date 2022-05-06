using Player;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "build_ball/Ability/Dash")]
    public class Dash : Ability
    {
        [SerializeField] public float VelocityMultiplier;

        protected override void AbilityStart(PlayerStats stats)
        {
            stats.Velocity *= VelocityMultiplier;
        }

        protected override void AbilityUpdate(PlayerStats stats)
        {
            // Do nothing
        }

        protected override void AbilityEnd(PlayerStats stats)
        {
            stats.Velocity /= VelocityMultiplier;
        }
    }
}
