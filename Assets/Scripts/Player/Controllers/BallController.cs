using System;
using Models;
using UnityEngine;

namespace Player.Controllers
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            var ball = other.GetComponent<BallMovement>();
            if (ball == null)
            {
                return;
            }

            if (ball.BallState == BallStateEnum.Stationary
                && Ball == null)
            {
                // Pick up the ball
                Ball = ball;
            }
            else if (Ball.BallState == BallStateEnum.Moving
                     && Ball.TeamAffiliation != _stats.TeamAffiliation)
            {
                // TODO: Get hit, drop ball?
            }
        }
    }
}
