using UnityEngine;

namespace Player.Controllers
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

        private Vector3 CalculateNewPosition(Vector2 movementDirection)
        {
            var directionToMove = new Vector3(movementDirection.x, movementDirection.y, 0f);
            return _stats.Velocity * directionToMove;
        }
    }
}
