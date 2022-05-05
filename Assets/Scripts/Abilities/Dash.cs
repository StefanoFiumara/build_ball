using Events;
using UnityEngine;

namespace Abilities
{
    public class Dash : Ability
    {
        [SerializeField] public float DashVelocity;

        protected override void TriggerAbility()
        {
            Actions.DashExecuted?.Invoke();
        }
    }
}
