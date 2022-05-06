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
            // TODO: adjust ball direction based on Player's facing direction?
        }
    }
}