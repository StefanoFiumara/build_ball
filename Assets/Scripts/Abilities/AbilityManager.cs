using Events;
using Player;
using UnityEngine;

namespace Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] public Dash dash;

        public void Start()
        {
            Actions.DashTriggered += DashTriggered;
        }

        private void DashTriggered(PlayerStats stats)
        {
            if (stats.CanDash()) {
                dash.Trigger();
            }
        }
    }
}
