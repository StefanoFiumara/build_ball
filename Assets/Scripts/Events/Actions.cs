using System;
using Player;

namespace Events
{
    public static class Actions
    {
        // Player actions
        public static Action DashExecuted;
        public static Action<PlayerStats> DashTriggered;
        public static Action RefreshDashComplete;
        public static Action<PlayerStats> StaminaUsed;
    }
}
