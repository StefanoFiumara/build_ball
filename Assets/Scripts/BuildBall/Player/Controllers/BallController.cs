using BuildBall.Models;
using Fusion;
using UnityEngine;

namespace BuildBall.Player.Controllers
{
    public class BallController : NetworkBehaviour
    {
        [Networked] private NetworkObject HeldBall { get; set; }

        private readonly Vector3 _ballOffsetVector = new(0.5f, 0.5f);

        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        public override void FixedUpdateNetwork()
        {
            if (HeldBall != null)
            {
                HeldBall.transform.position = transform.position + _ballOffsetVector;
            }
        }

        public void ShootBall(Vector2 direction)
        {
            var playerHasBall = HeldBall != null;
            if (!playerHasBall)
            {
                return;
            }

            HeldBall.GetComponent<BallMovementBehaviour>().SetThrownBy(_stats, direction);
            HeldBall = null;
        }

        private void DropBall()
        {
            if (HeldBall != null)
            {
                Debug.Log("Dropping Ball");
                HeldBall.GetComponent<BallMovementBehaviour>().SetStationary();
                HeldBall = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var ball = col.GetComponent<BallMovementBehaviour>();
            if (ball == null)
            {
                return;
            }

            if ((ball.BallState == BallStateEnum.Stationary || (ball.BallState == BallStateEnum.Moving && ball.TeamAffiliation == TeamAffiliationEnum.None))
                && HeldBall == null
                && !_stats.IsInvulnerable)
            {

                // Pick up the ball
                HeldBall = ball.GetComponent<NetworkObject>();
                ball.SetHeldBy(_stats);

                Debug.Log($"Ball picked up by team {_stats.TeamAffiliation}");
                Debug.Log($"HeldBall: {HeldBall.Id}");

            }
            else if (ball.BallState == BallStateEnum.Moving
                     && ball.TeamAffiliation != _stats.TeamAffiliation
                     && ball.TeamAffiliation != TeamAffiliationEnum.None
                     && !_stats.IsInvulnerable)
            {
                // Artificially make the ball bounce off of the player, since this is a trigger physics won't intervene
                ball.Bounce();
                _stats.HealthPointLoss();
                DropBall();
            }
        }
    }
}
