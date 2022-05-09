using UnityEngine;

namespace Player.Controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private GameObject BallPrefab;
        [SerializeField] private float ShotStrength;
         
        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        public void ShootBall()
        {
            var ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

            var movement = ball.GetComponent<BallMovement>();
            movement.Velocity = ShotStrength * _stats.ThrowVelocity;
            // TODO: adjust ball's travel direction based on Player's mouse position
        }
    }
}
