using UnityEngine;

namespace BuildBall.Player.Controllers
{
    [RequireComponent(typeof(PlayerStats))]
    public class MovementController : MonoBehaviour
    {
        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        public void Move(Vector2 direction)
        {
            var moveDelta = CalculateNewPosition(direction);
            transform.position += moveDelta * Time.deltaTime;
        }

        public void MoveToGraveyard()
        {
            transform.position = new Vector3(-1000, -1000, 0); // TODO RH: Is this the right way to move them off the field?
        }

        private Vector3 CalculateNewPosition(Vector2 movementDirection)
        {
            var directionToMove = new Vector3(movementDirection.x, movementDirection.y, 0f);

            return _stats.MovementVelocity * directionToMove;
        }
    }
}
