using UnityEngine;

namespace Player.Controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private GameObject BallPrefab;
        [SerializeField] private float ShotStrength;

        public void ShootBall()
        {
            var ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

            var movement = ball.GetComponent<BallMovement>();
            movement.Velocity = ShotStrength;
            // TODO: adjust ball's travel direction based on Player's mouse position
        }
    }
}
