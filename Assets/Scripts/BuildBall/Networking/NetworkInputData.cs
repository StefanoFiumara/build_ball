using Fusion;
using UnityEngine;

namespace BuildBall.Networking
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector2 MoveDirection;
        public NetworkBool IsShootPressed;
        public NetworkBool IsStandardAbilityPressed;
        public NetworkBool IsUltimateAbilityPressed;
    }
}
