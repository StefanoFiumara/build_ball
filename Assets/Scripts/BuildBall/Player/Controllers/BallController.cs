using BuildBall.Models;
using UnityEngine;

namespace BuildBall.Player.Controllers
{
    public class BallController : MonoBehaviour
    {
        public BallMovement Ball;
        [SerializeField] private float ShotStrength;
        private const float BallHoldXOffset = 0.5f;
        private const float BallHoldYOffset = 0.5f;
        private Vector3 _ballOffsetVector;

        private PlayerStats _stats;

        private void Start()
        {
            _ballOffsetVector = new Vector3(BallHoldXOffset, BallHoldYOffset);
        }

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if (Ball != null)
            {
                Ball.transform.position = transform.position + _ballOffsetVector;
            }
        }

        public void ShootBall()
        {
            var playerHasBall = Ball != null;
            if (!playerHasBall)
            {
                return;
            }

            Ball.Velocity = ShotStrength * _stats.ThrowVelocity;
            Ball.TeamAffiliation = _stats.TeamAffiliation;
            Ball.BallState = BallStateEnum.Moving;
            Ball = null;

            // TODO: adjust ball's travel direction based on Player's mouse position
        }

        private void DropBall()
        {
            Ball = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var ball = other.GetComponent<BallMovement>();
            if (ball == null)
            {
                return;
            }

            if ((ball.BallState == BallStateEnum.Stationary || (ball.BallState == BallStateEnum.Moving && ball.TeamAffiliation == TeamAffiliationEnum.None))
                && Ball == null
                && !_stats.IsInvulnerable)
            {
                // Pick up the ball
                Ball = ball;
                Ball.BallState = BallStateEnum.Held;
            }
            else if (ball.BallState == BallStateEnum.Moving
                     && ball.TeamAffiliation != _stats.TeamAffiliation
                     && ball.TeamAffiliation != TeamAffiliationEnum.None
                     && !_stats.IsInvulnerable)
            {
                _stats.HealthPointLoss();

                ball.TeamAffiliation = TeamAffiliationEnum.None;
                ball.Direction = new Vector3(ball.Direction.x * -1, ball.Direction.y * -1, ball.Direction.z);
                ball.Velocity *= 0.2f;

                DropBall();
            }
        }
    }
}
