using BuildBall.Models;
using Fusion;
using UnityEngine;

namespace BuildBall.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallMovementBehaviour : NetworkBehaviour
    {
        private const float STATIONARY_THRESHOLD = 0.5f;

        [Networked] public BallStateEnum BallState { get; private set; }
        [Networked] public TeamAffiliationEnum TeamAffiliation { get; set; }

        private Rigidbody2D _rigidbody;

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetHeldBy(PlayerStats player)
        {
            _rigidbody.isKinematic = true;
            BallState = BallStateEnum.Held;
            TeamAffiliation = player.TeamAffiliation;
        }

        public void SetStationary()
        {
            BallState = BallStateEnum.Stationary;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = false;
            TeamAffiliation = TeamAffiliationEnum.None;
        }

        public void SetThrownBy(PlayerStats player, Vector2 direction)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = direction * player.CurrentThrowSpeed;
            BallState = BallStateEnum.Moving;
            TeamAffiliation = player.TeamAffiliation;
        }

        public void Bounce()
        {
            _rigidbody.velocity = -_rigidbody.velocity;
        }

        public override void FixedUpdateNetwork()
        {
            if (BallState == BallStateEnum.Moving)
            {
                var velocity = _rigidbody.velocity;
                velocity -= (velocity * 0.95f * Runner.DeltaTime);
                _rigidbody.velocity = velocity;
                if (_rigidbody.velocity.magnitude < STATIONARY_THRESHOLD)
                {
                    _rigidbody.velocity = Vector2.zero;
                    BallState = BallStateEnum.Stationary;
                    TeamAffiliation = TeamAffiliationEnum.None;
                }
            }
        }
    }
}
